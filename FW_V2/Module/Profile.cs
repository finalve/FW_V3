using FW_V3.SubClass;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW_V3.Module
{
    [Serializable]
    public class Profile
    {
        public int energy { get; set; }
        public int max_energy { get; set; }
        public string last_mine_tx { get; set; }
        public int fee { get; set; }
        public JArray balance { get; set; }
        public string Wood { get; set; }
        public string Food { get; set; }
        public string Gold { get; set; }
    }
    [Serializable]
    public class Login
    {
        public string email { get; set; }
        public string pwd { get; set; }
        public string secret { get; set; }
        public string token { get; set; }
        public string notifytoken { get; set; }
      
        public string name { get; set; }

        public bool anchor { get; set; } = false;
        public List<string> privatekeys { get; set; } = null;
    }
    [Serializable]
    public class Account
    {
        public EosClass EosWrite { get; set; }
        public string email { get; set; }
        public string pwd { get; set; }
        public string secret { get; set; }
        public string token { get; set; }
        public string notifytoken { get; set; }
        public string name { get; set; }
        public bool anchor { get; set; } = false;
        public List<string> privatekeys { get; set; } = null;
        public bool stop { get; set; }
        public List<Tools> tools { get; set; } = new List<Tools>();
        public Profile profile { get; set; } = new Profile();
        public List<Food> Food { get; set; } = new List<Food>();
        public int Break { get; set; } = 0;
        public double CPU { get; set; } = -1;
    }
    [Serializable]
    public class Wallet
    {
        public static string WaxId { get; set; }
        public static string Memo { get; set; }
    }

    [Serializable]
    public class Crop
    {
        public static int Counts { get; set; }
    }
    [Serializable]
    public class Tools
    {
        public string asset_id { get; set; }
        public string type { get; set; }
        public string sort { get ; set; }
        public string name { get; set; }
        public int durability { get; set; }
        public int current_durability { get; set; }
        public long next_availability { get; set; }
        public string status { get; set; }
        public int Break { get; set; } = 0;
        public int times_claimed { get; set; } = 0;

        public int[] day_claims_at { get; set; }
        public int daily_claim_limit { get; set; } = 0;

        public ulong partner_id { get; set; } = 0;
        public Food food { get; set; }

    }
    [Serializable]
    public class Food
    {
        public string asset_id { get; set; }
        public string name { get; set; }
    }
}
