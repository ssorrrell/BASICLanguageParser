using System;
using NUnit.Framework;
using System.IO;
using Antlr4.Runtime;
using BASICLanguageParser.UT.Common;

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
