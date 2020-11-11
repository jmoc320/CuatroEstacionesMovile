using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroEstaciones.Core.Models {

    public class Servicio : BaseModel {
        private int _id;
        private string _nombre;
        private string _descripcion;
        private double _precio;
        private int _tipoServicioId;
        private TipoServicio _tipoServicio;

        [Key]
        [Column("ServicioId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get => _id; set => Set(ref _id, value); }

        public string Nombre { get => _nombre; set => Set(ref _nombre, value); }
        public string Descripcion { get => _descripcion; set => Set(ref _descripcion, value); }
        public double Precio { get => _precio; set => Set(ref _precio, value); }
        public int TipoServicioId { get => _tipoServicioId; set => Set(ref _tipoServicioId, value); }

        [ForeignKey("TipoServicioId")]
        public TipoServicio TipoServicio { get => _tipoServicio; set => Set(ref _tipoServicio, value); }
    }
}