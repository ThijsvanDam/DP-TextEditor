using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace DP_TextEditor
{
    public class FileSingleton
    {
        private static FileSingleton _instance = new FileSingleton();
        private File _data = new File();
        private FileStream _fileStream;

        private bool _savedEverything = true;

        public static FileSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FileSingleton();
            }
            return _instance;
        }

        public bool Exists()
        {
            return _fileStream != null;
        }

        public string GetData()
        {
            return _data.Value;
        }

        public string GetFilePath()
        {
            return _fileStream.Name;
        }

        public void CloseStream()
        {
            _fileStream.Close();
        }

        public void Save()
        {
            Save(_fileStream.Name);
        }

        public bool GetSaved()
        {
            return _savedEverything;
        }

        public void ResetFile()
        {
            _fileStream = null;
            _data.Value = null;
            _data.History = new Stack<string>();
        }

        public void Save(string filepath)
        {
            _fileStream?.Close();
            var bf = new BinaryFormatter();
            _fileStream = System.IO.File.Create(filepath);
            bf.Serialize(_fileStream, _data);
            _savedEverything = true;
        }

        public String Undo()
        {
            if (_data.History.Count > 1)
            {
                _data.History.Pop();
                _data.Value = _data.History.Peek();
                _savedEverything = true;
                return _data.Value;
            }
            return "";
        }

        public void Open(string fileName)
        {
            var bf = new BinaryFormatter();
            _fileStream = System.IO.File.Open(fileName, FileMode.Open);
            _data = (File)bf.Deserialize(_fileStream);
        }

        public void AddAction(string input)
        {
            if (input != "")
            {
                if (_data.Value != input)
                {
                    _savedEverything = false;
                    _data.Value = input;
                    _data.History.Push(input);
                    Console.WriteLine(input);
                }
            }
        }
    }
}