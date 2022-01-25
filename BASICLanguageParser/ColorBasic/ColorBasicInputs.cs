using Antlr4.Runtime;

namespace BASICLanguageParser
{
    public class ColorBasicInputs
    {
        public ColorBasicLexer lexer;
        public CommonTokenStream commonTokenStream;
        public ColorBasicParser parser;
        public ColorBasicBaseVisitor<object> visitor;

        public ColorBasicInputs(string document)
        {
            AntlrInputStream inputStream = new AntlrInputStream(document);
            lexer = new ColorBasicLexer(inputStream);
            lexer.AddErrorListener(new LexErrorListener());
            commonTokenStream = new CommonTokenStream(lexer);
            parser = new ColorBasicParser(commonTokenStream);
            parser.AddErrorListener(new ErrorListener());
        }
    }
}
