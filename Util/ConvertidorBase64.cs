using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace KomalliClienteEscritorio.Util
{
    public static class ConvertidorBase64
    {
        public static BitmapImage ConvertirBase64AImagen(string imagenBase64)
        {
            byte[] imagenBytes = Convert.FromBase64String(imagenBase64);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(imagenBytes);
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad; // Para cargar la imagen de manera inmediata
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}
