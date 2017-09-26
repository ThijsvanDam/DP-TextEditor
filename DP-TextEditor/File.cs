using System;
using System.Collections.Generic;

namespace DP_TextEditor
{
    [Serializable]
    public class File
    {
        public String Value { get; set; }
        public Stack<String> History { get; set; } = new Stack<string>();

        public File()
        {
            
        }

        public File(string value, Stack<string> history)
        {
            Value = value;
            History = history;
        }
        
    }
}