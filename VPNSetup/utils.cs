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
    public static string ParseCmd(string input, int index, string substring, char delim)
    {
      var result = String.Empty;
      string[] lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        if (line != null && line.Contains(substring))
        {
          var result_str = line.ToString();
          result = line.Split(delim)[index];
          break;
        }
      }
      return result;
    }
  }
}
