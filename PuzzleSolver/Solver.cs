using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PuzzleSolver
{
    public class Solver
    {
        private int col;
        private int row;
        private int _index;

        private GameFile _gameFile;

        public Solver(GameFile gameFile)
        {
            _gameFile = gameFile;
        }

        public void Solve(int index, SolveType solveType)
        {
            _index = index;
            col = _gameFile.Grid.GetColumnByIndex(_index);
            row = _gameFile.Grid.GetRowByIndex(_index);

            if ((solveType & SolveType.reduceByColumn) != 0)
            {
                SolveColumn();
            }
            if ((solveType & SolveType.reduceByRow) != 0)
            {
                SolveRow();
            }
            if ((solveType & SolveType.reduceByGroup) != 0)
            {
                SolveGroup();
            }
            if ((solveType & SolveType.eliminateColumn) != 0)
            {
                SolveColumnByElimination();
            }
            if ((solveType & SolveType.eliminateRow) != 0)
            {
                SolveRowByElimination();
            }
            if ((solveType & SolveType.eliminateGroup) != 0)
            {
                SolveGroupByElimination();
            }
            if ((solveType & SolveType.pairWise) != 0)
            {
                SolvePairs();
            }
        }

        private void SolveColumn()
        {
            // in this block, remove values that are the only value above this block in this column
            for (int i = 0; i < row; i++)
            {
                Block block = _gameFile.Grid[col, i];
                if (block.Choices == 1)
                {
                    _gameFile.Grid[_index].Values.Remove(block.Values[0]);
                }
            }
            // in this block, remove values that are the only value below this block in this column
            for (int i = row + 1; i < _gameFile.Grid.NumberOfColumns; i++)
            {
                Block block = _gameFile.Grid[col, i];
                if (block.Choices == 1)
                {
                    _gameFile.Grid[_index].Values.Remove(block.Values[0]);
                }
            }
        }

        private void SolveRow()
        {
            // in this block, remove values that are the only value to the left of this block in this row
            for (int i = 0; i < col; i++)
            {
                Block block = _gameFile.Grid[i, row];
                if (block.Choices == 1)
                {
                    _gameFile.Grid[_index].Values.Remove(block.Values[0]);
                }
            }
            // in this block, remove values that are the only value to the right of this block in this row
            for (int i = col + 1; i < _gameFile.Grid.NumberOfRows; i++)
            {
                Block block = _gameFile.Grid[i, row];
                if (block.Choices == 1)
                {
                    _gameFile.Grid[_index].Values.Remove(block.Values[0]);
                }
            }
        }

        private void SolveGroup()
        {
            //Check the group this Block is in, any other blocks that have a single value, remove from this block
            Group group = _gameFile.Grid.GetGroup(_gameFile.Grid[_index].GroupNumber);
            for (int i = 0; i < group.Members.Count; i++)
            {
                Block block = group.Members[i];
                if (block.Index == _gameFile.Grid[_index].Index)
                    continue;
                if (block.Choices == 1)
                {
                    _gameFile.Grid[_index].Values.Remove(block.Values[0]);
                }
            }
        }

        private void SolveColumnByElimination()
        {
            // Check the column for each number in this block to make sure these are in at least another block, if not, it is unique for this column, so it must be that number
            int colPos = col;
            for (int number = 1; number <= _gameFile.Grid.NumberOfColumns; number++)
            {
                Block foundBlock = null;
                bool foundTwice = false;
                for (int rowPos = 0; rowPos < _gameFile.Grid.NumberOfRows; rowPos++ )
                {
                    Block block = _gameFile.Grid[colPos, rowPos];
                    if (block.Values.Contains(number))
                    {
                        if (foundBlock != null)
                        {
                            foundTwice = true;
                            break;
                        }
                        foundBlock = block;
                    }
                }

                if (!foundTwice && foundBlock != null && foundBlock.Values.Count > 1)
                {
                    foundBlock.Values.Clear();
                    foundBlock.Values.Add(number);
                }
            }
        }

        private void SolveRowByElimination()
        {
            // Check the row for each number in this block to make sure these are in at least another block, if not, it is unique for this row, so it must be that number
            int rowPos = row;
            for (int number = 1; number <= _gameFile.Grid.NumberOfColumns; number++)
            {
                Block foundBlock = null;
                bool foundTwice = false;
                for (int colPos = 0; colPos < _gameFile.Grid.NumberOfRows; colPos++)
                {
                    Block block = _gameFile.Grid[colPos, rowPos];
                    if (block.Values.Contains(number))
                    {
                        if (foundBlock != null)
                        {
                            foundTwice = true;
                            break;
                        }
                        foundBlock = block;
                    }
                }

                if (!foundTwice && foundBlock != null && foundBlock.Values.Count > 1)
                {
                    foundBlock.Values.Clear();
                    foundBlock.Values.Add(number);
                }
            }
        }

        private void SolveGroupByElimination()
        {
            // Check the group for each number in this block to make sure these are in at least another block, if not, it is unique for this group, so it must be that number
            int groupID = _gameFile.Grid[_index].GroupNumber;
            Group group = _gameFile.Grid.GetGroup(groupID);
            int numberInGroup = group.Members.Count;
            for (int number = 1; number <= numberInGroup; number++)
            {
                Block foundBlock = null;
                bool foundTwice = false;
                foreach (Block block in group.Members)
                {
                    if (block.Values.Contains(number))
                    {
                        if (foundBlock != null)
                        {
                            foundTwice = true;
                            break;
                        }
                        foundBlock = block;
                    }
                }

                if (!foundTwice && foundBlock != null && foundBlock.Values.Count > 1)
                {
                    foundBlock.Values.Clear();
                    foundBlock.Values.Add(number);
                }
            }
        }

        private void SolvePairs()
        {
            Block block = _gameFile.Grid[_index];
            Block compareBlock = null;

            // The location of the row and column we are temporarly looking at for compareBlock
            int rowPos;
            int colPos;
            // Tells if a match was found
            bool matchFound = false;

            // Comparing all in the row first
            rowPos = row;
            for (colPos = 0; colPos < _gameFile.Grid.NumberOfRows; colPos++)
            {
                if (colPos == col)
                {
                    continue;
                }

                compareBlock = _gameFile.Grid[colPos, rowPos];
                // Only tests blocks with 2 matches
                if (compareBlock.Choices == 2 && block.Choices == 2)
                {
                    // Set to true for assuming there will be a match, change to false as soon as we find a mismatch.
                    matchFound = true;
                    foreach (int comaprerItem in compareBlock.Values)
                    {
                        if (!block.Values.Contains(comaprerItem))
                        {
                            matchFound = false;
                            break;
                        }
                    }
                }

                if (matchFound)
                {
                    // If an actual match was found, remove all the values from this match
                    ReduceByPair(col, row, block.GroupNumber, colPos, rowPos, compareBlock.GroupNumber);
                    matchFound = false;
                }
            }

            // Compare all in the column next
            colPos = col;
            for (rowPos = 0; rowPos < _gameFile.Grid.NumberOfRows; rowPos++)
            {
                if (rowPos == row)
                {
                    continue;
                }

                compareBlock = _gameFile.Grid[colPos, rowPos];
                // Only tests blocks with 2 matches
                if (compareBlock.Choices == 2 && block.Choices == 2)
                {
                    // Set to true for assuming there will be a match, change to false as soon as we find a mismatch.
                    matchFound = true;
                    foreach (int comaprerItem in compareBlock.Values)
                    {
                        if (!block.Values.Contains(comaprerItem))
                        {
                            matchFound = false;
                            break;
                        }
                    }
                }

                if (matchFound)
                {
                    // If an actual match was found, remove all the values from this match
                    ReduceByPair(col, row, block.GroupNumber, colPos, rowPos, compareBlock.GroupNumber);
                    matchFound = false;
                }
            }

            // Compare all in the group last
            int groupID = _gameFile.Grid[_index].GroupNumber;
            Group group = _gameFile.Grid.GetGroup(groupID);
            int numberInGroup = group.Members.Count;
            foreach (Block checkblock in group.Members)
            {
                if (checkblock == block)
                {
                    continue;
                }

                //compareBlock = _gameFile.Grid[colPos, rowPos];
                // Only tests blocks with 2 matches
                if (checkblock.Choices == 2 && block.Choices == 2)
                {
                    // Set to true for assuming there will be a match, change to false as soon as we find a mismatch.
                    matchFound = true;
                    foreach (int comaprerItem in checkblock.Values)
                    {
                        if (!block.Values.Contains(comaprerItem))
                        {
                            matchFound = false;
                            break;
                        }
                    }
                }

                if (matchFound)
                {
                    // If an actual match was found, remove all the values from this match
                    ReduceByPair(col, row, groupID, _gameFile.Grid.GetColumnByIndex(checkblock.Index), _gameFile.Grid.GetRowByIndex(checkblock.Index), groupID);
                    matchFound = false;
                }
            }
        }

        private void ReduceByPair(int col1, int row1, int group1, int col2, int row2, int group2)
        {
            int colPos = col1;
            int rowPos = row1;

            List<int> removeList = _gameFile.Grid[col1, row1].Values;

            if (col1 == col2)
            {
                // They are in the same column, remove all from the column they are in
                foreach (int removeItem in removeList)
                {
                    for (rowPos = 0; rowPos < _gameFile.Grid.NumberOfRows; rowPos++)
                    {
                        if (rowPos == row1 || rowPos == row2)
                        {
                            continue;
                        }
                        _gameFile.Grid[colPos, rowPos].Values.Remove(removeItem);
                    }
                }
            }

            if (row1 == row2)
            {
                // They are in the same row, remove all from the row they are in that match from either of these
                foreach (int removeItem in removeList)
                {
                    for (colPos = 0; colPos < _gameFile.Grid.NumberOfRows; colPos++)
                    {
                        if (colPos == col1 || colPos == col2)
                        {
                            continue;
                        }
                        _gameFile.Grid[colPos, rowPos].Values.Remove(removeItem);
                    }
                }
            }

            if (group1 == group2)
            {
                Group removeFromGroup = _gameFile.Grid.GetGroup(group1);
                foreach (int removeItem in removeList)
                {
                    foreach (Block compareBlock in removeFromGroup.Members)
                    {
                        if (compareBlock == _gameFile.Grid[col1, row1] || compareBlock == _gameFile.Grid[col2, row2])
                        {
                            continue;
                        }
                        compareBlock.Values.Remove(removeItem);
                    }
                }
            }
        }
    }

    enum SolveType
    {
        reduceByColumn = 1, reduceByRow = 2, reduceByGroup = 4, eliminateColumn = 8, eliminateRow = 16, eliminateGroup = 32, pairWise = 64, all = 127
    }
}
