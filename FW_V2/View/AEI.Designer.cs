
namespace FW_V3.View
{
    partial class AEI
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
            this.milk = new System.Windows.Forms.CheckBox();
            this.egg = new System.Windows.Forms.CheckBox();
            this.barley = new System.Windows.Forms.CheckBox();
            this.corn = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.autowithdraw = new System.Windows.Forms.CheckBox();
            this.mWood = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.aStack = new System.Windows.Forms.TextBox();
            this.cpuLimit = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.autoSell = new System.Windows.Forms.CheckBox();
            this.taxBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // milk
            // 
            this.milk.AutoSize = true;
            this.milk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.milk.Location = new System.Drawing.Point(12, 12);
            this.milk.Name = "milk";
            this.milk.Size = new System.Drawing.Size(55, 24);
            this.milk.TabIndex = 0;
            this.milk.Text = "Milk";
            this.milk.UseVisualStyleBackColor = true;
            // 
            // egg
            // 
            this.egg.AutoSize = true;
            this.egg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.egg.Location = new System.Drawing.Point(89, 12);
            this.egg.Name = "egg";
            this.egg.Size = new System.Drawing.Size(65, 24);
            this.egg.TabIndex = 1;
            this.egg.Text = "Eggs";
            this.egg.UseVisualStyleBackColor = true;
            // 
            // barley
            // 
            this.barley.AutoSize = true;
            this.barley.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.barley.Location = new System.Drawing.Point(89, 43);
            this.barley.Name = "barley";
            this.barley.Size = new System.Drawing.Size(72, 24);
            this.barley.TabIndex = 3;
            this.barley.Text = "Barley";
            this.barley.UseVisualStyleBackColor = true;
            // 
            // corn
            // 
            this.corn.AutoSize = true;
            this.corn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.corn.Location = new System.Drawing.Point(12, 43);
            this.corn.Name = "corn";
            this.corn.Size = new System.Drawing.Size(62, 24);
            this.corn.TabIndex = 4;
            this.corn.Text = "Corn";
            this.corn.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Lime;
            this.button1.Location = new System.Drawing.Point(188, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 98);
            this.button1.TabIndex = 5;
            this.button1.Text = "SET";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // autowithdraw
            // 
            this.autowithdraw.AutoSize = true;
            this.autowithdraw.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.autowithdraw.Location = new System.Drawing.Point(12, 73);
            this.autowithdraw.Name = "autowithdraw";
            this.autowithdraw.Size = new System.Drawing.Size(53, 24);
            this.autowithdraw.TabIndex = 6;
            this.autowithdraw.Text = "Tax";
            this.autowithdraw.UseVisualStyleBackColor = true;
            // 
            // mWood
            // 
            this.mWood.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.mWood.Location = new System.Drawing.Point(89, 101);
            this.mWood.Margin = new System.Windows.Forms.Padding(1);
            this.mWood.Name = "mWood";
            this.mWood.Size = new System.Drawing.Size(90, 26);
            this.mWood.TabIndex = 7;
            this.mWood.Text = "100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(12, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Above";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(12, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "STACK";
            // 
            // aStack
            // 
            this.aStack.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FW_V3.Properties.Settings.Default, "aStack", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.aStack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.aStack.Location = new System.Drawing.Point(89, 155);
            this.aStack.Name = "aStack";
            this.aStack.Size = new System.Drawing.Size(90, 26);
            this.aStack.TabIndex = 11;
            // 
            // cpuLimit
            // 
            this.cpuLimit.AutoSize = true;
            this.cpuLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cpuLimit.Location = new System.Drawing.Point(12, 192);
            this.cpuLimit.Name = "cpuLimit";
            this.cpuLimit.Size = new System.Drawing.Size(90, 24);
            this.cpuLimit.TabIndex = 13;
            this.cpuLimit.Text = "CpuLimit";
            this.cpuLimit.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "https://wax.greymass.com",
            "https://wax.eu.eosamsterdam.net",
            "https://wax.cryptolions.io",
            "https://wax.dapplica.io",
            "https://api.waxsweden.org",
            "https://api.wax.bountyblok.io",
            "https://api.wax.greeneosio.com",
            "https://api.wax.alohaeos.com"});
            this.comboBox1.Location = new System.Drawing.Point(58, 218);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(218, 28);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.Text = "https://wax.greymass.com";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(8, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "RPC";
            // 
            // autoSell
            // 
            this.autoSell.AutoSize = true;
            this.autoSell.Checked = global::FW_V3.Properties.Settings.Default.aSell;
            this.autoSell.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::FW_V3.Properties.Settings.Default, "aSell", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.autoSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.autoSell.Location = new System.Drawing.Point(12, 131);
            this.autoSell.Name = "autoSell";
            this.autoSell.Size = new System.Drawing.Size(88, 24);
            this.autoSell.TabIndex = 10;
            this.autoSell.Text = "AutoSell";
            this.autoSell.UseVisualStyleBackColor = true;
            // 
            // taxBox
            // 
            this.taxBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FW_V3.Properties.Settings.Default, "tax", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.taxBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.taxBox.Location = new System.Drawing.Point(89, 73);
            this.taxBox.Margin = new System.Windows.Forms.Padding(1);
            this.taxBox.Name = "taxBox";
            this.taxBox.Size = new System.Drawing.Size(90, 26);
            this.taxBox.TabIndex = 8;
            this.taxBox.Text = global::FW_V3.Properties.Settings.Default.tax;
            this.taxBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // AEI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 256);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.cpuLimit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.aStack);
            this.Controls.Add(this.autoSell);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.taxBox);
            this.Controls.Add(this.mWood);
            this.Controls.Add(this.autowithdraw);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.corn);
            this.Controls.Add(this.barley);
            this.Controls.Add(this.egg);
            this.Controls.Add(this.milk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AEI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Auto Exchange";
            this.Load += new System.EventHandler(this.AEI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox milk;
        private System.Windows.Forms.CheckBox egg;
        private System.Windows.Forms.CheckBox barley;
        private System.Windows.Forms.CheckBox corn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox autowithdraw;
        private System.Windows.Forms.TextBox mWood;
        private System.Windows.Forms.TextBox taxBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox autoSell;
        private System.Windows.Forms.TextBox aStack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cpuLimit;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
    }
}