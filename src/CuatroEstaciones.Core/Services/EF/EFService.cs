using CuatroEstaciones.Core.Misc;
using CuatroEstaciones.Core.Models;
using CuatroEstaciones.Core.Validations;
using FluentValidation;
using FluentValidation.Results;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CuatroEstaciones.Core.Services.EF {

    public class EFService : IEFService {
        private readonly IMvxMessenger _messenger;

        public EFService(IMvxMessenger messenger) {
            _messenger = messenger;
        }

        private IValidator GetValidator(Type validatorType) {
            if (validatorType == typeof(Cliente)) { return new ClienteValidator(); }
            if (validatorType == typeof(Servicio)) { return new ServicioValidator(); }
            if (validatorType == typeof(TipoServicio)) { return new TipoServicioValidator(); }
            if (validatorType == typeof(Venta)) { return new VentaValidator(); }
            if (validatorType == typeof(VentaServicio)) { return new VentaServicioValidator(); }
            return null;
        }

        public ObservableCollection<TModel> GetAll<TModel>() where TModel : BaseModel {
            using (var db = new DatabaseContext()) {
                return new ObservableCollection<TModel>(db.Set<TModel>().Include(db.GetIncludePaths(typeof(TModel))));
            }
        }

        public ValidationResult Validate<TModel>(TModel modelInLocal) where TModel : BaseModel {
            var context = new ValidationContext<TModel>(modelInLocal);
            return GetValidator(modelInLocal.GetType()).Validate(context);
        }

        /// <summary>
        /// Inserta o actualiza un registro en bd, en función de si existe o no
        /// </summary>
        /// <param name="modelInLocal"></param>
        public void InsertOrUpdate<TModel>(TModel modelInLocal) where TModel : BaseModel {
            if (modelInLocal != null) {
                // Ejecuto validacion y, si ha ido bien, continuo. Si no, muestro errores.
                var results = Validate<TModel>(modelInLocal);
                if (results.IsValid) {
                    try {
                        using (var db = new DatabaseContext()) {
                            // Obtengo objeto de bd
                            var modelInBD = db.Set<TModel>().SingleOrDefault(x => x.Id == modelInLocal.Id);

                            // Si el objeto está en la bd, lo actualizo. Si no, lo creo
                            if (modelInBD != null) {
                                db.Entry(modelInBD).CurrentValues.SetValues(modelInLocal);
                            }
                            else {
                                try {
                                    db.Attach(modelInLocal);
                                }
                                catch { }
                                db.Add(modelInLocal);
                            }

                            db.SaveChanges();
                        }

                        _messenger.Publish(new NotificationMessage(this, "Actualizar registro", "Registro actualizado correctamente"));
                    }
                    catch (Exception e) {
                        _messenger.Publish(new NotificationMessage(this, "Error", e.Message));
                    }
                }
                else {
                    _messenger.Publish(new NotificationMessage(this, "Error", results.ToString()));
                }
            }
            else {
                _messenger.Publish(new NotificationMessage(this, "Error", "Ningun objeto seleccionado"));
            }
        }

        public void Delete<TModel>(TModel modelInLocal) where TModel : BaseModel {
            if (modelInLocal != null) {
                try {
                    using (var db = new DatabaseContext()) {
                        // Obtengo objeto de bd
                        var modelInBD = db.Set<TModel>().SingleOrDefault(x => x.Id == modelInLocal.Id);

                        // Si el objeto está en bd, lo elimino. Si no, no hago nada
                        if (modelInBD != null) {
                            db.Remove(modelInBD);
                            _messenger.Publish(new NotificationMessage(this, "Eliminar registro", "Registro eliminado correctamente"));
                        }

                        // Guardo cambios en BD
                        db.SaveChanges();
                    }
                }
                catch (Exception e) {
                    _messenger.Publish(new NotificationMessage(this, "Error", e.Message));
                }
            }
            else {
                _messenger.Publish(new NotificationMessage(this, "Error", "Ningun objeto seleccionado"));
            }
        }

        /// <summary>
        /// Descarta los cambios del objeto con los valores de bd, produciendo el fin de la edicion
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="modelInLocal"></param>
        public void Cancel<TModel>(TModel modelInLocal) where TModel : BaseModel, new() {
            UndoOrCancel(modelInLocal, "Cancelar registro", "Registro cancelado de forma correcta");
        }

        /// <summary>
        /// Descarta los cambios del objeto con los valores de bd, permitiendo continuar con la edicion
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="modelInLocal"></param>
        public void Undo<TModel>(TModel modelInLocal) where TModel : BaseModel, new() {
            UndoOrCancel(modelInLocal, "Recuperar registro", "Registro recuperado de forma correcta");
        }

        /// <summary>
        /// Descarta los cambios del objeto con los valores de bd
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="modelInLocal"></param>
        private void UndoOrCancel<TModel>(TModel modelInLocal, string titulo, string mensaje) where TModel : BaseModel, new() {
            if (modelInLocal != null) {
                // Invoco al metodo de borrado en funcion del tipo recibido
                var tipoModelInLocal = modelInLocal.GetType();
                try {
                    using (var db = new DatabaseContext()) {
                        // Obtengo objeto de bd
                        var modelInBD = db.Set<TModel>().Include(db.GetIncludePaths(typeof(TModel))).SingleOrDefault(x => x.Id == modelInLocal.Id);
                        if (modelInBD == null) {
                            modelInBD = new TModel();
                        }

                        // Copio el valor de todas las propiedades del objeto bd
                        Util.CopyProperties(modelInBD, modelInLocal);
                    }

                    _messenger.Publish(new NotificationMessage(this, titulo, mensaje));
                }
                catch (Exception e) {
                    _messenger.Publish(new NotificationMessage(this, "Error", e.Message));
                }
            }
            else {
                _messenger.Publish(new NotificationMessage(this, "Error", "Ningun objeto seleccionado"));
            }
        }
    }
}