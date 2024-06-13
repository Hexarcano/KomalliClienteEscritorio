using KomalliClienteEscritorio.Login.Model;
using KomalliClienteEscritorio.Ordenes.Model;
using KomalliClienteEscritorio.Productos;
using KomalliClienteEscritorio.Productos.Model;
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
    /// Lógica de interacción para AgregarProducto.xaml
    /// </summary>
    public partial class AgregarProducto : Page
    {
        private MainWindow window;
        private Sesion sesion;
        private ProductoResponse respuesta;
        private ObservableCollection<Producto> productos;
        private List<Producto> productosEnCarrito;
        private bool isActualizar = false;
        private Guid ordenId = Guid.Empty;

        public AgregarProducto()
        {
            window = Application.Current.MainWindow as MainWindow;
            productos = new ObservableCollection<Producto>();
            productosEnCarrito = new List<Producto>();

            InitializeComponent();
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
            EditarOrdenes nuevaPage = new EditarOrdenes();
            nuevaPage.SetSesion(GetSesion());

            window.CambiarAPage(nuevaPage);
        }

        private void Agregar(object sender, RoutedEventArgs e)
        {
            productosEnCarrito.Clear();

            foreach (Producto item in lvProductos.SelectedItems)
            {
                productosEnCarrito.Add(item);
            }

            EditarOrdenes nuevaPage = new EditarOrdenes();
            nuevaPage.SetSesion(GetSesion());
            nuevaPage.SetProductosDeOrden(ordenId, productosEnCarrito);

            window.CambiarAPage(nuevaPage);
        }

        public async void CargarProductos()
        {
            respuesta = await Peticion.PeticionGET<ProductoResponse>("api/Producto", "", GetSesion());

            LlenarListView();
        }

        private void LlenarListView()
        {
            foreach (var producto in respuesta.Productos)
            {
                productos.Add(producto);
            }

            lvProductos.ItemsSource = productos;
        }

        public void SetOrderId(Guid orderId)
        {
            this.ordenId = orderId;
        }

        public Guid GetOrderId()
        {
            return this.ordenId;
        }
    }
}