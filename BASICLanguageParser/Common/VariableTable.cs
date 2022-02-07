using System;
using System.Collections.Generic;

namespace BASICLanguageParser.Common
{
    /**
     * Classes and Enums to reproduce the Variable Table that the BASIC interpreter maintains
     **/
    public class VariableTable
    {
        private Dictionary<string, VariableDefinition> variableDefinitions = new Dictionary<string, VariableDefinition>();

        public void AddVariable(string name, VariableType variableType, int arrayDimensions, int[] arraySize, string stringValue, float floatValue, int lineNumberDim)
        {
            VariableDefinition item = new VariableDefinition();
            item.Name = name;
            if (variableDefinitions.ContainsKey(name))
                throw new Exception("Variable Already Defined in VariableTable");
            item.VariableType = variableType;
            if (variableType == VariableType.StringArray ||
                variableType == VariableType.NumberArray)
            {
                item.ArrayDimensions = arrayDimensions;
                item.ArraySize = arraySize;
            }
            if (variableType == VariableType.String  ||
                variableType == VariableType.StringArray)
                item.StringValue = stringValue;
            else if (variableType == VariableType.Number ||
                     variableType == VariableType.NumberArray)
                item.FloatValue = floatValue;
            item.LineNumberDim = lineNumberDim;
            variableDefinitions.Add(name, item);
        }

        public void RemoveVariable(string name)
        {
            if (!variableDefinitions.ContainsKey(name))
                throw new Exception("Variable Already Not Defined in VariableTable");
            variableDefinitions.Remove(name);
        }

        public VariableDefinition Get(string name)
        {
            if (variableDefinitions.ContainsKey(name))
                return variableDefinitions[name];
            return null;
        }

        public int GetDataSize()
        {
            //figure the number of variables, dimensions, string/number, etc.
            //return the number of bytes
            return 0;
        }
    }

    public class VariableDefinition
    {
        public string Name { get; set; }
        public VariableType VariableType { get; set; } // string, number, stringArray, numberArray
        public int ArrayDimensions { get; set; } // 1 or more dimension
        public int[] ArraySize { get; set; } // 0-based array size of each dimension
        public string StringValue { get; set; } //value if a string variable
        public float FloatValue { get; set; } //value if a number variable
        public int LineNumberDim { get; set; } //line number where this variable is defined; none is not dimmed

    }

    public enum VariableType
    {
        Number,
        String,
        NumberArray,
        StringArray
    }
}
