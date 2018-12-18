using System;
using System.Collections.Generic;

namespace TicTacToe
{
    public delegate void CellChosenEventHandler(object sender, CellChosenEventArgs e);

    public class GameLogic
    {
        private const byte k_NumOfPlayers = 2;
        private readonly byte r_MaxNumOfMoves;
        private eGameType m_GameType;
        private byte m_BoardSize;
        private AIEngine m_AIEngine;

        public event CellChosenEventHandler CellChosen;

        private class AIEngine
        {
            public AIEngine(GameLogic i_GameLogic)
            {
                this.gameLogic = i_GameLogic;
            }

            private GameLogic gameLogic { get; }

            internal Board.Cell GetComputerMove(byte i_PlayerIndex)
            {
                Board.Cell resultMove;

                if (isEmpty(this.gameLogic.Players[GetOtherPlayerIndex(i_PlayerIndex)].m_MovesList))
                {
                    getRandomCell(out resultMove);
                }
                else
                {
                    getOptimalCell(i_PlayerIndex, out resultMove);
                }

                return resultMove;
            }

            private void getRandomCell(out Board.Cell o_ResultMove)
            {
                Random rnd = new Random();
                o_ResultMove = new Board.Cell((byte)rnd.Next(this.gameLogic.m_BoardSize), (byte)rnd.Next(this.gameLogic.m_BoardSize));
            }

            private void getOptimalCell(byte i_PlayerIndex, out Board.Cell o_ResultMove)
            {
                const byte k_FirstHumanMoveIndex = 0;
                const byte k_LastUnblockingEmptyCell = 0;
                Random rnd = new Random();
                Board.Cell targetedCell = this.gameLogic.Players[GetOtherPlayerIndex(i_PlayerIndex)].m_MovesList[k_FirstHumanMoveIndex];
                List<Board.Cell> unblockingEmptyCells = this.getUnblockingEmptyCells(targetedCell);
                if (unblockingEmptyCells.Count != k_LastUnblockingEmptyCell)
                {
                    o_ResultMove = unblockingEmptyCells[rnd.Next(unblockingEmptyCells.Count)];
                    if (!this.gameLogic.checkIfPlayerLost(o_ResultMove, i_PlayerIndex))
                    {
                        unblockingEmptyCells.Remove(o_ResultMove);
                    }
                }
                else
                {
                    List<Board.Cell> freeCellsInBoard = this.getFreeCellsInBoard();
                    o_ResultMove = freeCellsInBoard[rnd.Next(freeCellsInBoard.Count)];
                    while (this.gameLogic.checkIfPlayerLost(o_ResultMove, i_PlayerIndex))
                    {
                        const int k_LastComputerOptionalCell = 1;
                        if (freeCellsInBoard.Count == k_LastComputerOptionalCell)
                        {
                            break;
                        }

                        freeCellsInBoard.Remove(o_ResultMove);
                        o_ResultMove = freeCellsInBoard[rnd.Next(freeCellsInBoard.Count)];
                    }
                }
            }

            private List<Board.Cell> getFreeCellsInBoard()
            {
                List<Board.Cell> secondChoiceCells = new List<Board.Cell>();

                for (byte rowIndex = 0; rowIndex < this.gameLogic.m_BoardSize; rowIndex++)
                {
                    for (byte colIndex = 0; colIndex < this.gameLogic.m_BoardSize; colIndex++)
                    {
                        if (this.gameLogic.Board[rowIndex, colIndex].m_CellSign == eSign.Empty)
                        {
                            secondChoiceCells.Add(this.gameLogic.Board[rowIndex, colIndex]);
                        }
                    }
                }

                return secondChoiceCells;
            }

            private List<Board.Cell> getUnblockingEmptyCells(Board.Cell i_TargetedCell)
            {
                List<Board.Cell> optionalCells = new List<Board.Cell>();

                for (byte rowIndex = 0; rowIndex < this.gameLogic.m_BoardSize; rowIndex++)
                {
                    for (byte colIndex = 0; colIndex < this.gameLogic.m_BoardSize; colIndex++)
                    {
                        if (this.gameLogic.Board[rowIndex, colIndex].m_CellSign == eSign.Empty)
                        {
                            if (rowIndex != i_TargetedCell.m_CellRow &&
                                colIndex != i_TargetedCell.m_CellCol &&
                                !this.gameLogic.isCellOnDiagonal(this.gameLogic.Board[rowIndex, colIndex]))
                            {
                                optionalCells.Add(this.gameLogic.Board[rowIndex, colIndex]);
                            }
                        }
                    }
                }

                return optionalCells;
            }
        }

        public GameLogic(byte i_BoardSize, eGameType i_GameType, string i_Player1Name, string i_Player2Name)
        {
            this.initializeBoard(i_BoardSize);
            this.initializePlayers(i_GameType, i_Player1Name, i_Player2Name);

            this.r_MaxNumOfMoves = (byte)(this.Board.BoardSize * this.Board.BoardSize);
        }

        internal Board Board { get; set; }

        internal Player[] Players { get; } = new Player[k_NumOfPlayers];

        public eGameType GameType => this.m_GameType;

        private static bool isEmpty<T>(IReadOnlyCollection<T> i_List)
        {
            return i_List.Count == 0;
        }

        private static void getRandomPlayersOrder(out byte o_PlayerIndex, out byte o_OtherPlayerIndex)
        {
            Random rnd = new Random();
            o_PlayerIndex = (byte)rnd.Next(k_NumOfPlayers);
            o_OtherPlayerIndex = GetOtherPlayerIndex(o_PlayerIndex);
        }

        public static byte GetOtherPlayerIndex(byte i_PlayerIndex)
        {
            return (byte)((i_PlayerIndex + 1) % 2);
        }

        private void initializeBoard(byte i_BoardSize)
        {
            this.m_BoardSize = i_BoardSize;
            this.Board = new Board(this.m_BoardSize);
        }

        private void initializePlayers(eGameType i_GameType, string i_Player1Name, string i_Player2Name)
        {
            this.m_GameType = i_GameType;
            this.SetPlayers(i_Player1Name, i_Player2Name);
        }

        internal void SetPlayers(string i_Player1Name, string i_Player2Name)
        {
            byte playerIndex;
            byte otherPlayerIndex;
            getRandomPlayersOrder(out playerIndex, out otherPlayerIndex);
            this.Players[playerIndex] = new Player(ePlayerType.Human, eSign.X) { m_Name = i_Player1Name };
            if (this.m_GameType == eGameType.PersonVsPerson)
            {
                this.Players[otherPlayerIndex] = new Player(ePlayerType.Human, eSign.O) { m_Name = i_Player2Name };
            }
            else
            {
                // PersonVsComputer
                this.Players[otherPlayerIndex] = new Player(ePlayerType.Computer, eSign.O) { m_Name = "Computer" };
                this.m_AIEngine = new AIEngine(this);
            }
        }

        internal Board.Cell GetAIMove(byte i_PlayerIndex)
        {
            return m_AIEngine.GetComputerMove(i_PlayerIndex);
        }

        private bool checkIfPlayerLost(Board.Cell i_LastMove, byte i_PlayerIndex)
        {
            eSign playerSign = this.Players[i_PlayerIndex].Sign;

            bool gameFinishedFlag = this.checkRowOrColLose(i_LastMove, i_PlayerIndex, playerSign);
            if (!gameFinishedFlag && this.isCellOnDiagonal(i_LastMove))
            {
                gameFinishedFlag = this.checkDiagonalLose(i_LastMove, i_PlayerIndex, playerSign);
            }

            return gameFinishedFlag;
        }

        private bool isCellOnDiagonal(Board.Cell i_LastMove)
        {
            return i_LastMove.m_CellRow == i_LastMove.m_CellCol ||
                   i_LastMove.m_CellRow + i_LastMove.m_CellCol + 1 == this.Board.BoardSize;
        }

        private bool checkRowOrColLose(Board.Cell i_LastMove, byte i_PlayerIndex, eSign i_PlayerSign)
        {
            bool rowLoseFlag = true;
            bool colLoseFlag = true;
            byte currentCol = i_LastMove.m_CellCol;
            byte currentRow = i_LastMove.m_CellRow;
            for (byte colRowIndex = 0; colRowIndex < this.Board.BoardSize && (colLoseFlag || rowLoseFlag); colRowIndex++)
            {
                if (i_LastMove != this.Board[colRowIndex, currentCol])
                {
                    if (colLoseFlag && this.Board[colRowIndex, currentCol].m_CellSign != i_PlayerSign)
                    {
                        colLoseFlag = false;
                    }
                }

                if (i_LastMove != this.Board[currentRow, colRowIndex])
                {
                    if (rowLoseFlag && this.Board[currentRow, colRowIndex].m_CellSign != i_PlayerSign)
                    {
                        rowLoseFlag = false;
                    }
                }
            }

            bool rowOrColLoseFlag = rowLoseFlag || colLoseFlag;
            if (rowOrColLoseFlag)
            {
                this.Players[i_PlayerIndex].RoundStatus = rowLoseFlag ? eRoundStatus.RowLose : eRoundStatus.ColLose;
                this.Players[GetOtherPlayerIndex(i_PlayerIndex)].RoundStatus = eRoundStatus.Win;
            }

            return rowOrColLoseFlag;
        }

        private bool checkDiagonalLose(Board.Cell i_LastMove, byte i_PlayerIndex, eSign i_PlayerSign)
        {
            bool minorDiagonalLoseFlag = true;
            bool majorDiagonalLoseFlag = true;
            for (byte colRowIndex = 0; colRowIndex < this.Board.BoardSize; colRowIndex++)
            {
                if (i_LastMove != this.Board[(byte)(this.m_BoardSize - colRowIndex - 1), colRowIndex])
                {
                    if (minorDiagonalLoseFlag && this.Board[(byte)(this.Board.BoardSize - colRowIndex - 1), colRowIndex].m_CellSign != i_PlayerSign)
                    {
                        minorDiagonalLoseFlag = false;
                    }
                }

                if (i_LastMove != this.Board[colRowIndex, colRowIndex])
                {
                    if (majorDiagonalLoseFlag && this.Board[colRowIndex, colRowIndex].m_CellSign != i_PlayerSign)
                    {
                        majorDiagonalLoseFlag = false;
                    }
                }
            }

            bool minorOrMajorDiagonalLose = minorDiagonalLoseFlag || majorDiagonalLoseFlag;
            if (minorOrMajorDiagonalLose)
            {
                this.Players[i_PlayerIndex].RoundStatus = minorDiagonalLoseFlag ? eRoundStatus.MinorDiagonalLose : eRoundStatus.MajorDiagonalLose;
                this.Players[GetOtherPlayerIndex(i_PlayerIndex)].RoundStatus = eRoundStatus.Win;
            }

            return minorOrMajorDiagonalLose;
        }

        internal bool CheckCellLegality(Board.Cell o_ResultMove)
        {
            return this.Board.IsCellInRange(o_ResultMove) && this.Board.IsCellUsed(o_ResultMove);
        }

        internal void RegisterMove(Board.Cell i_CurrentMove, byte i_PlayerIndex)
        {
            this.Board[i_CurrentMove] = i_CurrentMove;
            this.Board[i_CurrentMove].m_CellSign = this.Players[i_PlayerIndex].Sign;
            if (this.Players[i_PlayerIndex].PlayerType == ePlayerType.Human)
            {
                this.Players[i_PlayerIndex].m_MovesList.Add(i_CurrentMove);
            }

            CellChosenEventArgs e =
                new CellChosenEventArgs
                {
                    m_CellSign = i_CurrentMove.m_CellSign,
                    m_CellIndex = Board.GetCellIndexInBoard(i_CurrentMove)
                };

            OnCellChosen(e);
        }

        protected virtual void OnCellChosen(CellChosenEventArgs e)
        {
            this.CellChosen?.Invoke(this, e);
        }

        internal bool CheckEndGame(Board.Cell i_CurrentMove, byte i_CurrentNumOfMoves, byte i_PlayerIndex)
        {
            bool isGameFinished = false;
            if (this.checkIfPlayerLost(i_CurrentMove, i_PlayerIndex))
            {
                isGameFinished = true;
                this.Players[GetOtherPlayerIndex(i_PlayerIndex)].Score++;
            }

            if (!isGameFinished)
            {
                isGameFinished = this.handleFullBoard(i_CurrentNumOfMoves);
            }

            return isGameFinished;
        }

        private bool handleFullBoard(byte i_CurrentNumOfMoves)
        {
            bool fullBoardFlag = false;
            if (i_CurrentNumOfMoves == this.r_MaxNumOfMoves)
            {
                fullBoardFlag = true;
                foreach (Player player in this.Players)
                {
                    player.RoundStatus = eRoundStatus.Tie;
                }
            }

            return fullBoardFlag;
        }
    }
}