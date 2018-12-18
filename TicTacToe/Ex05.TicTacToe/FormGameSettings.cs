using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class FormGameSettings
    {
        private const byte k_ButtonSize = 70;
        private const byte k_Margin = 10;
        private bool m_ValidSettings;
        private GameLogic m_GameLogic;
        private FormTicTacToeMisere m_FormTicTacToeMisere;

        public FormGameSettings()
        {
            InitializeComponent();
        }

        public GameLogic Logic
        {
            get => this.m_GameLogic;
            set => this.m_GameLogic = value;
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = !textBoxPlayer2.Enabled;
            if (this.textBoxPlayer2.Enabled)
            {
                this.textBoxPlayer2.Text = string.Empty;
            }
            else
            {
                textBoxPlayer2.Text = @"[Computer]";
            }
        }

        private void numericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (numericUpDown != null)
            {
                numericUpDownCols.Value = numericUpDown.Value;
            }
        }

        private void numericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (numericUpDown != null)
            {
                numericUpDownRows.Value = numericUpDown.Value;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            checkGameSettings();

            if (this.m_ValidSettings)
            {
                Hide();
                initializeGameBoard(out List<Button> cellButtons);
                initializeGameLogicAndGameForm(cellButtons);
                handelComputerFirstMove();
                m_FormTicTacToeMisere.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show(@"Please type your names.", @"Error!");
            }
        }

        private void handelComputerFirstMove()
        {
            const byte k_FirstToPlay = 0;
            if (this.m_GameLogic.Players[k_FirstToPlay].PlayerType == ePlayerType.Computer)
            {
                m_FormTicTacToeMisere.HandleComputerMove();
            }
        }

        private void initializeGameLogicAndGameForm(List<Button> i_CellButtons)
        {
            this.m_GameLogic = new GameLogic((byte)numericUpDownCols.Value, GetGameType(), textBoxPlayer1.Text, textBoxPlayer2.Text);
            m_FormTicTacToeMisere = new FormTicTacToeMisere(this.m_GameLogic, i_CellButtons);
            addPlayerNamesToBoard(textBoxPlayer1.Text, textBoxPlayer2.Text);
        }

        private void initializeGameBoard(out List<Button> o_CellButtons)
        {
            int verticalMargin = 0;
            int horizontalMargin = 0;
            o_CellButtons = new List<Button>();

            for (int curRowNumber = 0; curRowNumber < numericUpDownRows.Value; curRowNumber++)
            {
                for (int curColNumber = 0; curColNumber < numericUpDownCols.Value; curColNumber++)
                {
                    Button cellButton = new Button
                    {
                        Size = new Size(k_ButtonSize, k_ButtonSize),
                        Location = new Point(horizontalMargin, verticalMargin),
                        Name = $"{curRowNumber},{curColNumber}",
                        TabStop = false,
                    };
                    o_CellButtons.Add(cellButton);
                    horizontalMargin += k_ButtonSize;
                }

                horizontalMargin = 0;
                verticalMargin += k_ButtonSize;
            }
        }

        public eGameType GetGameType()
        {
            return checkBoxPlayer2.Checked ? eGameType.PersonVsPerson : eGameType.PersonVsComputer;
        }

        private void addPlayerNamesToBoard(string i_Player1Name, string i_Player2Name)
        {
            Label player1NameLabel = m_FormTicTacToeMisere.m_Player1NameLabel = new Label { Text = i_Player1Name + ':', AutoSize = true };
            Label player2NameLabel = m_FormTicTacToeMisere.m_Player2NameLabel = new Label { Text = checkBoxPlayer2.Checked ? i_Player2Name + ':' : "Computer:", AutoSize = true };
            Label player1ScoreLabel = m_FormTicTacToeMisere.m_Player1ScoreLabel = new Label { Text = @"0", AutoSize = true };
            Label player2ScoreLabel = m_FormTicTacToeMisere.m_Player2ScoreLabel = new Label { Text = @"0", AutoSize = true };

            this.placePlayersLabels(player1NameLabel, player2NameLabel, player1ScoreLabel, player2ScoreLabel);
            m_FormTicTacToeMisere.BoldCurrentPlayerName(true);
        }

        private void placePlayersLabels(Label i_Player1NameLabel, Label i_Player2NameLabel, Label i_Player1ScoreLabel, Label i_Player2ScoreLabel)
        {
            const byte k_ButtonMargin = 8;
            const byte k_ScoreMarginFromName = 6;

            i_Player1NameLabel.Left = (this.m_FormTicTacToeMisere.ClientSize.Width - i_Player1NameLabel.PreferredWidth
                                     - i_Player2NameLabel.PreferredWidth - i_Player1ScoreLabel.PreferredWidth
                                     - i_Player2ScoreLabel.PreferredWidth) / 2;
            i_Player1NameLabel.Top = this.m_FormTicTacToeMisere.ClientSize.Height + k_ButtonMargin;
            i_Player1NameLabel.Margin = new Padding(0, 0, 0, k_ButtonMargin);
            this.m_FormTicTacToeMisere.Controls.Add(i_Player1NameLabel);

            i_Player1ScoreLabel.Left = i_Player1NameLabel.Right + k_ScoreMarginFromName;
            i_Player1ScoreLabel.Top = i_Player1NameLabel.Top;
            i_Player1ScoreLabel.Margin = new Padding(0, 0, 0, k_ButtonMargin);
            this.m_FormTicTacToeMisere.Controls.Add(i_Player1ScoreLabel);

            i_Player2NameLabel.Left = i_Player1ScoreLabel.Right + k_Margin;
            i_Player2NameLabel.Top = i_Player1NameLabel.Top;
            i_Player2NameLabel.Margin = new Padding(0, 0, 0, k_ButtonMargin);
            this.m_FormTicTacToeMisere.Controls.Add(i_Player2NameLabel);

            i_Player2ScoreLabel.Left = i_Player2NameLabel.Right + k_ScoreMarginFromName;
            i_Player2ScoreLabel.Top = i_Player1NameLabel.Top;
            i_Player2ScoreLabel.Margin = new Padding(0, 0, 0, k_ButtonMargin);
            this.m_FormTicTacToeMisere.Controls.Add(i_Player2ScoreLabel);
        }

        private void checkGameSettings()
        {
            if (textBoxPlayer1.Text != string.Empty && textBoxPlayer2.Text != string.Empty)
            {
                this.m_ValidSettings = true;
            }
        }

        private void formGameSettings_Load(object sender, EventArgs e)
        {
        }
    }
}
