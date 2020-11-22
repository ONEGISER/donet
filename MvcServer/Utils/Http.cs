using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MvcServer.Utils {
    public class Http {
        public async Task<Object> RequestGet (string _url) {
            var uri = new Uri (_url);
            Console.WriteLine ($"url=>{uri}");

            var request = (HttpWebRequest) WebRequest.Create (uri);
            request.Method = "GET";
            try {
                var response = await request.GetResponseAsync () as HttpWebResponse;
                using (var reader = new StreamReader (response.GetResponseStream ())) {
                    var result = reader.ReadToEnd ();
                    return result;
                }
            } catch (System.Exception) {
                return null;
            }
        }

        public async Task<Object> RequestPost (string _url, string postdata) {
            var uri = new Uri (_url);
            Console.WriteLine ($"url=>{uri}");
            var request = (HttpWebRequest) WebRequest.Create (uri);
            request.ContentType = "application/json";
            request.Method = "POST";
            HttpWebResponse response = null;
            try {
                if (postdata == null) {
                    using (var writer = new StreamWriter (await request.GetRequestStreamAsync ())) {
                        writer.Write (postdata);
                        writer.Flush ();
                    }
                } else {
                    byte[] bytes;
                    bytes = Encoding.UTF8.GetBytes (postdata);
                    request.ContentLength = bytes.Length;
                    //将请求参数写入流
                    using (var writer = request.GetRequestStream ()) {
                        writer.Write (bytes, 0, bytes.Length);
                    }
                }

                response = (HttpWebResponse) await request.GetResponseAsync ();
                if (response.StatusCode != HttpStatusCode.OK) {
                    return null;
                }
                using (var reader = new StreamReader (response.GetResponseStream ())) {
                    var result = JObject.ReadFrom (new JsonTextReader (reader));
                    return result;
                }
            } catch (System.Exception) {
                return null;
            } finally {
                response?.Close ();
            }
        }
    }
}