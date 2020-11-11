using Android.App;
using Android.OS;
using CuatroEstaciones.Core.ViewModels;
using MvvmCross.Forms.Platforms.Android.Views;

namespace CuatroEstaciones.Droid {

    [Activity(
        Theme = "@style/AppTheme")]
    public class MainActivity : MvxFormsAppCompatActivity<MainViewModel> {

        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
        }
    }
}