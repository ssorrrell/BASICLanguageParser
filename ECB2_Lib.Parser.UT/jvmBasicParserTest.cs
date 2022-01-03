using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using ECB2_Lib.Parser;
using System.IO;

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
            int result = RunABS(txt);
            Assert.AreEqual(0, result, "Wrong Error Count ABS Case: '{0}'", txt);

            txt = "ABS\"G\")";
            result = RunABS(txt);
            Assert.AreEqual(1, result, "Wrong Error Count ABS Case: '{0}'", txt);
        }

        private int RunABS(string txt)
        {
            AntlrInputStream inputStream = new AntlrInputStream(txt);
            jvmBasicLexer lexer = new jvmBasicLexer(inputStream);
            lexer.AddErrorListener(new LexErrorListener());
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            jvmBasicParser parser = new jvmBasicParser(commonTokenStream);
            parser.AddErrorListener(new ErrorListener());
            jvmBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
            jvmBasicBaseVisitor<object> visitor = new jvmBasicBaseVisitor<object>();
            visitor.Visit(absDefinitionContext);
            parser.RemoveErrorListeners();
            return parser.NumberOfSyntaxErrors;
        }

        [Test]
        public void ProgTests()
        {
            string filename = "bottles_of_beer.bas";
            int result = RunProg(filename);
            Assert.AreEqual(0, result, "Wrong Error Count ProgTest Case: {0}", filename);

            filename = "hello_world.bas";
            result = RunProg(filename);
            Assert.AreEqual(0, result, "Wrong Error Count ProgTest Case: {0}", filename);
        }

        private int RunProg(string filename)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\bas\", filename);
            AntlrFileStream stream = new AntlrFileStream(path);
            jvmBasicLexer lexer = new jvmBasicLexer(stream);
            LexErrorListener lexErrorListener = new LexErrorListener();
            lexer.AddErrorListener(lexErrorListener);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            jvmBasicParser parser = new jvmBasicParser(commonTokenStream);
            ErrorListener parseErrorListener = new ErrorListener();
            parser.AddErrorListener(parseErrorListener);
            jvmBasicParser.ProgContext progContext = parser.prog();
            jvmBasicBaseVisitor<object> visitor = new jvmBasicBaseVisitor<object>();
            visitor.Visit(progContext);
            parser.RemoveErrorListeners();
            return lexErrorListener.Errors.Count + parseErrorListener.Errors.Count;
        }


     }
}