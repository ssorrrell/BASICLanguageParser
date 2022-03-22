using NUnit.Framework;
using BASICLanguageParser.ColorBasic2;

namespace BASICLanguageParser.UT.ColorBasic2
{
    public class OneLineCruncherTests
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
        public void EmptyStringsTests()
        {
            string inLine = "";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "";
            Assert.AreEqual(expected, result, "Empty string");
            inLine = "\r\n";
            result = c.CrunchOneLine(inLine);
            expected = "\r\n";
            Assert.AreEqual(expected, result, "Empty string + EOL");
        }

        [Test]
        public void REMTests()
        {
            string inLine = "REM Test Comment\r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x82 Test Comment\r\n";
            Assert.AreEqual(expected, result, "REM Comment");
            inLine = "REM Test:REM Ignored\r\n";
            result = c.CrunchOneLine(inLine);
            expected = "\x82 Test:REM Ignored\r\n";
            Assert.AreEqual(expected, result, "REM Comment with colon");
            inLine = "' Test Comment\r\n";
            result = c.CrunchOneLine(inLine);
            expected = "\x83 Test Comment\r\n";
            Assert.AreEqual(expected, result, "' Comment");
            inLine = "' Test:' Ignored\r\n";
            result = c.CrunchOneLine(inLine);
            expected = "\x83 Test:' Ignored\r\n";
            Assert.AreEqual(expected, result, "' Comment with colon");
        }

        [Test]
        public void PrintTests()
        {
            string inLine = "? \"Test Comment\"\r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x87 \"Test Comment\"\r\n";
            Assert.AreEqual(expected, result, "? Test");
            inLine = "? \"    \"\r\n";
            result = c.CrunchOneLine(inLine);
            expected = "\x87 \"    \"\r\n";
            Assert.AreEqual(expected, result, "? spaces");
        }

        [Test]
        public void SemiColonTests()
        {
            string inLine = "? \"Test Comment\";\"a\"\r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x87 \"Test Comment\";\"a\"\r\n";
            Assert.AreEqual(expected, result, "? Test SemiColon");
        }

        [Test]
        public void SpaceDelimTests()
        {
            string inLine = " BOB \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = " BOB \r\n";
            Assert.AreEqual(expected, result, "Space Delim Test");
        }
        /*************************************Internal****************************************/

    }


}
