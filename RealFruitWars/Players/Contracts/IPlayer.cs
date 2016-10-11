namespace RealFruitWars.Players.Contracts
{
    using RealFruitWars.Common;
    using RealFruitWars.Warriors.Contracts;

    public interface IPlayer
    {
        Position CurrentPosition { get; set; }

        char ObjectChar { get; }

        Warrior SelectedWarrior { get; }
    }
}
