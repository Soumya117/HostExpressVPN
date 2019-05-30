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
using System.Timers;

namespace VPNSetup
{
  public partial class Form1 : Form
  {
    private static System.Timers.Timer aTimer;
    command cmd = new command();
    ProcessCmd processCmd;

    public Form1()
    {
      InitializeComponent();
      aTimer = new System.Timers.Timer(10000);
      aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
      aTimer.Interval = 2000;
      aTimer.Enabled = true;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      new createNetwork().ShowDialog(this);
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
      NetworkAdapters.enable();
      display_hostedNetwork(connect_output);
    }

    private void button3_Click(object sender, EventArgs e)
    {
      var stop_cmd = cmd.stopHostedNetwork();
      processCmd = new ProcessCmd(stop_cmd);
      processCmd.start();
      var stop_output = processCmd.getOutput();
      NetworkAdapters.disable();
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

    private void pictureBox2_Click(object sender, EventArgs e)
    {
      Services.startServices();

      if (!InternetInfo.IsInternetAvailable)
      {
        MessageBox.Show("Please check your internet connection", "Error");
      }
    }

    private void pictureBox4_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void setInternetConnectionStatus()
    {
      if (InternetInfo.IsInternetAvailable)
      {
        status_pictureBox.Image = VPNSetup.Properties.Resources.imageedit_7_4663135021_new;
      }
      else
      {
        status_pictureBox.Image = VPNSetup.Properties.Resources.Webp_net_resizeimage;
      }
    }

    private string extract_status(string data)
    {
      string status = String.Empty;
      string[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        if (line != null && line.Contains("Status"))
        {
          var result_str = line.ToString();
          status = line.Split(':')[1];
          break;
        }
      }
      return status;
    }

    private void status()
    {
      setInternetConnectionStatus();
      var show_cmd = cmd.viewHostedNetwork();
      processCmd = new ProcessCmd(show_cmd);
      processCmd.start();
      var view_output = processCmd.getOutput();
      var ssid = extract_hostedNetwork(view_output);
      var status = extract_status(view_output.output);
      SetStatusText(status.Trim());
      SetSSIDText(ssid.Trim());
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
      status();
    }

    delegate void SetTextCallback(string text);

    private void SetStatusText(string text)
    {
      if (this.status_value.InvokeRequired)
      {
        SetTextCallback d = new SetTextCallback(SetStatusText);
        this.Invoke(d, new object[] { text });
      }
      else
      {
        this.status_value.Text = text;
      }
    }

    private void SetSSIDText(string text)
    {
      if (this.ssid_value.InvokeRequired)
      {
        SetTextCallback d = new SetTextCallback(SetSSIDText);
        this.Invoke(d, new object[] { text });
      }
      else
      {
        this.ssid_value.Text = text;
      }
    }
  }
}
