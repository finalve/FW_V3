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
    public partial class AEI : Form
    {
        public AEI()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.aMilk = milk.Checked;
            Properties.Settings.Default.aEggs = egg.Checked;
            Properties.Settings.Default.aCorn = corn.Checked;
            Properties.Settings.Default.aBarley = barley.Checked;
            Properties.Settings.Default.aWithdraw = autowithdraw.Checked;
            Properties.Settings.Default.mWood = int.Parse(mWood.Text);
        Properties.Settings.Default.aStack = int.Parse(aStack.Text);
            Properties.Settings.Default.tax = taxBox.Text;
            Properties.Settings.Default.aSell = autoSell.Checked;
            Properties.Settings.Default.aCpu = cpuLimit.Checked;
            Properties.Settings.Default.aRPC = comboBox1.Text;
            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
        }

        private void AEI_Load(object sender, EventArgs e)
        {
            milk.Checked = Properties.Settings.Default.aMilk;
            egg.Checked= Properties.Settings.Default.aEggs;
            corn.Checked=  Properties.Settings.Default.aCorn ;
            barley.Checked = Properties.Settings.Default.aBarley ;
           autowithdraw.Checked = Properties.Settings.Default.aWithdraw;
            mWood.Text =  Properties.Settings.Default.mWood.ToString();
            taxBox.Text = Properties.Settings.Default.tax.ToString();
            autoSell.Checked = Properties.Settings.Default.aSell;
            cpuLimit.Checked = Properties.Settings.Default.aCpu;
            aStack.Text = Properties.Settings.Default.aStack.ToString();
            comboBox1.Text = Properties.Settings.Default.aRPC;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
