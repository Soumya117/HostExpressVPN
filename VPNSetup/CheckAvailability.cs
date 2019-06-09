using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace VPNSetup
{
  public partial class CheckAvailability : Form
  {
    public static bool isHostedNetworkSupported = false;

    public CheckAvailability()
    {
      InitializeComponent();
    }

    public void CheckInternetConnection()
    {
      label10.Visible = true;
      label10.Text = "Checking...";
      if (InternetInfo.IsInternetAvailable)
      {
        label10.Text = "Available";
      }
      else
        label10.Text = "Unavailable";
    }

    public void CheckHostedNetwork()
    {
      label11.Visible = true;
      label11.Text = "Checking...";
      var show_drivers = Command.ShowDrivers();
      var show_output = ProcessCmd.GetOutput(show_drivers);
      if(String.IsNullOrEmpty(show_output))
      {
        return;
      }
      string status = String.Empty;
      string[] lines = show_output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        if (line != null && line.Contains("Hosted network supported"))
        {
          var result_str = line.ToString();
          status = line.Split(':')[1];
          status = status.Trim();
          break;
        }
      }
      if (String.IsNullOrEmpty(status))
        label11.Text = "Unknown";
      if (status == "No")
        label11.Text = "Not Supported";
      else if (status == "Yes")
      {
        label11.Text = "Supported";
        isHostedNetworkSupported = true;
      }
    }

    public void CheckEpressVPNAdapter()
    {
      label12.Visible = true;
      label12.Text = "Checking...";
      NetworkAdapters.EnableExpressVPNAdapter();
      var adapters = NetworkAdapters.SetAdapters();

      if (adapters.expressVpnGuid != null)
      {
        label12.Text = "Available";
      }
      else
        label12.Text = "Unavailable";
    }

    public void CheckServices()
    {
      label13.Visible = true;
      label13.Text = "Checking...";
      var status = Services.StartServices();
      if (status)
      {
        label13.Text = "Running";
      }
      else
        label13.Text = "Not Running";
    }

    private void pictureBox4_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    public void RunSetup()
    {
      try
      {
        CheckInternetConnection();
        CheckHostedNetwork();
        CheckEpressVPNAdapter();
        CheckServices();
      }
      catch (Exception e)
      {
        //TODO log
        Console.WriteLine("Error while running setup: " + e.ToString());
      }
    }
  }
}
