using System.ComponentModel;
using System.Windows.Forms;

namespace TicTacToe
{
    partial class FormGameSettings: Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.labelRows = new System.Windows.Forms.Label();
            this.labelCols = new System.Windows.Forms.Label();
            this.numericUpDownRows = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCols = new System.Windows.Forms.NumericUpDown();
            this.buttonStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCols)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayers.Location = new System.Drawing.Point(29, 16);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(70, 20);
            this.labelPlayers.TabIndex = 0;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.BackColor = System.Drawing.SystemColors.Control;
            this.labelPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer1.Location = new System.Drawing.Point(52, 51);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(75, 20);
            this.labelPlayer1.TabIndex = 1;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelPlayer2.Location = new System.Drawing.Point(80, 95);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(75, 20);
            this.labelPlayer2.TabIndex = 2;
            this.labelPlayer2.Text = "Player 2:";
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(164, 49);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(116, 22);
            this.textBoxPlayer1.TabIndex = 0;
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Location = new System.Drawing.Point(164, 93);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(116, 22);
            this.textBoxPlayer2.TabIndex = 2;
            this.textBoxPlayer2.Text = "[Computer]";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(56, 95);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(18, 17);
            this.checkBoxPlayer2.TabIndex = 1;
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelBoardSize.Location = new System.Drawing.Point(29, 159);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(97, 20);
            this.labelBoardSize.TabIndex = 6;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // labelRows
            // 
            this.labelRows.AutoSize = true;
            this.labelRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelRows.Location = new System.Drawing.Point(52, 196);
            this.labelRows.Name = "labelRows";
            this.labelRows.Size = new System.Drawing.Size(56, 20);
            this.labelRows.TabIndex = 7;
            this.labelRows.Text = "Rows:";
            // 
            // labelCols
            // 
            this.labelCols.AutoSize = true;
            this.labelCols.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelCols.Location = new System.Drawing.Point(181, 196);
            this.labelCols.Name = "labelCols";
            this.labelCols.Size = new System.Drawing.Size(48, 20);
            this.labelCols.TabIndex = 8;
            this.labelCols.Text = "Cols:";
            // 
            // numericUpDownRows
            // 
            this.numericUpDownRows.Location = new System.Drawing.Point(115, 194);
            this.numericUpDownRows.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRows.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownRows.Name = "numericUpDownRows";
            this.numericUpDownRows.Size = new System.Drawing.Size(49, 22);
            this.numericUpDownRows.TabIndex = 3;
            this.numericUpDownRows.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownRows.ValueChanged += new System.EventHandler(this.numericUpDownRows_ValueChanged);
            // 
            // numericUpDownCols
            // 
            this.numericUpDownCols.Location = new System.Drawing.Point(235, 194);
            this.numericUpDownCols.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCols.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownCols.Name = "numericUpDownCols";
            this.numericUpDownCols.Size = new System.Drawing.Size(45, 22);
            this.numericUpDownCols.TabIndex = 5;
            this.numericUpDownCols.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownCols.ValueChanged += new System.EventHandler(this.numericUpDownCols_ValueChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(33, 240);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(247, 23);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "Start!";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 289);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.numericUpDownCols);
            this.Controls.Add(this.numericUpDownRows);
            this.Controls.Add(this.labelCols);
            this.Controls.Add(this.labelRows);
            this.Controls.Add(this.labelBoardSize);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormGameSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.formGameSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCols)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelPlayers;
        private Label labelPlayer1;
        private Label labelPlayer2;
        private TextBox textBoxPlayer1;
        private TextBox textBoxPlayer2;
        private CheckBox checkBoxPlayer2;
        private Label labelBoardSize;
        private Label labelRows;
        private Label labelCols;
        private NumericUpDown numericUpDownRows;
        internal NumericUpDown numericUpDownCols;
        private Button buttonStart;
    }
}