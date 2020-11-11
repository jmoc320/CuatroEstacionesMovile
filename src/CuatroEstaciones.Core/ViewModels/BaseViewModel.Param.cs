using MvvmCross.ViewModels;

namespace CuatroEstaciones.Core.ViewModels {

    public abstract class BaseViewModel<TParameter> : BaseViewModel, IMvxViewModel<TParameter>
        where TParameter : notnull {

        public abstract void Prepare(TParameter parameter);
    }
}