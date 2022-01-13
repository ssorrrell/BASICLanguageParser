using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ECB2_Lib.Parser.UT
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
        protected void Negate()
        {
            const string functionName = "-";

            string test = "-5";
            int result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "-10";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void Numbers()
        {
            const string functionName = "Numbers";

            string test = "1";
            int result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "12";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "123";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "1234";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "12345";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "65536";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = ".1";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = ".1234";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "1.1";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "0.1569087";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "322456.1569087";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5.75E39";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "1.75E-39";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "2.75678E+39";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void ADD()
        {
            const string functionName = "+";

            string test = "5+5";
            int result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5+5+5";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5+10+5+7";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void SUB()
        {
            const string functionName = "-";

            string test = "5-5";
            int result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5-5-5";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5-10-5-7";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void MUL()
        {
            const string functionName = "*";

            string test = "5*5";
            int result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5*5*5";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5*10*5*7";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void DIV()
        {
            const string functionName = "/";

            string test = "5/5";
            int result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5/5/5";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5/10/5/7";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void EXP()
        {
            const string functionName = "^";

            string test = "5^2";
            int result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "8^2^3";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5^10^5^7";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void ADD_SUB()
        {
            const string functionName = "+-";

            string test = "5+2-1";
            int result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "8-2+3";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5+10+5-7";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5-10+5-7";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }

        [Test]
        protected void ADD_MUL()
        {
            const string functionName = "+*";

            string test = "5+2*1";
            int result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "8*2+3";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5*10*5+7";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));

            test = "5+10*5+7";
            result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }
        /*************************************Internal****************************************/

        protected virtual int RunAnd(string txt)
        {
            SetupLexerParser(txt);
            jvmBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
            int result = VisitNode(absDefinitionContext);
            return result;
        }
    }


}
