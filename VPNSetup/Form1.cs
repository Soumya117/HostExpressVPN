using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
    wait wait_dialog;
    public Form1()
    {
      InitializeComponent();
      aTimer = new System.Timers.Timer(10000);
      aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
      aTimer.Interval = 2000;
      aTimer.Enabled = true;
      this.ssid_value.Text = "Checking...";
      this.status_value.Text = "Checking...";
      status();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      new createNetwork().ShowDialog(this);
    }

    private string extract_hostedNetwork(string result)
    {
      if(String.IsNullOrEmpty(result))
      {
        return null;
      }
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

    private string start_hostedNertwork()
    {
      var start_cmd = cmd.startHostedNetwork();
      processCmd = new ProcessCmd(start_cmd);
      processCmd.start();
      var connect_output = processCmd.getOutput();
      return connect_output;
    }

    private void display_result(string result)
    {
      if (result != null)
      {
        MessageBox.Show(result, "Status");
      }
    }
    private void StartWork()
    {
      wait_dialog = new wait();
      wait_dialog.Show();
      BackgroundWorker worker = new BackgroundWorker();
      worker.DoWork += connectToNetwork;
      worker.RunWorkerCompleted += WorkerCompleted;
      worker.RunWorkerAsync();
    }

    private void connectToNetwork(object sender, DoWorkEventArgs e)
    {
      connectHostedNetwork();
    }

    private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      wait_dialog.Close();
    }

    private void connectHostedNetwork()
    {
      if (this.status_value.Text == "Started")
      {
        MessageBox.Show("Hosted network already started", "Info");
        return;
      }

      NetworkAdapters.enableExpressVPNAdapter();

      var show_cmd = cmd.viewHostedNetwork();
      processCmd = new ProcessCmd(show_cmd);
      processCmd.start();
      var result = processCmd.getOutput();
      if (String.IsNullOrEmpty(result))
      {
        return;
      }
      extract_hostedNetwork(result);
      var connect_output = start_hostedNertwork();
      NetworkAdapters.disableNetShare();
      NetworkAdapters.enableNetShare();
      display_result(connect_output);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      StartWork();
    }

    private void StopWork()
    {
      wait_dialog = new wait();
      wait_dialog.Show();
      BackgroundWorker worker = new BackgroundWorker();
      worker.DoWork += stopTheNetwork;
      worker.RunWorkerCompleted += WorkerCompleted;
      worker.RunWorkerAsync();
    }

    private void stopTheNetwork(object sender, DoWorkEventArgs e)
    {
      if (this.status_value.Text == "Not started")
      {
        MessageBox.Show("Hosted network not running..", "Info");
        return;
      }

      var stop_cmd = cmd.stopHostedNetwork();
      processCmd = new ProcessCmd(stop_cmd);
      processCmd.start();
      var stop_output = processCmd.getOutput();
      display_result(stop_output);
    }

    private void button3_Click(object sender, EventArgs e)
    {
      StopWork();
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      var show_pwd = cmd.showPassword();
      processCmd = new ProcessCmd(show_pwd);
      processCmd.start();
      var show_output = processCmd.getOutput();
      show_password(show_output);
    }

    private void show_password(string result)
    {
      if(String.IsNullOrEmpty(result))
      {
        return;
      }

      string[] lines = result.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
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
      string password = Interaction.InputBox(
      "Enter new password", 
      "Input", 
      "",
      -1, 
      -1);
 
      if (password.Length > 0)
      {
        if (password.Length < 8)
        {
          MessageBox.Show("Password must be atleast 8 characters.", "Info");
          return;
        }

        var show_cmd = cmd.viewHostedNetwork();
        processCmd = new ProcessCmd(show_cmd);
        processCmd.start();
        var view_output = processCmd.getOutput();
        var hostname = extract_hostedNetwork(view_output);
        var create_cmd = cmd.createHostedNetwork(hostname, password);
        processCmd = new ProcessCmd(create_cmd);
        processCmd.start();
        var create_output = processCmd.getOutput();
        show_passwordChangeSuccess(create_output);
      }
      else
        return;
    }

    private void show_passwordChangeSuccess(string result)
    {
      if (!String.IsNullOrEmpty(result))
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
      Application.Exit();
    }

    private void setInternetConnectionStatus()
    {
      if (InternetInfo.IsInternetAvailable && InternetAccess.pingLocalNetwork())
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
      if(String.IsNullOrEmpty(data))
      {
        return null;
      }
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

    private string extract_clients(string data)
    {
      if (String.IsNullOrEmpty(data))
        return null;
      var clients = String.Empty;
      string[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        if (line != null && line.Contains("Number of clients"))
        {
          var result_str = line.ToString();
          clients = line.Split(':')[1];
          break;
        }
        else
        {
          clients = "0";
        }
      }
      return clients;
    }

    private void status()
    {
      setInternetConnectionStatus();
      var show_cmd = cmd.viewHostedNetwork();
      processCmd = new ProcessCmd(show_cmd);
      processCmd.start();
      var view_output = processCmd.getOutput();
      if(String.IsNullOrEmpty(view_output))
      {
        return;
      }
      var ssid = extract_hostedNetwork(view_output);
      var status = extract_status(view_output);
      var clients = extract_clients(view_output);

      SetStatusText(status.Trim());
      SetSSIDText(ssid.Trim());
      SetClientText(clients.Trim());
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
      status();
    }

    delegate void SetTextCallback(string text);

    private void SetStatusText(string text)
    {
      if(String.IsNullOrEmpty(text))
      {
        return;
      }

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
      if (String.IsNullOrEmpty(text))
      {
        return;
      }

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
    private void SetClientText(string text)
    {
      if (String.IsNullOrEmpty(text))
      {
        return;
      }

      if (this.client_value.InvokeRequired)
      {
        SetTextCallback d = new SetTextCallback(SetClientText);
        this.Invoke(d, new object[] { text });
      }
      else
      {
        this.client_value.Text = text;
      }
    }

    private void pictureBox2_Click_1(object sender, EventArgs e)
    {
      this.Hide();
      new EntryForm().Show();
    }

    private void toolTip1_Popup(object sender, PopupEventArgs e)
    {

    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {

    }

    private void connection_label_Click(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void status_label_Click(object sender, EventArgs e)
    {

    }

    private void ssid_label_Click(object sender, EventArgs e)
    {

    }

    private void status_pictureBox_Click(object sender, EventArgs e)
    {

    }

    private void status_value_Click(object sender, EventArgs e)
    {

    }

    private void ssid_value_Click(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void client_value_Click(object sender, EventArgs e)
    {

    }
  }
}
