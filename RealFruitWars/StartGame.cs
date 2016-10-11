namespace RealFruitWars
{
    using RealFruitWars.Board;
    using RealFruitWars.Board.Common;
    using RealFruitWars.Renderer;
    using RealFruitWars.Engine;

    public class StartGame
    {
        static void Main()
        {
            WarriorEngine engine = new WarriorEngine();
            engine.Start(); 
        }
    }
}
