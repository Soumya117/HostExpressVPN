namespace VPNSetup
{
  partial class createNetwork
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(createNetwork));
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.pictureBox4 = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.BackColor = System.Drawing.Color.Transparent;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
      this.label1.Location = new System.Drawing.Point(24, 83);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(141, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "HostedNetwork Name";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.BackColor = System.Drawing.Color.Transparent;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
      this.label2.Location = new System.Drawing.Point(24, 112);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(68, 16);
      this.label2.TabIndex = 1;
      this.label2.Text = "Password";
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(188, 82);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(186, 20);
      this.textBox1.TabIndex = 2;
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(188, 111);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(186, 20);
      this.textBox2.TabIndex = 3;
      // 
      // button1
      // 
      this.button1.BackColor = System.Drawing.Color.LightSteelBlue;
      this.button1.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.button1.Location = new System.Drawing.Point(188, 146);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(50, 23);
      this.button1.TabIndex = 4;
      this.button1.Text = "Create";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // pictureBox4
      // 
      this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
      this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.pictureBox4.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
      this.pictureBox4.Location = new System.Drawing.Point(390, 12);
      this.pictureBox4.Name = "pictureBox4";
      this.pictureBox4.Size = new System.Drawing.Size(21, 20);
      this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pictureBox4.TabIndex = 72;
      this.pictureBox4.TabStop = false;
      this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
      // 
      // createNetwork
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Teal;
      this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.ClientSize = new System.Drawing.Size(423, 211);
      this.Controls.Add(this.pictureBox4);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "createNetwork";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "createNetwork";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.PictureBox pictureBox4;
  }
}