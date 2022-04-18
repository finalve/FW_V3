using FW_V3.Controller;
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
    public partial class Explorer : Form
    {
        private string NAME { get; set; }
        readController Read = new readController();
        public Explorer(string name)
        {
            InitializeComponent();
            NAME = name;
        }

        private async void Explorer_Load(object sender, EventArgs e)
        {
            TextID.Text = $"ID [ {NAME} ]";
            try
            {
                int fee = await Read.GetFee();
                withdrawWood.Text = $"Withdraw Tax {fee}%";
            }
            catch
            {

            }
            try
            {
                var act = EAV.ACT.Find(x => x.name == NAME);
                TextW.Text = act.profile.Wood;
                TextF.Text = act.profile.Food;
                TextG.Text = act.profile.Gold;
            
                foreach(var eq in act.tools)
                {
                    try
                    {
                        var unixTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                        string answer = "00:00:00";
                        var stack = Properties.Settings.Default.aStack * 3600;
                        long wait = eq.next_availability;
                        if(eq.type == "tools" && eq.name == "Wood")
                        {
                            wait = (stack - 3600) + wait;
                        }
                           
                        var Next = wait - unixTimestamp;

                        TimeSpan t = TimeSpan.FromSeconds(Next);
                        if (Next > 0)
                        {
                            answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                   t.Hours,
                                   t.Minutes,
                                   t.Seconds
                                  );
                        }
                        listview1.Items.Add(new ListViewItem(new string[] { eq.asset_id, eq.name, $"{eq.current_durability}/{eq.durability}", answer }));
                    }
                    catch
                    {

                    }
                }
            }
            catch
            {

            }
            timer1.Start();
        }

        private async void withdrawWood_Click(object sender, EventArgs e)
        {

            var act = EAV.ACT.Find(x => x.name == NAME);
            int fee = await Read.GetFee();
            var response = await act.Withdraw(act.profile.Wood, fee);
            if(response== "Success!")
            {
                act.profile = await Read.Profile(act);
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
                TextW.Text = act.profile.Wood;
                TextF.Text = act.profile.Food;
                TextG.Text = act.profile.Gold;
            }
            MessageBox.Show(response);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var act = EAV.ACT.Find(x => x.name == NAME);
            for(int i = 0;i<act.tools.Count;i++)
            {
                try
                {
                    var unixTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                    var stack = Properties.Settings.Default.aStack * 3600;
                    long wait = act.tools[i].next_availability;
                    if (act.tools[i].type == "tools" && act.tools[i].name == "Wood")
                    {
                        wait = (stack - 3600) + wait;
                    }
                    var Next = wait - unixTimestamp;

                    TimeSpan t = TimeSpan.FromSeconds(Next);
                    string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                              t.Hours,
                              t.Minutes,
                              t.Seconds
                             );
                    if (Next < 0)
                    {
                        listview1.Items[i].SubItems[3].Text = "00:00:00";

                    }
                    else
                    {
                        try { listview1.Items[i].SubItems[3].Text = answer; } catch { }

                    }
                }
                catch { }
               
             
            }
           
        }

        private void autoExchangeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var aei = new AEI())
            {
                aei.ShowDialog();
            }
        }
    }
}
