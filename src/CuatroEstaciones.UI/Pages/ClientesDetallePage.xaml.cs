using CuatroEstaciones.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace CuatroEstaciones.UI.Pages {

    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true, Title = "Clientes - Detalle")]
    public partial class ClientesDetallePage : MvxContentPage<ClientesDetalleViewModel> {

        public ClientesDetallePage() {
            InitializeComponent();
        }
    }
}