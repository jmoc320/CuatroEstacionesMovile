using CuatroEstaciones.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace CuatroEstaciones.UI.Pages {

    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Root, WrapInNavigationPage = false, Title = "NavigationMenu")]
    public partial class RootPage : MvxMasterDetailPage<RootViewModel> {

        public RootPage() {
            InitializeComponent();
        }
    }
}