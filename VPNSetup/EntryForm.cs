using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPNSetup
{
  public partial class EntryForm : Form
  {
    Wait wait_dialog;
    CheckAvailability check = new CheckAvailability();

    public EntryForm()
    {
      InitializeComponent();
    }

    private void pictureBox4_Click(object sender, EventArgs e)
    {
      Application.Exit();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      new Form1().Show();
    }

    private void StartWork()
    {
      wait_dialog = new Wait();
      BeginInvoke(new MethodInvoker(() =>
      {
        wait_dialog.ShowDialog(this);
      }));
      BackgroundWorker worker = new BackgroundWorker();
      worker.DoWork += DoWork;
      worker.RunWorkerCompleted += WorkerCompleted;
      worker.RunWorkerAsync();
    }

    private void DoWork(object sender, DoWorkEventArgs e)
    {
      check.RunSetup();
    }

    private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      wait_dialog.Close();
      check.ShowDialog(this);
    }

    private void label1_Click(object sender, EventArgs e)
    {
      StartWork();
    }

    private void StartCheck()
    {
      wait_dialog = new Wait();
      BeginInvoke(new MethodInvoker(() =>
      {
        wait_dialog.ShowDialog(this);
      }));
      BackgroundWorker worker = new BackgroundWorker();
      worker.DoWork += DoCheck;
      worker.RunWorkerCompleted += CheckCompleted;
      worker.RunWorkerAsync();
    }

    private void DoCheck(object sender, DoWorkEventArgs e)
    {
      var check = new CheckAvailability();
      check.CheckHostedNetwork();
    }

    private void CheckCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (CheckAvailability.isHostedNetworkSupported)
      {
        wait_dialog.Close();
        new Form1().Show();
        this.Hide();
      }
      else
      {
        MessageBox.Show("Hosted Network is not supported. \nPlease run initial setup.\nSee if Microsoft Hosted Network is supported.", "Info");
      }
    }

    private void label2_Click(object sender, EventArgs e)
    {
      StartCheck();
    }
  }
}
