namespace RealFruitWars.Board
{
    using RealFruitWars.Board.Common;
    using RealFruitWars.Common;

    public class WarriorBoard : IWarriorBoard
    {
        private char[,] board;

        public WarriorBoard()
        {
            this.board = new char[GlobalConstants.StandardGameTotalBoardRows, GlobalConstants.StandardGameTotalBoardCols];
            InitializeEmptyBoard();
        }

        public void InitializeEmptyBoard()
        {
            for (int r = 0; r < GlobalConstants.StandardGameTotalBoardRows; r++)
            {  
                for (int c = 0; c < GlobalConstants.StandardGameTotalBoardCols; c++)
                {
                    board[r, c] = GlobalConstants.EmtpyCellChar;
                }
            }
        }

        public char[,] Board
        {
            get { return this.board; }
        }
    }
}
