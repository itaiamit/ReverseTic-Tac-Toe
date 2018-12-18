namespace TicTacToe
{
    internal enum eRoundStatus
    {
        InGame = 0,
        RowLose = 1,
        ColLose = 2,
        MajorDiagonalLose = 3,
        MinorDiagonalLose = 4,
        Tie = 5,
        Win = 6
    }
}