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
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_currentFile.exists)
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
        
        

        private void tbInputField_KeyPress(object sender, KeyPressEventArgs e)
        {
            _currentFile.AddAction(tbInputField.Text);
        }
    }
}
