using KomalliClienteEscritorio.Login.Model;
using KomalliClienteEscritorio.Ordenes.Model;
using KomalliClienteEscritorio.Productos.View;
using KomalliClienteEscritorio.Request;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KomalliClienteEscritorio.Ordenes.View
{
    /// <summary>
    /// Lógica de interacción para MainOrdenes.xaml
    /// </summary>
    public partial class MainOrdenes : Page
    {
        private OrdenResponse respuesta;
        private ObservableCollection<Orden> ordenes;
        private Sesion sesion;
        private MainWindow window;

        public MainOrdenes()
        {
            ordenes = new ObservableCollection<Orden>();
            window = Application.Current.MainWindow as MainWindow;

            InitializeComponent();
        }

        private void LlenarListView()
        {
            if (respuesta.Ordenes != null)
            {
                foreach (var orden in respuesta.Ordenes)
                {
                    ordenes.Add(orden);
                }

                lvProductos.ItemsSource = ordenes;
            }
        }

        public async void CargarDatos()
        {
            respuesta = await Peticion.PeticionGET<OrdenResponse>("api/Orden", "", GetSesion());

            LlenarListView();
        }

        private void Actualizar(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Orden orden)
            {
                EditarOrdenes nuevaPage = new EditarOrdenes();
                nuevaPage.SetSesion(GetSesion());
                nuevaPage.CargarDatos(orden.Id);

                window.CambiarAPage(nuevaPage);
            }
        }

        private async void Eliminar(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Orden orden)
            {
                MessageBoxResult result = MessageBox.Show($"¿Está seguro que desea eliminar la orden: {orden.Id}?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // lógica para eliminar la categoría
                    ordenes.Remove(orden);
                    // aquí podrías llamar a un método para eliminar la categoría en la base de datos, por ejemplo:
                    await Peticion.PeticionDELETE($"api/Orden/{orden.Id}", "", GetSesion());
                    MessageBox.Show($"Orden {orden.Id} eliminada.");
                }
            }
        }

        public void SetSesion(Sesion sesion)
        {
            this.sesion = sesion;
        }

        public Sesion GetSesion()
        {
            return this.sesion;
        }

        private void Atras(object sender, RoutedEventArgs e)
        {
            AdminHome.View.AdminHome nuevaPage = new AdminHome.View.AdminHome();
            nuevaPage.SetSesion(GetSesion());

            window.CambiarAPage(nuevaPage);
        }

        private void Agregar(object sender, RoutedEventArgs e)
        {
            EditarOrdenes nuevaPagina = new EditarOrdenes();
            nuevaPagina.SetSesion(GetSesion());

            window!.CambiarAPage(nuevaPagina);
        }

        private void Buscar(object sender, RoutedEventArgs e)
        {

        }
    }
}
