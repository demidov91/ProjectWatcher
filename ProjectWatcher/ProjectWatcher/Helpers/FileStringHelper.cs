using System;
using System.Linq;
using SystemSettings;

namespace ProjectWatcher.Helpers
{
    public static class FileStringHelper
    {
        public static String FileFormatvalueIntoProgramFormat(this String fileFormat)
        {
            String[] multyselect = fileFormat.Split('|');
            multyselect = Array.ConvertAll(multyselect, x => x.CutWhitespaces()).Where(x => x.Length > 0).ToArray();
            if (multyselect.Length == 1)
            {
                return multyselect[0]; 
            }
            else
            {
                String tooBig = String.Concat(multyselect.Select(x => x + '\n'));
                return tooBig.Remove(tooBig.Length - 1, 1);
            }
            
        }
    }
}