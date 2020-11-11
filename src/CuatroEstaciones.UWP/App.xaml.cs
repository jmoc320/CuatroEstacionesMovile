using MvvmCross.Forms.Platforms.Uap.Views;

namespace CuatroEstaciones.UWP {

    sealed partial class App {

        public App() {
            InitializeComponent();
        }
    }

    public abstract class MvxFormsApp : MvxWindowsApplication<Setup, Core.App, UI.App, MainPage> {
    }
}