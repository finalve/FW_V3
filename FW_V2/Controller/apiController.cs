using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FW_V3.Controller
{
    class apiController
    {
       
    }

    class WaxAPI
    {
        public CookieContainer cookies = new CookieContainer();
        public static IEnumerable<string> sign(string token, string trx)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://public-wax-on.wax.io/wam/sign"));
                var json = "{";
                json += "\"serializedTransaction\" : [" + trx + "],";
                json += "\"website\" : \"play.farmersworld.io\",";
                json += "\"description\" : \"jwt is insecure\"";
                json += "}";
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";

                request.UserAgent = "Android";
                request.Headers.Add("x-access-token", token);
                using (var stream = new StreamWriter(request.GetRequestStream())) stream.Write(json);

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                var data = JObject.Parse(responseString);

                return data["signatures"].Select(x => x.ToString()).ToArray();
            }
            catch (WebException ex)
            {
                var responseString = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                dynamic data = JObject.Parse(responseString);
                return new string[] { data["message"] };
            }
        }

        public dynamic postSession(string id, string pwd, string capt = null, string cookie1 = null, string cookie2 = null)
        {
            var uri = new Uri("https://all-access.wax.io/api/session");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            if (!Directory.Exists(@"cookies"))
                Directory.CreateDirectory(@"cookies");
            try
            {
                var json = "{";
                json += "\"password\" : \"" + pwd + "\",";
                json += "\"username\" : \"" + id + "\",";
                json += "\"redirectTo\" : \"\"";
                json += "}";
                request.Method = "POST";
                request.UserAgent = "Android";
                request.ContentType = "application/json;charset=UTF-8";
                request.CookieContainer = cookies;
                using (var stream = new StreamWriter(request.GetRequestStream())) stream.Write(json);

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return JObject.Parse(responseString);
            }
            catch (WebException ex)
            {
                var responseString = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                Console.WriteLine(ex.Message); return JObject.Parse(responseString); ;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
        }

        public string json2FA(string code, string token2fa)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://all-access.wax.io/api/session/2fa"));
            try
            {
                var json = "{";
                json += "\"code\" : \"" + code + "\",";
                json += "\"token2fa\" : \"" + token2fa + "\"";
                json += "}";
                request.Method = "POST";
                request.UserAgent = "Android";
                request.ContentType = "application/json;charset=UTF-8";
                request.CookieContainer = cookies;

                using (var stream = new StreamWriter(request.GetRequestStream())) stream.Write(json);

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                return "Invalid Code";
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
        }

        public string posttos(string id, string token)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://all-access.wax.io/api/tos"));
            try
            {
                var json = "{";
                json += "\"tos_id\" : " + id + ",";
                json += "\"tos_accepted\" : true ,";
                json += "\"age_accepted\" : true ,";
                json += "\"singleUseToken\" : \"" + token + "\"";
                json += "}";
                request.Method = "POST";
                request.UserAgent = "Android";
                request.ContentType = "application/json;charset=UTF-8";
                request.CookieContainer = cookies;

                using (var stream = new StreamWriter(request.GetRequestStream())) stream.Write(json);

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (WebException ex)
            {
                var responseString = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
        }
        public string Gettos(string code)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri($"https://all-access.wax.io/api/tos"));
            request.Method = "get";
            request.Referer = $"https://all-access.wax.io/tos?token={code}";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Android";
            request.Accept = "application/json, text/plain, */*";

            var response = (HttpWebResponse)request.GetResponse();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        public string GetWamID(string token)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri($"https://api-idm.wax.io/v1/accounts/auto-accept/login"));
            request.Method = "get";
            request.Referer = $"https://wallet.wax.io/";
            request.Headers.Add("origin", "https://wallet.wax.io");
            request.Headers.Add("cookie", $"session_token={token}");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json, text/plain, */*";
            request.UserAgent = "Android";
            var response = (HttpWebResponse)request.GetResponse();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        public string getSession()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://all-access.wax.io/api/session"));
            request.Method = "get";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Android";
            request.Accept = "application/json, text/plain, */*";
            request.CookieContainer = cookies;

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }

        public static string get(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            CookieContainer cookiese = new CookieContainer();

            request.Method = "get";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.UserAgent = "Andriod";
            request.UserAgent = "Android";

            request.CookieContainer = cookiese;
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }
       
        public async Task<JToken> Atomic(string name)
        {
            var url = string.Format("{0}/atomicassets/v1/assets?page=1&limit=1000&template_blacklist=260676&collection_name=farmersworld&owner={1}", "https://wax.api.atomicassets.io", name);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            try
            {
                request.Method = "get";
                request.UserAgent = "Android";
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = await new StreamReader( response.GetResponseStream()).ReadToEndAsync();
                return JObject.Parse(responseString)["data"];
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }

        }

          public static async Task<bool> notify(string msg,string token)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var postData = string.Format("message={0}", msg);
                var data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers.Add("Authorization", "Bearer " + token);

                using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);
                //  using (var stream = request.GetRequestStream()) stream.Read(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
