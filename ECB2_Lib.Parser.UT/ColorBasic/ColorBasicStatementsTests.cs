using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ECB2_Lib.Parser.UT
{
    public class ColorBasicStatementsTests : BaseTest
    {
        /**
         * Tokens: FOR TO STEP NEXT IF THEN ELSE GO TO SUB RETURN ON RUN REM ' END STOP CONT LIST LLIST
         */

        [SetUp]
        public void Setup()
        {
        }

        /*************************************Tests****************************************/

        [Test]
        protected void Data()
        {
            const string functionName = "DATA";
            string test = "DATA 45,CAT,98,DOG,24,.3,1000";
            int result = RunData(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }



        /*************************************Internal****************************************/

        protected virtual int RunData(string txt)
        {
            SetupLexerParser(txt);
            ColorBasicParser.DatastmtContext absDefinitionContext = parser.datastmt();
            int result = VisitNode(absDefinitionContext);
            return result;
        }
    }


}
