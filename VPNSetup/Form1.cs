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

    public void extract_hostedNetwork(Output data)
    {
      var result = data.output;
      if (result != null && result.Contains("SSID"))
      {
        var result_str = result.ToString();
        var ssid = result_str.Split(':')[1];
        var hostedNetwork = string.Join(" ", ssid.Split('"').Where((x, i) => i % 2 != 0));
        start_hostedNertwork(hostedNetwork);
      }
      //if (data.error != null)
      //{
      //  MessageBox.Show(data.error, "Error");
      //}
    }

    public void start_hostedNertwork(string hostedNetwork)
    {
      var start_cmd = cmd.startHostedNetwork(hostedNetwork);
      processCmd = new ProcessCmd(start_cmd);
      var connect_output = processCmd.start();
      connect_hostedNetwork(connect_output);
    }

    public void connect_hostedNetwork(Output result)
    {
      if(result.output != null)
      {
        MessageBox.Show(result.output, "Status");
      }
      //if (result.error != null)
      //{
      //  MessageBox.Show(result.error, "Error");
      //}
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var show_cmd = cmd.viewHostedNetwork();
      processCmd = new ProcessCmd(show_cmd);
      var view_output = processCmd.start();
      extract_hostedNetwork(view_output);
    }

    public void stop_hostedNetwork(Output result)
    {
      if(result.output != null)
      {
        MessageBox.Show(result.output, "Status");
      }
      //if (result.error != null)
      //{
      //  MessageBox.Show(result.error, "Error");
      //}
    }

    private void button3_Click(object sender, EventArgs e)
    {
      var stop_cmd = cmd.stopHostedNetwork();
      processCmd = new ProcessCmd(stop_cmd);
      var stop_output = processCmd.start();
      stop_hostedNetwork(stop_output);
    }
  }
}
