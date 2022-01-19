using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ECB2_Lib.Parser.UT
{
    public class ColorBasicMemoryTests: BaseTest
    {
        /**
         * Tokens: PEEK POKE CLEAR NEW DATA RESTORE DIM MEM READ DEF FN USR EXEC
         */

        [SetUp]
        public void Setup()
        {
        }

        /*************************************Tests****************************************/

        [Test]
        protected void ABS()
        {
            const string functionName = "ABS";
            string test = "Y = ABS(5)";
            int result = RunABS(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }



        /*************************************Internal****************************************/

        protected virtual int RunABS(string txt)
        {
            SetupLexerParser(txt);
            ColorBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
            int result = VisitNode(absDefinitionContext);
            return result;
        }
    }


}
