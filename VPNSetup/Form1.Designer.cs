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
      this.connect = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.button3 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // connect
      // 
      this.connect.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.connect.Location = new System.Drawing.Point(109, 99);
      this.connect.Name = "connect";
      this.connect.Size = new System.Drawing.Size(153, 43);
      this.connect.TabIndex = 0;
      this.connect.Text = "Connect to hostedNetwork";
      this.connect.UseVisualStyleBackColor = true;
      this.connect.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.button2.Location = new System.Drawing.Point(310, 99);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(153, 43);
      this.button2.TabIndex = 1;
      this.button2.Text = "Create new hostedNetwork";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new System.Drawing.Point(122, 194);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(83, 13);
      this.linkLabel1.TabIndex = 2;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Show Password";
      // 
      // button3
      // 
      this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.button3.Location = new System.Drawing.Point(109, 148);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(153, 43);
      this.button3.TabIndex = 3;
      this.button3.Text = "Stop hostedNetwork";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(582, 264);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.connect);
      this.Name = "Form1";
      this.Text = "VPNSetup";
      this.ResumeLayout(false);
      this.PerformLayout();

        }

    #endregion

    private System.Windows.Forms.Button connect;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Button button3;
  }
}

