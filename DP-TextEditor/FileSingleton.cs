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
        public bool exists = false;
        public static FileSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FileSingleton();
            }
            return _instance;
        }

        public string GetData()
        {
            return _data.Value;
        }

        public string GetFilePath()
        {
            return path;
        }

        public void Save()
        {
            Save(path);
        }

        public void Save(string filepath)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream _file = System.IO.File.Create(filepath);
            bf.Serialize(_file, _data);
            _file.Close();
        }

        public String Undo()
        {
            _data.History.Pop();
            _data.Value = _data.History.Peek();
            return _data.Value;
        }

        public void Open(string fileName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream _file = System.IO.File.Open(fileName, FileMode.Open);
            _data = (File)bf.Deserialize(_file);
            exists = true;
        }

        public void AddAction(string input)
        {
            _data.Value = input;
            _data.History.Push(input);
        }
    }
}