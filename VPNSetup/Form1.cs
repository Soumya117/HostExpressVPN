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
using System.IO;

namespace VPNSetup
{
  public partial class Form1 : Form
  {

    command cmd = new command();
    ProcessCmd processCmd;

    public Form1()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      new createNetwork().Show();
    }

    static void start_hostedNetwork(object sender, DataReceivedEventArgs e)
    {
      var result = e.Data;
      if (result != null && result.Contains("SSID"))
      {
        var result_str = result.ToString();
        var ssid = result_str.Split(':')[1];
        var hostedNetwork = string.Join(" ", ssid.Split('"').Where((x, i) => i % 2 != 0));
        Console.WriteLine(hostedNetwork);
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var show_cmd = cmd.viewHostedNetwork();
      processCmd = new ProcessCmd(show_cmd, start_hostedNetwork);
      processCmd.start();
    }

    static void stop_hostedNetwork(object sender, DataReceivedEventArgs e)
    {
      Console.WriteLine(e.Data);
    }

    private void button3_Click(object sender, EventArgs e)
    {
      var stop_cmd = cmd.stopHostedNetwork();
      processCmd = new ProcessCmd(stop_cmd, stop_hostedNetwork);
      processCmd.start();
    }
  }
}
