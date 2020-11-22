using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MvcServer.Controllers {
    [Route ("api/[controller]")]
    public class BaiDuAIController : Controller {

        public BaiDuAIController () { }
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        [HttpGet ("getToken")]
        public async Task<string> GetToken () {
            String authHost = "https://aip.baidubce.com/oauth/2.0/token";
            String clientId = "56eOeNE3Fp7gTlzxMkzHPAHt";
            String clientSecret = "cTbjV3FPCNvVQhzLpmmTUdkbuOgRBOMC";
            HttpClient client = new HttpClient ();

            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>> ();
            paraList.Add (new KeyValuePair<string, string> ("grant_type", "client_credentials"));
            paraList.Add (new KeyValuePair<string, string> ("client_id", clientId));
            paraList.Add (new KeyValuePair<string, string> ("client_secret", clientSecret));
            HttpResponseMessage response = client.PostAsync (authHost, new FormUrlEncodedContent (paraList)).Result;
            String result = response.Content.ReadAsStringAsync ().Result;
            return result;
        }

        /// <summary>
        /// 人像动漫化
        /// </summary>
        /// <returns></returns>
        [HttpPost ("selfie_anime")]
        public async Task<string> SelfieAnime (string base64) {
            var accessToken = await GetToken ();
            var accessTokenObject = JsonConvert.DeserializeObject<dynamic> (accessToken.ToString ());
            string token = accessTokenObject["access_token"].ToString ();
            string authHost = "https://aip.baidubce.com/rest/2.0/image-process/v1/selfie_anime?access_token=" + token;
            HttpClient client = new HttpClient ();
            List<KeyValuePair<String, String>> paraList = new List<KeyValuePair<string, string>> ();
            paraList.Add (new KeyValuePair<string, string> ("image", base64));
            HttpResponseMessage response = client.PostAsync (authHost, new FormUrlEncodedContent (paraList)).Result;
            String result = response.Content.ReadAsStringAsync ().Result;
            return result;
        }
    }
}