using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TicTacToe.Properties;

namespace TicTacToe
{
    public partial class FormTicTacToeMisere
    {
        private const byte k_Player1Index = 0;
        private const byte k_Player2Index = 1;
        private readonly List<Button> r_CellButtons;
        private readonly GameLogic r_GameLogic;
        internal byte m_CurrentNumOfMoves;
        private bool m_IsGameFinished;
        internal byte m_PlayerIndexTurn;
        public Label m_Player1NameLabel;
        public Label m_Player2NameLabel;
        public Label m_Player1ScoreLabel;
        public Label m_Player2ScoreLabel;

        public FormTicTacToeMisere(GameLogic i_GameLogic, List<Button> i_CellButtons)
        {
            this.InitializeComponent();
            this.r_GameLogic = i_GameLogic;
            this.r_GameLogic.CellChosen += cellButton_PrintingMove;
            this.r_GameLogic.CellChosen += cellButton_CheckingGameOver;
            this.r_CellButtons = i_CellButtons;

            foreach (Button cellButton in i_CellButtons)
            {
                this.Controls.Add(cellButton);
                cellButton.Click += CellButton_Click;
            }
        }

        internal void CellButton_Click(object sender, EventArgs e)
        {
            Board.Cell clickedCell;
            bool validCell = Board.Cell.Parse(((Button)sender).Name, out clickedCell);

            Button button = (Button)sender;
            if (button != null && button.Enabled && validCell)
            {
                m_CurrentNumOfMoves++;
                this.r_GameLogic.RegisterMove(clickedCell, m_PlayerIndexTurn);
            }

            if (this.r_GameLogic.GameType == eGameType.PersonVsComputer && !this.m_IsGameFinished)
            {
                this.m_PlayerIndexTurn = GameLogic.GetOtherPlayerIndex(this.m_PlayerIndexTurn);
                this.HandleComputerMove();
            }
            else if (this.r_GameLogic.GameType == eGameType.PersonVsPerson)
            {
                this.m_PlayerIndexTurn = GameLogic.GetOtherPlayerIndex(this.m_PlayerIndexTurn);
            }
        }

        private void cellButton_CheckingGameOver(object i_Sender, CellChosenEventArgs i_E)
        {
            Board.Cell.Parse(r_CellButtons[i_E.m_CellIndex].Name, out Board.Cell clickedCell);

            this.m_IsGameFinished = this.r_GameLogic.CheckEndGame(clickedCell, this.m_CurrentNumOfMoves, this.m_PlayerIndexTurn);

            if (this.m_IsGameFinished)
            {
                byte winnerPlayerIndex = GameLogic.GetOtherPlayerIndex(this.m_PlayerIndexTurn);
                this.showResults(winnerPlayerIndex);
                this.resetRound();
            }
        }

        private void cellButton_PrintingMove(object i_Sender, CellChosenEventArgs i_E)
        {
            changeCellButtonAppearance(r_CellButtons[i_E.m_CellIndex], i_E.m_CellSign);
            BoldCurrentPlayerName(false);
        }

        private void changeCellButtonAppearance(Button i_CellButton, eSign i_PlayerSign)
        {
            i_CellButton.Enabled = false;
            i_CellButton.BackgroundImage = i_PlayerSign == eSign.O ? Resources.O : Resources.X;
            i_CellButton.BackgroundImageLayout = ImageLayout.Stretch;
        }

        internal void HandleComputerMove()
        {
            this.m_CurrentNumOfMoves++;
            Board.Cell computerMove = this.r_GameLogic.GetAIMove(this.m_PlayerIndexTurn);
            this.r_GameLogic.RegisterMove(computerMove, this.m_PlayerIndexTurn);
            this.MarkCellWithComputerSign(computerMove, this.m_PlayerIndexTurn);
            this.m_IsGameFinished = this.r_GameLogic.CheckEndGame(computerMove, this.m_CurrentNumOfMoves, this.m_PlayerIndexTurn);
            this.m_PlayerIndexTurn = GameLogic.GetOtherPlayerIndex(this.m_PlayerIndexTurn);
            if (this.m_IsGameFinished)
            {
                this.showResults(this.m_PlayerIndexTurn);
                this.resetRound();
            }

            this.boldPlayer(k_Player1Index);
        }

        internal void BoldCurrentPlayerName(bool i_IsFirstTurn)
        {
            bool player1TurnFlag = this.r_GameLogic.Players[this.m_PlayerIndexTurn].m_Name + ':' == this.m_Player1NameLabel.Text;
            if (player1TurnFlag)
            {
                boldPlayer(i_IsFirstTurn ? k_Player1Index : k_Player2Index);
            }
            else
            {
                boldPlayer(i_IsFirstTurn ? k_Player2Index : k_Player1Index);
            }
        }

        private void boldPlayer(byte i_PlayerIndex)
        {
            if (i_PlayerIndex == 0)
            {
                m_Player1NameLabel.Font = m_Player1ScoreLabel.Font = new Font(m_Player1NameLabel.Font, FontStyle.Bold);
                m_Player2NameLabel.Font = m_Player2ScoreLabel.Font = new Font(m_Player2NameLabel.Font, FontStyle.Regular);
            }
            else
            {
                m_Player2NameLabel.Font = m_Player2ScoreLabel.Font = new Font(m_Player2NameLabel.Font, FontStyle.Bold);
                m_Player1NameLabel.Font = m_Player1ScoreLabel.Font = new Font(m_Player1NameLabel.Font, FontStyle.Regular);
            }
        }

        internal void MarkCellWithComputerSign(Board.Cell i_ComputerMove, byte i_PlayerIndex)
        {
            bool found = false;
            string cellCoordinates = $"{i_ComputerMove.m_CellRow},{i_ComputerMove.m_CellCol}";

            while (!found)
            {
                foreach (Button cellButton in this.r_CellButtons)
                {
                    if (cellButton.Name == cellCoordinates)
                    {
                        found = true;
                        this.changeCellButtonAppearance(cellButton, i_ComputerMove.m_CellSign);
                        break;
                    }
                }
            }
        }

        private void showResults(byte i_WinnerPlayerIndex)
        {
            bool isTie = this.r_GameLogic.Players[i_WinnerPlayerIndex].RoundStatus == eRoundStatus.Tie;
            DialogResult dialogResult = MessageBox.Show(
                isTie
                    ? @"Tie!
Would you like to play another round?"
                                                            : $@"The winner is {this.r_GameLogic.Players[i_WinnerPlayerIndex].m_Name}!
Would you like to play another round?",
                isTie ? "A Tie!" : "A Win!",
                MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (Control control in this.Controls)
                {
                    if (control is Button)
                    {
                        (control as Button).BackgroundImage = null;
                        (control as Button).Enabled = true;
                        (control as Button).TabStop = false;
                    }
                }

                this.m_CurrentNumOfMoves = 0;
                if (!isTie)
                {
                    this.updateScoreLabels(i_WinnerPlayerIndex);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show(@"Bye bye!", @"Game over.");
                Close();
            }
        }

        private void updateScoreLabels(byte i_WinnerPlayerIndex)
        {
            string winnerName = this.r_GameLogic.Players[i_WinnerPlayerIndex].m_Name + ':';
            if (winnerName == this.m_Player2NameLabel.Text)
            {
                this.m_Player2ScoreLabel.Text = this.r_GameLogic.Players[i_WinnerPlayerIndex].Score.ToString();
            }
            else
            {
                this.m_Player1ScoreLabel.Text = this.r_GameLogic.Players[i_WinnerPlayerIndex].Score.ToString();
            }
        }

        private void resetRound()
        {
            this.r_GameLogic.Board.ResetBoard();
            foreach (Player player in this.r_GameLogic.Players)
            {
                player.RoundStatus = eRoundStatus.InGame;
                player.m_MovesList.Clear();
            }

            this.m_IsGameFinished = false;
        }
    }
}
