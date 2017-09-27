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
        private File _previousVersion = new File("", new Stack<string>());
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
            return _data.GetValue();
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

        public void EmptyFile()
        {
            _fileStream = null;
            _data.SetValue(null);
            _data.SetHistory(new Stack<string>());
            _previousVersion.SetValue(null);
            _previousVersion.SetHistory(new Stack<string>());
        }

        public string ResetFile()
        {
            if (_data.GetValue() != null)
            {
                _data.SetValue(_previousVersion.GetValue());
                _data.SetHistory(_previousVersion.GetHistory());
                return _data.GetValue();
            }
            return "";
        }

        public void Save(string filepath)
        {
            _fileStream?.Close();
            _previousVersion = new File(_data.GetValue(), _data.GetHistory());
            var bf = new BinaryFormatter();
            _fileStream = System.IO.File.Create(filepath);
            bf.Serialize(_fileStream, _data);
            _savedEverything = true;
        }

        public string Undo()
        {
            if (_data.CountHistory() > 1)
            {
                _data.HistoryPop();
                _data.SetValue(_data.HistoryPeek());
                _savedEverything = true;
                return _data.GetValue();
            }
            return "";
        }

        public void Open(string fileName)
        {
            var bf = new BinaryFormatter();
            _fileStream = System.IO.File.Open(fileName, FileMode.Open);
            _data = (File)bf.Deserialize(_fileStream);
            _previousVersion = new File(_data.GetValue(), _data.GetHistory());
        }

        public void AddAction(string input)
        {
            if (input != "")
            {
                if (_data.GetValue() != input)
                {
                    _savedEverything = false;
                    _data.SetValue(input);
                    _data.HistoryPush(input);
                }
            }
        }
    }
}