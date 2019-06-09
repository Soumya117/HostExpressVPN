using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPNSetup
{
  class Util
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
          result = line.Split(':')[index];
          break;
        }
      }
      return result;
    }
  }
}
