using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNSetup
{
  class command
  {
    public string stopHostedNetwork()
    {
      var stop_cmd = "netsh wlan stop hostedNetwork";
      return stop_cmd;
    }

    public string viewHostedNetwork()
    {
      var show_cmd = "netsh wlan show hostedNetwork";
      return show_cmd;
    }

    public string createHostedNetwork(string hostname, string password)
    {
      var create_cmd = "NETSH WLAN set hostednetwork mode = allow ssid = "+ hostname + " key = " + password;
      return create_cmd;
    }

    public string startHostedNetwork()
    {
      var start_cmd = "netsh wlan start hostedNetwork";
      return start_cmd;
    }

    public string showPassword()
    {
      var show_pwd = "netsh wlan show hostedNetwork security";
      return show_pwd;
    }
  }
}
