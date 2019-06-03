using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Management;

namespace VPNSetup
{
  struct AdpaterGuid
  {
    public string expressVpnGuid;
    public string virtualNetworkGuid;
  }

  class NetworkAdapters
  {
    public static IEnumerable<NetworkInterface> GetIPv4EthernetAndWirelessInterfaces()
    {
      return
          from nic in NetworkInterface.GetAllNetworkInterfaces()
          where nic.Supports(NetworkInterfaceComponent.IPv4)
          where (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
             || (nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
          select nic;
    }

    public static AdpaterGuid setAdapters()
    {
      AdpaterGuid adapterGuid = new AdpaterGuid();
      foreach (var nic in GetIPv4EthernetAndWirelessInterfaces())
      {
        if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
         nic.Description == "ExpressVPN Tap Adapter")
        {
          adapterGuid.expressVpnGuid = nic.Id;
        }

        if(nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
         nic.Description == "Microsoft Hosted Network Virtual Adapter")
        {
          adapterGuid.virtualNetworkGuid = nic.Id;
        }
      }
      return adapterGuid;
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
        expressAdapter =  "\"" + expressAdapter + "\"";
        var enable_adapter = cmd.enableAdapter(expressAdapter);
        process = new ProcessCmd(enable_adapter);
        process.start();
      }
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

      var id_share = adapters.expressVpnGuid;
      setPropertiesTrue("IsIcsPublic", id_share);

      var id_home = adapters.virtualNetworkGuid;
      setPropertiesTrue("IsIcsPrivate", id_home);
    }

    public static void disableNetShare()
    {
      var adapters = setAdapters();

      var id_share = adapters.expressVpnGuid;
      setPropertiesFalse("IsIcsPublic", id_share);

      var id_home = adapters.virtualNetworkGuid;
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
  }
}
