namespace RealFruitWars.Renderer
{
    using RealFruitWars.Players.Contracts;
    using RealFruitWars.Warriors.Contracts;

    public interface IRenderer
    {
        void RenderMainMenu();

        void RenderBoard(char[,] board);

        void ClearConsole();

        /// <summary>
        /// Renders the "Start New Game?" question.
        /// </summary>
        /// <returns>Yes or No</returns>
        bool StartNewGame();

        /// <summary>
        /// Renders the win message.
        /// </summary>
        /// <param name="player">The winning player.</param>
        void RenderWinGame(IPlayer player);

        void RenderPlayerTurnMessage(IPlayer player);

        /// <summary>
        /// Renders the current Power and Speed of the player.
        /// </summary>
        void RenderPlayerInfo(IPlayer player);

        /// <summary>
        /// Renders the Draw Game message.
        /// </summary>
        void RenderDrawGame();

        /// <summary>
        /// Read the choice of Warrior of the player.
        /// </summary>
        /// <param name="instructions">Displays the Player1/Player2 Choose Warrior message.</param>
        /// <returns>The correct Warrior that has been chosen.</returns>
        Warrior ReadPlayerChoice(string instructions);
    }
}
