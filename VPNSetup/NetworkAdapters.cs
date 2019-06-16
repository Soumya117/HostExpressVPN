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
      try
      {
        foreach (var nic in GetIPv4EthernetAndWirelessInterfaces())
        {
          if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
           Util.Equals(nic.Description, "ExpressVPN Tap Adapter"))
          {
            adapterGuid.expressVpnGuid = nic.Id;
          }

          if (nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
           Util.Equals(nic.Description, "Microsoft Hosted Network Virtual Adapter"))
          {
            adapterGuid.virtualNetworkGuid = nic.Id;
          }
        }
      }
      catch (Exception e)
      {
        //TODO log
        Console.WriteLine("Error while retreiving interfaces: " + e.ToString());
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
      string status = Util.ParseCmd(show_output, 0, expressAdapter,' ');
      if( Util.Equals(status, "Disabled"))
      {
        expressAdapter =  "\"" + expressAdapter + "\"";
        var enable_adapter = Command.EnableAdapter(expressAdapter);
        ProcessCmd.GetOutput(enable_adapter);
      }
    }

    public static string GetExpressVPNAdapter()
    {
      string adpater_name = String.Empty;
      try
      {
        SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionId != NULL");
        ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
        foreach (ManagementObject item in searchProcedure.Get())
        {
          if (Util.Equals((string)item["Name"], "ExpressVPN Tap Adapter"))
          {
            adpater_name = (item["NetConnectionID"]).ToString();
            break;
          }
        }
      }
      catch (Exception e)
      {
        //TODO log
        Console.WriteLine("Error while retrieving expressvpn adapter name: " + e.ToString());
      }
      return adpater_name;
    }

    public static void EnableNetShare()
    {
      var adapters = SetAdapters();

      var id_share = adapters.expressVpnGuid;
      SetProperties("IsIcsPublic", id_share, true);

      var id_home = adapters.virtualNetworkGuid;
      SetProperties("IsIcsPrivate", id_home, true);
    }

    public static void DisableNetShare()
    {
      var adapters = SetAdapters();

      var id_share = adapters.expressVpnGuid;
      SetProperties("IsIcsPublic", id_share, false);

      var id_home = adapters.virtualNetworkGuid;
      SetProperties("IsIcsPrivate", id_home, false);
    }

    public static void SetProperties(string flag, string guid, bool value)
    {
      try
      {
        ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\Microsoft\\HomeNet");

        ObjectQuery query = new ObjectQuery("SELECT * FROM HNet_ConnectionProperties ");
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
        ManagementObjectCollection queryCollection = searcher.Get();

        foreach (ManagementObject m in queryCollection)
        {
          var conn_uid = m["Connection"].ToString();
          conn_uid = string.Join(" ", conn_uid.Split('"').Where((x, i) => i % 2 != 0));
          if (Util.Equals(conn_uid, guid))
          {
            try
            {
              PropertyDataCollection properties = m.Properties;
              foreach (PropertyData prop in properties)
              {
                if (prop.Name == flag && ((Boolean)prop.Value) == !value)
                {
                  prop.Value = value;
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
      catch (Exception e)
      {
        //TODO log
        Console.WriteLine("Error while setting property " + e.ToString());
        Console.WriteLine("GUID: " + guid + " Flag : " + flag + " Value: " + value);
      }
    }
  }
}
