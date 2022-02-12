using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace BASICLanguageParser.UT
{
    public class TestBaseTest
    {
        public const string ERROR_TEMPLATE = "Test {0} {1}"; //function Name being tested, test fragment

        public TestBasicLexer lexer;
        public CommonTokenStream commonTokenStream;
        public TestBasicParser parser;
        public TestBasicBaseVisitor<object> visitor;
        public LexErrorListener lexerErrorListener;
        public ErrorListener errorListener;

        protected void SetupLexerParser(string txt)
        {
            AntlrInputStream inputStream = new AntlrInputStream(txt);
            lexer = new TestBasicLexer(inputStream);
            lexerErrorListener = new LexErrorListener();
            lexer.AddErrorListener(lexerErrorListener);
            commonTokenStream = new CommonTokenStream(lexer);
            parser = new TestBasicParser(commonTokenStream);
            errorListener = new ErrorListener();
            parser.AddErrorListener(errorListener);
            
        }

        protected int VisitNode(RuleContext context)
        {
            visitor = new TestBasicBaseVisitor<object>();
            visitor.Visit(context);
            parser.RemoveErrorListeners();
            return parser.NumberOfSyntaxErrors;
        }
    }
}
