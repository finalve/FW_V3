using FW_V3.Controller;
using FW_V3.Module;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW_V3.SubClass
{
   public static class WorldClass
    {
        public static string PackArray(this object[] Arr)
        {
            int i = 0;
            StringBuilder hex = new StringBuilder();
            foreach (var s in Arr)
            {
                i++;
                if (i < Arr.Length)
                    hex.Append(s + ",");
                else
                    hex.Append(s);
            }
            return hex.ToString();
        }

        public static string CreateSession(this Account act)
        {
            bool twofa = false;
            WaxAPI API = new WaxAPI();
            string session_token = null;
            string code;
            var token = API.postSession(act.email, act.pwd);
            if (token["errors"] != null)
                return token["errors"]?[0]["message"];
            if (token["token2fa"] != null)
            {
                session_token = token["token2fa"];
                twofa = true;
            }
            if (twofa)
            {
                var auth = new GoogleAuth2FA(act.secret);
                code = auth.GeneratePin();
                if (code == null)
                    return "Please Check Secret!";
                var tos = API.json2FA(code, session_token);
                try
                {
                    var tandc = JObject.Parse(tos);
                    if (tandc["tandc_token"] != null)
                    {
                        var idtos = API.Gettos(tandc["tandc_token"].ToString());
                        var id = JObject.Parse(idtos);
                        API.posttos(id["id"].ToString(), tandc["tandc_token"].ToString());
                    }
                }
                catch
                {

                }
            }

            try
            {
                string json = API.getSession();
                var jobject = JObject.Parse(json);
                act.token = jobject["token"].ToString();
            }
            catch { }
            return "Session Success!";
        }
    }
}
