using KomalliClienteEscritorio.Login.Model;
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

namespace KomalliClienteEscritorio.Login.View
{
    /// <summary>
    /// Lógica de interacción para LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private MainWindow window;

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            lbError.Visibility = Visibility.Hidden;

            string usuario = tbUsuario.Text;
            string contrasenia = pbContrasenia.Password;

            DatosLogin datos = new DatosLogin()
            {
                Usuario = usuario,
                Contrasenia = contrasenia
            };

            LoginResponse? respuesta = await Peticion.PeticionPOST<LoginResponse>("api/cliente/login", datos, null);

            if (respuesta != null)
            {
                Sesion sesion = new Sesion()
                {
                    TokenType = respuesta.TokenType,
                    Token = respuesta.AccessToken
                };

                if (respuesta.AccessToken != null)
                {
                    window = Application.Current.MainWindow as MainWindow;

                    AdminHome.View.AdminHome nuevaPagina = new AdminHome.View.AdminHome();
                    nuevaPagina.SetSesion(sesion);

                    window!.CambiarAPage(nuevaPagina);
                }
                else
                {
                    lbError.Content = respuesta.Mensaje;
                    lbError.Visibility = Visibility.Visible;
                }
            }
            else
            {
                lbError.Content = "No fue posible conectar con el servidor";
                lbError.Visibility = Visibility.Visible;
            }

        }
    }
}
