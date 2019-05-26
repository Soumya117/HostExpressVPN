using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VPNSetup
{
  public partial class Form1 : Form
  {

    command cmd = new command();
    ProcessCmd processCmd;

    public Form1()
    {
      InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      new createNetwork().Show();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var show_cmd = cmd.viewHostedNetwork();
      processCmd = new ProcessCmd(show_cmd);
      processCmd.start();

      //parse cmd result and get hosted network.
      //then call start command. 
      //var parseResult = "";
      //var start_cmd = cmd.startHostedNetwork(parseResult);
      //processCmd = new ProcessCmd(start_cmd);
      //var process = processCmd.getProcess();
      //process.Start();
      //while (!process.StandardOutput.EndOfStream)
      //{
      //  string line = process.StandardOutput.ReadLine();
      //  Console.WriteLine("Show output: ", line);
      //}
    }

    private void button3_Click(object sender, EventArgs e)
    {
      var stop_cmd = cmd.stopHostedNetwork();
      processCmd = new ProcessCmd(stop_cmd);
      processCmd.start();
    }
  }
}
