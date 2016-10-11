namespace RealFruitWars.Common
{
    using System.Collections.Generic;

    public class GlobalConstants
    {
        public static readonly int StandardGameNumberOfPlayers = 2;
        public static readonly int StandardGameTotalBoardRows = 8;
        public static readonly int StandardGameTotalBoardCols = 8;
        public static readonly char EmtpyCellChar = '-';
        public static readonly char PlayerOneChar = '1';
        public static readonly char PlayerTwoChar = '2';
        public static readonly char AppleChar = 'A';
        public static readonly char PearChar = 'P';
        public static readonly int AppleCount = 4;
        public static readonly int PearCount = 3;
        public static readonly int PlayerDistance = 3;
        public static readonly int FruitDistance = 2;
        public static readonly IEnumerable<string> ValidPlayerChoice = new List<string> { "1", "2", "3" };
        public static readonly IEnumerable<string> YesOrNo = new List<string> { "y", "n"};
    }
}
