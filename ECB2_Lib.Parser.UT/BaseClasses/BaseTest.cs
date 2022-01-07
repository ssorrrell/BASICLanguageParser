using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ECB2_Lib.Parser.UT
{
    public class BaseTest
    {
        public const string ERROR_TEMPLATE = "Test {0} {1}"; //function Name being tested, test fragment

        public jvmBasicLexer lexer;
        public CommonTokenStream commonTokenStream;
        public jvmBasicParser parser;
        public jvmBasicBaseVisitor<object> visitor;

        protected void SetupLexerParser(string txt)
        {
            AntlrInputStream inputStream = new AntlrInputStream(txt);
            lexer = new jvmBasicLexer(inputStream);
            lexer.AddErrorListener(new LexErrorListener());
            commonTokenStream = new CommonTokenStream(lexer);
            parser = new jvmBasicParser(commonTokenStream);
            parser.AddErrorListener(new ErrorListener());
        }

        protected int VisitNode(RuleContext context)
        {
            visitor = new jvmBasicBaseVisitor<object>();
            visitor.Visit(context);
            parser.RemoveErrorListeners();
            return parser.NumberOfSyntaxErrors;
        }
    }
}
