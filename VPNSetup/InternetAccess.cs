using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace VPNSetup
{
  class InternetAccess
  {
    private static string GetIpAddress()
    {
      string ipAddress = String.Empty;
      foreach (var nic in NetworkAdapters.GetIPv4EthernetAndWirelessInterfaces())
      {
          if(nic.Description == "Microsoft Hosted Network Virtual Adapter")
          {
            Console.WriteLine(nic.Description);
            foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
            {
              if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
              {
                ipAddress = ip.Address.ToString();
              }
            }
          }
      }
      return ipAddress;
    }

    public static bool pingLocalNetwork()
    {
      bool success = false;
      try
      {
        var ping = new Ping();
        var pingReply = ping.Send(GetIpAddress());
        if (pingReply != null)
        {
          Console.WriteLine("Status: " + pingReply.Status
          + "\n Time: " + pingReply.RoundtripTime
          + "\n Address: " + pingReply.Address);
          success = true;
        }
      }
      catch
      {
        MessageBox.Show("Some issue while pinging...!", "Error");
      }
      return success;
    }
  }
}
