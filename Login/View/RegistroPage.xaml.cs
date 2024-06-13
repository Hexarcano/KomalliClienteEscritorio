using KomalliClienteEscritorio.Categorias.View;
using KomalliClienteEscritorio.Login.Model;
using KomalliClienteEscritorio.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Lógica de interacción para RegistroPage.xaml
    /// </summary>
    public partial class RegistroPage : Page
    {
        private MainWindow window;

        public RegistroPage()
        {
            window = Application.Current.MainWindow as MainWindow;

            InitializeComponent();
        }

        private void Atras(object sender, RoutedEventArgs e)
        {
            LoginPage nuevaPage = new LoginPage();

            window.CambiarAPage(nuevaPage);
        }

        private void Registrar(object sender, RoutedEventArgs e)
        {
            string nombre = tbNombre.Text;
            string apellidoPaterno = tbApellidoPaterno.Text;
            string apellidoMaterno = tbApellidoMaterno.Text;
            string email = tbEmail.Text;
            string usuario = tbUsuario.Text;
            string contrasenia = pbContrasenia.Password.ToString();

            DatosRegistro datosRegistro = new DatosRegistro()
            {
                Nombre = nombre,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                Email = email,
                Usuario = usuario,
                Contrasenia = contrasenia
            };

            var respuesta = Peticion.PeticionPOST<LoginResponse>("api/cliente/registrar", datosRegistro, null);

            if (respuesta != null)
            {
                LoginPage nuevaPage = new LoginPage();

                window.CambiarAPage(nuevaPage);
            }
        }
    }
}
