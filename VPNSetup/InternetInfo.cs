using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace VPNSetup
{
  class InternetInfo
  {
    public static bool IsInternetAvailable
    {
      get { return NetworkInterface.GetIsNetworkAvailable(); }
    }

  }
}