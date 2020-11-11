using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuatroEstaciones.Core.Models {

    public class Venta : BaseModel {
        private int _id;
        private string _fecha;
        private double _importeEfectivo;
        private double _importeTarjeta;
        private double _importeBizum;
        private double _importeTransferencia;
        private string _pagado;
        private int _clienteId;
        private Cliente _cliente;

        public Venta() {
            Fecha = DateTimeOffset.Now.ToString("yyyy-MM-dd");
            EstaPagado = true;
        }

        [Key]
        [Column("VentaId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get => _id; set => Set(ref _id, value); }

        public string Fecha { get => _fecha; set => Set(ref _fecha, value); }
        public double ImporteEfectivo { get => _importeEfectivo; set { Set(ref _importeEfectivo, value); RaisePropertyChanged("ImporteTotal"); RaisePropertyChanged("DescuentoAplicado"); } }
        public double ImporteTarjeta { get => _importeTarjeta; set { Set(ref _importeTarjeta, value); RaisePropertyChanged("ImporteTotal"); RaisePropertyChanged("DescuentoAplicado"); } }
        public double ImporteBizum { get => _importeBizum; set { Set(ref _importeBizum, value); RaisePropertyChanged("ImporteTotal"); RaisePropertyChanged("DescuentoAplicado"); } }
        public double ImporteTransferencia { get => _importeTransferencia; set { Set(ref _importeTransferencia, value); RaisePropertyChanged("ImporteTotal"); RaisePropertyChanged("DescuentoAplicado"); } }
        public string Pagado { get => _pagado; set { Set(ref _pagado, value); RaisePropertyChanged("EstaPagado"); } }
        public int ClienteId { get => _clienteId; set => Set(ref _clienteId, value); }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get => _cliente; set => Set(ref _cliente, value); }

        public ObservableCollection<VentaServicio> VentaServicios { get; set; }

        [NotMapped]
        public double ImporteTotal => ImporteEfectivo + ImporteTarjeta + ImporteBizum + ImporteTransferencia;

        [NotMapped]
        public double CosteServicios {
            get {
                double costeServicios = 0;
                if (VentaServicios != null) {
                    foreach (VentaServicio ventaServicio in VentaServicios) {
                        if (ventaServicio.Servicio != null) {
                            costeServicios += ventaServicio.Servicio.Precio;
                        }
                    }
                }
                return costeServicios;
            }
        }

        [NotMapped]
        public string DescuentoAplicado => CosteServicios == 0
                    ? "0%"
                    : ImporteTotal > 0 ? Math.Round((1 - (ImporteTotal / CosteServicios)) * 100, 2).ToString() + "%" : "100%";

        [NotMapped]
        public bool EstaPagado {
            get => Pagado == "S";
            set => Pagado = value ? "S" : "N";
        }
    }
}