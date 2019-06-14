using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace VPNSetup
{
  class HostedNetwork
  {
    public static string ExtractHostedNetwork(string input)
    {
      var hostedNetwork = Util.ParseCmd(input, 1, "SSID", ':');
      var ssid = string.Join(" ", hostedNetwork.Split('"').Where((x, i) => i % 2 != 0));
      return ssid;
    }

    public static string Start()
    {
      var start_cmd = Command.StartHostedNetwork();
      var connect_output = ProcessCmd.GetOutput(start_cmd);
      return connect_output;
    }

    public static string Stop()
    {
      var stop_cmd = Command.StopHostedNetwork();
      var stop_output = ProcessCmd.GetOutput(stop_cmd);
      return stop_output;
    }

    public static string Connect()
    {
      NetworkAdapters.EnableExpressVPNAdapter();
      var view_output = ViewHostedNetwork();
      if (String.IsNullOrEmpty(view_output))
      {
        return null;
      }
      var connect_output = Start();
      NetworkAdapters.DisableNetShare();
      NetworkAdapters.EnableNetShare();
      return connect_output;
    }

    public static string Create(string ssid, string key)
    {
      var create_cmd = Command.CreateHostedNetwork(ssid, key);
      var create_output = ProcessCmd.GetOutput(create_cmd);
      return create_output;
    }

    public static string ShowPassword()
    {
      var show_pwd = Command.ShowPassword();
      var input = ProcessCmd.GetOutput(show_pwd);
      if (String.IsNullOrEmpty(input))
      {
        //TODO log
        Console.WriteLine("Result is empty while querying password");
      }
      var key = Util.ParseCmd(input, 1, "User security key", ':');
      return key;
    }

    public static string ChangePassword()
    {
      var output = String.Empty;
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
          return null;
        }

        var view_output = ViewHostedNetwork();
        var hostname = ExtractHostedNetwork(view_output);
        var create_cmd = Command.CreateHostedNetwork(hostname, password);
        output = ProcessCmd.GetOutput(create_cmd);
      }
      else
        return null;

      return output;
    }
    public static string ExtractStatus(string input)
    {
      string status = Util.ParseCmd(input, 1, "Status", ':');
      return status;
    }

    public static string ExtractClients(string input)
    {
      var clients = Util.ParseCmd(input, 1, "Number of clients", ':');
      return clients;
    }

    public static string ViewHostedNetwork()
    {
      var show_cmd = Command.ViewHostedNetwork();
      var view_output = ProcessCmd.GetOutput(show_cmd);
      if (String.IsNullOrEmpty(view_output))
      {
        //TODO log
        Console.WriteLine("View hosted network returned empty..");
      }
      return view_output;
    }
  }
}
