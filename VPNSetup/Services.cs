using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ServiceProcess;
using System.Windows.Forms;

namespace VPNSetup
{
  class Services
  {
    public static ServiceController GetService(string service_name)
    {
      ServiceController[] scServices = ServiceController.GetServices();
      foreach (ServiceController scTemp in scServices)
      {
        if (scTemp.ServiceName == service_name)
        {
          return scTemp;
        }
      }
      return null;
    }

    public static ServiceController start(string service_name)
    {
      var serviceController = GetService(service_name);
      if(serviceController.Status != ServiceControllerStatus.Running)
      {
        serviceController.Start();
        while (serviceController.Status == ServiceControllerStatus.Stopped)
        {
          Thread.Sleep(1000);
          serviceController.Refresh();
        }
      }
      return serviceController;
    }

    public static bool startServices()
    {
     var remoteController = start("RemoteAccess");
     var sharedController = start("SharedAccess");

     return remoteController.Status == ServiceControllerStatus.Running 
        && sharedController.Status == ServiceControllerStatus.Running;
    }
  }
}
