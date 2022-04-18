using FW_V3.Controller;
using FW_V3.Module;
using FW_V3.SubClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FW_V3
{
    class EAV
    {
        public static void Welcome()
        {
            login = Loader();
            foreach (var log in login)
                ACT.Add(new Account
                {
                    email = log.email,
                    pwd = log.pwd,
                    secret = log.secret,
                    name = log.name,
                    token = log.token,
                    notifytoken = log.notifytoken,
                    anchor = log.anchor,
                    privatekeys = log.privatekeys
                }) ;
        }
        public static List<Login> Loader()
        {
            var json = File.ReadAllText(@"Config/wallet.json");
            try
            {
                var deser = JsonSerializer.Deserialize<WAXaccount>(json);
                return deser.wax;
            }
            catch
            {
                return new List<Login>();
            }
        }
        public static string RndRPC()
        {
            Random random = new Random();
            var rnd = random.Next(0, 8);

            if (rnd == 0)
                rpc = "https://wax.pink.gg";
            else if (rnd == 1)
                rpc = "https://wax.greymass.com";
            else if (rnd == 2)
                rpc = "https://wax.eu.eosamsterdam.net";
            else if (rnd == 3)
                rpc = "https://wax.cryptolions.io";
            else if (rnd == 4)
                rpc = "https://wax.dapplica.io";
            else if (rnd == 5)
                rpc = "https://api.waxsweden.org";
            else if (rnd == 6)
                rpc = "https://api.wax.bountyblok.io";
            else if (rnd == 7)
                rpc = "https://api.wax.greeneosio.com";
            else
                rpc = "https://api.wax.alohaeos.com";
            return rpc;
        }
        public static EosClass EOSIO { get; set; }
        public static string ChainId { get; set; } = "1064487b3cd1a897ce03ae5b6a865651747e2e152090f99c1d19d44e01aea5a4";
        public static int block_num { get; set; } = 0;
        public static string rpc { get; set; } = "https://chain.wax.io";
        public static int fee { get; set; }
        public static List<Account> ACT { get; set; } = new List<Account>();
        public static List<Login> login { get; set; }

        public static List<Food> Food { get; set; }
    }
}
