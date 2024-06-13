using KomalliClienteEscritorio.Categorias.Model;
using KomalliClienteEscritorio.Categorias.View;
using KomalliClienteEscritorio.Login.Model;
using KomalliClienteEscritorio.Productos.Model;
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

namespace KomalliClienteEscritorio.Productos
{
    /// <summary>
    /// Lógica de interacción para MainProductos.xaml
    /// </summary>
    public partial class MainProductos : Page
    {
        private ProductoResponse respuesta;
        private ObservableCollection<Producto> productos;
        private Sesion sesion;
        private MainWindow window;

        public MainProductos()
        {
            productos = new ObservableCollection<Producto>();
            window = Application.Current.MainWindow as MainWindow;

            InitializeComponent();
        }

        private void LlenarListView()
        {
            foreach (var producto in respuesta.Productos)
            {
                productos.Add(producto);
            }

            lvProductos.ItemsSource = productos;
        }

        public async void CargarDatos()
        {
            respuesta = await Peticion.PeticionGET<ProductoResponse>("api/Producto", "", GetSesion());

            LlenarListView();
        }

        private void Actualizar(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Producto producto)
            {
                EditarProducto nuevaPage = new EditarProducto();
                nuevaPage.SetSesion(GetSesion());
                nuevaPage.CargarProductoYCategorias(producto.Id);

                window.CambiarAPage(nuevaPage);
            }
        }

        private async void Eliminar(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Producto producto)
            {
                MessageBoxResult result = MessageBox.Show($"¿Está seguro que desea eliminar el producto: {producto.Nombre}?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    // Lógica para eliminar la categoría
                    productos.Remove(producto);
                    // Aquí podrías llamar a un método para eliminar la categoría en la base de datos, por ejemplo:
                    await Peticion.PeticionDELETE($"api/Producto/{producto.Id}", "", GetSesion());
                    MessageBox.Show($"Producto {producto.Nombre} eliminado.");
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
            EditarProducto nuevaPagina = new EditarProducto();
            nuevaPagina.SetSesion(GetSesion());
            nuevaPagina.CargarSoloCategorias();

            window!.CambiarAPage(nuevaPagina);
        }

        private void Buscar(object sender, RoutedEventArgs e)
        {

        }
    }
}
