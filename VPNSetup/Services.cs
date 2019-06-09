using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Management;

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

    public static ServiceController Start(string service_name)
    {
      var serviceController = GetService(service_name);
      if(serviceController == null)
      {
        MessageBox.Show("Service {0} is not running.");
        return null;
      }
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

    public static bool StartServices()
    {
       var remoteController = Start("RemoteAccess");
       ChangeStartup("RemoteAccess");

       var sharedController = Start("SharedAccess");
       ChangeStartup("SharedAccess");

      if (remoteController == null || sharedController == null)
      {
        return false;
      }

       return remoteController.Status == ServiceControllerStatus.Running 
        && sharedController.Status == ServiceControllerStatus.Running;
    }

    public static void ChangeStartup(string service_name)
    {
      var remoteStartup = StartupType(service_name);
      if(!String.IsNullOrEmpty(remoteStartup) && remoteStartup != "Auto")
      {
        ConfigureStartup(service_name);
      }
    }

    public static string StartupType(string serviceName)
    {
      string startupType = String.Empty;

        if (serviceName != null)
        {
          string path = "Win32_Service.Name='" + serviceName + "'";
          ManagementPath p = new ManagementPath(path);
          ManagementObject ManagementObj = new ManagementObject(p);
          startupType =  ManagementObj["StartMode"].ToString();
        }
      return startupType;
    }

    public static void ConfigureStartup(string serviceName)
    {
      var value = "Automatic";
      if (serviceName != null)
      {
        string path = "Win32_Service.Name='" + serviceName + "'";
        ManagementPath p = new ManagementPath(path);
        ManagementObject ManagementObj = new ManagementObject(p);
        object[] parameters = new object[1];
        parameters[0] = value;
        ManagementObj.InvokeMethod("ChangeStartMode", parameters);
      }
    }
  }
}
