using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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

    static void create_hostedNetwork(object sender, DataReceivedEventArgs e)
    {
      Console.WriteLine("Creating...");
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var create_cmd = cmd.createHostedNetwork(textBox1.Text, textBox2.Text);
      processCmd = new ProcessCmd(create_cmd, create_hostedNetwork);
      processCmd.start();
    }
  }
}