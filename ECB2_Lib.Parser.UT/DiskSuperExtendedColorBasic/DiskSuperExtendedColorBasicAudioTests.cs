using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ECB2_Lib.Parser.UT
{
    public class DiskSuperExtendedColorBasicAudioTests : ExtendedColorBasicAudioTests
    {
        /**
         * Tokens: SOUND AUDIO OFF
         */

        [SetUp]
        public new void Setup()
        {
        }

        /*************************************Tests****************************************/


        /*************************************Internal****************************************/

        protected override int RunSound(string txt)
        {
            SetupLexerParser(txt);
            jvmBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
            int result = VisitNode(absDefinitionContext);
            return result;
        }
    }


}
