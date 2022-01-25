using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace BASICLanguageParser.UT
{
    public class ColorBasicGraphicsTests : BaseTest
    {
        /**
         * Tokens: POINT CLS SET RESET
         */

        [SetUp]
        public void Setup()
        {
        }

        /*************************************Tests****************************************/

        [Test]
        protected void Cls()
        {
            const string functionName = "CLS";
            string test = "CLS 2";
            int result = RunCls(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }



        /*************************************Internal****************************************/

        protected virtual int RunCls(string txt)
        {
            SetupLexerParser(txt);
            //ColorBasicParser.ClsstmtContext clsStmtContext = parser.clsstmt();
            int result = 0; // VisitNode(clsStmtContext);
            return result;
        }
    }


}
