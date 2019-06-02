using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

namespace VPNSetup
{
  public partial class createNetwork : Form
  {
    command cmd = new command();
    ProcessCmd processCmd;
    wait wait_dialog;
    bool success = false;

    public createNetwork()
    {
      InitializeComponent();
    }

    private void StartWork()
    {
      wait_dialog = new wait();
      wait_dialog.Show();
      BackgroundWorker worker = new BackgroundWorker();
      worker.DoWork += DoWork;
      worker.RunWorkerCompleted += WorkerCompleted;
      worker.RunWorkerAsync();
    }

    private void DoWork(object sender, DoWorkEventArgs e)
    {
      create();
    }

    private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      wait_dialog.Close();
      if(success)
      {
        this.Hide();
      }
    }

    public void create_hostedNetwork(string result)
    {
      if(!String.IsNullOrEmpty(result))
      {
        var message = MessageBox.Show(result, "Status", MessageBoxButtons.OK);
        if (message == DialogResult.OK)
        {
          success = true;         
        }
      }
    }

    private void create()
    {
      var create_cmd = cmd.createHostedNetwork(textBox1.Text, textBox2.Text);
      processCmd = new ProcessCmd(create_cmd);
      processCmd.start();
      var create_output = processCmd.getOutput();
      create_hostedNetwork(create_output);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      StartWork();
    }

    private void pictureBox4_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}