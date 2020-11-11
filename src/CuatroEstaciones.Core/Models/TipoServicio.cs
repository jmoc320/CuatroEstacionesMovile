using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroEstaciones.Core.Models {

    public class TipoServicio : BaseModel {
        private int _id;
        private string _nombre;
        private string _descripcion;

        [Key]
        [Column("TipoServicioId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get => _id; set => Set(ref _id, value); }

        public string Nombre { get => _nombre; set => Set(ref _nombre, value); }
        public string Descripcion { get => _descripcion; set => Set(ref _descripcion, value); }
        public ObservableCollection<Servicio> Servicios { get; set; }
    }
}