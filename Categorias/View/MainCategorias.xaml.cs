using KomalliClienteEscritorio.AdminHome.View;
using KomalliClienteEscritorio.Categorias.Model;
using KomalliClienteEscritorio.Login.Model;
using KomalliClienteEscritorio.Request;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    /// Lógica de interacción para MainCategorias.xaml
    /// </summary>
    public partial class MainCategorias : Page
    {
        private CategoriaResponse respuesta;
        private ObservableCollection<Categoria> categorias;
        private Sesion sesion;
        private MainWindow window;

        public MainCategorias()
        {
            categorias = new ObservableCollection<Categoria>();
            window = Application.Current.MainWindow as MainWindow;

            InitializeComponent();
        }

        private void LlenarListView()
        {
            foreach (var categoria in respuesta.Categorias)
            {
                categorias.Add(categoria);
            }

            lvCategorias.ItemsSource = categorias;
        }

        public async void CargarDatos()
        {
            respuesta = await Peticion.PeticionGET<CategoriaResponse>("api/CategoriaProducto", "", GetSesion());

            LlenarListView();
        }

        private void Actualizar(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Categoria categoria)
            {
                EditarCategoria nuevaPage = new EditarCategoria();
                nuevaPage.SetSesion(GetSesion());
                nuevaPage.CargarDatos(categoria.Id);

                window.CambiarAPage(nuevaPage);
            }
        }

        private async void Eliminar(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is Categoria categoria)
            {
                MessageBoxResult result = MessageBox.Show($"¿Está seguro que desea eliminar la categoría: {categoria.Nombre}?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes)
                {
                    // Lógica para eliminar la categoría
                    categorias.Remove(categoria);
                    // Aquí podrías llamar a un método para eliminar la categoría en la base de datos, por ejemplo:
                    await Peticion.PeticionDELETE($"api/CategoriaProducto/{categoria.Id}", "", GetSesion());
                    MessageBox.Show($"Categoría {categoria.Nombre} eliminada.");
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
            EditarCategoria nuevaPagina = new EditarCategoria();
            nuevaPagina.SetSesion(GetSesion());

            window!.CambiarAPage(nuevaPagina);
        }

        private void Buscar(object sender, RoutedEventArgs e)
        {

        }
    }
}
