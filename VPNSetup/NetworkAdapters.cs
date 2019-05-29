using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
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
        if (nic.Description.Contains("Virtual"))
        {
          continue;
        }

        if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.Description != "ExpressVPN Tap Adapter")
        {
          continue;
        }

        if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.Description == "ExpressVPN Tap Adapter")
        {
          shareConnection = IcsManager.FindConnectionByIdOrName(nic.Id);
        }

        else
        {
          homeConnection = IcsManager.FindConnectionByIdOrName(nic.Id);
        }
      }
      return new NetShare(shareConnection, homeConnection);
    }

    public static void disable()
    {
      var adapters = setAdapters();
      IcsManager.disableSharing(adapters.SharedConnection, adapters.HomeConnection);
    }

    public static void enable()
    {
      var adapters = setAdapters();

      var connectionToShare = adapters.SharedConnection;
      var homeConnection = adapters.HomeConnection;

      if (!adapters.Exists)
      {
        MessageBox.Show("Error", "Adapters are not configured properly");
      }

      IcsManager.ShareConnection(connectionToShare, homeConnection);
    }
  }
}
