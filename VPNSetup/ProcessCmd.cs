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

  public struct Output
  {
    public string output;
    public string error;
  }

  public class ProcessCmd
  {
    Process process;
    public Output result; 

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

    public Output getOutput()
    {
      return result;
    }

    public void start()
    {
      process.Start();
      var output = process.StandardOutput.ReadToEnd();
      output.Trim().Replace("\r", string.Empty);
      output.Trim().Replace("\n", string.Empty);
      result.output = output;
      result.error = process.StandardError.ReadToEnd();
      process.WaitForExit();
    }
  }
}