using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPNSetup
{
  class TroubleShoot
  {

    public static void RunTroubleShoot()
    {
      ResetDns();
      RestartServices();
      RestartHostedNetwork();
    }

    public static void ResetDns()
    {
      var output = String.Empty;

      var winsock_reset = Command.WinsockReset();
      output = ProcessCmd.GetOutput(winsock_reset);
      //TODO log
      Console.WriteLine("Winsock reset: " + output);

      var ip_reset = Command.IpReset();
      output = ProcessCmd.GetOutput(ip_reset);
      //TODO log
      Console.WriteLine("IP reset: " + output);

      var ip_release = Command.IpRelease();
      output = ProcessCmd.GetOutput(ip_release);
      //TODO log
      Console.WriteLine("IP release: " + output);

      var ip_renew = Command.IpRenew();
      output = ProcessCmd.GetOutput(ip_renew);
      //TODO log
      Console.WriteLine("IP renew: " + output);

      var flush_dns = Command.FlushDns();
      output = ProcessCmd.GetOutput(flush_dns);
      //TODO log
      Console.WriteLine("Flush DNS: " + output);
    }

    public static void RestartServices()
    {
      var output = Services.RestartServices();
      //TODO log
      Console.WriteLine("Services are running: " + output);
    }

    public static void RestartHostedNetwork()
    {
      var output = String.Empty;

      output = HostedNetwork.Stop();
      //TODO log
      Console.WriteLine(output);

      output = HostedNetwork.Start();
      //TODO log
      Console.WriteLine(output);
    }
  }
}
