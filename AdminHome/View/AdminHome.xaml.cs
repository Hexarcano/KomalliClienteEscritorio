using KomalliClienteEscritorio.Categorias.View;
using KomalliClienteEscritorio.Login.Model;
using KomalliClienteEscritorio.Login.View;
using KomalliClienteEscritorio.Ordenes.View;
using KomalliClienteEscritorio.Productos;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KomalliClienteEscritorio.AdminHome.View
{
    /// <summary>
    /// Lógica de interacción para AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Page
    {
        private MainWindow window;
        private Sesion sesion;

        public AdminHome()
        {
            InitializeComponent();

            window = Application.Current.MainWindow as MainWindow;
        }

        private void AdministrarCategorias(object sender, RoutedEventArgs e)
        {
            MainCategorias nuevaPagina = new MainCategorias();
            nuevaPagina.SetSesion(GetSesion());
            nuevaPagina.CargarDatos();

            window!.CambiarAPage(nuevaPagina);
        }

        private void AdministrarProductos(object sender, RoutedEventArgs e)
        {
            MainProductos nuevaPagina = new MainProductos();
            nuevaPagina.SetSesion(GetSesion());
            nuevaPagina.CargarDatos();

            window!.CambiarAPage(nuevaPagina);
        }

        private void AdministrarOrdenes(object sender, RoutedEventArgs e)
        {
            MainOrdenes nuevaPagina = new MainOrdenes();
            nuevaPagina.SetSesion(GetSesion());
            nuevaPagina.CargarDatos();

            window!.CambiarAPage(nuevaPagina);
        }

        private void AdministrarClientes(object sender, RoutedEventArgs e)
        {
            //MainCategorias nuevaPagina = new MainCategorias();
            //nuevaPagina.SetSesion(GetSesion());
            //nuevaPagina.CargarDatos();

            //window!.CambiarAPage(nuevaPagina);
        }

        public void SetSesion(Sesion sesion)
        {
            this.sesion = sesion;
        }

        public Sesion GetSesion()
        {
            return this.sesion;
        }

        private async void CerrarSesion(object sender, RoutedEventArgs e)
        {
            LoginResponse? respuesta = await Peticion.PeticionPOST<LoginResponse>("api/cliente/logout", "", GetSesion());

            if (respuesta != null)
            {
                LoginPage nuevaPage = new LoginPage();

                window.CambiarAPage(nuevaPage);
            }
        }
    }
}
