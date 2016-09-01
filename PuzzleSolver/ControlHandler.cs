using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleSolver
{
    class ControlHandler
    {
        private GameFile _game;
        private int _selectedIndex;
        private int _lastChangedIndex;

        public ControlHandler(GameFile game)
        {
            _selectedIndex = 0;
            _lastChangedIndex = -1;

            _game = game;
        }

        public void Select(int column, int row)
        {
            _selectedIndex = _game.Grid.ToIndex(column, row);
        }

        public Block GetSelected()
        {
            return _game.Grid[_selectedIndex];
        }

        public void SelectChanged(int column, int row)
        {
            _lastChangedIndex = _game.Grid.ToIndex(column, row);
        }

        public Block GetSelectedChanged()
        {
            return _game.Grid[_lastChangedIndex];
        }
    }
}
