using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PuzzleSolver
{
    class Settings
    {
        public string AutoLoadFile
        {
            get { return _autoLoadFile; }
        }

        private string _settingFileName = "settings.dat";
        private string _autoLoadFile;

        public Settings()
        {
            Load();
        }

        public void Load()
        {
            try
            {
                StreamReader sr = new StreamReader(File.OpenRead(_settingFileName));

                _autoLoadFile = sr.ReadLine();

                sr.Close();
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message);
                MessageBox.Show("Unable to open file.");
            }
        }

        public void Save()
        {
            try
            {
                StreamWriter sw = new StreamWriter(File.OpenWrite(_settingFileName));

                sw.WriteLine(_autoLoadFile);

                sw.Close();
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message);
                MessageBox.Show("Unable to open file.");
            }
        }

    }
}
