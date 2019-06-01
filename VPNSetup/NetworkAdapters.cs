using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Management;
using System.Windows.Forms;
using NETCONLib;

namespace VPNSetup
{
  class NetworkAdapters
  {
    public static NetShare setAdapters()
    {
      INetConnection shareConnection = null;
      INetConnection homeConnection = null;


      foreach (var nic in IcsManager.GetIPv4EthernetAndWirelessInterfaces())
      {

        if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
         nic.Description == "ExpressVPN Tap Adapter")
        {
          shareConnection = IcsManager.FindConnectionByIdOrName(nic.Id);
        }

        if(nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
         nic.Description == "Microsoft Hosted Network Virtual Adapter")
        {
          homeConnection = IcsManager.FindConnectionByIdOrName(nic.Id);
        }

      }
      return new NetShare(shareConnection, homeConnection);
    }

    public static void disableSharing()
    {
      var adapters = setAdapters();
      IcsManager.disableSharing(adapters.SharedConnection, adapters.HomeConnection);
    }

    public static void enableExpressVPNAdapter()
    {
      var expressAdapter = getExpressVPNAdapter();
      command cmd = new command();
      var show_adapters = cmd.showAdapters();
      ProcessCmd process = new ProcessCmd(show_adapters);
      process.start();
      var show_output = process.getOutput();
      if(String.IsNullOrEmpty(show_output))
      {
        return;
      }
      string status = String.Empty;
      string[] lines = show_output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        if (line != null && line.Contains(expressAdapter))
        {
          var result_str = line.ToString();
          status = line.Split()[0];
          break;
        }
      }

      if( status == "Disabled")
      {
        Console.WriteLine("Enabling..." + expressAdapter);
        expressAdapter =  "\"" + expressAdapter + "\"";
        var enable_adapter = cmd.enableAdapter(expressAdapter);
        process = new ProcessCmd(enable_adapter);
        process.start();
      }
    }

    public static void enableSharing()
    {
      var adapters = setAdapters();

      var connectionToShare = adapters.SharedConnection;
      var homeConnection = adapters.HomeConnection;

      if (!adapters.Exists)
      {
        MessageBox.Show("Adapters are not configured properly", "Error");
        return;
      }

      IcsManager.ShareConnection(connectionToShare, homeConnection);
    }

    public static string getExpressVPNAdapter()
    {
      string adpater_name = String.Empty;

      SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
      ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
      foreach (ManagementObject item in searchProcedure.Get())
      {
        if (((string)item["Name"]) == "ExpressVPN Tap Adapter")
        {
          Console.WriteLine("NetConnectionID : {0}", item["NetConnectionID"]);
          adpater_name = (item["NetConnectionID"]).ToString();
          break;
        }
      }
      return adpater_name;
    }
  }
}
