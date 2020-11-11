using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroEstaciones.Core.Models {

    public class Cliente : BaseModel {
        private int _id;
        private string _nombre;
        private string _apellidos;
        private string _telefono;
        private string _correo;
        private string _fechaNacimiento;
        private string _profesion;

        [Key]
        [Column("ClienteId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get => _id; set => Set(ref _id, value); }

        public string Nombre { get => _nombre; set { Set(ref _nombre, value); RaisePropertyChanged("ClienteCompleto"); RaisePropertyChanged("NombreCompleto"); } }
        public string Apellidos { get => _apellidos; set { Set(ref _apellidos, value); RaisePropertyChanged("ClienteCompleto"); RaisePropertyChanged("NombreCompleto"); } }
        public string Telefono { get => _telefono; set => Set(ref _telefono, value); }
        public string Correo { get => _correo; set => Set(ref _correo, value); }
        public string FechaNacimiento { get => _fechaNacimiento; set => Set(ref _fechaNacimiento, value); }
        public string Profesion { get => _profesion; set => Set(ref _profesion, value); }

        [NotMapped]
        public string ClienteCompleto => Nombre + " " + Apellidos + " " + Telefono;

        [NotMapped]
        public string NombreCompleto => Nombre + " " + Apellidos;
    }
}