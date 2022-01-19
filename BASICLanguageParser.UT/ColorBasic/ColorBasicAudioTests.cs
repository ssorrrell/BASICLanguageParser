using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace BASICLanguageParser.UT
{
    public class ColorBasicAudioTests : BaseTest
    {
        /**
         * Tokens: SOUND AUDIO OFF
         */

        [SetUp]
        public void Setup()
        {
        }

        /*************************************Tests****************************************/

        [Test]
        protected void Sound()
        {
            const string functionName = "SOUND";
            string test = "SOUND 33,22";
            int result = RunSound(test);
            Assert.AreEqual(0, result, string.Format(ERROR_TEMPLATE, functionName, test));
        }



        /*************************************Internal****************************************/

        protected virtual int RunSound(string txt)
        {
            SetupLexerParser(txt);
            ColorBasicParser.SoundstmtContext soundStmtContext = parser.soundstmt();
            int result = VisitNode(soundStmtContext);
            return result;
        }
    }


}
