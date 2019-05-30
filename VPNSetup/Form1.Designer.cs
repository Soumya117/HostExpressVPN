namespace VPNSetup
{
    partial class Form1
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.connect = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.button3 = new System.Windows.Forms.Button();
      this.linkLabel2 = new System.Windows.Forms.LinkLabel();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.pictureBox4 = new System.Windows.Forms.PictureBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.connection_label = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.status_label = new System.Windows.Forms.Label();
      this.ssid_label = new System.Windows.Forms.Label();
      this.status_pictureBox = new System.Windows.Forms.PictureBox();
      this.status_value = new System.Windows.Forms.Label();
      this.ssid_value = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.status_pictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // connect
      // 
      this.connect.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.connect.BackColor = System.Drawing.SystemColors.ActiveBorder;
      this.connect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("connect.BackgroundImage")));
      this.connect.Cursor = System.Windows.Forms.Cursors.Hand;
      this.connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.connect.ForeColor = System.Drawing.Color.Lavender;
      this.connect.Location = new System.Drawing.Point(12, 54);
      this.connect.Name = "connect";
      this.connect.Size = new System.Drawing.Size(168, 33);
      this.connect.TabIndex = 0;
      this.connect.Text = "Connect to hostedNetwork";
      this.connect.UseVisualStyleBackColor = false;
      this.connect.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.button2.BackColor = System.Drawing.SystemColors.ActiveBorder;
      this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
      this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button2.ForeColor = System.Drawing.Color.Lavender;
      this.button2.Location = new System.Drawing.Point(12, 149);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(168, 35);
      this.button2.TabIndex = 1;
      this.button2.Text = "Create new hostedNetwork";
      this.button2.UseVisualStyleBackColor = false;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // linkLabel1
      // 
      this.linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
      this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.linkLabel1.Location = new System.Drawing.Point(20, 241);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(104, 16);
      this.linkLabel1.TabIndex = 2;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Show Password";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // button3
      // 
      this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.button3.BackColor = System.Drawing.SystemColors.ActiveBorder;
      this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
      this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
      this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button3.ForeColor = System.Drawing.Color.Lavender;
      this.button3.Location = new System.Drawing.Point(12, 104);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(168, 30);
      this.button3.TabIndex = 3;
      this.button3.Text = "Stop hostedNetwork";
      this.button3.UseVisualStyleBackColor = false;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // linkLabel2
      // 
      this.linkLabel2.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
      this.linkLabel2.Cursor = System.Windows.Forms.Cursors.Hand;
      this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.linkLabel2.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.linkLabel2.Location = new System.Drawing.Point(21, 222);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new System.Drawing.Size(118, 16);
      this.linkLabel2.TabIndex = 4;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "Change Password";
      this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
      // 
      // pictureBox4
      // 
      this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
      this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
      this.pictureBox4.Location = new System.Drawing.Point(549, 5);
      this.pictureBox4.Name = "pictureBox4";
      this.pictureBox4.Size = new System.Drawing.Size(21, 20);
      this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox4.TabIndex = 70;
      this.pictureBox4.TabStop = false;
      this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
      // 
      // pictureBox1
      // 
      this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.pictureBox1.Location = new System.Drawing.Point(331, 45);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(228, 153);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 71;
      this.pictureBox1.TabStop = false;
      // 
      // connection_label
      // 
      this.connection_label.AutoSize = true;
      this.connection_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.connection_label.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.connection_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.connection_label.Location = new System.Drawing.Point(343, 66);
      this.connection_label.Name = "connection_label";
      this.connection_label.Size = new System.Drawing.Size(143, 17);
      this.connection_label.TabIndex = 72;
      this.connection_label.Text = "Internet Connection : ";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.label2.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.label2.Location = new System.Drawing.Point(343, 108);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(111, 17);
      this.label2.TabIndex = 73;
      this.label2.Text = "Hosted Network";
      // 
      // status_label
      // 
      this.status_label.AutoSize = true;
      this.status_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.status_label.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.status_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.status_label.Location = new System.Drawing.Point(343, 136);
      this.status_label.Name = "status_label";
      this.status_label.Size = new System.Drawing.Size(56, 17);
      this.status_label.TabIndex = 74;
      this.status_label.Text = "Status : ";
      // 
      // ssid_label
      // 
      this.ssid_label.AutoSize = true;
      this.ssid_label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.ssid_label.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ssid_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.ssid_label.Location = new System.Drawing.Point(343, 158);
      this.ssid_label.Name = "ssid_label";
      this.ssid_label.Size = new System.Drawing.Size(47, 17);
      this.ssid_label.TabIndex = 77;
      this.ssid_label.Text = "SSID : ";
      // 
      // status_pictureBox
      // 
      this.status_pictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.status_pictureBox.Image = global::VPNSetup.Properties.Resources.imageedit_7_4663135021_new;
      this.status_pictureBox.Location = new System.Drawing.Point(482, 65);
      this.status_pictureBox.Name = "status_pictureBox";
      this.status_pictureBox.Size = new System.Drawing.Size(19, 21);
      this.status_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.status_pictureBox.TabIndex = 103;
      this.status_pictureBox.TabStop = false;
      // 
      // status_value
      // 
      this.status_value.AutoSize = true;
      this.status_value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.status_value.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.status_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.status_value.Location = new System.Drawing.Point(416, 136);
      this.status_value.Name = "status_value";
      this.status_value.Size = new System.Drawing.Size(0, 17);
      this.status_value.TabIndex = 104;
      // 
      // ssid_value
      // 
      this.ssid_value.AutoSize = true;
      this.ssid_value.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.ssid_value.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ssid_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.ssid_value.Location = new System.Drawing.Point(416, 158);
      this.ssid_value.Name = "ssid_value";
      this.ssid_value.Size = new System.Drawing.Size(0, 17);
      this.ssid_value.TabIndex = 105;
      // 
      // Form1
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.DarkSlateBlue;
      this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.ClientSize = new System.Drawing.Size(582, 264);
      this.Controls.Add(this.ssid_value);
      this.Controls.Add(this.status_value);
      this.Controls.Add(this.status_pictureBox);
      this.Controls.Add(this.connection_label);
      this.Controls.Add(this.ssid_label);
      this.Controls.Add(this.status_label);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.pictureBox4);
      this.Controls.Add(this.linkLabel2);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.connect);
      this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "VPNSetup";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.status_pictureBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

    #endregion

    private System.Windows.Forms.Button connect;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.LinkLabel linkLabel2;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.PictureBox pictureBox4;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Label connection_label;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label status_label;
    private System.Windows.Forms.Label ssid_label;
    private System.Windows.Forms.PictureBox status_pictureBox;
    private System.Windows.Forms.Label status_value;
    private System.Windows.Forms.Label ssid_value;
  }
}

