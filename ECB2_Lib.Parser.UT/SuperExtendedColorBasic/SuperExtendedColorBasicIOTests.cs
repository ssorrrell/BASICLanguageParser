using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ECB2_Lib.Parser.UT
{
    public class SuperExtendedColorBasicIOTests : ExtendedColorBasicIOTests
    {
        /**
         * Tokens: INPUT INKEY$ CLOAD CSAVE PRINT OPEN CLOSE MOTOR TAB SKIPF EOF
         */

        [SetUp]
        public new void Setup()
        {
        }

        /*************************************Tests****************************************/


        /*************************************Internal****************************************/

        protected override int RunCLoad(string txt)
        {
            SetupLexerParser(txt);
            jvmBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
            int result = VisitNode(absDefinitionContext);
            return result;
        }
    }


}
