namespace RealFruitWars.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RealFruitWars.Board;
    using RealFruitWars.Board.Common;
    using RealFruitWars.Common;
    using RealFruitWars.Players;
    using RealFruitWars.Players.Contracts;
    using RealFruitWars.Renderer;
    using RealFruitWars.Warriors.Contracts;

    public class WarriorEngine
    {
        private static readonly string PlayerOneInstructions = string.Format("Player1, please choose a warrior.       {0}Insert 1 for turtle / 2 for monkey / 3 for pigeon", Environment.NewLine);
        private static readonly string PlayerTwoInstructions = string.Format("Player2, please choose a warrior.        {0}Insert 1 for turtle / 2 for monkey / 3 for pigeon", Environment.NewLine);

        private IRenderer renderer;
        private IWarriorBoard board;
        private IPlayer currentPlayer;
        private List<IPlayer> players;
        
        /// <summary>
        /// Renders the Main Menu, where players can choose their Warrior and Initializes the starting board.
        /// </summary>
        public void Start()
        {
            this.renderer = new ConsoleRenderer();
            this.board = new WarriorBoard();                      

            renderer.RenderMainMenu();
            InitializeGameObjects(board);

            StartGame();
        }

        /// <summary>
        /// Using a random generator, the method sets the position of the players and fruits in the correct way, based on the preset conditions.
        /// </summary>
        /// <param name="board">Uses the board as reference for the positioning of the new elements.</param>
        public void InitializeGameObjects(IWarriorBoard board)
        {
            this.players = InitializePlayers(board);
            InializeFruits(board);
        }

        /// <summary>
        /// Sets the position and the character of each player on the board after they have chosen their Warrior
        /// </summary>
        /// <param name="board"></param>
        /// <returns>A list of the two players with information about their position</returns>
        public List<IPlayer> InitializePlayers(IWarriorBoard board)
        {
            IPlayer playerOne = GetPlayer(board, GlobalConstants.PlayerOneChar, PlayerOneInstructions);
            IPlayer playerTwo = GetPlayer(board, GlobalConstants.PlayerTwoChar, PlayerTwoInstructions, GlobalConstants.PlayerOneChar);

            return new List<IPlayer>() { playerOne, playerTwo };
        }

        private void StartGame()
        {
            this.renderer.RenderBoard(board.Board);
            while (true)
            {                
                this.renderer.RenderPlayerInfo(this.players[0]);
                this.renderer.RenderPlayerInfo(this.players[1]);

                var currentPlayer = GetCurrentPlayer();
                this.renderer.RenderPlayerTurnMessage(currentPlayer);

                ExecutePlayerTurn();
                this.currentPlayer.SelectedWarrior.GainPower();
            }
            
        }

        private void ExecutePlayerTurn()
        {
            // Gets current player's speed.
            var turn = this.currentPlayer.SelectedWarrior.Speed;
            while (turn > 0)
            {
                var test = Console.ReadKey();
                var consoleKey = test.Key;

                bool validKey = true;
                bool validMove = true;
                switch (consoleKey)
                {
                    case ConsoleKey.DownArrow:
                        validMove = MoveDown();
                        break;
                    case ConsoleKey.LeftArrow:
                        validMove = MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        validMove = MoveRight();
                        break;
                    case ConsoleKey.UpArrow:
                        validMove = MoveUp();
                        break;
                    default:
                        validKey = false;
                        break;
                }

                if (validKey && validMove)
                {
                    this.renderer.RenderBoard(this.board.Board);
                    turn--;
                }
            }
        }

        /// <returns>If it was a leagal move.</returns>
        private bool MoveLeft()
        {
            var row = this.currentPlayer.CurrentPosition.Row;
            var col = this.currentPlayer.CurrentPosition.Col;
            var opponent = this.players.Single(p => p != currentPlayer);

            if (col - 1 < 0)
            {
                return false;
            }

            MakeMove(row, col - 1, opponent);
            this.currentPlayer.CurrentPosition.Col--;

            return true;
        }

        private bool MoveRight()
        {
            var row = this.currentPlayer.CurrentPosition.Row;
            var col = this.currentPlayer.CurrentPosition.Col;
            var opponent = this.players.Single(p => p != currentPlayer);

            if (col + 1 >= GlobalConstants.StandardGameTotalBoardCols)
            {
                return false;
            }

            MakeMove(row, col + 1, opponent);
            this.currentPlayer.CurrentPosition.Col++;

            return true;
        }

        private bool MoveUp()
        {
            var row = this.currentPlayer.CurrentPosition.Row;
            var col = this.currentPlayer.CurrentPosition.Col;
            var opponent = this.players.Single(p => p != currentPlayer);

            if (row - 1 < 0)
            {
                return false;
            }


            MakeMove(row - 1, col, opponent);
            this.currentPlayer.CurrentPosition.Row--;

            return true;
        }

        private bool MoveDown()
        {
            var row = this.currentPlayer.CurrentPosition.Row;
            var col = this.currentPlayer.CurrentPosition.Col;
            var opponent = this.players.Single(p => p != currentPlayer);

            if (row + 1 >= GlobalConstants.StandardGameTotalBoardRows)
            {
                return false;
            }
            
            MakeMove(row + 1, col, opponent);
            this.currentPlayer.CurrentPosition.Row++;

            return true;
        }

        private void MakeMove(int row, int col, IPlayer opponent)
        {
            ClearCurrentPlayerChar();

            // Check if cell is occupied
            if (this.board.Board[row, col] != GlobalConstants.EmtpyCellChar)
            {
                if (this.board.Board[row, col] == opponent.ObjectChar)
                {
                    EndGame(opponent);
                }
                else if (this.board.Board[row, col] == GlobalConstants.AppleChar)
                {
                    this.currentPlayer.SelectedWarrior.EatPower++;
                }
                else
                {
                    this.currentPlayer.SelectedWarrior.Speed++;
                }
            }

            // Moves current player to new position
            this.board.Board[row, col] = this.currentPlayer.ObjectChar;
        }

        private void ClearCurrentPlayerChar()
        {
            this.board.Board[this.currentPlayer.CurrentPosition.Row, this.currentPlayer.CurrentPosition.Col] = GlobalConstants.EmtpyCellChar;
        }

        private IPlayer GetCurrentPlayer()
        {
            if (this.currentPlayer != null)
            {
                ShufflePlayers();
            }
            else
            {
                this.currentPlayer = this.players[0];
            }

            return this.currentPlayer;
        }

        private void ShufflePlayers()
        {
            if (currentPlayer == this.players[0])
            {
                this.currentPlayer = this.players[1];
            }
            else
            {
                this.currentPlayer = this.players[0];
            }
        }

        private void InializeFruits(IWarriorBoard board)
        {
            for (int i = 0; i < GlobalConstants.AppleCount; i++)
            {
                InitializeRandomPosition(board, GlobalConstants.FruitDistance, GlobalConstants.AppleChar, GlobalConstants.AppleChar, GlobalConstants.PearChar);
            }

            for (int i = 0; i < GlobalConstants.PearCount; i++)
            {
                InitializeRandomPosition(board, GlobalConstants.FruitDistance, GlobalConstants.PearChar, GlobalConstants.AppleChar, GlobalConstants.PearChar);
            }
        }

        private Player GetPlayer(IWarriorBoard board, char playerChar, string instructions, params char[] keepAwayChars)
        {
            Warrior playerWarrior = this.renderer.ReadPlayerChoice(instructions);
            Position position = InitializeRandomPosition(board, GlobalConstants.PlayerDistance, playerChar, keepAwayChars);

            return new Player(playerChar, position, playerWarrior);
        }

        private bool IsGoodPosition(IWarriorBoard board, int row, int col, int distance, params char[] keepAwayChars)
        {
            if (board.Board[row, col] != GlobalConstants.EmtpyCellChar)
            {
                return false;
            }

            if (keepAwayChars.Length == 0)
            {
                return true;
            }

            for (int r = 0; r < GlobalConstants.StandardGameTotalBoardRows; r++)
            {
                for (int c = 0; c < GlobalConstants.StandardGameTotalBoardCols; c++)
                {
                    if (Math.Abs(row - r) + Math.Abs(col - c) < distance &&
                        keepAwayChars.Contains(board.Board[r, c]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private Position InitializeRandomPosition(IWarriorBoard board, int distance, char character, params char[] keepAwayChars)
        {
            var random = new Random();

            var r = random.Next(0, GlobalConstants.StandardGameTotalBoardRows);
            var c = random.Next(0, GlobalConstants.StandardGameTotalBoardCols);

            while (!IsGoodPosition(board, r, c, distance, keepAwayChars))
            {
                r = random.Next(0, GlobalConstants.StandardGameTotalBoardRows);
                c = random.Next(0, GlobalConstants.StandardGameTotalBoardCols);
            }

            board.Board[r, c] = character;
            return new Position(r,c);
        }

        private void EndGame(IPlayer opponent)
        {
            if (this.currentPlayer.SelectedWarrior.Power == opponent.SelectedWarrior.Power)
            {
                this.renderer.ClearConsole();
                this.renderer.RenderDrawGame();
            }
            else if (this.currentPlayer.SelectedWarrior.Power > opponent.SelectedWarrior.Power)
            {
                this.board.Board[opponent.CurrentPosition.Row, opponent.CurrentPosition.Col] = this.currentPlayer.ObjectChar;              
                
                this.renderer.RenderBoard(this.board.Board);
                this.renderer.RenderWinGame(this.currentPlayer);
            }
            else
            {
                this.renderer.RenderBoard(this.board.Board);
                this.renderer.RenderWinGame(opponent);
            }

            if (this.renderer.StartNewGame())
            {
                this.renderer.ClearConsole();
                this.Start();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
