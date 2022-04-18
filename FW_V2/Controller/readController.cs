using EosSharp.Core.Api.v1;
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
    class readController
    {
        public async Task<double> GetAccount(Account act)
        {
            try
            {
                var account = await EAV.EOSIO.GetAccount(act.name);
               return double.Parse(account.cpu_limit.used.ToString()) / double.Parse(account.cpu_limit.max.ToString()) * 100;
            }
            catch
            {
                EAV.RndRPC();
                return -1;
            }
        
        }
        public async Task<int> GetFee()
        {
            try
            {
                var data = await EAV.EOSIO.GetTableRows(new GetTableRowsRequest() { json = true, code = "farmersworld", scope = "farmersworld", table = "config", lower_bound = "", upper_bound = "", limit = 1 });
                var fee = JObject.Parse(data.rows[0].ToString());
                return int.Parse(fee["fee"].ToString());
            }
            catch
            {
                EAV.RndRPC();
                return -1;
            }
        }
        public async Task<Profile> Profile(Account act)
        {
            try
            {
                var data = await EAV.EOSIO.GetTableRows(new GetTableRowsRequest() { json = true, code = "farmersworld", scope = "farmersworld", table = "accounts", lower_bound = act.name, upper_bound = act.name, index_position = "1", key_type = "i64", limit = 100 });
                var v = JObject.Parse(data.rows[0].ToString());

                return new Profile
                {
                    energy = Convert.ToInt32(v["energy"]),
                    max_energy = Convert.ToInt32(v["max_energy"]),
                    last_mine_tx = v["last_mine_tx"].ToString(),
                    balance = JArray.Parse(v["balances"].ToString())
                };

            }
            catch
            {
                EAV.RndRPC();
                return null;
            }
        }
       
        public async void MBS(Account act)
        {
            try
            {
                var data = await EAV.EOSIO.GetTableRows(new GetTableRowsRequest() { json = true, code = "farmersworld", scope = "farmersworld", table = "mbs", lower_bound = act.name, upper_bound = act.name, index_position = "2", key_type = "i64", limit = 100 });
              
                foreach (var rows in data.rows)
                {
                    CheckStatus(act, rows,true);
                }
            }
            catch
            {
                EAV.RndRPC();
            }
        }

        public async void Tools(Account act)
        {
            try
            {
                var data = await EAV.EOSIO.GetTableRows(new GetTableRowsRequest() { json = true, code = "farmersworld", scope = "farmersworld", table = "tools", lower_bound = act.name, upper_bound = act.name, index_position = "2", key_type = "i64", limit = 100 });
               
                foreach (var rows in data.rows)
                {
                    CheckStatus(act, rows);
                }
            }
            catch
            {
                EAV.RndRPC();
            }
        }

        public async void Animal(Account act)
        {
            try
            {
                var data = await EAV.EOSIO.GetTableRows(new GetTableRowsRequest() { json = true, code = "farmersworld", scope = "farmersworld", table = "animals", lower_bound = act.name, upper_bound = act.name, index_position = "2", key_type = "i64", limit = 100 });
                var data2 = await EAV.EOSIO.GetTableRows(new GetTableRowsRequest() { json = true, code = "farmersworld", scope = "farmersworld", table = "anmconf", lower_bound = "", upper_bound = "", index_position = "1", key_type = "", limit = 100 });
                foreach (var rows in data.rows)
                {
                    CheckAnimal(act, rows, data2);
                }
            }
            catch
            {
                EAV.RndRPC();
            }
        }

        public async void Corn(Account act)
        {
            try
            {
                var data = await EAV.EOSIO.GetTableRows(new GetTableRowsRequest() { json = true, code = "farmersworld", scope = "farmersworld", table = "crops", lower_bound = act.name, upper_bound = act.name, index_position = "2", key_type = "i64", limit = 100 });
                var data2 = await EAV.EOSIO.GetTableRows(new GetTableRowsRequest() { json = true, code = "farmersworld", scope = "farmersworld", table = "cropconf", lower_bound = "", upper_bound = "", index_position = "1", key_type = "", limit = 100 });
              
                foreach (var rows in data.rows)
                {
                    CheckCorn(act, rows,data2);
                }
            }
            catch
            {
                EAV.RndRPC();
            }
        }

        public void CheckStatus(Account act,object data,bool member = false)
        {
            try
            {
                var v = JObject.Parse(data.ToString());
                var search = act.tools.Find(x => x.asset_id == v["asset_id"].ToString());
                var type = "tools";
                var name = v["type"].ToString();
                if (member)
                {
                    type = "member";
                    name = "Member";
                }
                    
                if (search == null)
                {
                    act.tools.Add(new Tools()
                    {
                        asset_id = v["asset_id"].ToString(),
                        type = type,
                        name = name,
                        sort = name,
                        durability = Convert.ToInt32(v["durability"]),
                        current_durability = Convert.ToInt32(v["current_durability"]),
                        next_availability = Convert.ToInt64(v["next_availability"])
                    });
                }
                else
                {
                    search.current_durability = Convert.ToInt32(v["current_durability"]);
                    search.next_availability = Convert.ToInt64(v["next_availability"]);
                };
            } catch
            {
                EAV.RndRPC();
            }
          
        }

        public void CheckAnimal(Account act, object data,GetTableRowsResponse data2)
        {
            try
            {
                var List = new List<string>();
                var v = JObject.Parse(data.ToString());
                var search = act.tools.Find(x => x.asset_id == v["asset_id"].ToString());
                var type = "animal";
                var name = v["name"].ToString();
                if (name == "Calf")
                {
                    if (int.Parse(v["gender"].ToString()) == 1)
                        name = "Calf (Male)";
                    else
                        name = "Calf (FeMale)";
                }
                if (search == null)
                {
                    foreach(var req in data2.rows)
                    {
                      
                        var b = JObject.Parse(req.ToString());
                      
                        if (b["name"].ToString()== name)
                        {
                            act.tools.Add(new Tools()
                            {
                                asset_id = v["asset_id"].ToString(),
                                type = type,
                                name = name,
                                sort = name,
                                durability = Convert.ToInt32(b["required_claims"]),
                                current_durability = Convert.ToInt32(v["times_claimed"]),
                                next_availability = Convert.ToInt64(v["next_availability"]),
                                times_claimed = Convert.ToInt32(v["times_claimed"]),
                                day_claims_at = v["day_claims_at"].Select(x=>(int)x).ToArray(),
                                daily_claim_limit = Convert.ToInt32(b["daily_claim_limit"]),
                                partner_id = ulong.Parse(v["partner_id"].ToString())
                            });
                            break;
                        }
                      
                    }
                 
                }
                else
                {
                    search.day_claims_at = v["day_claims_at"].Select(x => (int)x).ToArray();
                    search.current_durability = Convert.ToInt32(v["times_claimed"]);
                    search.next_availability = Convert.ToInt64(v["next_availability"]);
                    search.partner_id = ulong.Parse(v["partner_id"].ToString());
                };
            }
            catch
            {
                EAV.RndRPC();
            }
        }

        public void CheckCorn(Account act, object data, GetTableRowsResponse data2)
        {
            try
            {
                var List = new List<string>();
                var v = JObject.Parse(data.ToString());
                var search = act.tools.Find(x => x.asset_id == v["asset_id"].ToString());
                var type = "corn";
                var name = v["name"].ToString();
                
                if (search == null)
                {
                    foreach (var req in data2.rows)
                    {

                        var b = JObject.Parse(req.ToString());
                        if (b["name"].ToString() == name)
                        {
                            act.tools.Add(new Tools()
                            {
                                asset_id = v["asset_id"].ToString(),
                                type = type,
                                name = name,
                                sort = name,
                                durability = Convert.ToInt32(b["required_claims"]),
                                current_durability = Convert.ToInt32(v["times_claimed"]),
                                next_availability = Convert.ToInt64(v["next_availability"]),
                                times_claimed = Convert.ToInt32(v["times_claimed"])
                            });
                            break;
                        }

                    }

                }
                else
                {
                    search.current_durability = Convert.ToInt32(v["times_claimed"]);
                    search.next_availability = Convert.ToInt64(v["next_availability"]);
                };
            }
            catch
            {
                EAV.RndRPC();
            }
        }

        public async void PetFood(Account act)
        {
            WaxAPI api = new WaxAPI();
            List<Food> check = new List<Food>();
            try
            {
               
                var food = await api.Atomic(act.name);
            
                if (food != null)
                {
                    foreach (var pf in food)
                    {
                        var search = act.Food.Find(x => x.asset_id == pf["asset_id"].ToString());
                        //if (pf["name"].ToString().ToLower().Contains("seed"))
                        //    continue;
                        check.Add(new Food
                        {
                            asset_id = pf["asset_id"].ToString(),
                            name = pf["name"].ToString()
                        });
                        if (search == null)
                        {
                            act.Food.Add(new Food
                            {
                                asset_id = pf["asset_id"].ToString(),
                                name = pf["name"].ToString()
                            });
                        }
                    }
                    foreach (var vali in act.Food)
                    {
                        var search = check.Find(x => x.asset_id == vali.asset_id);
                        if (search == null)
                        {
                            act.Food.Remove(vali);
                        }
                    }
                }
            }
            catch { }
          
        }

        public async Task<string> GetFWW(Account act)
        {
            List<string> All = await EAV.EOSIO.GetCurrencyBalance("farmerstoken", act.name, "FWW");
            return All[0];
        }
    }
}
