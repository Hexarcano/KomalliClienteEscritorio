using KomalliClienteEscritorio.Login.Model;
using KomalliClienteEscritorio.Ordenes.Model;
using KomalliClienteEscritorio.Productos;
using KomalliClienteEscritorio.Productos.Model;
using KomalliClienteEscritorio.Request;
using System;
using System.Collections.Generic;
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
        private OrdenResponse respuesta;
        private bool isActualizar = false;
        private List<Producto> productos;
        private Guid ordenId = Guid.Empty;

        public EditarOrdenes()
        {
            window = Application.Current.MainWindow as MainWindow;
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
            MainProductos nuevaPage = new MainProductos();
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
            if (sender is Button button && button.Tag is Orden orden)
            {
                //MessageBoxResult result = MessageBox.Show($"¿Está seguro que desea eliminar la orden: {orden.Id}?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                //if (result == MessageBoxResult.Yes)
                //{
                //    // Lógica para eliminar la categoría
                //    ordenes.Remove(orden);
                //    // Aquí podrías llamar a un método para eliminar la categoría en la base de datos, por ejemplo:
                //    await Peticion.PeticionDELETE($"api/Producto/{orden.Id}", "", GetSesion());
                //    MessageBox.Show($"Orden {orden.Id} eliminada.");
                //}
            }
        }

        private async void Guardar(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombreCliente = tbNombreCliente.Text;
                double precioTotal = double.Parse(tbPrecioTotal.Text);
                bool categoria = (bool)cbPagado.SelectedValue;
                string comentario = tvComentario.Text;

                //if (isActualizar)
                //{
                //    Producto producto = new Producto()
                //    {
                //        Id = id,
                //        Nombre = nombre,
                //        Precio = precio,
                //        PorcentajeDescuento = descuento,
                //        CategoriaProductoId = categoria
                //    };

                //    respuestaProducto = await Peticion.PeticionPUT<ProductoResponse>("api/Producto", productoId.ToString(), producto, GetSesion());
                //}
                //else
                //{
                //    Producto producto = new Producto()
                //    {
                //        Nombre = nombre,
                //        Precio = precio,
                //        PorcentajeDescuento = descuento,
                //        CategoriaProductoId = categoria
                //    };

                //    respuestaProducto = await Peticion.PeticionPOST<ProductoResponse>("api/Producto", producto, GetSesion());
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al convertir imagen a Base64: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void CargarDatos(Guid ordenId)
        {
            respuesta = await Peticion.PeticionGET<OrdenResponse>("api/Orden", ordenId.ToString(), GetSesion());
        }

        private void LlenarDatos()
        {
            if (respuesta.Ordenes != null)
            {
                // TODO
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

            //foreach (var item in respuesta.Productos)
            //{
            //    var producto = new ProductoOrden()
            //    {

            //    };

            //    productos.Add(producto);
            //}

            if(productos.Count > 0)
            {
                foreach (var item in productos)
                {
                    var producto = new ProductoOrden()
                    {
                        ProductoId = item.Id,
                        Cantidad = 1,
                        PrecioUnitario = item.Precio,
                        
                    };

                    //productos.Add(producto);
                }
            }

            lvProductos.ItemsSource = productos;
        }
    }
}
