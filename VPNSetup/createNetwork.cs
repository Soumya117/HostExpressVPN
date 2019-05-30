using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace VPNSetup
{
  public partial class createNetwork : Form
  {
    command cmd = new command();
    ProcessCmd processCmd;

    public createNetwork()
    {
      InitializeComponent();
    }

    public void create_hostedNetwork(Output result)
    {
      if(!String.IsNullOrEmpty(result.output))
      {
        var message = MessageBox.Show(result.output, "Status", MessageBoxButtons.OK);
        if (message == DialogResult.OK)
        {
          this.Hide();
        }
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      var create_cmd = cmd.createHostedNetwork(textBox1.Text, textBox2.Text);
      processCmd = new ProcessCmd(create_cmd);
      processCmd.start();
      var create_output = processCmd.getOutput();
      create_hostedNetwork(create_output);
    }

    private void pictureBox4_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}