using NUnit.Framework;
using BASICLanguageParser.ColorBasic2;

namespace BASICLanguageParser.UT.ColorBasic2
{
    public class CruncherTests
    {
        /**
         * Tokens: 
         */

        [SetUp]
        public void Setup()
        {
        }

        /*************************************Tests****************************************/

        [Test]
        public void FirstTests()
        {
            string inLine = "";
            Cruncher c = new Cruncher();
            string result  = c.CrunchOneLine(inLine);
            string expected = "";
            Assert.AreEqual(expected, result);
        }



        /*************************************Internal****************************************/
    }


}
