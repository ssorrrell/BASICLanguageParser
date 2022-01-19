using System;
using NUnit.Framework;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ECB2_Lib.Parser.UT
{
    public class DiskExtendedColorBasicDiskTests : BaseTest
    {
        /**
         * Tokens: DIR DRIVE FIELD FILES KILL LOAD LSET MERGE RENAME RSET SAVE WRITE VERIFY UNLOAD DSKINI BACKUP COPY TO DSKI$ DSKO$ DOS FREE LOC LOF AS PRINT
         */

        [SetUp]
        public void Setup()
        {
        }

        /*************************************Tests****************************************/


        /*************************************Internal****************************************/

        //protected virtual int RunCLoad(string txt)
        //{
        //    SetupLexerParser(txt);
        //    jvmBasicParser.AbsfuncContext absDefinitionContext = parser.absfunc();
        //    int result = VisitNode(absDefinitionContext);
        //    return result;
        //}
    }


}
