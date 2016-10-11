namespace RealFruitWars.Board.Common
{
    using RealFruitWars.Common;
    using RealFruitWars.Warriors.Contracts;

    public interface IWarriorBoard
    {
        char[,] Board { get; }
    }
}
