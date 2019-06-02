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
          adpater_name = (item["NetConnectionID"]).ToString();
          break;
        }
      }
      return adpater_name;
    }

    public static void enableNetShare()
    {
      var adapters = setAdapters();
      var id_share = IcsManager.GetProperties(adapters.SharedConnection).Guid;
      setPropertiesTrue("IsIcsPublic", id_share);

      var id_home = IcsManager.GetProperties(adapters.HomeConnection).Guid;
      setPropertiesTrue("IsIcsPrivate", id_home);
    }

    public static void disableNetShare()
    {
      var adapters = setAdapters();

      var id_share = IcsManager.GetProperties(adapters.SharedConnection).Guid;
      setPropertiesFalse("IsIcsPublic", id_share);

      var id_home = IcsManager.GetProperties(adapters.HomeConnection).Guid;
      setPropertiesFalse("IsIcsPrivate", id_home);
    }

    public static void setPropertiesFalse(string flag, string guid)
    {
      ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\Microsoft\\HomeNet");

      ObjectQuery query = new ObjectQuery("SELECT * FROM HNet_ConnectionProperties ");

      ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
      ManagementObjectCollection queryCollection = searcher.Get();

      foreach (ManagementObject m in queryCollection)
      {
        var conn_uid = m["Connection"].ToString();
        conn_uid = string.Join(" ", conn_uid.Split('"').Where((x, i) => i % 2 != 0));
        if (conn_uid == guid)
        {
          try
          {
            PropertyDataCollection properties = m.Properties;
            foreach (PropertyData prop in properties)
            {
              if (prop.Name == flag && ((Boolean)prop.Value) == true)
              {
                prop.Value = false;
                m.Put();
                break;
              }
            }
          }
          catch (Exception e)
          {
            Console.WriteLine("ex " + e.Message);
            continue;
          }
          break;
        }
      }
    }

    public static void setPropertiesTrue(string flag, string guid) 
    {
      ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\Microsoft\\HomeNet");

      ObjectQuery query = new ObjectQuery("SELECT * FROM HNet_ConnectionProperties ");

      ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
      ManagementObjectCollection queryCollection = searcher.Get();

      foreach (ManagementObject m in queryCollection)
      {
        var conn_uid = m["Connection"].ToString();
        conn_uid = string.Join(" ", conn_uid.Split('"').Where((x, i) => i % 2 != 0));
        if (conn_uid == guid)
        {
          try
          {
            PropertyDataCollection properties = m.Properties;
            foreach (PropertyData prop in properties)
            {
              if (prop.Name == flag && ((Boolean)prop.Value) == false)
              {
                prop.Value = true;
                m.Put();
                break;
              }
            }
          }
          catch (Exception e)
          {
            Console.WriteLine("ex " + e.Message);
            continue;
          }
          break;
        }
      }
    }

    public static void Disable_ICS_WMI()
    {
      ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\Microsoft\\HomeNet");

      ObjectQuery query = new ObjectQuery("SELECT * FROM HNet_ConnectionProperties ");

      ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
      ManagementObjectCollection queryCollection = searcher.Get();

      foreach (ManagementObject m in queryCollection)
      {
        try
        {
          PropertyDataCollection properties = m.Properties;
          foreach (PropertyData prop in properties)
          {
            if (prop.Name == "IsIcsPrivate" && ((Boolean)prop.Value) == true)
            {
              Console.WriteLine("Private Connection : {0}", m["Connection"]);
            }
            else if (prop.Name == "IsIcsPublic" && ((Boolean)prop.Value) == true)
            {
              Console.WriteLine("Public Connection : {0}", m["Connection"]);
            }
          }
        }
        catch (Exception e)
        {
          Console.WriteLine("ex " + e.Message);
          continue;
        }
      }
    }
  }
}
