using CuatroEstaciones.Core.Models;
using CuatroEstaciones.Core.Services.EF;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CuatroEstaciones.Core.ViewModels {

    public class ClientesDetalleViewModel : BaseViewModel<Cliente> {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;
        private EFService _efService;

        private Cliente _clienteSeleccionado;

        public IMvxAsyncCommand GuardarCambiosCommand { get; private set; }


        // Constructores e inicializadores
        public ClientesDetalleViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger) {
            _navigationService = navigationService;
            _messenger = messenger;
            GuardarCambiosCommand = new MvxAsyncCommand(GuardarCambios);
        }

        public override async Task Initialize() {
            await base.Initialize();

            _efService = new EFService(_messenger);
        }

        public override void Prepare(Cliente parameter) {
            if (parameter != null) {
                ClienteSeleccionado = parameter;
            }
            else {
                ClienteSeleccionado = new Cliente();
            }
        }

        // Propiedades expuestas
        public Cliente ClienteSeleccionado {
            get { return _clienteSeleccionado; }
            set { SetProperty(ref _clienteSeleccionado, value); }
        }

        // Comandos
        private async Task GuardarCambios() {
            _efService.InsertOrUpdate<Cliente>(_clienteSeleccionado);
            await _navigationService.Navigate<ClientesViewModel>();
        }

    }
}