using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace BASICLanguageParser.UT
{
    public class ColorBasicFunctionTests: BaseTest
    {
        /**
         * Tokens: SGN, INT, ABS, RND, SIN, PEEK, LEN, STR$, VAL, ASC, CHR$, JOYSTK, LEFT$, RIGHT$, MID$, INKEY$
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
            //ColorBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
            int result = 0; // VisitNode(absDefinitionContext);
            return result;
        }
    }


}
