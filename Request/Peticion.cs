using KomalliClienteEscritorio.Login.Model;
using KomalliClienteEscritorio.Shared;
using KomalliClienteEscritorio.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace KomalliClienteEscritorio.Request
{
    public static class Peticion
    {
        private static readonly HttpClient cliente = new HttpClient();

        public static async Task<T> PeticionPOST<T>(string path, object datos, Sesion sesion) where T : ISerializable
        {
            try
            {
                string ruta = $"{Constantes.urlBase}/{path}";
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(datos);

                if (sesion != null)
                {
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(sesion.TokenType, sesion.Token);
                }

                var contenido = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage respuestaHTTP = await cliente.PostAsync(ruta, contenido);
                respuestaHTTP.EnsureSuccessStatusCode();

                string body = await respuestaHTTP.Content.ReadAsStringAsync();

                T respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(body)!;

                return respuesta;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error en la solicitud POST: {e.Message}");

                return default(T); // Devolver el valor predeterminado de T en caso de error
            }
        }

        public static async Task<T> PeticionGET<T>(string path, string id, Sesion sesion) where T : ISerializable
        {
            try
            {
                string ruta = $"{Constantes.urlBase}/{path}/{id}";

                cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(sesion.TokenType, sesion.Token);
                HttpResponseMessage respuestaHTTP = await cliente.GetAsync(ruta);
                respuestaHTTP.EnsureSuccessStatusCode();

                string body = await respuestaHTTP.Content.ReadAsStringAsync();

                T respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(body)!;

                return respuesta;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error en la solicitud POST: {e.Message}");

                return default(T); // Devuelve el valor predeterminado de T si hay un error
            }
        }

        public static async Task<T> PeticionPUT<T>(string path, string id, object datos, Sesion sesion) where T : ISerializable
        {
            try
            {
                string ruta = $"{Constantes.urlBase}/{path}/{id}";
                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(datos);

                if (sesion != null)
                {
                    cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(sesion.TokenType, sesion.Token);
                }

                var contenido = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage respuestaHTTP = await cliente.PutAsync(ruta, contenido);
                respuestaHTTP.EnsureSuccessStatusCode();

                string body = await respuestaHTTP.Content.ReadAsStringAsync();

                T respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(body)!;

                return respuesta;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error en la solicitud POST: {e.Message}");

                return default(T); // Devolver el valor predeterminado de T en caso de error
            }
        }

        public static async Task<bool> PeticionDELETE(string path, string id, Sesion sesion)
        {
            string ruta = $"{Constantes.urlBase}/{path}/{id}";

            cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(sesion.TokenType, sesion.Token);
            HttpResponseMessage respuestaHTTP = await cliente.DeleteAsync(ruta);

            if (respuestaHTTP.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }
    }
}
