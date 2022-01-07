using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ECB2_Lib.Parser.UT
{
    public class ColorBasicOperatorsTests : BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        /*************************************Tests****************************************/

        [Test]
        public void AND()
        {
            const string functionName = "AND";
            string test = "Y = ABS(5)";
            int result = RunAnd(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }



        /*************************************Internal****************************************/

        private int RunAnd(string txt)
        {
            SetupLexerParser(txt);
            jvmBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
            int result = VisitNode(absDefinitionContext);
            return result;
        }
    }


}
