using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BASICLanguageParser.Common;

namespace BASICLanguageParser.ColorBasic2
{
    /**
     * Color Basic Cruncher
     * Based on the Assembly Language Deconstruction in the Unravelled series
     * Similar to a lexer, but does not convert all character collections into tokens
     * like ANTLR4.  Transcoded as close as possible to the original.
     * Converts keywords into tokens above ASCII 128.  Should create a string that is the same
     * length as what Color BASIC would make.
     **/
    public class Cruncher
    {
        private Dictionary<string, string> PrimaryTokenDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> SecondaryTokenDictionary = new Dictionary<string, string>();
        private Constants constants;

        public Cruncher()
        {
            constants = new Constants();
            LoadPrimaryTokenDictionary();
            LoadSecondaryTokenDictionary();
        }


        public string CrunchOneLine(string inputBuffer) //main loop
        {
            bool IllegalToken = false;
            bool Data = false;
            string scanDelimiter = "";
            string outputBuffer = "";
            string tokenPotential = "";

            //B82D get character
            for (int inputPointer = 0; inputPointer < inputBuffer.Length; inputPointer++)
            {
                string currentCharacter = inputBuffer[inputPointer].ToString();
                if (scanDelimiter.Length > 0 && currentCharacter == scanDelimiter)
                {   //loop till delimiter found
                    if (currentCharacter == scanDelimiter)
                    {   //delimiter found
                        outputBuffer = outputBuffer + currentCharacter;
                        scanDelimiter = "";
                        continue;
                    }
                    //looking for delimiter
                    outputBuffer = outputBuffer + currentCharacter;
                    continue;
                }
                else if (constants.rgASCII_NUMBERS.IsMatch(currentCharacter))
                {   //don't crunch ASCII Numbers
                    outputBuffer = outputBuffer + currentCharacter;
                    continue;
                }
                else if (constants.rgLOWER_CASE_ALPHA.IsMatch(currentCharacter))
                {   //don't crunch Lower Case Alpha
                    outputBuffer = outputBuffer + currentCharacter;
                    continue;
                }
                else if (currentCharacter == " ")
                {   //don't crunch spaces
                    outputBuffer = outputBuffer + currentCharacter;
                    scanDelimiter = " "; //set ScanDelimiter to space
                    continue;
                }
                else if (currentCharacter == "\"")
                {
                    outputBuffer = outputBuffer + currentCharacter;
                    scanDelimiter = "\""; //set ScanDelimiter to double quote
                    continue;
                }
                else if (Data)
                {   //data flag set?
                    outputBuffer = outputBuffer + currentCharacter;
                    if (currentCharacter == ":")
                    {   //end of subline clear flags
                        //B829
                        IllegalToken = false;
                        Data = false;
                    }
                    continue;
                }
                else if (currentCharacter == "?")
                {   // print shortcut character
                    outputBuffer = outputBuffer + PrimaryTokenDictionary["PRINT"]; //save x87 to stream
                    continue;
                }
                else if (currentCharacter == "'")
                {   // single apostrophe
                    outputBuffer = outputBuffer + PrimaryTokenDictionary["'"]; //save x83 to stream
                    scanDelimiter = Constants.EOL; //scan to end of line
                    continue;
                }
                else if (currentCharacter == ";")
                {
                    outputBuffer = outputBuffer + currentCharacter;
                    continue;
                } else
                {
                    tokenPotential = tokenPotential + currentCharacter;
                    //see if token exists in tables
                    string tokenActual = ReplaceStringWithToken(tokenPotential);
                    if (tokenActual.Length > 0)
                    {
                        if (tokenActual == PrimaryTokenDictionary["ELSE"])
                        {
                            outputBuffer = outputBuffer + ":";
                        }
                        else if (tokenActual == PrimaryTokenDictionary["DATA"])
                        {
                            Data = true;
                        }
                        else if (tokenActual == PrimaryTokenDictionary["REM"])
                        {
                            scanDelimiter = Constants.EOL; //scan to end of line
                        }
                        else
                        {
                            IllegalToken = false;
                            Data = false;
                        }
                        tokenPotential = "";
                        tokenActual = "";
                        outputBuffer = outputBuffer + tokenActual;
                        continue;
                    }
                }
            }
            return outputBuffer;
        }

        private string ReplaceStringWithToken(string s)
        {
            string returnValue = "";
            if (PrimaryTokenDictionary.ContainsKey(s))
            {
                returnValue = PrimaryTokenDictionary[s];
            }
            if (SecondaryTokenDictionary.ContainsKey(s))
            {
                returnValue = "\xFF" +  SecondaryTokenDictionary[s];
            }
            return returnValue;
        }

        private void LoadPrimaryTokenDictionary()
        {
            PrimaryTokenDictionary.Add("FOR", "\x80");
            PrimaryTokenDictionary.Add("GO", "\x81");
            PrimaryTokenDictionary.Add("REM", "\x82");
            PrimaryTokenDictionary.Add("'", "\x83");
            PrimaryTokenDictionary.Add("ELSE", "\x84");
            PrimaryTokenDictionary.Add("IF", "\x85");
            PrimaryTokenDictionary.Add("DATA", "\x86");
            PrimaryTokenDictionary.Add("PRINT", "\x87");
            PrimaryTokenDictionary.Add("ON", "\x88");
            PrimaryTokenDictionary.Add("INPUT", "\x89");
            PrimaryTokenDictionary.Add("END", "\x8A");
            PrimaryTokenDictionary.Add("NEXT", "\x8B");
            PrimaryTokenDictionary.Add("DIM", "\x8C");
            PrimaryTokenDictionary.Add("READ", "\x8D");
            PrimaryTokenDictionary.Add("RUN", "\x8E");
            PrimaryTokenDictionary.Add("RESTORE", "\x8F");
            PrimaryTokenDictionary.Add("RETURN", "\x90");
            PrimaryTokenDictionary.Add("STOP", "\x91");
            PrimaryTokenDictionary.Add("POKE", "\x92");
            PrimaryTokenDictionary.Add("CONT", "\x93");
            PrimaryTokenDictionary.Add("LIST", "\x94");
            PrimaryTokenDictionary.Add("CLEAR", "\x95");
            PrimaryTokenDictionary.Add("NEW", "\x96");
            PrimaryTokenDictionary.Add("CLOAD", "\x97");
            PrimaryTokenDictionary.Add("CSAVE", "\x98");
            PrimaryTokenDictionary.Add("OPEN", "\x99");
            PrimaryTokenDictionary.Add("CLOSE", "\x9A");
            PrimaryTokenDictionary.Add("LLIST", "\x9B");
            PrimaryTokenDictionary.Add("SET", "\x9C");
            PrimaryTokenDictionary.Add("RESET", "\x9D");
            PrimaryTokenDictionary.Add("CLS", "\x9E");
            PrimaryTokenDictionary.Add("MOTOR", "\x9F");
            PrimaryTokenDictionary.Add("SOUND", "\xA0");
            PrimaryTokenDictionary.Add("AUDIO", "\xA1");
            PrimaryTokenDictionary.Add("EXEC", "\xA2");
            PrimaryTokenDictionary.Add("SKIPF", "\xA3");
            PrimaryTokenDictionary.Add("TAB(", "\xA4");
            PrimaryTokenDictionary.Add("TO", "\xA5");
            PrimaryTokenDictionary.Add("SUB", "\xA6");
            PrimaryTokenDictionary.Add("THEN", "\xA7");
            PrimaryTokenDictionary.Add("NOT", "\xA8");
            PrimaryTokenDictionary.Add("STEP", "\xA9");
            PrimaryTokenDictionary.Add("OFF", "\xAA");
            PrimaryTokenDictionary.Add("+", "\xAB");
            PrimaryTokenDictionary.Add("-", "\xAC");
            PrimaryTokenDictionary.Add("*", "\xAD");
            PrimaryTokenDictionary.Add("/", "\xAE");
            PrimaryTokenDictionary.Add("^", "\xAF");
            PrimaryTokenDictionary.Add("AND", "\xB0");
            PrimaryTokenDictionary.Add("OR", "\xB1");
            PrimaryTokenDictionary.Add(">", "\xB2");
            PrimaryTokenDictionary.Add("=", "\xB3");
            PrimaryTokenDictionary.Add("<", "\xB4");
        }

        private void LoadSecondaryTokenDictionary()
        {
            SecondaryTokenDictionary.Add("SIGN", "\x80");
            SecondaryTokenDictionary.Add("INT", "\x81");
            SecondaryTokenDictionary.Add("ABS", "\x82");
            SecondaryTokenDictionary.Add("USR", "\x83");
            SecondaryTokenDictionary.Add("RND", "\x84");
            SecondaryTokenDictionary.Add("SIN", "\x85");
            SecondaryTokenDictionary.Add("PEEK", "\x86");
            SecondaryTokenDictionary.Add("LEN", "\x87");
            SecondaryTokenDictionary.Add("STR$", "\x88");
            SecondaryTokenDictionary.Add("VAL", "\x89");
            SecondaryTokenDictionary.Add("ASC", "\x8A");
            SecondaryTokenDictionary.Add("CHR$", "\x8B");
            SecondaryTokenDictionary.Add("EOF", "\x8C");
            SecondaryTokenDictionary.Add("JOYSTK", "\x8D");
            SecondaryTokenDictionary.Add("LEFT$", "\x8E");
            SecondaryTokenDictionary.Add("RIGHT$", "\x8F");
            SecondaryTokenDictionary.Add("MID$", "\x90");
            SecondaryTokenDictionary.Add("POINT", "\x91");
            SecondaryTokenDictionary.Add("INKEY$", "\x92");
            SecondaryTokenDictionary.Add("MEM", "\x93");
        }

    }



    
}
