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
  public partial class createNetwork : Form
  {
    command cmd = new command();
    ProcessCmd processCmd;
    public createNetwork()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var create_cmd = cmd.createHostedNetwork(textBox1.Text, textBox2.Text);
      processCmd = new ProcessCmd(create_cmd);
      processCmd.start();
    }
  }
}