using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BASICLanguageParser.Common
{
    public class Constants
    {
        public static string EOL = "\r\n";
        public static string ASCII_NUMBERS = @"\d";
        public static string LOWER_CASE_ALPHA = @"\[a-z]";

        public Regex rgASCII_NUMBERS = new Regex(ASCII_NUMBERS);
        public Regex rgLOWER_CASE_ALPHA = new Regex(LOWER_CASE_ALPHA);
    }
}
