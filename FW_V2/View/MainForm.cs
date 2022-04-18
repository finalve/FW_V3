using EosSharp;
using EosSharp.Core;
using FW_V3.Controller;
using FW_V3.Module;
using FW_V3.SubClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FW_V3.View
{
    public partial class MainForm : Form
    {
        private readController Read = new readController();
        private string RefreshThis;
        private string textload = "-";
        //Chicken Egg

        private string[] Cow = new string[] { "Calf", "Calf (FeMale)", "Calf (Male)", "Dairy Cow", "Bull" };
        private string[] Chicken = new string[] { "Chick", "Chicken" };
        private string[] Corn = new string[] { "Barley Seed", "Corn Seed" };
        public MainForm()
        {
            InitializeComponent();
            #region Welcome
            EAV.Welcome();
            RefreshThis = EAV.ACT[0].name;
            TextRPC.Text = EAV.RndRPC();
            #endregion
        }
        private string Between(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return strSource;
        }
        bool main = false;
        private async void TimeMain_Tick(object sender, EventArgs e)
        {
            if (!main)
            {
                main = true;
                string response = "?????????";
                foreach (var act in EAV.ACT)
                {

                    if ((act.CPU > 90 || act.CPU < 1) && Properties.Settings.Default.aCpu)
                        continue;
                    if (act.Break > 0)
                    {
                        act.Break--;
                        continue;
                    }
                    else
                    {
                        act.Break = 5;
                        Food ex = null;


                        if (Properties.Settings.Default.aMilk)
                        {
                            ex = act.Food.Find(x => x.name == "Milk");

                            if (ex != null)
                            {
                                var assset_ids = new string[] { ex.asset_id };
                                response = await act.Exchange(assset_ids, ex.name);
                                if (!response.ToLower().Contains("something is wong"))
                                {
                                    var res = $"Exchange {ex.name} Remove {ex.asset_id}!*";
                                    msg($"{act.name} {res}", act.notifytoken, Color.Gold);
                                    response = "?????????";
                                    act.Food.Remove(ex);
                                }
                                else
                                {
                                    act.Food.Remove(ex);
                                    msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                }
                            }
                        }
                        if (Properties.Settings.Default.aEggs)
                        {
                            ex = act.Food.Find(x => x.name == "Chicken Egg");

                            if (ex != null)
                            {
                                var assset_ids = new string[] { ex.asset_id };
                                response = await act.Exchange(assset_ids, ex.name);
                                if (!response.ToLower().Contains("something is wong"))
                                {
                                    var res = $"Exchange {ex.name} Remove {ex.asset_id}!*";
                                    msg($"{act.name} {res}", act.notifytoken, Color.Gold);
                                    response = "?????????";
                                    act.Food.Remove(ex);
                                }
                                else
                                {
                                    act.Food.Remove(ex);
                                    msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                }
                            }
                        }
                        if (Properties.Settings.Default.aCorn)
                        {
                            ex = act.Food.Find(x => x.name == "Corn");
                            if (ex != null)
                            {
                                var assset_ids = new string[] { ex.asset_id };
                                response = await act.Exchange(assset_ids, ex.name);
                                if (!response.ToLower().Contains("something is wong"))
                                {
                                    var res = $"Exchange {ex.name} Remove {ex.asset_id}!*";
                                    msg($"{act.name} {res}", act.notifytoken, Color.Gold);
                                    response = "?????????";
                                    act.Food.Remove(ex);
                                }
                                else
                                {
                                    msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                }
                            }
                        }
                        if (Properties.Settings.Default.aBarley)
                        {
                            ex = act.Food.Find(x => x.name == "Barley");

                            if (ex != null)
                            {
                                var assset_ids = new string[] { ex.asset_id };
                                response = await act.Exchange(assset_ids, ex.name);
                                if (!response.ToLower().Contains("something is wong"))
                                {
                                    var res = $"Exchange {ex.name} Remove {ex.asset_id}!*";
                                    msg($"{act.name} {res}", act.notifytoken, Color.Gold);
                                    response = "?????????";
                                    try
                                    {
                                        act.Food.Remove(ex);
                                    }
                                    catch { }

                                }
                                else
                                {
                                    msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                }
                            }
                        }
                        if (act.profile.energy < 150 || (Properties.Settings.Default.aStack > 1 && act.profile.energy < act.profile.max_energy))
                        {
                            var recovery = act.profile.max_energy - act.profile.energy;
                            try
                            {
                                var fwf = double.Parse(act.profile.Food.Split(' ')[0]);
                                var sum = (double)recovery / 5;
                                if (fwf < sum)
                                {
                                    recovery = (int)fwf * 5;
                                    msg($"{act.name} Lower Food FWF {fwf} Recovery {recovery}", act.notifytoken, Color.Red);
                                }
                            }
                            catch
                            {
                                act.Break = 5;
                                continue;
                            }

                            response = await act.Recovery(recovery);
                        }
                    }
                    if (act.tools != null)
                    {
                        try
                        {
                            foreach (var eq in act.tools)
                            {
                                if ((act.CPU > 90 || act.CPU < 1) && Properties.Settings.Default.aCpu)
                                {
                                    act.Break = 30;
                                    break;
                                }
                                if (eq.Break > 0)
                                {
                                    eq.Break--;
                                }
                                else
                                {
                                    var stack = Properties.Settings.Default.aStack * 3600;
                                    long wait = eq.next_availability;
                                    if (eq.type == "tools" && eq.name == "Wood")
                                    {
                                        wait = (stack - 3600) + wait;
                                    }
                                    var unixTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

                                    var Next = wait - unixTimestamp;
                                    if (Next < 0)
                                    {
                                        eq.Break = 5;
                                        eq.next_availability = unixTimestamp + 30;
                                        if (eq.type == "member")
                                        {
                                            response = await act.MemberClaim(eq.asset_id);

                                            if (response.ToLower().Contains("invalid token"))
                                            {
                                                msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                                response = act.CreateSession();
                                                if (response == "Please Check Mail!")
                                                {
                                                    msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                                    act.stop = true;
                                                }
                                                else
                                                {
                                                    act.stop = false;
                                                }
                                                var selectID = EAV.login.Find(x => x.name == act.name);
                                                selectID.token = act.token;
                                                EAV.login.saveJS();
                                                msg($"{act.name} {response}", act.notifytoken, Color.DodgerBlue);
                                                continue;

                                            }
                                            msg($"{act.name} {response}", act.notifytoken, Color.Gold);
                                            continue;
                                        }
                                        else if (eq.type == "tools")
                                        {
                                            double durability = 100;
                                            try
                                            {
                                                durability = (double)eq.current_durability / (double)eq.durability * 100;
                                            }
                                            catch
                                            {

                                            }

                                            if (durability < 50)
                                            {
                                                var fwg = double.Parse(act.profile.Gold.Split(' ')[0]);
                                                var sum = ((double)eq.durability - (double)eq.current_durability) / 5;
                                                if (fwg < sum)
                                                {
                                                    msg($"{act.name} Lower Gold", act.notifytoken, Color.Red);
                                                    act.Break = 60;
                                                    break;
                                                }

                                                response = await act.Repair(eq.asset_id);
                                                msg($"{act.name} {response}", act.notifytoken, Color.Lime);
                                            }

                                            response = await act.ToolsClaim(eq.asset_id);

                                        }
                                        else if (eq.type == "corn")
                                        {
                                            response = await act.Water(eq.asset_id, eq.name);
                                            if (eq.current_durability >= 41)
                                            {
                                                if (act.profile.energy < 300)
                                                {
                                                    var recovery = act.profile.max_energy - act.profile.energy;
                                                    try
                                                    {
                                                        var fwf = double.Parse(act.profile.Food.Split(' ')[0]);
                                                        var sum = (double)recovery / 5;
                                                        if (fwf < sum)
                                                        {
                                                            recovery = (int)fwf * 5;
                                                            msg($"{act.name} Lower Food FWF {fwf} Recovery {recovery}", act.notifytoken, Color.Red);
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        act.Break = 5;
                                                        continue;
                                                    }

                                                    response = await act.Recovery(recovery);
                                                }
                                                Food SeedFind = null;
                                                SeedFind = act.Food.Find(x => x.name == $"{eq.name} Seed");

                                                act.tools.Remove(eq);

                                                if (!response.ToLower().Contains("something is wong"))
                                                {
                                                    Read.Corn(act);
                                                    response = $"Claim {eq.name} Remove  Please Wear Seed New In Game!*";
                                                    msg($"{act.name} {response}", act.notifytoken, Color.Gold);

                                                    break;
                                                }
                                                else
                                                {
                                                    Read.Corn(act);
                                                    msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                                    EAV.RndRPC();
                                                    EAV.EOSIO = new EosClass(new EosConfigurator() { HttpEndpoint = EAV.rpc, ChainId = EAV.ChainId, ExpireSeconds = 60 }, new HttpHandler());
                                                    break;
                                                }
                                            }

                                        }
                                        else if (eq.type == "animal")
                                        {

                                            Food finding;
                                            string Foods = "Barley";
                                            if (eq.name == "Bull")
                                            {
                                                eq.Break = 3600;
                                                continue;
                                            }
                                            //if (Cow.Contains(eq.name) || Chicken.Contains(eq.name))
                                            //{
                                            //    finding = act.Food.Find(x => x.name == "Barley");
                                            //}

                                            else if (eq.name == "Baby Calf")
                                            {
                                                finding = act.Food.Find(x => x.name == "Milk");
                                                Foods = "Milk";
                                            }
                                            else
                                                finding = act.Food.Find(x => x.name == "Barley");

                                            if (eq.name == "Chicken Egg")
                                            {
                                                response = await act.Egg(eq.asset_id, eq.name);
                                                if (response.ToLower().Contains("something is wong"))
                                                    eq.Break = 600;
                                            }
                                            else
                                            {
                                                if (eq.partner_id != 0)
                                                {
                                                    eq.Break = 1800;
                                                    continue;
                                                }
                                                if (finding != null)
                                                {
                                                    var checkTime = unixTimestamp - eq.day_claims_at[0];
                                                    if (checkTime >= 86400 || eq.day_claims_at.Count() < 4 || eq.name.ToLower().Contains("cow"))
                                                    {
                                                        var assset_ids = new string[] { finding.asset_id };
                                                        response = await act.Feed(assset_ids, eq.asset_id, eq.name);
                                                        act.Food.Remove(finding);
                                                        if (!response.ToLower().Contains("something is wong"))
                                                        {

                                                            if (eq.name.ToLower().Contains("calf"))
                                                                if (eq.current_durability >= 16)
                                                                {
                                                                    act.tools.Remove(eq);
                                                                    response = $"{eq.asset_id} {eq.name} Evolution*";
                                                                }
                                                            if (eq.name.ToLower().Contains("cow"))
                                                            {
                                                                if (eq.current_durability >= 5)
                                                                {
                                                                    response = $"{eq.name} Asset ID {eq.asset_id}  Claim Milk";
                                                                }
                                                            }
                                                            if (eq.name.ToLower().Contains("Chicken"))
                                                            {
                                                                if (eq.current_durability >= 27)
                                                                {
                                                                    response = $"{eq.name} Asset ID {eq.asset_id}  Claim Eggs";
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            eq.Break = 100;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        eq.Break = 30;
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    response = $"{eq.name} {eq.asset_id} {Foods} not Enougn";
                                                    eq.Break = 100;
                                                }
                                            }
                                        }

                                        ReadData(act);
                                        if (response.ToLower().Contains("not enougn"))
                                        {
                                            msg($"{act.name} {eq.name} {response}", act.notifytoken, Color.Red);
                                            eq.Break = 200;
                                        }
                                        else if (response.ToLower().Contains("invalid token"))
                                        {
                                            msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                            response = act.CreateSession();
                                            if (response == "Please Check Mail!")
                                            {
                                                msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                                act.stop = true;
                                            }
                                            else
                                            {
                                                act.stop = false;
                                            }
                                            var selectID = EAV.login.Find(x => x.name == act.name);
                                            selectID.token = act.token;
                                            EAV.login.saveJS();
                                            msg($"{act.name} {response}", act.notifytoken, Color.DodgerBlue);

                                        }
                                        else if (response.ToLower().Contains("something is wong"))
                                        {
                                            msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                            EAV.RndRPC();
                                            EAV.EOSIO = new EosClass(new EosConfigurator() { HttpEndpoint = EAV.rpc, ChainId = EAV.ChainId, ExpireSeconds = 60 }, new HttpHandler());
                                            act.EosWrite = EAV.EOSIO;
                                            if (EAV.block_num == 0)
                                                EAV.block_num = 1;
                                            else
                                                EAV.block_num = 0;
                                        }
                                        else if (response == "?????????")
                                            msg($"{act.name} {response}", act.notifytoken, Color.DodgerBlue);
                                        else if (response == "Limit")
                                        {
                                            response = $"{eq.name} Exceeded rate limit. Cooling down for 60 minutes";
                                            msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                            act.Break = 600;
                                            break;
                                        }
                                        else
                                        {
                                            double check_bonus = 0;
                                            try
                                            {
                                                check_bonus = double.Parse(Between(response, "Bonus ", " "));
                                            }
                                            catch
                                            {

                                            }

                                            if (check_bonus > 0 || response.ToLower().Contains("claim") || response.ToLower().Contains("evolution wear"))
                                                msg($"{act.name} {response}", act.notifytoken, Color.Gold);
                                            else
                                                msg($"{act.name} {response}", act.notifytoken, Color.Lime);
                                        }
                                    }
                                }
                            }
                        } catch { }
                    }
                }
                main = false;
            }
        }
        bool re = false;
        bool data = false;
        private async void Refresh_Tick(object sender, EventArgs e)
        {

        }
        private void ReadData(Account act)
        {
            if (!data)
            {
                data = true;
                Read.MBS(act);
                Read.Tools(act);
                Read.Animal(act);
                Read.Corn(act);
                Read.PetFood(act);
                data = false;
            }
        }
        private async void Next_Tick(object sender, EventArgs e)
        {
            if (!re)
            {
                foreach (var act in EAV.ACT)
                {
                    act.EosWrite = EAV.EOSIO;
                    var profile = await Read.Profile(act);
                    ReadData(act);
                    if (Properties.Settings.Default.aWithdraw)
                    {
                        int fee = await Read.GetFee();
                        try
                        {

                            if (fee <= int.Parse(Properties.Settings.Default.tax) && double.Parse(act.profile.Wood.Split(' ')[0]) > Convert.ToDouble(Properties.Settings.Default.mWood))
                            {
                                var response = await act.Withdraw(act.profile.Wood, fee);
                                if (!response.ToLower().Contains("something is wong"))
                                {
                                    response = $"WithDraw {act.profile.Wood} !*";
                                    msg($"{act.name} {response}", act.notifytoken, Color.HotPink);
                                }
                            }
                        }
                        catch
                        {

                        }


                        try
                        {
                            if (Properties.Settings.Default.aSell)
                            {
                                if (fee >= int.Parse(Properties.Settings.Default.tax))
                                {
                                    var FWW = await Read.GetFWW(act);
                                    double CheckFWW = double.Parse(FWW.Split(' ')[0]);
                                    if (CheckFWW >= 50)
                                    {
                                        var response = await act.SellFWW(FWW, act.name);
                                        msg($"{act.name} {response}", act.notifytoken, Color.HotPink);
                                    }
                                }

                            }
                        }
                        catch
                        {

                        }
                    }

                    if (profile != null)
                    {
                        act.profile = profile;
                        try
                        {
                            foreach (var str in act.profile.balance)
                            {
                                var coin = str.ToString().Split(' ');

                                if (coin[1] == "WOOD")
                                {
                                    act.profile.Wood = str.ToString();
                                }

                                if (coin[1] == "FOOD")
                                {
                                    act.profile.Food = str.ToString();
                                }

                                if (coin[1] == "GOLD")
                                {
                                    act.profile.Gold = str.ToString();
                                }
                            }
                        }
                        catch { }

                    }
                }
                TextRPC.Text = EAV.rpc;
                RefreshTitle();
            }
        }

        private async void msg(string msg, string token = null, Color color = default)
        {
            if (viewLogs.Items.Count > 250)
                viewLogs.Items.RemoveAt(viewLogs.Items.Count - 1);
            if (viewLogs.InvokeRequired)
            {
                viewLogs.Invoke((MethodInvoker)delegate ()
                {
                    viewLogs.Items.Insert(0, $"[{DateTime.Now}] {msg}").ForeColor = color;
                    if (token != null && token != "")
                        WaxAPI.notify(msg, token);
                });
            }
            else
            {
                viewLogs.Items.Insert(0, $"[{DateTime.Now}] {msg}").ForeColor = color;
                if (token != null && token != "")
                    WaxAPI.notify(msg, token);
            }

        }

        private async void loader()
        {
            await Task.Run(() =>
            {
                if (textload == "- - -")
                    textload = "/ - /";
                else if (textload == "/ - /")
                    textload = "-- - --";
                else if (textload == "-- - --")
                    textload = "\\ - \\";
                else if (textload == "\\ - \\")
                    textload = "I - I";
                else if (textload == "I - I")
                    textload = "/ - /";
                else
                    textload = "- - -";

                status.Text = $"{textload}";
            });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loader();
        }

        private void viewAccounts_DoubleClick(object sender, EventArgs e)
        {
            RefreshThis = viewAccounts.SelectedItems[0].Text;
            RefreshTitle();
            using (Explorer form = new Explorer(viewAccounts.SelectedItems[0].Text))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {

                }
            }

        }

        private async void MainForm_Load(object sender, EventArgs e)
        {

            EAV.EOSIO = new EosClass(new EosConfigurator() { HttpEndpoint = EAV.rpc, ChainId = EAV.ChainId, ExpireSeconds = 60 }, new HttpHandler());

            foreach (var act in EAV.ACT)
            {
                var profile = await Read.Profile(act);
                ReadData(act);
                if (profile != null)
                {
                    act.profile = profile;
                    try
                    {
                        foreach (var str in act.profile.balance)
                        {
                            var coin = str.ToString().Split(' ');

                            if (coin[1] == "WOOD")
                            {
                                act.profile.Wood = str.ToString();
                            }

                            if (coin[1] == "FOOD")
                            {
                                act.profile.Food = str.ToString();
                            }

                            if (coin[1] == "GOLD")
                            {
                                act.profile.Gold = str.ToString();
                            }
                        }
                    }
                    catch
                    {

                    }

                    if (!act.anchor)
                    {
                        act.EosWrite = EAV.EOSIO;
                        if (act.token == "" || act.token == null)
                        {
                            var response = "?????";
                            msg($"{act.name} token is null", act.notifytoken, Color.Red);
                            response = act.CreateSession();
                            if (response == "Please Check Mail!")
                            {
                                msg($"{act.name} {response}", act.notifytoken, Color.Red);
                                act.stop = true;
                            }
                            else
                            {
                                act.stop = false;
                            }
                            var selectID = EAV.login.Find(x => x.name == act.name);
                            selectID.token = act.token;
                            EAV.login.saveJS();
                            msg($"{act.name} {response}", act.notifytoken, Color.DodgerBlue);
                        }
                    }
                    else
                    {
                        act.EosWrite = new EosClass(new EosConfigurator()
                        {
                            HttpEndpoint = EAV.rpc,
                            ChainId = EAV.ChainId,
                            ExpireSeconds = 60,
                            SignProvider = new EosSharp.Core.Providers.DefaultSignProvider(act.privatekeys)
                        }, new HttpHandler());
                    }
                }
                RefreshTitle();
                act.tools = new List<Tools>();
                viewAccounts.Items.Add(act.name);

            }


            //Refresh.Start();
            TimeMain.Start();
            Next.Start();
            status.ForeColor = Color.DodgerBlue;
        }
        bool title = false;
        private async void RefreshTitle()
        {
            if (!title)
            {
                title = true;
                var Act = EAV.ACT.Find(x => x.name == RefreshThis);
                try
                {
                    var usedCPU = await Read.GetAccount(Act);
                    TextW.Text = Act.profile.Wood;
                    TextF.Text = Act.profile.Food;
                    TextG.Text = Act.profile.Gold;
                    TextE.Text = $"Energy [ {Act.profile.energy}/{Act.profile.max_energy} ]";

                    TextB.Text = $"Barley [ {Act.Food.Select(x => x.name == "Barley").Count()} ]";
                    if (usedCPU > 0)
                    {
                        Act.CPU = usedCPU;
                        TextC.Text = $"CPU [ {usedCPU.ToString("n2")} % ]";
                    }

                }
                catch { }
                title = false;
            }


        }

        private void viewLogs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void exchangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var aei = new AEI())
            {
                aei.ShowDialog();
                EAV.rpc = Properties.Settings.Default.aRPC;
                EAV.EOSIO = new EosClass(new EosConfigurator() { HttpEndpoint = EAV.rpc, ChainId = EAV.ChainId, ExpireSeconds = 60 }, new HttpHandler());
                foreach (var act in EAV.ACT)
                    act.EosWrite = EAV.EOSIO;
            }
        }

        private void programToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
