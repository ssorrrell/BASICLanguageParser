using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace BASICLanguageParser.UT
{
    public class BaseTest
    {
        public const string ERROR_TEMPLATE = "Test {0} {1}"; //function Name being tested, test fragment

        public ColorBasicLexer lexer;
        public CommonTokenStream commonTokenStream;
        public ColorBasicParser parser;
        public ColorBasicBaseVisitor<object> visitor;

        protected void SetupLexerParser(string txt)
        {
            AntlrInputStream inputStream = new AntlrInputStream(txt);
            lexer = new ColorBasicLexer(inputStream);
            lexer.AddErrorListener(new LexErrorListener());
            commonTokenStream = new CommonTokenStream(lexer);
            parser = new ColorBasicParser(commonTokenStream);
            parser.AddErrorListener(new ErrorListener());
        }

        protected int VisitNode(RuleContext context)
        {
            visitor = new ColorBasicBaseVisitor<object>();
            visitor.Visit(context);
            parser.RemoveErrorListeners();
            return parser.NumberOfSyntaxErrors;
        }
    }
}
