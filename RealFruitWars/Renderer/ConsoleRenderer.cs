namespace RealFruitWars.Renderer
{
    using System;
    using System.Linq;
    using System.Threading;
    using RealFruitWars.Common.Console;
    using RealFruitWars.Common;
    using RealFruitWars.Warriors.Contracts;
    using RealFruitWars.Warriors;
    using RealFruitWars.Players.Contracts;

    public class ConsoleRenderer : IRenderer
    {
        private const string InvalidInputMessage = "Invalid input, please try again!";
        private const string Logo = "FRUIT WAR";

        public void RenderMainMenu()
        {
            ConsoleHelpers.SetCursorAtCenter(Logo.Length);
            Console.WriteLine(Logo);

            Thread.Sleep(2000);
            Console.Clear();
        }

        public Warrior ReadPlayerChoice(string instruction)
        {
            Console.WriteLine(instruction);
            var key = Console.ReadKey().KeyChar.ToString();
            while (!GlobalConstants.ValidPlayerChoice.Contains(key))
            {
                Console.WriteLine();
                Console.WriteLine(InvalidInputMessage);
                key = Console.ReadKey().KeyChar.ToString();
            }

            Console.WriteLine();

            switch (key)
            {            
                case "1":
                    return new Turtle();
                case "2":
                    return new Monkey();
                case "3":
                    return new Pigeon();
                default:
                    throw new ArgumentException("Invalid Character!");
            }
        }

        public void RenderBoard(char[,] board)
        {
            Console.Clear();
            for (int r = 0; r < GlobalConstants.StandardGameTotalBoardRows; r++)
            {
                var row = new char[GlobalConstants.StandardGameTotalBoardCols];

                for (int c = 0; c < GlobalConstants.StandardGameTotalBoardCols; c++)
                {   
                    row[c] = board[r, c];
                }

                Console.WriteLine(new string(row));
            }
        }

        public void RenderPlayerInfo(IPlayer player)
        {
            Console.WriteLine(string.Format("Player{0}: {1} Power; {2} Speed", player.ObjectChar, player.SelectedWarrior.Power, player.SelectedWarrior.Speed));

        }

        public void RenderPlayerTurnMessage(IPlayer player)
        {
            Console.WriteLine(string.Format("Player{0}, make a move please!", player.ObjectChar));
        }

        public void RenderDrawGame()
        {
            Console.WriteLine("Draw game.");
        }

        public void RenderWinGame(IPlayer winner)
        {
            Console.WriteLine(string.Format("Player {0} wins the game. \nPigeon with Power: {1}, Speed: {2}", winner.ObjectChar, winner.SelectedWarrior.Power, winner.SelectedWarrior.Speed));
        }

        public bool StartNewGame()
        {
            Console.WriteLine("Do you want to start a rematch? (y/n)");
            
            var key = Console.ReadKey().KeyChar.ToString();
            while (!GlobalConstants.YesOrNo.Contains(key))
            {
                Console.WriteLine();
                Console.WriteLine(InvalidInputMessage);
                key = Console.ReadKey().KeyChar.ToString();
            }

            return key == "y" ? true : false;
        }

        public void ClearConsole()
        {
            Console.Clear();
        }
    }
}
