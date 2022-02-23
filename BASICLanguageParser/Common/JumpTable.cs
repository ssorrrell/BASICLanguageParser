using System;
using System.Collections.Generic;

namespace BASICLanguageParser.Common
{
    /**
     * Line number references; Gotos, Gosubs, Returns
     **/
    public class JumpTable
    {
        private Dictionary<int, JumpDefinition> JumpDefinitions = new Dictionary<int, JumpDefinition>();

        public void AddJump(int LineNumber, string TestExpression, JumpType jumpType, List<int> JumpList)
        {
            JumpDefinition item = new JumpDefinition();
            item.LineNumber = LineNumber;
            if (JumpDefinitions.ContainsKey(LineNumber))
                throw new Exception("Line Already Defined in JumpTable");
            item.TestExpression = TestExpression;
            item.JumpType = jumpType;
            item.Destinations = JumpList;
            JumpDefinitions.Add(LineNumber, item);
        }

        public void RemoveJump(int Jump)
        {
            if (!JumpDefinitions.ContainsKey(Jump))
                throw new Exception("Line Not Defined in JumpTable");
            JumpDefinitions.Remove(Jump);
        }

        public JumpDefinition Get(int Jump)
        {
            if (JumpDefinitions.ContainsKey(Jump))
                return JumpDefinitions[Jump];
            return null;
        }
    }

    //Jump Info like size and type
    public class JumpDefinition
    {
        public int LineNumber { get; set; }
        public JumpType JumpType { get; set; }
        public string TestExpression { get; set; }
        public List<int> Destinations { get; set; }
    }

    //Data Type
    public enum JumpType
    {
        IfThen,
        IfElse,
        OnGosub,
        OnGoto,
        Goto,
        GoSub,
        Return
    }

}
