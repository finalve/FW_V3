
using FW_V3.SubClass;

namespace FW_V3.View
{
    partial class Explorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TextID = new System.Windows.Forms.ToolStripMenuItem();
            this.TextW = new System.Windows.Forms.ToolStripMenuItem();
            this.withdrawWood = new System.Windows.Forms.ToolStripMenuItem();
            this.TextF = new System.Windows.Forms.ToolStripMenuItem();
            this.TextG = new System.Windows.Forms.ToolStripMenuItem();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listview1 = new FW_V3.SubClass.ListViewNF();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.autoExchangeItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "AssetID";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Durability";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Countdown";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Status";
            this.columnHeader5.Width = 90;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TextID,
            this.TextW,
            this.TextF,
            this.TextG});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(501, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // TextID
            // 
            this.TextID.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoExchangeItemToolStripMenuItem});
            this.TextID.ForeColor = System.Drawing.Color.LimeGreen;
            this.TextID.Name = "TextID";
            this.TextID.Size = new System.Drawing.Size(99, 20);
            this.TextID.Text = "ID [ waxwallet ]";
            // 
            // TextW
            // 
            this.TextW.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.withdrawWood});
            this.TextW.ForeColor = System.Drawing.Color.LimeGreen;
            this.TextW.Name = "TextW";
            this.TextW.Size = new System.Drawing.Size(87, 20);
            this.TextW.Text = "0.0000 Wood";
            // 
            // withdrawWood
            // 
            this.withdrawWood.Name = "withdrawWood";
            this.withdrawWood.Size = new System.Drawing.Size(125, 22);
            this.withdrawWood.Text = "Withdraw";
            this.withdrawWood.Click += new System.EventHandler(this.withdrawWood_Click);
            // 
            // TextF
            // 
            this.TextF.ForeColor = System.Drawing.Color.LimeGreen;
            this.TextF.Name = "TextF";
            this.TextF.Size = new System.Drawing.Size(82, 20);
            this.TextF.Text = "0.0000 Food";
            // 
            // TextG
            // 
            this.TextG.ForeColor = System.Drawing.Color.LimeGreen;
            this.TextG.Name = "TextG";
            this.TextG.Size = new System.Drawing.Size(80, 20);
            this.TextG.Text = "0.0000 Gold";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "AssetID";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Type";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Durability";
            this.columnHeader8.Width = 100;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Countdown";
            this.columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Status";
            this.columnHeader10.Width = 90;
            // 
            // listview1
            // 
            this.listview1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listview1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.listview1.HideSelection = false;
            this.listview1.Location = new System.Drawing.Point(2, 27);
            this.listview1.Name = "listview1";
            this.listview1.Size = new System.Drawing.Size(496, 390);
            this.listview1.TabIndex = 5;
            this.listview1.UseCompatibleStateImageBehavior = false;
            this.listview1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Asset_ID";
            this.columnHeader11.Width = 120;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Type";
            this.columnHeader12.Width = 120;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Durability";
            this.columnHeader13.Width = 120;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Countdown";
            this.columnHeader14.Width = 120;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // autoExchangeItemToolStripMenuItem
            // 
            this.autoExchangeItemToolStripMenuItem.Name = "autoExchangeItemToolStripMenuItem";
            this.autoExchangeItemToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.autoExchangeItemToolStripMenuItem.Text = "Auto Exchange Item";
            this.autoExchangeItemToolStripMenuItem.Click += new System.EventHandler(this.autoExchangeItemToolStripMenuItem_Click);
            // 
            // Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 421);
            this.Controls.Add(this.listview1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Explorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Explorer";
            this.Load += new System.EventHandler(this.Explorer_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TextW;
        private System.Windows.Forms.ToolStripMenuItem withdrawWood;
        private System.Windows.Forms.ToolStripMenuItem TextF;
        private System.Windows.Forms.ToolStripMenuItem TextG;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ToolStripMenuItem TextID;
        private ListViewNF listview1;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem autoExchangeItemToolStripMenuItem;
    }
}