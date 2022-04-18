
using FW_V3.SubClass;

namespace FW_V3.View
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exchangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TextW = new System.Windows.Forms.ToolStripMenuItem();
            this.TextF = new System.Windows.Forms.ToolStripMenuItem();
            this.TextG = new System.Windows.Forms.ToolStripMenuItem();
            this.TextB = new System.Windows.Forms.ToolStripMenuItem();
            this.TextE = new System.Windows.Forms.ToolStripMenuItem();
            this.TextC = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeMain = new System.Windows.Forms.Timer(this.components);
            this.Refresh = new System.Windows.Forms.Timer(this.components);
            this.Next = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TextRPC = new System.Windows.Forms.ToolStripStatusLabel();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.viewLogs = new FW_V3.SubClass.ListViewNF();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.viewAccounts = new FW_V3.SubClass.ListViewNF();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.TextW,
            this.TextF,
            this.TextG,
            this.TextB,
            this.TextE,
            this.TextC});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(876, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exchangeToolStripMenuItem});
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(131, 20);
            this.programToolStripMenuItem.Text = "Program                      ";
            this.programToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.programToolStripMenuItem.Click += new System.EventHandler(this.programToolStripMenuItem_Click);
            // 
            // exchangeToolStripMenuItem
            // 
            this.exchangeToolStripMenuItem.Name = "exchangeToolStripMenuItem";
            this.exchangeToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.exchangeToolStripMenuItem.Text = "Exchange";
            this.exchangeToolStripMenuItem.Click += new System.EventHandler(this.exchangeToolStripMenuItem_Click);
            // 
            // TextW
            // 
            this.TextW.Name = "TextW";
            this.TextW.Size = new System.Drawing.Size(81, 20);
            this.TextW.Text = "0.000 Wood";
            // 
            // TextF
            // 
            this.TextF.Name = "TextF";
            this.TextF.Size = new System.Drawing.Size(76, 20);
            this.TextF.Text = "0.000 Food";
            // 
            // TextG
            // 
            this.TextG.Name = "TextG";
            this.TextG.Size = new System.Drawing.Size(74, 20);
            this.TextG.Text = "0.000 Gold";
            // 
            // TextB
            // 
            this.TextB.Name = "TextB";
            this.TextB.Size = new System.Drawing.Size(74, 20);
            this.TextB.Text = "Barley [ 0 ]";
            // 
            // TextE
            // 
            this.TextE.Name = "TextE";
            this.TextE.Size = new System.Drawing.Size(78, 20);
            this.TextE.Text = "Energy [ 0 ]";
            // 
            // TextC
            // 
            this.TextC.Name = "TextC";
            this.TextC.Size = new System.Drawing.Size(65, 20);
            this.TextC.Text = "CPU [ 0 ]";
            // 
            // TimeMain
            // 
            this.TimeMain.Interval = 2000;
            this.TimeMain.Tick += new System.EventHandler(this.TimeMain_Tick);
            // 
            // Refresh
            // 
            this.Refresh.Interval = 30000;
            this.Refresh.Tick += new System.EventHandler(this.Refresh_Tick);
            // 
            // Next
            // 
            this.Next.Interval = 30000;
            this.Next.Tick += new System.EventHandler(this.Next_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TextRPC,
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 464);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(876, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TextRPC
            // 
            this.TextRPC.ForeColor = System.Drawing.Color.Blue;
            this.TextRPC.Name = "TextRPC";
            this.TextRPC.Size = new System.Drawing.Size(43, 17);
            this.TextRPC.Text = "RPC [ ]";
            // 
            // status
            // 
            this.status.ForeColor = System.Drawing.Color.Red;
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(12, 17);
            this.status.Text = "-";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // viewLogs
            // 
            this.viewLogs.BackColor = System.Drawing.Color.Black;
            this.viewLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.viewLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.viewLogs.ForeColor = System.Drawing.Color.White;
            this.viewLogs.HideSelection = false;
            this.viewLogs.Location = new System.Drawing.Point(141, 24);
            this.viewLogs.Name = "viewLogs";
            this.viewLogs.Size = new System.Drawing.Size(735, 440);
            this.viewLogs.TabIndex = 2;
            this.viewLogs.UseCompatibleStateImageBehavior = false;
            this.viewLogs.View = System.Windows.Forms.View.Details;
            this.viewLogs.SelectedIndexChanged += new System.EventHandler(this.viewLogs_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Logs";
            this.columnHeader2.Width = 701;
            // 
            // viewAccounts
            // 
            this.viewAccounts.BackColor = System.Drawing.Color.Black;
            this.viewAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.viewAccounts.Dock = System.Windows.Forms.DockStyle.Left;
            this.viewAccounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.viewAccounts.ForeColor = System.Drawing.Color.Lime;
            this.viewAccounts.HideSelection = false;
            this.viewAccounts.Location = new System.Drawing.Point(0, 24);
            this.viewAccounts.Name = "viewAccounts";
            this.viewAccounts.Size = new System.Drawing.Size(141, 440);
            this.viewAccounts.TabIndex = 0;
            this.viewAccounts.UseCompatibleStateImageBehavior = false;
            this.viewAccounts.View = System.Windows.Forms.View.Details;
            this.viewAccounts.DoubleClick += new System.EventHandler(this.viewAccounts_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Account";
            this.columnHeader1.Width = 111;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 486);
            this.Controls.Add(this.viewLogs);
            this.Controls.Add(this.viewAccounts);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewNF viewAccounts;
        private ListViewNF viewLogs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
   
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Timer TimeMain;
        private System.Windows.Forms.ToolStripMenuItem TextW;
        private System.Windows.Forms.ToolStripMenuItem TextF;
        private System.Windows.Forms.ToolStripMenuItem TextG;
        private System.Windows.Forms.ToolStripMenuItem TextE;
        private System.Windows.Forms.ToolStripMenuItem TextC;
        private System.Windows.Forms.Timer Refresh;
        private System.Windows.Forms.Timer Next;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel TextRPC;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem exchangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TextB;
    }
}