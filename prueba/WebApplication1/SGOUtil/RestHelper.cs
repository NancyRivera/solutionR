using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using System.Net;
using System.Text;

namespace SGOUtil
{
    public static class RestHelper
    {
        public static Respuesta Execute(string api_key, string controller, string typeMethod, string metodoYParametros = "", string postString = "")
        {
            var respuesta = new Respuesta();
            try
            {
                string URL = string.Concat(ConfigurationManager.AppSettings["API"], controller, "/", metodoYParametros);
                HttpWebRequest webRequest = WebRequest.Create(URL) as HttpWebRequest;
                webRequest.Method = typeMethod;
                webRequest.ContentType = "application/json; charset=utf-8";
                if (!string.IsNullOrEmpty(api_key))
                {
                    webRequest.Headers.Add("Authorization", "Bearer " + api_key);
                }
                var tipoMethod = typeMethod.ToEnumValue<Util.HttMethod>();
                if (tipoMethod != Util.HttMethod.GET && tipoMethod != Util.HttMethod.DELETE)
                {
                    byte[] data = Encoding.UTF8.GetBytes(postString);
                    webRequest.ContentLength = data.Length;
                    System.IO.Stream postStream = webRequest.GetRequestStream();
                    postStream.Write(data, 0, data.Length);
                }
                HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                string body = reader.ReadToEnd();
                reader.Close();
                webResponse.Close();
                respuesta = !string.IsNullOrEmpty(body) ? JsonConvert.DeserializeObject<Respuesta>(body) : new Respuesta();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return respuesta;
        }
        public static string IsAuthenticated(string api_key, string controller, string typeMethod, string metodoYParametros = "")
        {
            var respuesta = "";
            try
            {
                string URL = string.Concat(ConfigurationManager.AppSettings["API"], controller, "/", metodoYParametros);
                HttpWebRequest webRequest = WebRequest.Create(URL) as HttpWebRequest;
                webRequest.Method = typeMethod;
                webRequest.ContentType = "application/json; charset=utf-8";
                if (!string.IsNullOrEmpty(api_key))
                {
                    webRequest.Headers.Add("Authorization", "Bearer " + api_key);
                }
                HttpWebResponse webResponse = webRequest.GetResponse() as HttpWebResponse;
                System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                string body = reader.ReadToEnd();
                reader.Close();
                webResponse.Close();
                respuesta = body;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return respuesta;
        }
    }//end class

    public static class RestApi<TRequest, TResponse> where TRequest : class where TResponse : class, new()
    {
        public static TResponse Execute(string controller, string metodo, Util.HttMethod typeMethod, TRequest request, string api_key = "")
        {
            try
            {
                string URL = $"{ConfigurationManager.AppSettings["API"]}{controller}";
                var client = new RestClient(URL);
                client.Timeout = 5000; // 5000 milliseconds == 5 seconds
                var method = Method.POST;
                if (typeMethod == Util.HttMethod.GET)
                {
                    method = Method.GET;
                }
                else if (typeMethod == Util.HttMethod.PUT)
                {
                    method = Method.PUT;
                }
                else if (typeMethod == Util.HttMethod.DELETE)
                {
                    method = Method.DELETE;
                }
                else
                {
                    method = Method.POST;
                }
                var restRequest = new RestRequest(metodo, method)
                {
                    RequestFormat = DataFormat.Json
                };
                if (!string.IsNullOrEmpty(api_key))
                {
                    restRequest.AddParameter("Authorization", $"Bearer {api_key}", ParameterType.HttpHeader);
                }
                if (method != Method.GET && method != Method.DELETE)
                {
                    restRequest.AddJsonBody(request);
                }
                var restResponse = client.Execute<TResponse>(restRequest);
                return restResponse?.Data;
                //if (restResponse.IsSuccessful)
                //    return restResponse?.Data;
                //else
                //    return new TResponse();
            }
            catch (System.Exception ex)
            {

                throw;
            }
            
        }

    }//end class
}
