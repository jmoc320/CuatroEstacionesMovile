using CuatroEstaciones.Core.Models;
using CuatroEstaciones.Core.Services.EF;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CuatroEstaciones.Core.ViewModels {

    public class ClientesViewModel : BaseViewModel<Cliente> {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMvxMessenger _messenger;
        private readonly MvxSubscriptionToken _token;
        private EFService _efService;

        private Cliente _clienteSeleccionado;
        private ObservableCollection<Cliente> _listaClientes;

        public IMvxAsyncCommand CrearClienteCommand { get; private set; }
        public IMvxAsyncCommand EditarClienteCommand { get; private set; }
        public IMvxCommand EliminarClienteCommand { get; private set; }

        // Constructores e inicializadores
        public ClientesViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger) {
            _navigationService = navigationService;
            _messenger = messenger;
            _token = messenger.Subscribe<NotificationMessage>(OnNotificationMessage);

            CrearClienteCommand = new MvxAsyncCommand(CrearCliente);
            EditarClienteCommand = new MvxAsyncCommand(EditarCliente);
            EliminarClienteCommand = new MvxCommand(EliminarCliente);
        }

        private void OnNotificationMessage(NotificationMessage obj) {
            Application.Current.MainPage.DisplayAlert(obj.Title, obj.Message, "OK");
        }

        public override async Task Initialize() {
            await base.Initialize();

            _efService = new EFService(_messenger);
            ListaClientes = _efService.GetAll<Cliente>();

            if (_clienteSeleccionado != null) {
                ClienteSeleccionado = _listaClientes.Where(x => x.Id == _clienteSeleccionado.Id).FirstOrDefault();
            }
        }

        public override void Prepare(Cliente parameter) {
            if (parameter != null) {
                ClienteSeleccionado = parameter;
            }
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

            if (Application.Current.MainPage is MasterDetailPage masterDetailPage) {
                masterDetailPage.IsPresented = false;
            }
            else if (Application.Current.MainPage is NavigationPage navigationPage
                     && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail) {
                nestedMasterDetail.IsPresented = false;
            }
        }

        private async Task EditarCliente() {
            await _navigationService.Navigate<ClientesDetalleViewModel, Cliente>(_clienteSeleccionado);

            if (Application.Current.MainPage is MasterDetailPage masterDetailPage) {
                masterDetailPage.IsPresented = false;
            }
            else if (Application.Current.MainPage is NavigationPage navigationPage
                     && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail) {
                nestedMasterDetail.IsPresented = false;
            }
        }

        private void EliminarCliente() {
            _efService.Delete<Cliente>(_clienteSeleccionado);
        }
    }
}