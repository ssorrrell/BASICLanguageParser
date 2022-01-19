using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace BASICLanguageParser.UT
{
    public class ExtendedColorBasicFunctionTests: ColorBasicFunctionTests
    {
        /**
         * Tokens: SGN, INT, ABS, RND, SIN, PEEK, LEN, STR$, VAL, ASC, CHR$, JOYSTK, LEFT$, RIGHT$, MID$, INKEY$
         */

        [SetUp]
        public new void Setup()
        {
        }

        /*************************************Tests****************************************/

        /*************************************Internal****************************************/

        //protected override int RunABS(string txt)
        //{
        //    SetupLexerParser(txt);
        //    jvmBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
        //    int result = VisitNode(absDefinitionContext);
        //    return result;
        //}
    }


}
