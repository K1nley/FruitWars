namespace RealFruitWars.Players
{
    using RealFruitWars.Common;
    using RealFruitWars.Players.Contracts;
    using RealFruitWars.Warriors.Contracts;

    public class Player : IPlayer
    {
        public Player(char objectChar, Position startPosition, Warrior warrior)
        {
            this.ObjectChar = objectChar;
            this.CurrentPosition = startPosition;
            this.SelectedWarrior = warrior;

        }

        public Position CurrentPosition { get; set; }

        public char ObjectChar { get; private set; }

        public Warrior SelectedWarrior { get; private set; }
    }
}
