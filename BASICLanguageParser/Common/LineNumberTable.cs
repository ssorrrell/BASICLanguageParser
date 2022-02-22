using System;
using System.Collections.Generic;

namespace BASICLanguageParser.Common
{
    /**
     * Classes and Enums to reproduce the Variable Table that the BASIC interpreter maintains
     **/
    public class LineNumberTable
    {
        private Dictionary<int, LineNumberDefinition> lineNumberDefinitions = new Dictionary<int, LineNumberDefinition>();

        public void AddLineNumber(int lineNumber, int length)
        {
            LineNumberDefinition item = new LineNumberDefinition();
            item.Length = length;
            if (lineNumberDefinitions.ContainsKey(lineNumber))
                throw new Exception("Line Already Defined in LineNumberTable");
            lineNumberDefinitions.Add(lineNumber, item);
        }

        public void RemoveLineNumber(int lineNumber)
        {
            if (!lineNumberDefinitions.ContainsKey(lineNumber))
                throw new Exception("Line Not Defined in LineNumberTable");
            lineNumberDefinitions.Remove(lineNumber);
        }

        public LineNumberDefinition Get(int lineNumber)
        {
            if (lineNumberDefinitions.ContainsKey(lineNumber))
                return lineNumberDefinitions[lineNumber];
            return null;
        }
    }

    //LineNumber Info like size and type
    public class LineNumberDefinition
    {
        public int LineNumber { get; set; }
        public int Length { get; set; }
    }

}
