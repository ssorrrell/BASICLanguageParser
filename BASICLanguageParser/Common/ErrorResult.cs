using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BASICLanguageParser.Common
{
    public class ErrorResult
    {
        public int ErrorCount = 0;
        public int ParserErrorCount = 0;
        public int LexerErrorCount = 0;
        public List<string> ParserErrorList = null;
        public List<string> LexerErrorList = null;
    }
}
