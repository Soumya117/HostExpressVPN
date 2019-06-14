using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNSetup
{
  class Command
  {
    public static string StopHostedNetwork()
    {
      var stop_cmd = "netsh wlan stop hostedNetwork";
      return stop_cmd;
    }

    public static string ViewHostedNetwork()
    {
      var show_cmd = "netsh wlan show hostedNetwork";
      return show_cmd;
    }

    public static string CreateHostedNetwork(string hostname, string password)
    {
      var create_cmd = "NETSH WLAN set hostednetwork mode = allow ssid = "+ hostname + " key = " + password;
      return create_cmd;
    }

    public static string StartHostedNetwork()
    {
      var start_cmd = "netsh wlan start hostedNetwork";
      return start_cmd;
    }

    public static string ShowPassword()
    {
      var show_pwd = "netsh wlan show hostedNetwork security";
      return show_pwd;
    }

    public static string ShowDrivers()
    {
      var show_drivers = "netsh wlan show drivers";
      return show_drivers;
    }

    public static string ShowAdapters()
    {
      var show_adapters = "netsh interface show interface";
      return show_adapters;
    }

    public static string EnableAdapter(string name)
    {
      var enable_adapter = "netsh interface set interface "+name+" enable";
      return enable_adapter;
    }

    public static string WinsockReset()
    {
      var winsock_reset = "netsh winsock reset";
      return winsock_reset;
    }

    public static string IpReset()
    {
      var ip_reset = "netsh int ip reset";
      return ip_reset;
    }

    public static string IpRelease()
    {
      var ip_release = "ipconfig /release";
      return ip_release;
    }

    public static string IpRenew()
    {
      var ip_renew = "ipconfig /renew";
      return ip_renew;

    }

    public static string FlushDns()
    {
      var flush_dns = "ipconfig /flushdns";
      return flush_dns;
    }
  }
}