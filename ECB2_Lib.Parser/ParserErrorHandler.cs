using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace ECB2_Lib.Parser
{
    public class ParserErrorHandler<T> : IAntlrErrorListener<T>
    {
        public void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] T offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e)
        {
            throw e;
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, T offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new NotImplementedException();
        }
    }
}