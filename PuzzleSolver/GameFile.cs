using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PuzzleSolver
{
    class GameFile
    {
        public BlockGrid Grid
        {
            get { return _grid;}
        }

        public bool Complete;

        public Solver Solver;

        public string LoadedFile
        {
            get { return _fileName; }
        }

        private string _fileName;
        private BlockGrid _grid;

        
        public GameFile(string fileName)
        {
            _fileName = fileName;

            _grid = new BlockGrid();

            Solver = new Solver(this);

            Complete = false;

            Load();
        }

        public void Load()
        {
            _grid.Load(_fileName);
        }

        public void Save()
        {
            _grid.Save(_fileName);
        }
    }
}
