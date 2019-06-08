using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPNSetup
{
  class utils
  {
    public static string ParseCmd(int index, string substring)
    {
      var result = String.Empty;
      string[] lines = result.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        if (line != null && line.Contains(substring))
        {
          var result_str = line.ToString();
          var ssid = line.Split(':')[index];
          result = string.Join(" ", ssid.Split('"').Where((x, i) => i % 2 != 0));
          break;
        }
      }
      return result;
    }
  }
}
