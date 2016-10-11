namespace RealFruitWars.Common.Console
{
    using System;

    public class ConsoleHelpers
    {
        /// <summary>
        /// Sets the cursor at center in order for the Logo message to be displayed correctly.
        /// </summary>
        /// <param name="lengthOfMessage">Takes into consideration the length of the message in order to center it correctly.</param>
        public static void SetCursorAtCenter(int lengthOfMessage)
        {
            int centerRow = Console.WindowHeight / 2;
            int centerCol = Console.WindowWidth / 2 - lengthOfMessage / 2;
            Console.SetCursorPosition(centerCol, centerRow);
        }
    }
}
