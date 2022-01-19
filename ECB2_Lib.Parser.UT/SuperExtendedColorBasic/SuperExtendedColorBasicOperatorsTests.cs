using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ECB2_Lib.Parser.UT
{
    public class SuperExtendedColorBasicOperatorsTests : ExtendedColorBasicOperatorsTests
    {
        /**
         * Tokens: + - * / ^ > = < AND OR NOT :
         */

        [SetUp]
        public new void Setup()
        {
        }

        /*************************************Tests****************************************/


        /*************************************Internal****************************************/

        //protected override int RunAnd(string txt)
        //{
        //    SetupLexerParser(txt);
        //    jvmBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
        //    int result = VisitNode(absDefinitionContext);
        //    return result;
        //}
    }


}
