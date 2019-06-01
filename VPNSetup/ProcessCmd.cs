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
    public string result; 

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

    public string getOutput()
    {
      return result;
    }

    public void start()
    {
      process.Start();
      var success = process.StandardOutput.ReadToEnd();
      var error = process.StandardError.ReadToEnd();

      if(!String.IsNullOrEmpty(error))
      {
        MessageBox.Show(error, "Error");
      }

      result = success;
      trim_output(result);

      process.WaitForExit();
    }

    private void trim_output(string result)
    {
      result.Trim().Replace("\r", string.Empty);
      result.Trim().Replace("\n", string.Empty);
    }
  }
}