using CuatroEstaciones.Core.Models;
using CuatroEstaciones.Core.Services.EF;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CuatroEstaciones.Core.ViewModels {

    public class ClientesViewModel : BaseViewModel {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;
        private EFService _efService;

        private Cliente _clienteSeleccionado;
        private ObservableCollection<Cliente> _listaClientes;

        public IMvxAsyncCommand CrearClienteCommand { get; private set; }
        public IMvxAsyncCommand EditarClienteCommand { get; private set; }
        public IMvxAsyncCommand EliminarClienteCommand { get; private set; }

        // Constructores e inicializadores
        public ClientesViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger) {
            _navigationService = navigationService;
            _messenger = messenger;

            CrearClienteCommand = new MvxAsyncCommand(CrearCliente);
            EditarClienteCommand = new MvxAsyncCommand(EditarCliente);
            EliminarClienteCommand = new MvxAsyncCommand(EliminarCliente);
        }

        public override async Task Initialize() {
            await base.Initialize();

            _efService = new EFService(_messenger);
            ListaClientes = _efService.GetAll<Cliente>();
        }

        // Propiedades expuestas
        public Cliente ClienteSeleccionado {
            get { return _clienteSeleccionado; }
            set { SetProperty(ref _clienteSeleccionado, value); }
        }

        public ObservableCollection<Cliente> ListaClientes {
            get { return _listaClientes; }
            set { SetProperty(ref _listaClientes, value); }
        }

        // Comandos
        private async Task CrearCliente() {
            await _navigationService.Navigate<ClientesDetalleViewModel,Cliente>(null);
        }

        private async Task EditarCliente() {
            await _navigationService.Navigate<ClientesDetalleViewModel, Cliente>(ClienteSeleccionado);
        }

        private async Task EliminarCliente() {
            _efService.Delete<Cliente>(ClienteSeleccionado);
        }
    }
}