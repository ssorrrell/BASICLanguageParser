using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ECB2_Lib.Parser.UT
{
    public class ExtendedColorBasicStatementsTests : ColorBasicStatementsTests
    {
        /**
         * Tokens: FOR TO STEP NEXT IF THEN ELSE GO TO SUB RETURN ON RUN REM '
         */

        [SetUp]
        public new void Setup()
        {
        }

        /*************************************Tests****************************************/


        /*************************************Internal****************************************/

        //protected override int RunData(string txt)
        //{
        //    SetupLexerParser(txt);
        //    jvmBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
        //    int result = VisitNode(absDefinitionContext);
        //    return result;
        //}
    }


}
