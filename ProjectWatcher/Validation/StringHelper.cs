using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SystemSettings
{
    public static class StringHelper
    {
        public static String DeleteWhitespacesPattern = @"\A\s*(?<pureString>.*?)\s*\z";

        public static String CutWhitespaces(this String stringWithWhitespaces)
        {
            if(stringWithWhitespaces == null)
            {
                return "";
            }
            return Regex.Match(stringWithWhitespaces, DeleteWhitespacesPattern).Groups["pureString"].Value;
        }
    }
}
