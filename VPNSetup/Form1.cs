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
using System.Timers;

namespace VPNSetup
{
  public partial class Form1 : Form
  {
    private static System.Timers.Timer aTimer;

    Wait wait_dialog;
    public Form1()
    {
      InitializeComponent();
      aTimer = new System.Timers.Timer(10000);
      aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
      aTimer.Interval = 2000;
      aTimer.Enabled = true;
      this.ssid_value.Text = "Checking...";
      this.status_value.Text = "Checking...";
      Status();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      new createNetwork().ShowDialog(this);
    }

    private void DisplayOutput(string result, string failure)
    {
      if (result != null)
      {
        MessageBox.Show(result, "Status");
      }
      else
      {
        //TODO log
        Console.WriteLine(failure);
      }
    }

    private void StartWork()
    {
      wait_dialog = new Wait();
      wait_dialog.Show();
      BackgroundWorker worker = new BackgroundWorker();
      worker.DoWork += connectToNetwork;
      worker.RunWorkerCompleted += WorkerCompleted;
      worker.RunWorkerAsync();
    }

    private void connectToNetwork(object sender, DoWorkEventArgs e)
    {
      if (this.status_value.Text == "Started")
      {
        MessageBox.Show("Hosted network already started", "Info");
        return;
      }
      var connect_output = HostedNetwork.Connect();
      DisplayOutput(connect_output, "Returned empty while creating hosted network");
    }

    private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      wait_dialog.Close();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      StartWork();
    }

    private void StopWork()
    {
      wait_dialog = new Wait();
      wait_dialog.Show();
      BackgroundWorker worker = new BackgroundWorker();
      worker.DoWork += StopTheNetwork;
      worker.RunWorkerCompleted += WorkerCompleted;
      worker.RunWorkerAsync();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      StopWork();
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      var key = HostedNetwork.ShowPassword();
      MessageBox.Show(key, "Password");
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      var output = HostedNetwork.ChangePassword();
      show_passwordChangeSuccess(output, "Error while changing password");
    }

    private void show_passwordChangeSuccess(string result, string failure)
    {
      if (!String.IsNullOrEmpty(result))
      {
        MessageBox.Show("Password Changed successfully");
      }
      else
      {
        Console.WriteLine(failure);
      }
    }

    private void pictureBox4_Click(object sender, EventArgs e)
    {
      Application.Exit();
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

    private void validate(string data)
    {
      if (String.IsNullOrEmpty(data))
      {  
         //TODO log
         Console.WriteLine(data + " is not available for the moment");
      }
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
      Status();
    }

    delegate void SetTextCallback(string text);

    private void SetStatusText(string text)
    {
      if (String.IsNullOrEmpty(text))
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

    private void StopTheNetwork(object sender, DoWorkEventArgs e)
    {
      if (this.status_value.Text == "Not started")
      {
        MessageBox.Show("Hosted network not running..", "Info");
        return;
      }
      var output = HostedNetwork.Stop();
      DisplayOutput(output, "Returned null while stopping hosted network");
    }

    private void Status()
    {
      var view_output = HostedNetwork.ViewHostedNetwork();
      var ssid = HostedNetwork.ExtractHostedNetwork(view_output);
      var status = HostedNetwork.ExtractStatus(view_output);
      var clients = HostedNetwork.ExtractClients(view_output);
      if (String.IsNullOrEmpty(clients))
      {
        clients = "0";
      }
      setInternetConnectionStatus();
      SetStatusText(status.Trim());
      SetSSIDText(ssid.Trim());
      SetClientText(clients.Trim());
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
  }
}
