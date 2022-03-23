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

        private int inputPointer = 0;
        private string inputBuffer = "";
        private string outputBuffer = "";
        private string currentCharacter = "";
        private string nextCharacter = "";
        private bool IllegalToken = false;
        private bool Data = false;
        private bool doSubLineCheck = false;
        private bool doSpaceCheck = false;

        public Cruncher()
        {
            constants = new Constants();
            LoadPrimaryTokenDictionary();
            LoadSecondaryTokenDictionary();
        }

        public string CrunchOneLine(string inputBuffer) //main loop
        {
            string scanDelimiter = "";
            //string outputBuffer = "";
            string tokenPotential = "";
            this.inputBuffer = inputBuffer; //save input
            this.outputBuffer = ""; //clear output

            //B82D get character
            for (inputPointer = 0; inputPointer < inputBuffer.Length; inputPointer++)
            {
                bool foundEOF;
                currentCharacter = inputBuffer[inputPointer].ToString();  //B82D
                if ((inputPointer + 1) < inputBuffer.Length)
                    nextCharacter = inputBuffer[inputPointer + 1].ToString();

                if (currentCharacter == "\r" && nextCharacter == "\n")
                {   //EOL breakout of loop
                    outputBuffer = outputBuffer + "\r\n";
                    break;
                }

                if (doSubLineCheck)
                {
                    if (CheckForEndOfSubline()) //if subline, reset flags, save character, loop - B829
                    {
                        doSubLineCheck = false;
                        SaveCharacter();
                        continue;
                    }
                }

                if (IllegalToken)
                {
                    //B844 - space processing
                    doSpaceCheck = true;
                }
                //check for upper case, go to B852 if not
                if ((int)currentCharacter[0] >= (int)'A' && (int)currentCharacter[0] <= (int)'Z')
                {
                    doSpaceCheck = true;
                }
                else
                {
                    if ((int)currentCharacter[0] < (int)'0')
                    {   //characters below '0'
                        IllegalToken = false; //B842
                        doSpaceCheck = true;
                    }
                    else if ((int)currentCharacter[0] <= (int)'9')
                    {   //characters below or equal to '9'  0..9
                        SaveCharacter(); //B852
                        doSubLineCheck = true;
                        continue;
                    }
                }
                //if (doSpaceCheck) //B844  - space processing
                //{
                    if (currentCharacter == " ")
                    {   //don't crunch spaces
                        SaveCharacter(); //B852
                        doSubLineCheck = true;
                        continue;
                    }
                    scanDelimiter = currentCharacter;
                    if (currentCharacter == "\"") //B886
                    {
                        int result = ScanTillDelimiter(scanDelimiter);
                        if (result == 1)
                        {   //found delimiter
                            continue;
                        }
                        if (result == 2)
                        {   //EOL
                            outputBuffer = outputBuffer + "\r\n"; //B85C
                            break;
                        }
                        if (result == 3)
                        {   //EOF
                            break;
                        }
                        doSubLineCheck = true;
                        continue;
                    }
                    if (Data) //B84D - not marked
                    {   //data flag set?
                        //outputBuffer = outputBuffer + currentCharacter;
                        int result = ScanTillDelimiter(scanDelimiter);
                        if (result == 1)
                        {   //found delimiter
                            continue;
                        }
                        if (result == 2)
                        {   //EOL
                            outputBuffer = outputBuffer + "\r\n"; //B85C
                            break;
                        }
                        if (result == 3)
                        {   //EOF
                            break;
                        }
                        doSubLineCheck = true;
                        continue;
                    }
                    if ()
                    SaveCharacter(); //B852
                    doSubLineCheck = true;
                    if (currentCharacter == "?") //B86B
                    {   // print shortcut character
                        outputBuffer = outputBuffer + PrimaryTokenDictionary["PRINT"]; //save x87 to stream
                        continue; //note: doesn't save current character and loops again
                    }
                    if (currentCharacter == "\'") //B873
                    {   //REM processing
                        outputBuffer = outputBuffer + PrimaryTokenDictionary["\'"]; //save x83 to stream
                        ProcessRemToken();
                        break;
                    }
                    if ((int)currentCharacter[0] > (int)'0' && (int)currentCharacter[0] < (int)'<') //B88A
                    {   //gets less than ASCII <
                        // 0-9 : ; 
                        SaveCharacter(); //B852
                        doSubLineCheck = true;
                        continue;
                    }
                    tokenPotential = tokenPotential + currentCharacter; //B892
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
                        else
                        {
                            IllegalToken = false;
                            Data = false;
                        }
                        outputBuffer = outputBuffer + tokenActual;
                        if (tokenActual == PrimaryTokenDictionary["REM"])
                        {
                            ProcessRemToken();
                            break;
                        }
                        tokenPotential = "";
                        tokenActual = "";
                        continue;
                    }
                //}
            }
            //outputBuffer = outputBuffer + Constants.EOL;
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

        private void SaveCharacter()
        {
            outputBuffer = outputBuffer + currentCharacter; //save character
        }

        private void MovePointerBackOne()
        {
            inputPointer--; //decrement pointer
            currentCharacter = inputBuffer[inputPointer].ToString();
            if ((inputPointer + 1) < inputBuffer.Length)
                nextCharacter = inputBuffer[inputPointer + 1].ToString();
        }

        private bool IncrementPointer()
        {
            inputPointer++; //increment pointer
            if (inputPointer < inputBuffer.Length) //check EOF
            {
                currentCharacter = inputBuffer[inputPointer].ToString();
                if ((inputPointer + 1) < inputBuffer.Length)
                    nextCharacter = inputBuffer[inputPointer + 1].ToString();
            }
            else
            {
                return true; //EOF found
            }
            return false; //no EOF on the next character
        }

        private bool SaveCharacterIncrementPointer()
        {
            SaveCharacter(); //save character
            return IncrementPointer();
        }

        private int ScanTillDelimiter(string scanDelimiter)
        {
            bool foundEOF = SaveCharacterIncrementPointer();
            while (currentCharacter != scanDelimiter && (currentCharacter + nextCharacter) != Constants.EOL && !foundEOF)
            { //scan thru till delimiter or EOL or EOF  //B87E
                foundEOF = SaveCharacterIncrementPointer();
            }
            if ((currentCharacter + nextCharacter) == Constants.EOL)
                return 2; //EOL found
            if (inputPointer >= inputBuffer.Length)
                return 3; //EOF found
            MovePointerBackOne();
            return 1; //delimiter found
        }

        private bool CheckForEndOfSubline()
        {
            if (currentCharacter == ":") //B852
            {   //end of subline clear flags
                //B829
                IllegalToken = false;
                Data = false;
                return true;
            }
            return false;
        }

        private int ProcessRemToken()
        {
            bool foundEOF = IncrementPointer();
            int result = 3;
            if (!foundEOF)
            {
                result = ScanTillDelimiter("\r"); //set ScanDelimiter to EOL
                if (result == 1 || result == 2)
                {   //found delimiter || EOL
                    outputBuffer = outputBuffer + "\r\n";
                }
                if (result == 3)
                {   //EOF
                }
            }
            return result;
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
