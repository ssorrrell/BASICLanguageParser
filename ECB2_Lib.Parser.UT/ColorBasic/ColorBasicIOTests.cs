using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ECB2_Lib.Parser.UT
{
    public class ColorBasicIOTests : BaseTest
    {
        /**
         * Tokens: INPUT INKEY$ CLOAD CSAVE PRINT OPEN CLOSE MOTOR TAB SKIPF EOF
         */

        [SetUp]
        public void Setup()
        {
        }

        /*************************************Tests****************************************/

        [Test]
        protected void CLoad()
        {
            const string functionName = "CLOAD";
            string test = "CLOAD \"PUPPIES\"";
            int result = RunCLoad(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }



        /*************************************Internal****************************************/

        protected virtual int RunCLoad(string txt)
        {
            SetupLexerParser(txt);
            ColorBasicParser.CloadstmtContext cloadStmtContext = parser.cloadstmt();
            int result = VisitNode(cloadStmtContext);
            return result;
        }
    }


}
