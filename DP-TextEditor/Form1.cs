using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_TextEditor
{
    public partial class Form1 : Form
    {
        private readonly FileSingleton _currentFile;

        public Form1()
        {
            InitializeComponent();
            _currentFile = FileSingleton.GetInstance();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "" || openFileDialog.FileName != null)
            {
                _currentFile.Open(openFileDialog.FileName);
                tbInputField.Text = _currentFile.GetData();
            }
            Console.WriteLine(_currentFile.GetFilePath());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_currentFile.Exists())
            {
                _currentFile.Save(_currentFile.GetFilePath());
            }
            else
            {
                saveFileDialog.ShowDialog();
                _currentFile.Save(saveFileDialog.FileName);
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            tbInputField.Text = _currentFile.Undo();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tbInputField.Text = _currentFile.ResetFile();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_currentFile.GetSaved())
            {
                string messageBoxText = "Do you want to save your changes?";
                string caption = "DP-TextEditor";
                MessageBoxButtons button = MessageBoxButtons.YesNoCancel;
                MessageBoxIcon icon = MessageBoxIcon.Warning;
                DialogResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                switch (result)
                {
                    case DialogResult.Yes:
                        _currentFile.Save();
                        _currentFile.CloseStream();
                        break;
                    case DialogResult.No:
                        _currentFile.EmptyFile();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void tbInputField_KeyUp(object sender, KeyEventArgs e)
        {
            if (InputKey((int)e.KeyCode))
                _currentFile.AddAction(tbInputField.Text);
        }

        private void tbInputField_KeyDown(object sender, KeyEventArgs e)
        {
            if(InputKey((int)e.KeyCode))
                _currentFile.AddAction(tbInputField.Text);
        }

        private bool InputKey(int input)
        {
            if (input >= 96 && input <= 111 ||
                input >= 65 && input <= 90 ||
                input >= 48 && input <= 57 ||
                input == 8 ||
                input == 32 ||
                input == 46)
                return true;
            return false;
        }
    }
}
 