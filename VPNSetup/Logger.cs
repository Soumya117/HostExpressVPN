using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace VPNSetup
{
  class Logger
  {
    public static void LogTextEvent(RichTextBox TextEventLog, string EventText)
    {
      if (TextEventLog.InvokeRequired)
      {
        TextEventLog.BeginInvoke(new Action(delegate {
          LogTextEvent(TextEventLog, EventText);
        }));
        return;
      }

      string nDateTime = DateTime.Now.ToString("hh:mm:ss tt") + " - ";

      // color text.
      TextEventLog.SelectionStart = TextEventLog.Text.Length;

      // newline if first line, append if else.
      if (TextEventLog.Lines.Length == 0)
      {
        TextEventLog.AppendText(nDateTime + EventText);
        TextEventLog.ScrollToCaret();
        TextEventLog.AppendText(System.Environment.NewLine);
      }
      else
      {
        TextEventLog.AppendText(nDateTime + EventText + System.Environment.NewLine);
        TextEventLog.ScrollToCaret();
      }
    }
  }
}
