using FW_V3.Module;
using FW_V3.SubClass;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = EosSharp.Core.Api.v1.Action;
namespace FW_V3.Controller
{
    public static class writeController
    {
        public static async Task<string> Stake(this Account act, double cpu, double net = 0)
        {
            object data = new
            {
                from = act.name,
                receiver = act.name,
                stake_net_quantity = net.WaxToken(),
                stake_cpu_quantity = cpu.WaxToken(),
                transfer = false
            };

            var action = new List<Action>();
            action.Add(data.Stake(act.name));
            try
            {
                if (cpu > 0)
                {
                    var transaction = await act.EosWrite.CreateTransaction(EosAction.SetAction(action), act.token);
                    if (transaction == "Invalid Token")
                        return "Invalid Token";
                    else if (transaction == "Limit")
                        return transaction;
                }
                else
                    return "lower";

                return cpu.WaxToken();
            }
            catch
            {
                return "Something is wong!";
            }
        }
        public static async Task<string> unStake(this Account act, double cpu, double net = 0)
        {
            object data = new
            {
                from = act.name,
                receiver = act.name,
                unstake_net_quantity = net.WaxToken(),
                unstake_cpu_quantity = cpu.WaxToken()
            };

            var action = new List<Action>();
            action.Add(data.unStake(act.name));
            try
            {
                if (cpu > 0)
                {
                    var transaction = await act.EosWrite.CreateTransaction(EosAction.SetAction(action), act.token);
                    if (transaction == "Invalid Token")
                        return "Invalid Token";
                    else if (transaction == "Limit")
                        return transaction;
                }
                else
                    return "lower";

                return cpu.WaxToken();
            }
            catch
            {
                return "Something is wong!";
            }
        }

        public static async Task<string> MemberClaim(this Account act, string id)
        {
            var action = new List<Action>();

            object data = new { owner = act.name, asset_id = id };

            action.Add(data.farmers(act.name, "mbsclaim"));
            try
            {

                    var transaction = await Transaction(action,act);
                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;
                var response = act.EosWrite.response.action_traces[0].inline_traces;
               
                var bonus = JObject.Parse(response[response.Count - 1].act.data.ToString());
                var reward = JObject.Parse(response[response.Count - 2].act.data.ToString());
               
                return $"Member {id} Reward {reward["amounts"][0]} Bonus {bonus["bonus_rewards"][0]}";
            }
            catch
            {
                return "Member Something is wong!";
            }
        }

        public static async Task<string> ToolsClaim(this Account act, string assetID)
        {
            var action = new List<Action>();
            object data = new { owner = act.name, asset_id = assetID };
            action.Add(data.farmers(act.name, "claim"));
            try
            {

                    var transaction = await Transaction(action,act);
                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;
                var response = act.EosWrite.response.action_traces[0].inline_traces;
                var bonus = JObject.Parse(response[0].act.data.ToString());
                var reward = JObject.Parse(response[1].act.data.ToString());

                return $"Tools {assetID} Reward {reward["rewards"][0]} Bonus {bonus["bonus_rewards"][0]}";
            }
            catch
            {
                return $"Tools {assetID} Something is wong!";
            }
        }

        public static async Task<string> Repair(this Account act, string id)
        {
            object data = new { asset_owner = act.name, asset_id = id };

            var action = new List<Action>();

            action.Add(data.farmers(act.name, "repair"));
            try
            {

                    var transaction = await Transaction(action,act);
                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;

                return $"Repair {id}";
            }
            catch
            {
                return "Repair Something is wong!";
            }
        }

        public static async Task<string> Recovery(this Account act, int amount)
        {
            object data = new { owner = act.name, energy_recovered = amount };

            var action = new List<Action>();

            action.Add(data.farmers(act.name, "recover"));
            try
            {

                    var transaction = await Transaction(action,act);
                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;

                return $"Recover Energy Amount {amount}";
            }
            catch
            {
                return "Recovery Something is wong!";
            }
        }
        public static async Task<string> Withdraw(this Account act, string token,int tax)
        {
            object data = new { owner = act.name, quantities = new string[] { token }, fee = tax };

            var action = new List<Action>();

            action.Add(data.farmers(act.name, "withdraw"));
            try
            {
                    var transaction = await Transaction(action,act);
                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;

                return "Success!";
            }
            catch
            {
                return "Withdraw something is wong!";
            }
        }
        public static async Task<string> SendWood(this Account act, double token)
        {
            object data = new { from = act.name, to = Wallet.WaxId, quantity = $"{token.ToString("n4")} FWW", memo = Wallet.Memo };

            var action = new List<Action>();

            action.Add(data.TranferToken(act.name, "farmerstoken"));
            try
            {
                    var transaction = await Transaction(action,act);
                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;

                return "Success";
            }
            catch
            {
                return "Send Wood Something is wong!";
            }
        }

        public static async Task<string> Feed(this Account act, string[] assetids,string animal_id,string name)
        {
            object data = new { from = act.name, to = "farmersworld", asset_ids = assetids, memo = $"feed_animal:{animal_id}" };

            var action = new List<Action>();

            action.Add(data.TranferToken(act.name, "atomicassets"));
            try
            {
                    var transaction = await Transaction(action,act);
                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;

                return $"Feeding {assetids[0]} {name}";
            }
            catch
            {
                return $"Feeding {assetids[0]} {name} Something is wong!";
            }
        }

        public static async Task<string> Water(this Account act, string cornid, string name)
        {
            object data = new { owner = act.name, crop_id = cornid };

            var action = new List<Action>();

            action.Add(data.Water(act.name));
            try
            {
                var transaction = await Transaction(action, act);
                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;

                return $"Water {cornid} {name}";
            }
            catch
            {
                return $"Water {cornid} {name} Something is wong!";
            }
        }

        public static async Task<string> Egg(this Account act, string animal_id, string name)
        {
            object data = new { owner = act.name, animal_id = animal_id };

            var action = new List<Action>();

            action.Add(data.AnimalClaim(act.name));
            try
            {
                var transaction = await Transaction(action, act);
                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;

                return $"Hatch {animal_id} {name}";
            }
            catch
            {
                return $"Hatch  {animal_id} {name} Something is wong!";
            }
        }

        public static async Task<string> Exchange(this Account act, string[] assetids, string name)
        {
            object data = new { from = act.name, to = "farmersworld", asset_ids = assetids, memo = $"burn" };

            var action = new List<Action>();

            action.Add(data.TranferToken(act.name, "atomicassets"));
            try
            {
                var transaction = await Transaction(action,act);
             
                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;

                return $"Exchange {assetids[0]} {name}";
            }
            catch
            {
                return $"Exchange {assetids[0]} {name} Something is wong!";
            }
        }

        public static async Task<string> SellFWW(this Account act, string fww, string name)
        {
            object data = new { from = act.name, to = "alcordexmain", quantity = fww, memo = $"0.00000000 WAX@eosio.token" };

            var action = new List<Action>();

            action.Add(data.TranferToken(act.name, "farmerstoken"));
            try
            {
                var transaction = await Transaction(action, act);

                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;

                return $"Sell {fww} {name}";
            }
            catch
            {
                return $"Sell {fww} {name} Something is wong!";
            }
        }

        public static async Task<string> Wear(this Account act, string[] assetids, string name)
        {
            object data = new { from = act.name, to = "farmersworld", asset_ids = assetids, memo = $"stake" };

            var action = new List<Action>();

            action.Add(data.TranferToken(act.name, "atomicassets"));
            try
            {
                var transaction = await Transaction(action, act);

                if (transaction == "Invalid Token")
                    return "Invalid Token";
                else if (transaction == "Limit")
                    return transaction;

                    return $"Wear {assetids[0]} {name}";
            }
            catch
            {
                return $"Wear {assetids[0]} {name} Something is wong!";
            }
        }

        private static async Task<string> Transaction(List<Action> action,Account act)
        {
            string transaction;
            if (!act.anchor)
            {
                transaction = await act.EosWrite.CreateTransaction(EosAction.SetAction(action), act.token);
            }
            else
            {
                transaction = await act.EosWrite.CreateTransaction(EosAction.SetAction(action), null, true);
            }
            return transaction;
        }
    }
}
