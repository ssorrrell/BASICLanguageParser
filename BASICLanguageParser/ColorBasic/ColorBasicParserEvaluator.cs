using Antlr4.Runtime;
using BASICLanguageParser.Common;

namespace BASICLanguageParser
{
    public class ColorBasicParserEvaluator
    {
        public ErrorResult ParseDocument(string document)
        {
            AntlrInputStream inputStream = new AntlrInputStream(document);
            ColorBasicLexer lexer = new ColorBasicLexer(inputStream);
            LexErrorListener lexErrorListener = new LexErrorListener();
            lexer.AddErrorListener(lexErrorListener);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            ColorBasicParser parser = new ColorBasicParser(commonTokenStream);
            ErrorListener parseErrorListener = new ErrorListener();
            parser.AddErrorListener(parseErrorListener);
            ColorBasicParser.ProgContext progContext = parser.prog();
            ColorBasicBaseVisitor<object> visitor = new ColorBasicBaseVisitor<object>();
            visitor.Visit(progContext);
            parser.RemoveErrorListeners();
            ErrorResult errorResult = new ErrorResult();
            errorResult.ErrorCount = lexErrorListener.Errors.Count + parseErrorListener.Errors.Count;
            errorResult.LexerErrorCount = lexErrorListener.Errors.Count;
            errorResult.ParserErrorCount = parseErrorListener.Errors.Count;
            errorResult.LexerErrorList = lexErrorListener.Errors;
            errorResult.ParserErrorList = parseErrorListener.Errors;
            return errorResult;
        }
    }
}
