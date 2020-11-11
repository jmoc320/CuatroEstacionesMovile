using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroEstaciones.Core.Models {

    public class VentaServicio : BaseModel {
        private int _id;
        private string _nota;
        private int _ventaId;
        private Venta _venta;
        private int _servicioId;
        private Servicio _servicio;

        [Key]
        [Column("VentaServicioId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get => _id; set => Set(ref _id, value); }

        public string Nota { get => _nota; set => Set(ref _nota, value); }
        public int VentaId { get => _ventaId; set => Set(ref _ventaId, value); }

        [ForeignKey("VentaId")]
        public Venta Venta { get => _venta; set => Set(ref _venta, value); }

        public int ServicioId { get => _servicioId; set => Set(ref _servicioId, value); }

        [ForeignKey("ServicioId")]
        public Servicio Servicio { get => _servicio; set => Set(ref _servicio, value); }
    }
}