using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace VPNSetup
{
  class InternetAccess
  {
    public static void GetIpAddresses()
    {
      foreach (var nic in NetworkAdapters.GetIPv4EthernetAndWirelessInterfaces())
      {
          if(nic.Description == "Microsoft Hosted Network Virtual Adapter")
          {
            Console.WriteLine(nic.Description);
            foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
            {
              if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
              {
                Console.WriteLine(ip.Address.ToString());
              }
            }
          }
      }
    }
  }
}
