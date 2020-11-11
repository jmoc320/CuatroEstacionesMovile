using MvvmCross.ViewModels;

namespace CuatroEstaciones.Core.ViewModels {

    public abstract class BaseViewModel<TParameter, TResult> : BaseViewModelResult<TResult>, IMvxViewModel<TParameter, TResult>
        where TParameter : notnull
        where TResult : notnull {

        public abstract void Prepare(TParameter parameter);
    }
}