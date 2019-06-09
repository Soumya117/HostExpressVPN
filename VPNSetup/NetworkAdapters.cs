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

    public static AdpaterGuid SetAdapters()
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

    public static void EnableExpressVPNAdapter()
    {
      var expressAdapter = GetExpressVPNAdapter();
      Command cmd = new Command();
      var show_adapters = Command.ShowAdapters();
      var show_output = ProcessCmd.GetOutput(show_adapters);
      if(String.IsNullOrEmpty(show_output))
      {
        return;
      }
      string status = Util.ParseCmd(show_output, 0, expressAdapter);
      if( status == "Disabled")
      {
        expressAdapter =  "\"" + expressAdapter + "\"";
        var enable_adapter = Command.EnableAdapter(expressAdapter);
        ProcessCmd.GetOutput(enable_adapter);
      }
    }

    public static string GetExpressVPNAdapter()
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

    public static void EnableNetShare()
    {
      var adapters = SetAdapters();

      var id_share = adapters.expressVpnGuid;
      SetPropertiesTrue("IsIcsPublic", id_share);

      var id_home = adapters.virtualNetworkGuid;
      SetPropertiesTrue("IsIcsPrivate", id_home);
    }

    public static void DisableNetShare()
    {
      var adapters = SetAdapters();

      var id_share = adapters.expressVpnGuid;
      SetPropertiesFalse("IsIcsPublic", id_share);

      var id_home = adapters.virtualNetworkGuid;
      SetPropertiesFalse("IsIcsPrivate", id_home);
    }

    public static void SetPropertiesFalse(string flag, string guid)
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

    public static void SetPropertiesTrue(string flag, string guid) 
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
