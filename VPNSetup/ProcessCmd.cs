using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;

namespace VPNSetup
{
  public class ProcessCmd
  {
    Process process;

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
      Console.WriteLine("Arguments: " + startInfo.Arguments);
      startInfo.Verb = "runas";
      process.StartInfo = startInfo;
      process.ErrorDataReceived += cmd_Error;
      process.OutputDataReceived += cmd_DataReceived;
    }

    public void start()
    {
      process.Start();
      process.BeginErrorReadLine();
      process.BeginOutputReadLine();
    }

    static void cmd_DataReceived(object sender, DataReceivedEventArgs e)
    {
      Console.WriteLine(e.Data);
    }

    static void cmd_Error(object sender, DataReceivedEventArgs e)
    {
      Console.WriteLine(e.Data);
    }
  }
}
