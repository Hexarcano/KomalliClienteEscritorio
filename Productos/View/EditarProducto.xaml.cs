using KomalliClienteEscritorio.Categorias.Model;
using KomalliClienteEscritorio.Categorias.View;
using KomalliClienteEscritorio.Login.Model;
using KomalliClienteEscritorio.Productos.Model;
using KomalliClienteEscritorio.Request;
using KomalliClienteEscritorio.Util;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace KomalliClienteEscritorio.Productos.View
{
    /// <summary>
    /// Lógica de interacción para EditarProducto.xaml
    /// </summary>
    public partial class EditarProducto : Page
    {
        private MainWindow window;
        private Sesion sesion;
        private ProductoResponse respuestaProducto;
        private string rutaArchivo;
        private bool isActualizar = false;
        private int productoId = 0;
        private string cadenaBase64;

        public EditarProducto()
        {
            window = Application.Current.MainWindow as MainWindow;

            InitializeComponent();
        }

        private void Atras(object sender, RoutedEventArgs e)
        {
            MainProductos nuevaPage = new MainProductos();
            nuevaPage.SetSesion(GetSesion());
            nuevaPage.CargarDatos();

            window.CambiarAPage(nuevaPage);
        }

        public void SetSesion(Sesion sesion)
        {
            this.sesion = sesion;
        }

        public Sesion GetSesion()
        {
            return this.sesion;
        }

        public async void CargarProductoYCategorias(int idProducto)
        {
            respuestaProducto = await Peticion.PeticionGET<ProductoResponse>("api/Producto", idProducto.ToString(), GetSesion());

            productoId = idProducto;
            tbNombre.Text = respuestaProducto.Productos[0].Nombre;
            tbPrecio.Text = respuestaProducto.Productos[0].Precio.ToString();
            tbDescuento.Text = respuestaProducto.Productos[0].PorcentajeDescuento.ToString();
            //imgProducto.Source = ConvertidorBase64.ConvertirBase64AImagen(respuestaProducto.Productos[0].ImagenBase64);

            CargarSoloCategorias();

            isActualizar = true;
        }

        public async void CargarSoloCategorias()
        {
            CategoriaResponse respuestaCategorias = await Peticion.PeticionGET<CategoriaResponse>("api/CategoriaProducto", "", GetSesion());
            cbCategorias.ItemsSource = respuestaCategorias.Categorias;
            cbCategorias.DisplayMemberPath = "Nombre";
            cbCategorias.SelectedValuePath = "Id";

            if (productoId > 0)
            {
                cbCategorias.SelectedValue = respuestaProducto.Productos[0].CategoriaProductoId;
            } else
            {
                cbCategorias.SelectedValue = 0;
            }
        }

        private async void Guardar(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = productoId;
                string nombre = tbNombre.Text;
                double precio = double.Parse(tbPrecio.Text);
                int descuento = int.Parse(tbDescuento.Text);
                int categoria = (int)cbCategorias.SelectedValue;

                if (isActualizar)
                {
                    Producto producto = new Producto()
                    {
                        Id = id,
                        Nombre = nombre,
                        Precio = precio,
                        PorcentajeDescuento = descuento,
                        CategoriaProductoId = categoria
                    };

                    respuestaProducto = await Peticion.PeticionPUT<ProductoResponse>("api/Producto", productoId.ToString(), producto, GetSesion());
                }
                else
                {
                    Producto producto = new Producto()
                    {
                        Nombre = nombre,
                        Precio = precio,
                        PorcentajeDescuento = descuento,
                        CategoriaProductoId = categoria
                    };

                    respuestaProducto = await Peticion.PeticionPOST<ProductoResponse>("api/Producto", producto, GetSesion());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al convertir imagen a Base64: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
