using FW_V3.Module;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW_V3.Controller
{
    class jsonController
    {

    }
    [Serializable]
    public static class JSON
    {
        public static void saveJS(this List<Login> act)
        {
            var save = new WAXaccount() { wax = act };
            var t = JsonSerializer.Serialize(save);
            File.WriteAllText(@"Config//wallet.json", t);
        }
    }
    [Serializable]
    public class WAXaccount
    {
        public List<Login> wax { get; set; }
    }
}
