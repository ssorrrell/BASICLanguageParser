using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ECB2_Lib.Parser;

namespace ECB2_Lib.Parser.UT
{
    public class jvmBasicParserTest
    {
        //private SyntaxErrorListener _errorListener = new SyntaxErrorListener();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MathFunctionTests()
        {
            //AntlrFileStream stream = new AntlrFileStream(@".\bas\sample1.bas");
            ////ICharStream stream = CharStreams.fromString(input);
            //ITokenSource lexer = new jvmBasicLexer(stream);
            //ITokenStream tokens = new CommonTokenStream(lexer);
            //jvmBasicParser parser = new jvmBasicParser(tokens);
            //parser.BuildParseTree = true;

            //parser.AddErrorListener(_errorListener);


            //IParseTree tree = parser..CompileParseTreePattern(stream, 0).PatternTree;
            //Console.WriteLine(tree.ToStringTree());

            //Assert.Pass();

            string txt = "ABS(-5)";
            AntlrInputStream inputStream = new AntlrInputStream(txt);
            jvmBasicLexer lexer = new jvmBasicLexer(inputStream);
            lexer.AddErrorListener(new ParserErrorHandler<int>());
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            jvmBasicParser parser = new jvmBasicParser(commonTokenStream);
            parser.AddErrorListener(new ParserErrorHandler<object>());
            jvmBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
            jvmBasicBaseVisitor<object> visitor = new jvmBasicBaseVisitor<object>();
            visitor.Visit(absDefinitionContext);
            Assert.True(parser.NumberOfSyntaxErrors == 0);
            parser.RemoveErrorListeners();

            txt = "ABS(\"G\")";
            inputStream = new AntlrInputStream(txt);
            lexer = new jvmBasicLexer(inputStream);
            lexer.AddErrorListener(new ParserErrorHandler<int>());
            commonTokenStream = new CommonTokenStream(lexer);
            parser = new jvmBasicParser(commonTokenStream);
            parser.AddErrorListener(new ParserErrorHandler<object>());
            absDefinitionContext = parser.absfunc();
            visitor = new jvmBasicBaseVisitor<object>();
            visitor.Visit(absDefinitionContext);
            Assert.True(parser.NumberOfSyntaxErrors == 1);
            parser.RemoveErrorListeners();
        }
    }
}