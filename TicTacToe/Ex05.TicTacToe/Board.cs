using System;

namespace TicTacToe
{
    internal class Board
    {
        internal class Cell
        {
            internal eSign m_CellSign = eSign.Empty;
            internal byte m_CellRow;
            internal byte m_CellCol;

            public Cell(byte i_CellRow, byte i_CellCol)
            {
                this.m_CellRow = i_CellRow;
                this.m_CellCol = i_CellCol;
            }

            public static bool Parse(string i_String, out Cell o_Result)
            {
                bool validInput = false;
                o_Result = null;

                if (!string.IsNullOrWhiteSpace(i_String))
                {
                    string[] tokens = i_String.Split(',');

                    if (byte.TryParse(tokens[0], out byte rowNum) && byte.TryParse(tokens[1], out byte colNum))
                    {
                        o_Result = new Cell(rowNum, colNum);
                        validInput = true;
                    }
                }

                return validInput;
            }
        }

        private readonly byte r_NumOfRows;

        private readonly byte r_NumOfCols;

        private Cell[,] m_PlayBoard;

        public Board(byte i_BoardSize)
        {
            this.r_NumOfRows = this.r_NumOfCols = this.BoardSize = i_BoardSize;
            this.initializeBoard();
        }

        public byte BoardSize { get; }

        private void initializeBoard()
        {
            this.m_PlayBoard = new Cell[this.r_NumOfRows, this.r_NumOfCols];
            for (byte curRow = 0; curRow < this.r_NumOfRows; curRow++)
            {
                for (byte curCol = 0; curCol < this.r_NumOfCols; curCol++)
                {
                    this.m_PlayBoard[curRow, curCol] = new Cell(curRow, curCol);
                }
            }
        }

        internal Cell this[Cell i_CurrentCell]
        {
            get => this.m_PlayBoard[i_CurrentCell.m_CellRow, i_CurrentCell.m_CellCol];
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.m_PlayBoard[i_CurrentCell.m_CellRow, i_CurrentCell.m_CellCol] = i_CurrentCell;
            }
        }

        internal Cell this[byte i_CurRow, byte i_CurCol] => this.m_PlayBoard[i_CurRow, i_CurCol];

        internal byte GetCellIndexInBoard(Cell i_CurrentCell)
        {
            return (byte)((i_CurrentCell.m_CellRow * this.BoardSize) + i_CurrentCell.m_CellCol);
        }

        internal void ResetBoard()
        {
            for (byte curRow = 0; curRow < this.r_NumOfRows; curRow++)
            {
                for (byte curCol = 0; curCol < this.r_NumOfCols; curCol++)
                {
                    this.m_PlayBoard[curRow, curCol].m_CellSign = eSign.Empty;
                }
            }
        }

        internal bool IsCellInRange(Cell i_Cell)
        {
            bool inRange = i_Cell.m_CellRow < this.r_NumOfRows && i_Cell.m_CellCol < this.r_NumOfCols;
            return inRange;
        }

        internal bool IsCellUsed(Cell i_Cell)
        {
            bool isFreeCell = this.m_PlayBoard[i_Cell.m_CellRow, i_Cell.m_CellCol].m_CellSign == eSign.Empty;
            return isFreeCell;
        }
    }
}