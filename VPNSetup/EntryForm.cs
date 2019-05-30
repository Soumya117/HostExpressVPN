using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPNSetup
{
  public partial class EntryForm : Form
  {
    public EntryForm()
    {
      InitializeComponent();
    }

    private void pictureBox4_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      //check if microsoft hosted network driver is there..
      //check if express vnp adapter is present
      //check if services are up
      //check if the machine has internet
    }

    private void button2_Click(object sender, EventArgs e)
    {
      new Form1().Show();
    }

    private void label1_Click(object sender, EventArgs e)
    {
      new CheckAvailability().Show();
    }

    private void label2_Click(object sender, EventArgs e)
    {
      new Form1().Show();
    }
  }
}
