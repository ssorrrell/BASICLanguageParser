using System;
using NUnit.Framework;
using System.IO;
using Antlr4.Runtime;
using BASICLanguageParser.Common;

namespace BASICLanguageParser.UT
{
    public class ColorBasicProgramTests : BaseTest
    {
        protected string programPath = @"..\..\..\bas\";

        [SetUp]
        public void Setup()
        {
        }

        /*************************************Tests****************************************/

        [Test]
        public void HelloWorld()
        {
            var filename = "hello_world.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void IOStatements()
        {
            var filename = "io_statements.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void LASER_STAR_Program()
        {   //this is actually an ecb file
            var filename = "laser_star.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void LINE_JUSTIFIER_Program()
        {   //this is actually an ecb file because of string$
            var filename = "line_justifier.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void GOSUB_example_Program()
        {   //this is actually an ecb file because of string$
            var filename = "GOSUB example.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void MathExpressions()
        {
            var filename = "math_expressions.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void MemoryStatments()
        {
            var filename = "memory_statements.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void PrintStatements()
        {
            var filename = "print_statements.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void StringExpressions()
        {
            var filename = "string_expressions.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void SimpleTest()
        {   //tests line number, LET token, math expressions, variables names
            var filename = "simple_test.bas";
            var result = RunProg(filename);
            Assert.AreEqual(3, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void StringExpressionTest()
        {   //tests line number, LET token, math expressions, variables names
            var filename = "string_expression_test.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void GotoGosubTest()
        {   //tests line number, LET token, math expressions, variables names
            var filename = "goto_gosub_test.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void OnGotoGosubTest()
        {   //tests line number, LET token, math expressions, variables names
            var filename = "on_goto_gosub_test.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }

        [Test]
        public void IfThenElseTest()
        {   //tests line number, LET token, math expressions, variables names
            var filename = "if_then_else_test.bas";
            var result = RunProg(filename);
            Assert.AreEqual(0, result.ErrorCount, "Wrong Error Count ProgTest Case: {0}", filename, result);
        }
        /*************************************Internal****************************************/

        private ErrorResult RunProg(string filename)
        {
            string path = Path.Combine(Environment.CurrentDirectory, programPath, filename);
            AntlrFileStream stream = new AntlrFileStream(path);
            ColorBasicLexer lexer = new ColorBasicLexer(stream);
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
