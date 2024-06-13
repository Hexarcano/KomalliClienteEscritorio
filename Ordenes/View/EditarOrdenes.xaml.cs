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
    /// Lógica de interacción para EditarOrdenes.xaml
    /// </summary>
    public partial class EditarOrdenes : Page
    {
        private MainWindow window;
        private Sesion sesion;
        private OrdenResponse respuestaOrden;
        private ProductoResponse respuestaProducto;
        private bool isActualizar = false;
        private List<Producto> productos;
        private ObservableCollection<ProductoOrdenListview> listview;
        private Guid ordenId = Guid.Empty;

        public EditarOrdenes()
        {
            window = Application.Current.MainWindow as MainWindow;
            listview = new ObservableCollection<ProductoOrdenListview>();
            productos = new List<Producto>();

            List<bool> bools = new List<bool>()
            {
                true,
                false
            };

            InitializeComponent();

            cbPagado.ItemsSource = bools;
            cbPagado.SelectedValue = true;
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
            MainOrdenes nuevaPage = new MainOrdenes();
            nuevaPage.SetSesion(GetSesion());
            nuevaPage.CargarDatos();

            window.CambiarAPage(nuevaPage);
        }

        private void Agregar(object sender, RoutedEventArgs e)
        {
            AgregarProducto nuevaPage = new AgregarProducto();
            nuevaPage.SetSesion(GetSesion());
            nuevaPage.CargarProductos();

            window.CambiarAPage(nuevaPage);
        }

        private async void Eliminar(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is ProductoOrdenListview productoOrden)
            {
                MessageBoxResult result = MessageBox.Show($"¿Está seguro que desea eliminar el producto: {productoOrden.ProductoId} de la orden?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    var producto = productos.Find(p => p.Id == productoOrden.ProductoId);
                    productos.Remove(producto);
                    LlenarListView();

                    MessageBox.Show($"Orden {producto.Id} eliminada.");
                }
            }
        }

        private async void Guardar(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombreCliente = tbNombreCliente.Text;
                double precioTotal = double.Parse(tbPrecioTotal.Text);
                bool pagado = (bool)cbPagado.SelectedValue;
                string comentario = tvComentario.Text;

                if (isActualizar)
                {
                    List<ProductoOrdenRegistro> productosRegistro = new List<ProductoOrdenRegistro>();

                    foreach (var item in productos)
                    {
                        productosRegistro.Add(new ProductoOrdenRegistro()
                        {
                            ProductoId = item.Id,
                            Cantidad = 1,
                            PrecioUnitario = item.Precio,
                            SubtotalProductos = precioTotal
                        });
                    }

                    OrdenRegistro orden = new OrdenRegistro()
                    {
                        Id = respuestaOrden.Ordenes.First().Id,
                        NombreCliente = nombreCliente,
                        PrecioTotal = precioTotal,
                        Comentario = comentario,
                        Pagado = pagado,
                        Productos = productosRegistro
                    };

                    
                    respuestaOrden = await Peticion.PeticionPUT<OrdenResponse>("api/Orden", respuestaOrden.Ordenes.First().Id.ToString(), orden, GetSesion());
                }
                else
                {
                    List<ProductoOrdenRegistro> productosRegistro = new List<ProductoOrdenRegistro>();

                    foreach (var item in productos)
                    {
                        productosRegistro.Add(new ProductoOrdenRegistro()
                        {
                            ProductoId = item.Id,
                            Cantidad = 1,
                            PrecioUnitario = item.Precio,
                            SubtotalProductos = precioTotal
                        });
                    }

                    OrdenRegistro orden = new OrdenRegistro()
                    {
                        NombreCliente = nombreCliente,
                        PrecioTotal = precioTotal,
                        Comentario = comentario,
                        Pagado = pagado,
                        Productos = productosRegistro
                    };

                    respuestaOrden = await Peticion.PeticionPOST<OrdenResponse>("api/Orden", orden, GetSesion());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al convertir imagen a Base64: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void CargarDatos(Guid ordenId)
        {
            respuestaOrden = await Peticion.PeticionGET<OrdenResponse>("api/Orden", ordenId.ToString(), GetSesion());

            if (respuestaOrden.Productos.Count > 0)
            {
                foreach (var item in respuestaOrden.Productos)
                {
                    respuestaProducto = await Peticion.PeticionGET<ProductoResponse>("api/Producto", item.ProductoId.ToString(), GetSesion());
                    productos.Add(respuestaProducto.Productos.First());
                }
            }

            LlenarDatos();
            tbNombreCliente.IsEnabled = false;
            isActualizar = true;
            btnAgregar.Visibility = Visibility.Hidden;
        }

        private void LlenarDatos()
        {
            tbNombreCliente.Text = respuestaOrden.Ordenes[0].NombreCliente;
            tbPrecioTotal.Text = respuestaOrden.Ordenes[0].PrecioTotal.ToString();
            cbPagado.SelectedValue = respuestaOrden.Ordenes[0].Pagado;
            tvComentario.Text = respuestaOrden.Ordenes[0].Comentario;

            if (respuestaOrden.Ordenes != null)
            {
                LlenarListView();
            }
        }

        public void SetProductosDeOrden(Guid ordenId, List<Producto> productosEnCarrito)
        {
            if (ordenId != Guid.Empty)
            {
                CargarDatos(ordenId);
            }

            productos = productosEnCarrito;

            LlenarListView();
        }

        private void LlenarListView()
        {
            listview.Clear();

            if (productos.Count > 0)
            {
                foreach (var item in productos)
                {
                    var producto = new ProductoOrdenListview()
                    {
                        ProductoId = item.Id,
                        Cantidad = 1,
                        PrecioUnitario = item.Precio,
                        NombreProducto = item.Nombre,
                        SubtotalProductos = 0
                    };

                    listview.Add(producto);
                }
            }

            lvProductos.ItemsSource = listview;
        }
    }
}
