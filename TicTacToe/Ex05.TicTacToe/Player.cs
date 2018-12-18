using System.Collections.Generic;

namespace TicTacToe
{
    internal class Player
    {
        internal string m_Name;
        internal List<Board.Cell> m_MovesList = new List<Board.Cell>();

        public Player(ePlayerType i_PlayerType, eSign i_Sign)
        {
            this.PlayerType = i_PlayerType;
            this.Sign = i_Sign;
        }

        public eRoundStatus RoundStatus { get; set; }

        public eSign Sign { get; }

        public ushort Score { get; set; } = 0;

        public ePlayerType PlayerType { get; }
    }
}
