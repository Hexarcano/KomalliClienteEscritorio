using KomalliClienteEscritorio.Categorias.Model;
using KomalliClienteEscritorio.Login.Model;
using KomalliClienteEscritorio.Request;
using KomalliClienteEscritorio.Util;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace KomalliClienteEscritorio.Categorias.View
{
    /// <summary>
    /// Lógica de interacción para EditarCategoria.xaml
    /// </summary>
    public partial class EditarCategoria : Page
    {
        private MainWindow window;
        private Sesion sesion;
        private CategoriaResponse respuesta;
        private string rutaArchivo;
        private bool isActualizar = false;
        private int categoriaId = 0;
        private string cadenaBase64;

        public EditarCategoria()
        {
            window = Application.Current.MainWindow as MainWindow;

            InitializeComponent();
        }

        private void Atras(object sender, RoutedEventArgs e)
        {
            MainCategorias nuevaPage = new MainCategorias();
            nuevaPage.SetSesion(GetSesion());
            nuevaPage.CargarDatos();

            window.CambiarAPage(nuevaPage);
        }

        private void BuscarImagen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Seleccionar Archivo";
            openFileDialog.Filter = "Archivos de Imagen|*.jpg;*.jpeg;*.png;*.gif|Todos los archivos|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            bool? resultado = openFileDialog.ShowDialog();

            if (resultado == true)
            {
                rutaArchivo = openFileDialog.FileName;
                imgCategoria.Source = new BitmapImage(new Uri(rutaArchivo));
            }
        }

        private async void Guardar(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isActualizar)
                {
                    var nombre = tbNombre.Text.ToString();

                    Categoria categoria = new Categoria()
                    {
                        Id = categoriaId,
                        Nombre = nombre,
                        ImagenBase64 = cadenaBase64
                    };

                    respuesta = await Peticion.PeticionPUT<CategoriaResponse>("api/CategoriaProducto", categoriaId.ToString(), categoria, GetSesion());
                }
                else
                {
                    var nombre = tbNombre.Text.ToString();
                    byte[] bytesImagen = File.ReadAllBytes(rutaArchivo);

                    string base64String = Convert.ToBase64String(bytesImagen);

                    Categoria categoria = new Categoria()
                    {
                        Nombre = nombre,
                        ImagenBase64 = base64String
                    };

                    respuesta = await Peticion.PeticionPOST<CategoriaResponse>("api/CategoriaProducto", categoria, GetSesion());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al convertir imagen a Base64: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public async void CargarDatos(int idCategoria)
        {
            respuesta = await Peticion.PeticionGET<CategoriaResponse>("api/CategoriaProducto", idCategoria.ToString(), GetSesion());

            categoriaId = idCategoria;
            cadenaBase64 = respuesta.Categorias[0].ImagenBase64;

            tbNombre.Text = respuesta.Categorias[0].Nombre;
            imgCategoria.Source = ConvertidorBase64.ConvertirBase64AImagen(respuesta.Categorias[0].ImagenBase64);

            isActualizar = true;
        }
    }
}
