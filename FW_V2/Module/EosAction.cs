using EosSharp.Core.Api.v1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = EosSharp.Core.Api.v1.Action;

namespace FW_V3.Module
{
    public static class EosAction
    {
        static Action Action(this object prop, string account, string name, string id)
        {
            return new Action()
            {
                account = account,
                authorization = new List<PermissionLevel>() { new PermissionLevel() { actor = id, permission = "active" } },
                name = name,
                data = prop
            };
        }
        public static Transaction SetAction(this List<Action> List)
        {
            return new Transaction()
            {
                actions = List
            };
        }
        public static string WaxToken(this double token)
        {
            return $"{token.ToString("n8")} WAX";
        }
        public static Action Stake(this object prop, string id)
        {
            return prop.Action("eosio", "delegatebw", id);
        }
        public static Action unStake(this object prop, string id)
        {
            return prop.Action("eosio", "undelegatebw", id);
        }
        public static Action Swap(this object prop, string id)
        {
            return prop.Action("alien.worlds", "transfer", id);
        }
        public static Action TranferWax(this object prop, string id)
        {
            return prop.Action("eosio.token", "transfer", id);
        }
        public static Action TranferToken(this object prop, string id,string token)
        {
            return prop.Action(token, "transfer", id);
        }
        public static Action farmers(this object prop, string id,string type)
        {
            return prop.Action("farmersworld", type, id);
        }

        public static Action Water(this object prop, string id)
        {
            return prop.Action("farmersworld", "cropclaim", id);
        }

        public static Action AnimalClaim(this object prop, string id)
        {
            return prop.Action("farmersworld", "anmclaim", id);
        }

        public static Action Markets(this object prop, string id)
        {
            return prop.Action("farmersworld", "anmclaim", id);
        }

    }
}
