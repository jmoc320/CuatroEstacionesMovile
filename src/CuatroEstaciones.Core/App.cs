using CuatroEstaciones.Core.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace CuatroEstaciones.Core {

    public class App : MvxApplication {

        public override void Initialize() {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<RootViewModel>();
        }
    }
}