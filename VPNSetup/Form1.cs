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
using Microsoft.VisualBasic;

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

    private string extract_hostedNetwork(Output data)
    {
      var result = data.output;
      string hostedNetwork = String.Empty;
      string[] lines = result.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        if (line != null && line.Contains("SSID"))
        {
          var result_str = line.ToString();
          var ssid = line.Split(':')[1];
          hostedNetwork = string.Join(" ", ssid.Split('"').Where((x, i) => i % 2 != 0));
          break;
        }
      }
      return hostedNetwork;
    }

    private Output start_hostedNertwork()
    {
      var start_cmd = cmd.startHostedNetwork();
      processCmd = new ProcessCmd(start_cmd);
      processCmd.start();
      var connect_output = processCmd.getOutput();
      return connect_output;
    }

    private void display_hostedNetwork(Output result)
    {
      if (result.output != null)
      {
        MessageBox.Show(result.output, "Status");
      }
    }

    private void stop_hostedNetwork(Output result)
    {
      if (result.output != null)
      {
        MessageBox.Show(result.output, "Status");
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var show_cmd = cmd.viewHostedNetwork();
      processCmd = new ProcessCmd(show_cmd);
      processCmd.start();
      var view_output = processCmd.getOutput();
      extract_hostedNetwork(view_output);
      var connect_output = start_hostedNertwork();
      display_hostedNetwork(connect_output);

    }

    private void button3_Click(object sender, EventArgs e)
    {
      var stop_cmd = cmd.stopHostedNetwork();
      processCmd = new ProcessCmd(stop_cmd);
      processCmd.start();
      var stop_output = processCmd.getOutput();
      stop_hostedNetwork(stop_output);
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      var show_pwd = cmd.showPassword();
      processCmd = new ProcessCmd(show_pwd);
      processCmd.start();
      var show_output = processCmd.getOutput();
      show_password(show_output);
    }

    private void show_password(Output result)
    {
      string[] lines = result.output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (string line in lines)
      {
        if (line.Contains("User security key"))
        {
          var key = line.Split(':')[1];
          MessageBox.Show("Password is: " + key, "Info");
          return;
        }
      }
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      string password = Interaction.InputBox("Enter new password", "Input", "", -1, -1);
      if (password.Length > 0)
      {
        var show_cmd = cmd.viewHostedNetwork();
        processCmd = new ProcessCmd(show_cmd);
        processCmd.start();
        var view_output = processCmd.getOutput();
        var hostname = extract_hostedNetwork(view_output);
        var create_cmd = cmd.createHostedNetwork(hostname, password);
        processCmd = new ProcessCmd(create_cmd);
        processCmd.start();
        var create_output = processCmd.getOutput();
        create_hostedNetwork(create_output);
      }
      else
        return;
    }

    private void create_hostedNetwork(Output result)
    {
      if (!String.IsNullOrEmpty(result.output))
      {
        MessageBox.Show("Password Changed successfully");
      }
    }
  }
}
