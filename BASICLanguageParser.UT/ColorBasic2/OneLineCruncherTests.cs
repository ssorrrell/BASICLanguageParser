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

        [Test]
        public void ForTests()
        {
            string inLine = "FOR \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x80 \r\n";
            Assert.AreEqual(expected, result, "FOR Test");
        }

        [Test]
        public void GoTests()
        {
            string inLine = "GO \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x81 \r\n";
            Assert.AreEqual(expected, result, "GO Test");
        }

        [Test]
        public void ElseTests()
        {
            string inLine = "ELSE \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x84 \r\n";
            Assert.AreEqual(expected, result, "ELSE Test");
        }

        [Test]
        public void IfTests()
        {
            string inLine = "IF \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x85 \r\n";
            Assert.AreEqual(expected, result, "IF Test");
        }

        [Test]
        public void DataTests()
        {
            string inLine = "DATA 12,abc, XyZ\r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x86 12,abc, XyZ\r\n";
            Assert.AreEqual(expected, result, "DATA Test");
        }

        [Test]
        public void PrintTests2()
        {
            string inLine = "PRINT \"Message\"\r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x87 \"Message\"\r\n";
            Assert.AreEqual(expected, result, "PRINT Test");
        }

        [Test]
        public void OnTests()
        {
            string inLine = "ON \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x88 \r\n";
            Assert.AreEqual(expected, result, "ON Test");
        }

        [Test]
        public void InputTests()
        {
            string inLine = "INPUT \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x89 \r\n";
            Assert.AreEqual(expected, result, "INPUT Test");
        }

        [Test]
        public void EndTests()
        {
            string inLine = "END \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x8A \r\n";
            Assert.AreEqual(expected, result, "END Test");
        }

        [Test]
        public void NextTests()
        {
            string inLine = "NEXT \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x8B \r\n";
            Assert.AreEqual(expected, result, "NEXT Test");
        }

        [Test]
        public void DimTests()
        {
            string inLine = "DIM \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x8C \r\n";
            Assert.AreEqual(expected, result, "DIM Test");
        }

        [Test]
        public void ReadTests()
        {
            string inLine = "READ \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x8D \r\n";
            Assert.AreEqual(expected, result, "READ Test");
        }

        [Test]
        public void RunTests()
        {
            string inLine = "RUN \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x8E \r\n";
            Assert.AreEqual(expected, result, "RUN Test");
        }

        [Test]
        public void RestoreTests()
        {
            string inLine = "RESTORE \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x8F \r\n";
            Assert.AreEqual(expected, result, "RESTORE Test");
        }

        [Test]
        public void ReturnTests()
        {
            string inLine = "RETURN \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x90 \r\n";
            Assert.AreEqual(expected, result, "RETURN Test");
        }

        [Test]
        public void StopTests()
        {
            string inLine = "STOP \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x91 \r\n";
            Assert.AreEqual(expected, result, "STOP Test");
        }

        [Test]
        public void PokeTests()
        {
            string inLine = "POKE \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x92 \r\n";
            Assert.AreEqual(expected, result, "POKE Test");
        }

        [Test]
        public void ContTests()
        {
            string inLine = "CONT \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x93 \r\n";
            Assert.AreEqual(expected, result, "CONT Test");
        }

        [Test]
        public void ListTests()
        {
            string inLine = "LIST \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x94 \r\n";
            Assert.AreEqual(expected, result, "LIST Test");
        }

        [Test]
        public void ClearTests()
        {
            string inLine = "CLEAR \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x95 \r\n";
            Assert.AreEqual(expected, result, "CLEAR Test");
        }

        [Test]
        public void NewTests()
        {
            string inLine = "NEW \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x96 \r\n";
            Assert.AreEqual(expected, result, "NEW Test");
        }

        [Test]
        public void CLoadTests()
        {
            string inLine = "CLOAD \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x97 \r\n";
            Assert.AreEqual(expected, result, "CLOAD Test");
        }

        [Test]
        public void CSaveTests()
        {
            string inLine = "CSAVE \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x98 \r\n";
            Assert.AreEqual(expected, result, "CSAVE Test");
        }

        [Test]
        public void OpenTests()
        {
            string inLine = "OPEN \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x99 \r\n";
            Assert.AreEqual(expected, result, "OPEN Test");
        }

        [Test]
        public void CloseTests()
        {
            string inLine = "CLOSE \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x9A \r\n";
            Assert.AreEqual(expected, result, "CLOSE Test");
        }

        [Test]
        public void LListTests()
        {
            string inLine = "LLIST \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x9B \r\n";
            Assert.AreEqual(expected, result, "LLIST Test");
        }

        [Test]
        public void SetTests()
        {
            string inLine = "SET \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x9C \r\n";
            Assert.AreEqual(expected, result, "SET Test");
        }

        [Test]
        public void ResetTests()
        {
            string inLine = "RESET \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x9D \r\n";
            Assert.AreEqual(expected, result, "RESET Test");
        }

        [Test]
        public void ClsTests()
        {
            string inLine = "CLS \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x9E \r\n";
            Assert.AreEqual(expected, result, "CLS Test");
        }

        [Test]
        public void MotorTests()
        {
            string inLine = "MOTOR \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\x9F \r\n";
            Assert.AreEqual(expected, result, "MOTOR Test");
        }

        [Test]
        public void SoundTests()
        {
            string inLine = "SOUND \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xA0 \r\n";
            Assert.AreEqual(expected, result, "SOUND Test");
        }

        [Test]
        public void AudioTests()
        {
            string inLine = "AUDIO \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xA1 \r\n";
            Assert.AreEqual(expected, result, "AUDIO Test");
        }

        [Test]
        public void ExecTests()
        {
            string inLine = "EXEC \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xA2 \r\n";
            Assert.AreEqual(expected, result, "EXEC Test");
        }

        [Test]
        public void SkipFTests()
        {
            string inLine = "SKIPF \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xA3 \r\n";
            Assert.AreEqual(expected, result, "SKIPF Test");
        }

        [Test]
        public void TabTests()
        {
            string inLine = "TAB(5) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xA45) \r\n";
            Assert.AreEqual(expected, result, "TAB( Test");
        }

        [Test]
        public void ToTests()
        {
            string inLine = "TO \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xA5 \r\n";
            Assert.AreEqual(expected, result, "TO Test");
        }

        [Test]
        public void SubTests()
        {
            string inLine = "SUB \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xA6 \r\n";
            Assert.AreEqual(expected, result, "SUB Test");
        }

        [Test]
        public void ThenTests()
        {
            string inLine = "THEN \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xA7 \r\n";
            Assert.AreEqual(expected, result, "THEN Test");
        }

        [Test]
        public void NotTests()
        {
            string inLine = "NOT \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xA8 \r\n";
            Assert.AreEqual(expected, result, "NOT Test");
        }

        [Test]
        public void StepTests()
        {
            string inLine = "STEP \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xA9 \r\n";
            Assert.AreEqual(expected, result, "STEP Test");
        }

        [Test]
        public void OffTests()
        {
            string inLine = "OFF \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xAA \r\n";
            Assert.AreEqual(expected, result, "OFF Test");
        }

        [Test]
        public void PlusTests()
        {
            string inLine = "+1 \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xAB1 \r\n";
            Assert.AreEqual(expected, result, "+ Test");
        }

        [Test]
        public void MinusTests()
        {
            string inLine = "-1 \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xAC1 \r\n";
            Assert.AreEqual(expected, result, "- Test");
        }

        [Test]
        public void TimesTests()
        {
            string inLine = "1*1 \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "1\xAD1 \r\n";
            Assert.AreEqual(expected, result, "* Test");
        }

        [Test]
        public void DivTests()
        {
            string inLine = "1/1 \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "1\xAE\x31 \r\n";
            Assert.AreEqual(expected, result, "/ Test");
        }

        [Test]
        public void ExpTests()
        {
            string inLine = "1^1 \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "1\xAF1 \r\n";
            Assert.AreEqual(expected, result, "^ Test");
        }

        [Test]
        public void AndTests()
        {
            string inLine = "1 AND 1 \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "1 \xB0 1 \r\n";
            Assert.AreEqual(expected, result, "AND Test");
        }

        [Test]
        public void OrTests()
        {
            string inLine = "1 OR 1 \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "1 \xB1 1 \r\n";
            Assert.AreEqual(expected, result, "OR Test");
        }

        [Test]
        public void GreaterThanTests()
        {
            string inLine = "1 > 1 \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "1 \xB2 1 \r\n";
            Assert.AreEqual(expected, result, "> Test");
        }

        [Test]
        public void EqualTests()
        {
            string inLine = "1 = 1 \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "1 \xB3 1 \r\n";
            Assert.AreEqual(expected, result, "= Test");
        }

        [Test]
        public void LessThanTests()
        {
            string inLine = "1 < 1 \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "1 \xB4 1 \r\n";
            Assert.AreEqual(expected, result, "< Test");
        }

        [Test]
        public void SignTests()
        {
            string inLine = "SIGN(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x80(1) \r\n";
            Assert.AreEqual(expected, result, "SIGN Test");
        }

        [Test]
        public void IntTests()
        {
            string inLine = "INT(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x81(1) \r\n";
            Assert.AreEqual(expected, result, "INT Test");
        }

        [Test]
        public void AbsTests()
        {
            string inLine = "ABS(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x82(1) \r\n";
            Assert.AreEqual(expected, result, "Abs Test");
        }

        [Test]
        public void UsrTests()
        {
            string inLine = "USR(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x83(1) \r\n";
            Assert.AreEqual(expected, result, "USR Test");
        }

        [Test]
        public void RndTests()
        {
            string inLine = "RND(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x84(1) \r\n";
            Assert.AreEqual(expected, result, "RND Test");
        }

        [Test]
        public void SinTests()
        {
            string inLine = "SIN(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x85(1) \r\n";
            Assert.AreEqual(expected, result, "SIN Test");
        }

        [Test]
        public void PeekTests()
        {
            string inLine = "PEEK(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x86(1) \r\n";
            Assert.AreEqual(expected, result, "PEEK Test");
        }

        [Test]
        public void LenTests()
        {
            string inLine = "LEN(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x87(1) \r\n";
            Assert.AreEqual(expected, result, "LEN Test");
        }

        [Test]
        public void StrTests()
        {
            string inLine = "STR$(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x88(1) \r\n";
            Assert.AreEqual(expected, result, "STR$ Test");
        }

        [Test]
        public void ValTests()
        {
            string inLine = "VAL(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x89(1) \r\n";
            Assert.AreEqual(expected, result, "VAL Test");
        }

        [Test]
        public void AscTests()
        {
            string inLine = "ASC(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x8A(1) \r\n";
            Assert.AreEqual(expected, result, "ASC Test");
        }

        [Test]
        public void ChrTests()
        {
            string inLine = "CHR$(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x8B(1) \r\n";
            Assert.AreEqual(expected, result, "CHR$ Test");
        }

        [Test]
        public void EofTests()
        {
            string inLine = "EOF(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x8C(1) \r\n";
            Assert.AreEqual(expected, result, "EOF Test");
        }

        [Test]
        public void JoystkTests()
        {
            string inLine = "JOYSTK(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x8D(1) \r\n";
            Assert.AreEqual(expected, result, "JOYSTK Test");
        }

        [Test]
        public void LeftTests()
        {
            string inLine = "LEFT$(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x8E(1) \r\n";
            Assert.AreEqual(expected, result, "LEFT$ Test");
        }

        [Test]
        public void RightTests()
        {
            string inLine = "RIGHT$(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x8F(1) \r\n";
            Assert.AreEqual(expected, result, "RIGHT$ Test");
        }

        [Test]
        public void MidTests()
        {
            string inLine = "MID$(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x90(1) \r\n";
            Assert.AreEqual(expected, result, "MID$ Test");
        }

        [Test]
        public void PointTests()
        {
            string inLine = "POINT(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x91(1) \r\n";
            Assert.AreEqual(expected, result, "POINT Test");
        }

        [Test]
        public void InkeyTests()
        {
            string inLine = "INKEY$(1) \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x92(1) \r\n";
            Assert.AreEqual(expected, result, "INKEY$ Test");
        }

        [Test]
        public void MemTests()
        {
            string inLine = "MEM \r\n";
            Cruncher c = new Cruncher();
            string result = c.CrunchOneLine(inLine);
            string expected = "\xFF\x93 \r\n";
            Assert.AreEqual(expected, result, "MEM Test");
        }
        /*************************************Internal****************************************/

    }


}
