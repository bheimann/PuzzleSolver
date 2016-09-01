using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PuzzleSolver
{
    class BlockGrid
    {
        public int NumberOfColumns
        {
            get { return _numCols; }
            set { _numCols = value; }
        }

        public int NumberOfRows
        {
            get { return _numRows; }
            set { _numRows = value; }
        }

        public int TotalBlockCount
        {
            get { return _blocks.Count; }
        }

        /// <summary>
        /// Gets a block from a specific index.
        /// </summary>
        /// <param name="index">The index of the Block to get.</param>
        /// <returns>A Block retrieved by its index.</returns>
        public Block this[int index]
        {
            get { return GetBlockByIndex(index); }
        }

        /// <summary>
        /// Gets a block from a specific column and row.
        /// </summary>
        /// <param name="column">The column this Block is in.</param>
        /// <param name="row">The row this Block is in.</param>
        /// <returns>A Block in the specified row and column.</returns>
        public Block this[int column, int row]
        {
            get { return GetBlock(column, row); }
        }


        private List<Block> _blocks;
        private List<Group> _groups;
        private int _numCols;
        private int _numRows;
        private int _numGroups;

        /// <summary>
        /// Initalize to a grid of size 1 by 1.
        /// </summary>
        public BlockGrid()
        {
            _numCols = 1;
            _numRows = 1;

            _blocks = new List<Block>();
            _groups = new List<Group>();
        }

        /// <summary>
        /// Loads a file in the format: 
        /// 
        /// #NumRows #NumCols
        /// #FirstValueForBox1 #NextValueForBox1 ... #LastValueForBox1
        /// #FirstValueForBox2 #NextValueForBox2 ... #LastValueForBox2
        /// ...
        /// #FirstValueForBoxN #NextValueForBoxN ... #LastValueForBoxN
        /// #Group1Index1 #Group1Index2 ... #Group1Index1
        /// #Group2Index1 #Group2Index2 ... #Group2Index1
        /// ...
        /// #GroupNIndex1 #GroupNIndex2 ... #GroupNIndex1
        /// 
        /// </summary>
        /// <param name="fileName">The name of the file to load.</param>
        public void Load(string fileName)
        {
            try
            {
                StreamReader sr = new StreamReader(File.OpenRead(fileName));

                string line = sr.ReadLine();
                string[] values = line.Split(' ');

                _numCols = Int32.Parse(values[0]);
                _numRows = Int32.Parse(values[1]);
                _numGroups = Int32.Parse(values[2]);

                line = sr.ReadLine();

                _blocks.Clear();
                int totalNumBoxes = _numCols * _numRows;
                int [] defaultArray = {1, 2, 3, 4, 5, 6, 7, 8, 9};
                while (line != null && totalNumBoxes > _blocks.Count)
                {
                    Block block = new Block(_blocks.Count);
                    if (line != "")
                    {
                        values = line.Trim().Split(' ');
                        foreach (string value in values)
                        {
                            block.Add(Int32.Parse(value));
                        }
                    }
                    else
                    {
                        block.Add(defaultArray);
                    }

                    _blocks.Add(block);

                    line = sr.ReadLine();
                }

                _groups.Clear();
                // Load all the group numbers
                for (int groupNum = 0; groupNum < _numGroups; groupNum++)
                {
                    Group group = new Group(groupNum);
                    values = line.Trim().Split(' ');
                    foreach (string value in values)
                    {
                        Block block = _blocks[Int32.Parse(value)];
                        block.GroupNumber = groupNum;
                        group.Add(block);
                    }
                    _groups.Add(group);

                    line = sr.ReadLine();
                }

                sr.Close();

                if (totalNumBoxes != _blocks.Count)
                {
                    throw new FormatException("The file does not have the correct number of lines.");
                }
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message);
                MessageBox.Show("Unable to open file.");
            }
            catch (FormatException fe)
            {
                Console.WriteLine(fe.Message);
                MessageBox.Show("File not formatted correctly.");
            }
        }

        /// <summary>
        /// Saves a file in the format:
        /// 
        /// #NumRows #NumCols
        /// #FirstValueForBox1 #NextValueForBox1 ... #LastValueForBox1
        /// #FirstValueForBox2 #NextValueForBox2 ... #LastValueForBox2
        /// ...
        /// #FirstValueForBoxN #NextValueForBoxN ... #LastValueForBoxN
        /// #Group1Index1 #Group1Index2 ... #Group1Index1
        /// #Group2Index1 #Group2Index2 ... #Group2Index1
        /// ...
        /// #GroupNIndex1 #GroupNIndex2 ... #GroupNIndex1
        /// 
        /// </summary>
        /// <param name="fileName">The name of the file to save.</param>
        public void Save(string fileName)
        {
            try
            {
                StreamWriter sw = new StreamWriter(File.OpenWrite(fileName));

                sw.WriteLine("{0} {1} {2}", _numCols, _numRows, _numGroups);

                foreach (Block block in _blocks)
                {
                    string tempBuffer = string.Empty;
                    foreach (int item in block.Values)
                        tempBuffer += item + " ";
                    sw.WriteLine(tempBuffer.Trim());
                }

                foreach (Group group in _groups)
                {
                    string tempBuffer = string.Empty;
                    foreach (Block block in group.Members)
                    {
                        tempBuffer += block.Index + " ";
                    }
                    sw.WriteLine(tempBuffer.Trim());
                }

                sw.Close();
            }
            catch (IOException ioe)
            {
                Console.WriteLine(ioe.Message);
                MessageBox.Show("Unable to open file.");
            }
        }

        public Block GetBlockByIndex(int index)
        {
            if (index < 0)
            {
                return null;
            }
            else
            {
                return _blocks[index];
            }
        }

        public Group GetGroup(int groupNumber)
        {
            return _groups[groupNumber];
        }

        public int GetRowByIndex(int index)
        {
            return index / _numRows;
        }

        public int GetColumnByIndex(int index)
        {
            return index % _numCols;
        }

        public Block GetBlock(int column, int row)
        {
            return _blocks[ToIndex(column, row)];
        }

        public int ToIndex(int column, int row)
        {
            return column + row * _numCols;
        }
    }
}
