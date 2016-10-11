namespace RealFruitWars.Warriors.Contracts
{
    using System;

    public abstract class Warrior
    {
        protected Warrior(int speed, int power)
        {
            this.Speed = speed;
            this.Power = power;
        }

        public int Speed { get; set; }

        public int Power { get; set; }

        public int EatPower { get; set; }

        public void GainPower()
        {
            this.Power += this.EatPower;
            this.EatPower = 0;
        }

    }
}
