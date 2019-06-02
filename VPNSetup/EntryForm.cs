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
    wait wait_dialog;
    CheckAvailability check = new CheckAvailability();
    public EntryForm()
    {
      InitializeComponent();
    }

    private void pictureBox4_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      new Form1().Show();
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
      check.runSetup();
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

    private void label2_Click(object sender, EventArgs e)
    {
      new Form1().ShowDialog(this);
    }
  }
}
