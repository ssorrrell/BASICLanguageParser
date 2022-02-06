using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using BASICLanguageParser.Common;

namespace BASICLanguageParser.UT
{
    public class ColorBasicOperatorsTests : BaseTest
    {
        /**
         * Tokens: + - * / ^ > = < AND OR NOT :
         */

        [SetUp]
        public void Setup()
        {
        }

        /*************************************Tests****************************************/
        [Test]
        public void Let()
        {
            const string functionName = "-";

            string test = "A=A+5";
            //int result = RunExpression(test);
            SetupLexerParser(test);
            ColorBasicParser.VariableassignmentContext varAssignContext = parser.variableassignment();
            int result = VisitNode(varAssignContext);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "I = I / 12 * .01";
            SetupLexerParser(test);
            ColorBasicParser.LetstmtContext letStmtContext = parser.letstmt();
            result = VisitNode(letStmtContext);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "30 I=I/12*.01";
            AntlrInputStream stream = new AntlrInputStream(test);
            ColorBasicLexer lexer = new ColorBasicLexer(stream);
            LexErrorListener lexErrorListener = new LexErrorListener();
            lexer.AddErrorListener(lexErrorListener);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            ColorBasicParser parser2 = new ColorBasicParser(commonTokenStream);
            ErrorListener parseErrorListener = new ErrorListener();
            parser2.AddErrorListener(parseErrorListener);
            ColorBasicParser.ProgContext progContext = parser2.prog();
            ColorBasicBaseVisitor<object> visitor = new ColorBasicBaseVisitor<object>();
            visitor.Visit(progContext);
            parser.RemoveErrorListeners();
            ErrorResult errorResult = new ErrorResult();
            errorResult.ErrorCount = lexErrorListener.Errors.Count + parseErrorListener.Errors.Count;
            errorResult.LexerErrorCount = lexErrorListener.Errors.Count;
            errorResult.ParserErrorCount = parseErrorListener.Errors.Count;
            errorResult.LexerErrorList = lexErrorListener.Errors;
            errorResult.ParserErrorList = parseErrorListener.Errors;
        }

        [Test]
        protected void Negate()
        {
            const string functionName = "-";

            string test = "-5";
            int result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "-10";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void Numbers()
        {
            const string functionName = "Numbers";

            string test = "1";
            int result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "12";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "123";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "1234";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "12345";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "65536";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = ".1";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = ".1234";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "1.1";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "0.1569087";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "322456.1569087";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5.75E39";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "1.75E-39";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "2.75678E+39";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void ADD()
        {
            const string functionName = "+";

            string test = "5+5";
            int result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5+5+5";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5+10+5+7";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void SUB()
        {
            const string functionName = "-";

            string test = "5-5";
            int result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5-5-5";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5-10-5-7";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void MUL()
        {
            const string functionName = "*";

            string test = "5*5";
            int result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5*5*5";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5*10*5*7";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void DIV()
        {
            const string functionName = "/";

            string test = "5/5";
            int result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5/5/5";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5/10/5/7";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void EXP()
        {
            const string functionName = "^";

            string test = "5^2";
            int result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "8^2^3";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5^10^5^7";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void ADD_SUB()
        {
            const string functionName = "+-";

            string test = "5+2-1";
            int result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "8-2+3";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5+10+5-7";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5-10+5-7";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void ADD_MUL()
        {
            const string functionName = "+*";

            string test = "5+2*1";
            int result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "8*2+3";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5*10*5+7";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5+10*5+7";
            result = RunExpression(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }
        /*************************************Internal****************************************/

        protected virtual int RunExpression(string txt)
        {
            SetupLexerParser(txt);
            //ColorBasicParser.ExpressionContext expressionContext = parser.expression();
            int result = 0; // VisitNode(expressionContext);
            return result;
        }
    }


}
