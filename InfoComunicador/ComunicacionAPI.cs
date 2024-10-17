using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InfoComunicador
{
    internal class ComunicacionAPI
    {
        public static string Get(string apiUrl, string tipo)
        {
            //string apiUrl = "http://192.168.17.20/InfoComunicador/cartel/"; // Reemplaza esto con la URL de tu API

            using HttpClient client = new();
            try
            {
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;
                    return ProcesarJson.GetCadena(jsonResponse, tipo);
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        //http://192.168.17.20/InfoComunicador/Cartel/Nuevo
        public static string PostCartel(string apiUrl, string cartel, int tamFuente)
        {
            apiUrl += "?Cartel=" + cartel + @"&TamFuente=" + tamFuente;

            using HttpClient client = new();
            try
            {
                HttpResponseMessage response = client.PostAsync(apiUrl, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;
                    return jsonResponse;
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch(Exception ex) 
            {
                return $"Error: {ex.Message}";
            }
        }

        public static string PutColor(string apiUrl, string color)
        {
            apiUrl += "?Color=" + color;

            using HttpClient client = new();
            try
            {
                HttpResponseMessage response = client.PutAsync(apiUrl, null).Result;

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = response.Content.ReadAsStringAsync().Result;
                    return jsonResponse;
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

    }

    public class ProcesarJson
    {
        /*
        public static string GetCadena(string json, string campo)
        {
            try
            {
                JObject jsonObject = JObject.Parse(json);
                JToken token = jsonObject[campo];

                if (token != null)
                {
                    return token.Value<string>();
                }
                else
                {
                    return $"Campo '{campo}' no encontrado en el JSON.";
                }
            }
            catch (Exception ex)
            {
                return $"Error al extraer el campo: {ex.Message}";
            }
        }
        */

        public static string GetCadena(string json, string campo)
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                {
                    return ".Error: El JSON recibido está vacío o es nulo.";
                }

                JObject jsonObject = JObject.Parse(json);

                if (jsonObject.TryGetValue(campo, out JToken token) && token != null)
                {
                    return token.Value<string>();
                }
                else
                {
                    return $".Error: Campo '{campo}' no encontrado en el JSON.";
                }
            }
            catch (JsonReaderException ex)
            {
                return $".Error: Formato JSON erroneo: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $".Error: desconocido: {ex.Message}";
            }
        }


        public static object DeserializarDesdeStream(Stream str)
        {
            try
            {
                JsonSerializer serializador = new();

                using StreamReader sr = new(str);
                using JsonTextReader lector = new(sr);
                object? ob = serializador.Deserialize(lector);

                if (ob == null)
                    throw new Exception("El objeto resultante es nulo.");

                return ob;

            }catch(Exception ex)
            {
                throw new Exception("No se pudo deserializar el objeto. Detalle: " + ex.Message);
            }
        }
    }

    public class AppConfig
    {
        public static string GetCampo(string archivo, string campo)
        {
            string json = "";
            try
            {
                using StreamReader streamReader = new(archivo);
                json = streamReader.ReadToEnd();
            }catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("No se encontró el archivo \"" + ex.FileName + "\"");
            }


            return ProcesarJson.GetCadena(json, campo);
        }
    }

    public class InfoComunicadorAux
    {
        public static string GetLlamada(string llamada)
        {
            string server = AppConfig.GetCampo("config.txt", "api_servidor");
            string ruta = AppConfig.GetCampo("config.txt", "api_ruta");

            if(server == null)
                throw new NullReferenceException("Error: campo \"api_servidor\" en el archivo config.txt es nulo.");

            if (ruta == null)
                throw new NullReferenceException("Error: campo \"api_ruta\" en el archivo config.txt es nulo.");

            if (server.Contains(".Error:"))
                throw new Exception(server);

            if(ruta.Contains(".Error:"))
                throw new Exception(ruta);

            return @"http://" + server + ruta + llamada;
        }
    }
}
