using System;
using System.Collections.Generic;
using System.Configuration;

namespace DP_TextEditor
{
    [Serializable]
    public class File
    {
        private String Value { get; set; }
        private Stack<String> History { get; set; } = new Stack<string>();

        public File()
        {
            
        }

        public File(string value, Stack<string> history)
        {
            Value = value;
            History = history;
        }

        public string GetValue()
        {
            return Value;
        }

        public void SetValue(string value)
        {
            Value = value;
        }

        public void SetHistory(Stack<string> history)
        {
            History = history;
        }

        public string HistoryPop()
        {
            return History.Pop();
        }
        
        public string HistoryPeek()
        {
            return History.Peek();
        }

        public void HistoryPush(string input)
        {
            History.Push(input);
        }

        public int CountHistory()
        {
            return History.Count;
        }

        public Stack<string> GetHistory()
        {
            return History;
        }
    }
}