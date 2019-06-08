using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows;

namespace VPNSetup
{
  public class ProcessCmd
  {
    Process process;
    private static string result; 

    public ProcessCmd(string arguments)
    {
      process = new Process();
      ProcessStartInfo startInfo = new ProcessStartInfo();
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      startInfo.FileName = "cmd.exe";
      startInfo.Arguments = "/c " + arguments;
      startInfo.RedirectStandardError = true;
      startInfo.RedirectStandardOutput = true;
      startInfo.UseShellExecute = false;
      startInfo.CreateNoWindow = true;
      startInfo.Verb = "runas";
      process.StartInfo = startInfo;
    }

    public static string GetOutput(string arguments)
    {
      var processCmd =  new ProcessCmd(arguments);
      processCmd.Start();
      return GetOutput();
    }

    private static string GetOutput()
    {
      return result;
    }

    public void Start()
    {
      process.Start();
      var success = process.StandardOutput.ReadToEnd();
      var error = process.StandardError.ReadToEnd();

      if(!String.IsNullOrEmpty(error))
      {
        MessageBox.Show(error, "Error");
      }

      result = success;
      Trim(result);

      process.WaitForExit();
      process.Close();
    }

    private void Trim(string result)
    {
      result.Trim().Replace("\r", string.Empty);
      result.Trim().Replace("\n", string.Empty);
    }
  }
}