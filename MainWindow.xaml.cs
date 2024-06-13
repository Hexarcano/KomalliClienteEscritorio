using KomalliClienteEscritorio.Login.Model;
using KomalliClienteEscritorio.Request;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KomalliClienteEscritorio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            frame.Navigate(new Uri("Login/View/LoginPage.xaml", UriKind.Relative));
        }

        public void CambiarAPage(Page nuevaPagina)
        {
            if (nuevaPagina != null)
            {
                frame.Navigate(nuevaPagina);
            }
        }
    }
}