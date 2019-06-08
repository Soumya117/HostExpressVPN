using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.ComponentModel;

namespace VPNSetup
{
  public partial class createNetwork : Form
  {
    Command cmd = new Command();
    ProcessCmd processCmd;
    Wait wait_dialog;
    bool success = false;

    public createNetwork()
    {
      InitializeComponent();
    }

    private void StartWork()
    {
      wait_dialog = new Wait();
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
      var create_cmd = Command.createHostedNetwork(textBox1.Text, textBox2.Text);
      var create_output = ProcessCmd.get_Output(create_cmd);
      create_hostedNetwork(create_output);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if(String.IsNullOrEmpty(this.textBox1.Text)
       || String.IsNullOrEmpty(this.textBox2.Text))
      {
        MessageBox.Show("Both host name and password is required!", "Info");
        return;
      }
      if (textBox2.Text.Length < 8)
      {
        MessageBox.Show("Password must be atleast 8 characters.", "Info");
        return;
      }
      StartWork();
    }

    private void pictureBox4_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}