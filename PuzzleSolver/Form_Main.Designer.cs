namespace PuzzleSolver
{
    partial class Form_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label_SetSize = new System.Windows.Forms.Label();
            this.textBox_Rows = new System.Windows.Forms.TextBox();
            this.textBox_Cols = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel_Main = new System.Windows.Forms.Panel();
            this.button_CalculateNext = new System.Windows.Forms.Button();
            this.button_Load = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.panel_BoxDisplay = new System.Windows.Forms.Panel();
            this.textBox_Values = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // label_SetSize
            // 
            this.label_SetSize.AutoSize = true;
            this.label_SetSize.Location = new System.Drawing.Point(9, 9);
            this.label_SetSize.Name = "label_SetSize";
            this.label_SetSize.Size = new System.Drawing.Size(76, 13);
            this.label_SetSize.TabIndex = 0;
            this.label_SetSize.Text = "Set Size (C,R):";
            // 
            // textBox_Rows
            // 
            this.textBox_Rows.Location = new System.Drawing.Point(12, 51);
            this.textBox_Rows.Name = "textBox_Rows";
            this.textBox_Rows.Size = new System.Drawing.Size(100, 20);
            this.textBox_Rows.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBox_Rows, "Rows");
            this.textBox_Rows.Visible = false;
            this.textBox_Rows.TextChanged += new System.EventHandler(this.textBox_Rows_TextChanged);
            // 
            // textBox_Cols
            // 
            this.textBox_Cols.Location = new System.Drawing.Point(12, 25);
            this.textBox_Cols.Name = "textBox_Cols";
            this.textBox_Cols.Size = new System.Drawing.Size(100, 20);
            this.textBox_Cols.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBox_Cols, "Columns");
            this.textBox_Cols.Visible = false;
            this.textBox_Cols.TextChanged += new System.EventHandler(this.textBox_Cols_TextChanged);
            // 
            // panel_Main
            // 
            this.panel_Main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Main.Location = new System.Drawing.Point(118, 12);
            this.panel_Main.Name = "panel_Main";
            this.panel_Main.Size = new System.Drawing.Size(350, 350);
            this.panel_Main.TabIndex = 3;
            this.panel_Main.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Main_Paint);
            this.panel_Main.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_Main_MouseClick);
            // 
            // button_CalculateNext
            // 
            this.button_CalculateNext.Location = new System.Drawing.Point(12, 77);
            this.button_CalculateNext.Name = "button_CalculateNext";
            this.button_CalculateNext.Size = new System.Drawing.Size(100, 23);
            this.button_CalculateNext.TabIndex = 4;
            this.button_CalculateNext.Text = "Calculate Next";
            this.button_CalculateNext.UseVisualStyleBackColor = true;
            this.button_CalculateNext.Click += new System.EventHandler(this.button_CalculateNext_Click);
            // 
            // button_Load
            // 
            this.button_Load.Location = new System.Drawing.Point(12, 310);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(100, 23);
            this.button_Load.TabIndex = 5;
            this.button_Load.Text = "&Load";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // button_Save
            // 
            this.button_Save.Enabled = false;
            this.button_Save.Location = new System.Drawing.Point(12, 339);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(100, 23);
            this.button_Save.TabIndex = 6;
            this.button_Save.Text = "&Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // panel_BoxDisplay
            // 
            this.panel_BoxDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_BoxDisplay.Location = new System.Drawing.Point(12, 131);
            this.panel_BoxDisplay.Name = "panel_BoxDisplay";
            this.panel_BoxDisplay.Size = new System.Drawing.Size(100, 100);
            this.panel_BoxDisplay.TabIndex = 7;
            this.panel_BoxDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_BoxDisplay_Paint);
            this.panel_BoxDisplay.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel_BoxDisplay_MouseClick);
            // 
            // textBox_Values
            // 
            this.textBox_Values.Location = new System.Drawing.Point(12, 237);
            this.textBox_Values.Name = "textBox_Values";
            this.textBox_Values.Size = new System.Drawing.Size(100, 20);
            this.textBox_Values.TabIndex = 8;
            this.textBox_Values.TextChanged += new System.EventHandler(this.textBox_Values_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "game";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Game Files|*.game";
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 379);
            this.Controls.Add(this.textBox_Values);
            this.Controls.Add(this.panel_BoxDisplay);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.button_CalculateNext);
            this.Controls.Add(this.panel_Main);
            this.Controls.Add(this.textBox_Cols);
            this.Controls.Add(this.textBox_Rows);
            this.Controls.Add(this.label_SetSize);
            this.Name = "Form_Main";
            this.Text = "Sudoku Solver";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Main_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_SetSize;
        private System.Windows.Forms.TextBox textBox_Rows;
        private System.Windows.Forms.TextBox textBox_Cols;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel_Main;
        private System.Windows.Forms.Button button_CalculateNext;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Panel panel_BoxDisplay;
        private System.Windows.Forms.TextBox textBox_Values;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

