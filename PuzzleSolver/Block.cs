using System;
using System.Collections.Generic;
using System.Text;

namespace PuzzleSolver
{
    /// <summary>
    /// Represents one block location on a grid for Sudoku and a few other games.
    /// </summary>
    class Block
    {
        /// <summary>
        /// All the possible numbers that can remain for this Block.
        /// </summary>
        public List<int> Values
        {
            get { return _values; }
        }

        /// <summary>
        /// The index in the BlockGrid for this Block.
        /// </summary>
        public int Index
        {
            get { return _index; }
        }

        /// <summary>
        /// The number of possibilities for this Block.
        /// </summary>
        public int Choices
        {
            get { return _values.Count; }
        }

        /// <summary>
        /// The group this Block is in.
        /// </summary>
        public int GroupNumber
        {
            get { return _groupNumber; }
            set { _groupNumber = value; }
        }


        private List<int> _values;
        private int _groupNumber;
        private int _index;

        public Block(int index)
        {
            _values = new List<int>();
            _groupNumber = -1;
            _index = index;
        }

        public Block(int index, int[] values)
        {
            _values = new List<int>();
            Add(values);
            _groupNumber = -1;
            _index = index;
        }

        public Block(int index, int groupNumber)
        {
            _values = new List<int>();
            _groupNumber = groupNumber;
            _index = index;
        }

        public Block(int index, int[] values, int groupNumber)
        {
            _values = new List<int>();
            Add(values);
            _groupNumber = groupNumber;
            _index = index;
        }

        /// <summary>
        /// Adds an array of values to the choices for this Block.
        /// </summary>
        /// <param name="values">The choices to be added.</param>
        public void Add(int[] values)
        {
            foreach (int value in values)
            {
                _values.Add(value);
            }
        }

        /// <summary>
        /// Adds a value to the choices for this Block.
        /// </summary>
        /// <param name="value">A choice to be added.</param>
        public void Add(int value)
        {
            _values.Add(value);
        }
    }
}
