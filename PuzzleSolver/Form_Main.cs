using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PuzzleSolver
{
    public partial class Form_Main : Form
    {
        #region Properties

        const string _AppName = "Sudoku Solver";

        Settings _settings;

        GameFile _gameFile;

        ControlHandler _actionHandler;

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Initalizes the Settings, GameFile, and ControlHandler along with all the Components.
        /// </summary>
        public Form_Main()
        {
            _settings = new Settings();

            _gameFile = new GameFile(_settings.AutoLoadFile);

            _actionHandler = new ControlHandler(_gameFile);

            InitializeComponent();
        }

        #endregion // Constructors

        #region On TextBox TextChanged

        private void textBox_Cols_TextChanged(object sender, EventArgs e)
        {
            // TODO: add so can change the number of columns
            if (1 == 1)
                return;

            //try
            //{
            //    int newNumCols = Int32.Parse(textBox_Cols.Text);
            //    if (newNumCols < gameFile.Grid.NumCols)
            //    {
            //        // Shrink the number of columns
            //        int startOfRemoving = 0;
            //        for (int i = gameFile.Grid.Count - 1; i >= startOfRemoving; i -= gameFile.Grid.NumCols)
            //        {
            //            gameFile.Grid.RemoveAt(i);
            //        }

            //        gameFile.Grid.NumCols = newNumCols;

            //        panel_Main.Invalidate();
            //    }
            //    else if (newNumCols > gameFile.Grid.NumCols)
            //    {
            //        // Grow the number of columns
            //        int endOfAdding = 0;
            //        for (int i = gameFile.Grid.NumCols; i < endOfAdding; i += gameFile.Grid.NumCols)
            //        {
            //            gameFile.Grid.Insert(i, new List<int>());
            //        }

            //        gameFile.Grid.NumCols = newNumCols;
            //        panel_Main.Invalidate();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    textBox_Cols.Text = "" + gameFile.Grid.NumCols;
            //    Console.WriteLine(ex.Message);
            //}
        }

        private void textBox_Rows_TextChanged(object sender, EventArgs e)
        {
            // TODO: add so can change the number of rows
            if (1 == 1)
                return;
            
            //try
            //{
            //    int newNumRows = Int32.Parse(textBox_Rows.Text);
            //    if (newNumRows < gameFile.Grid.NumRows)
            //    {
            //        // Shrink the number or rows
            //        int startOfRemoving = gameFile.Grid.NumCols * newNumRows;

            //        for (int i = gameFile.Grid.Count - 1; i >= startOfRemoving; i--)
            //        {
            //            gameFile.Grid.RemoveAt(i);
            //        }

            //        gameFile.Grid.NumRows = newNumRows;

            //        panel_Main.Invalidate();
            //    }
            //    else if (newNumRows > gameFile.Grid.NumRows)
            //    {
            //        // Grow the number of rows

            //        ;

            //        gameFile.Grid.NumRows = newNumRows;
            //        panel_Main.Invalidate();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    textBox_Cols.Text = "" + gameFile.Grid.NumCols;
            //    Console.WriteLine(ex.Message);
            //}
        }

        private void textBox_Values_TextChanged(object sender, EventArgs e)
        {
            ;
        }

        #endregion // On TextBox TextChanged

        #region On Button Click

        private void button_CalculateNext_Click(object sender, EventArgs e)
        {
            if (_gameFile.Complete)
            {
                return;
            }
            // Choices are:
            // 1. Reduce by row and column an box
            // 2. 
            // 3. Find like in row, column, or box

            //int numberOfChoices = 3;
            Random rand = new Random();
            int initalR = rand.Next(_gameFile.Grid.TotalBlockCount);
            int r = initalR;

            // Choose one box with not 1 number remaining
            //TODO: Change the way new locations are selected
            while (_gameFile.Grid.GetBlockByIndex(r).Values.Count == 1)
            {
                r++;
                r %= _gameFile.Grid.TotalBlockCount;
                if (r == initalR)
                {
                    _gameFile.Complete = true;
                    return;
                }
            }

            // One box has been chosen now, so reduce it if possible
            int col = _gameFile.Grid.GetColumnByIndex(r);
            int row = _gameFile.Grid.GetRowByIndex(r);

            _actionHandler.SelectChanged(col, row);

            _gameFile.Solver.Solve(r, SolveType.all);

            _gameFile.Solver.Solve(r, SolveType.reduceByColumn | SolveType.reduceByRow | SolveType.reduceByGroup);

            _gameFile.Solver.Solve(r, SolveType.eliminateColumn | SolveType.eliminateRow | SolveType.eliminateGroup);

            _gameFile.Solver.Solve(r, SolveType.pairWise);
            //TODO: Set to not include all everytime...  maybe
            //_gameFile.solver.solve(r, SolveType.all);

            panel_Main.Invalidate();
            panel_BoxDisplay.Invalidate();
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            //TODO: Change so loads from a dialogue box.
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                _gameFile = new GameFile(openFileDialog1.FileName);

            //textBox_Cols.Text = "" + _gameFile.Grid.NumCols;
            //textBox_Rows.Text = "" + _gameFile.Grid.NumRows;

                button_Save.Enabled = true;
                panel_Main.Invalidate();
                panel_BoxDisplay.Invalidate();

                this.Text = openFileDialog1.SafeFileName + " - " + _AppName;
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            _gameFile.Save();
        }

        #endregion // On Button Click

        #region On Panel Paint

        private void panel_Main_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = this.Font;
            // Width is only an guess.
            float fontWidth = font.Size / 2;
            float fontHeight = font.Size;
            Brush selected = Brushes.Blue;
            Brush available = Brushes.Coral;
            Brush removed = Brushes.LightGray;
            Pen pen = Pens.DarkBlue;
            RectangleF clipBounds = g.VisibleClipBounds;

            for (int i = 1; i <= _gameFile.Grid.NumberOfRows; i++)
            {
                g.DrawLine(pen, clipBounds.Left, clipBounds.Height * i / _gameFile.Grid.NumberOfRows, clipBounds.Right, clipBounds.Height * i / _gameFile.Grid.NumberOfRows);

                if (i % 3 == 0)
                {
                    g.DrawLine(pen, clipBounds.Left, clipBounds.Height * i / _gameFile.Grid.NumberOfRows - 1, clipBounds.Right, clipBounds.Height * i / _gameFile.Grid.NumberOfRows - 1);
                    g.DrawLine(pen, clipBounds.Left, clipBounds.Height * i / _gameFile.Grid.NumberOfRows + 1, clipBounds.Right, clipBounds.Height * i / _gameFile.Grid.NumberOfRows + 1);
                }
            }

            for (int i = 1; i <= _gameFile.Grid.NumberOfColumns; i++)
            {
                g.DrawLine(pen, clipBounds.Width * i / _gameFile.Grid.NumberOfColumns, clipBounds.Top, clipBounds.Width * i / _gameFile.Grid.NumberOfColumns, clipBounds.Bottom);

                if (i % 3 == 0)
                {
                    g.DrawLine(pen, clipBounds.Width * i / _gameFile.Grid.NumberOfColumns - 1, clipBounds.Top, clipBounds.Width * i / _gameFile.Grid.NumberOfColumns - 1, clipBounds.Bottom);
                    g.DrawLine(pen, clipBounds.Width * i / _gameFile.Grid.NumberOfColumns + 1, clipBounds.Top, clipBounds.Width * i / _gameFile.Grid.NumberOfColumns + 1, clipBounds.Bottom);
                }
            }

            // Checks each Block on the grid to see if it contains only 1 item, if so draw the number that the Block contains.
            for (int blockNumber = 0; blockNumber < _gameFile.Grid.TotalBlockCount; blockNumber++)
            {
                Block block = _gameFile.Grid[blockNumber];
                if (block.Choices == 1)
                {
                    int col = _gameFile.Grid.GetColumnByIndex(blockNumber);
                    int row = _gameFile.Grid.GetRowByIndex(blockNumber);
                    string blockValue = "" + block.Values[0];

                    // Draw the number if it the only one in the location.
                    Brush b = (blockNumber == _actionHandler.GetSelected().Index) ? selected : available;
                    g.DrawString(blockValue, font, b, clipBounds.Left + clipBounds.Width * (col + 1) / _gameFile.Grid.NumberOfColumns - (fontWidth) - clipBounds.Width / (2 * _gameFile.Grid.NumberOfColumns), clipBounds.Top + clipBounds.Height * (row + 1) / _gameFile.Grid.NumberOfRows - (fontHeight) - clipBounds.Height / (2 * _gameFile.Grid.NumberOfRows));
                }
            }

            //TODO: Check how this works, to ensure that I want to display this box every time, or just sometimes
            //if (_actionHandler.getSelected().Choices != 1)
            {
                // Show the selected Box
                int col = _gameFile.Grid.GetColumnByIndex(_actionHandler.GetSelected().Index);
                int row = _gameFile.Grid.GetRowByIndex(_actionHandler.GetSelected().Index);
                g.DrawRectangle(Pens.LightYellow, clipBounds.Left + clipBounds.Width * col / _gameFile.Grid.NumberOfColumns + 1, clipBounds.Top + clipBounds.Height * row / _gameFile.Grid.NumberOfRows + 1, clipBounds.Width / _gameFile.Grid.NumberOfColumns - 2, clipBounds.Height / _gameFile.Grid.NumberOfRows - 2);
            }
            if (_actionHandler.GetSelectedChanged() != null)
            {
//TODO: Fix the getSelectedChanged
                int col = _gameFile.Grid.GetColumnByIndex(_actionHandler.GetSelectedChanged().Index);
                int row = _gameFile.Grid.GetRowByIndex(_actionHandler.GetSelectedChanged().Index);
                g.DrawRectangle(Pens.LightSalmon, clipBounds.Left + clipBounds.Width * col / _gameFile.Grid.NumberOfColumns + 2, clipBounds.Top + clipBounds.Height * row / _gameFile.Grid.NumberOfRows + 2, clipBounds.Width / _gameFile.Grid.NumberOfColumns - 4, clipBounds.Height / _gameFile.Grid.NumberOfRows - 4);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_BoxDisplay_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            Font font = this.Font;
            // Width is only an guess.
            float fontWidth = font.Size / 2;
            float fontHeight = font.Size;
            Brush selected = Brushes.Blue;
            Brush available = Brushes.Coral;
            Brush removed = Brushes.LightGray;
            Pen pen = Pens.DarkBlue;
            RectangleF clipBounds = g.VisibleClipBounds;
            Block block = _actionHandler.GetSelected();

            for (int i = 1; i <= 9; i++)
            {
                Brush b = block.Values.Contains(i) ? available : removed;
                int col = (i - 1) % 3 + 1;
                int row = (i - 1) / 3 + 1;
                g.DrawString("" + i, font, b, clipBounds.Left + col * clipBounds.Width / 3 - (fontWidth) - clipBounds.Width / 6, clipBounds.Top + row * clipBounds.Height / 3 - (fontHeight) - clipBounds.Height / 6);
            }
            
            g.DrawLine(pen, clipBounds.Left, clipBounds.Height * 1 / 3, clipBounds.Right, clipBounds.Height * 1 / 3);
            g.DrawLine(pen, clipBounds.Left, clipBounds.Height * 2 / 3, clipBounds.Right, clipBounds.Height * 2 / 3);
            g.DrawLine(pen, clipBounds.Width * 1 / 3, clipBounds.Top, clipBounds.Width * 1 / 3, clipBounds.Bottom);
            g.DrawLine(pen, clipBounds.Width * 2 / 3, clipBounds.Top, clipBounds.Width * 2 / 3, clipBounds.Bottom);
        }

        #endregion // On Panel Paint

        #region On Panel MouseClick

        private void panel_BoxDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            int boxWidth = panel_BoxDisplay.Width / 3;
            int boxHeight = panel_BoxDisplay.Height / 3;
            int col = e.X / boxWidth;
            int row = e.Y / boxHeight;

            int clickedIndex = col + row * 3;
            int clickedValue = clickedIndex + 1;

            Block block = _actionHandler.GetSelected();

            // Left clicking of the mouse on the side panel removes it from the choices from that box
            if(e.Button == MouseButtons.Left)
            {
                // Left mouse button was pressed
                if (block.Values.Contains(clickedValue))
                {
                    // Remove from list
                    block.Values.Remove(clickedValue);
                    panel_BoxDisplay.Invalidate();
                    panel_Main.Invalidate();
                }
            }
            // Right clicking of the mouse on the side panel adds it from the choices from that box
            if (e.Button == MouseButtons.Right)
            {
                // Right mouse button was pressed
                if (!block.Values.Contains(clickedValue))
                {
                    // Add back to list
                    block.Values.Add(clickedValue);
                    panel_BoxDisplay.Invalidate();
                    panel_Main.Invalidate();
                }
            }
        }

        private void panel_Main_MouseClick(object sender, MouseEventArgs e)
        {
            int boxWidth = panel_Main.Width / _gameFile.Grid.NumberOfColumns;
            int boxHeight = panel_Main.Height / _gameFile.Grid.NumberOfRows;
            int col = e.X / boxWidth;
            int row = e.Y / boxHeight;

            // set the rows / columns to match where the click occured
            _actionHandler.Select(col, row);

            panel_BoxDisplay.Invalidate();
            panel_Main.Invalidate();
        }

        #endregion // On Panel MouseClick

        #region On Form FormClosed

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            _settings.Save();
        }

        #endregion // On Form FormClosed

        #region Helper Methods

        #endregion // Helper Methods

    }
}