using System;

namespace human
{
    public class Samurai : Human
    {
        public Samurai(string name) : base(name)
        {
            Health = 200;
        }
        public override int Attack(Human target)
        {
            if (target.Health < 50)
            {
                target.Health = target.Health;
            }
            Console.WriteLine($"{target.Name} was attacked! Their health is now {target.Health}");
            return target.Health;
        }
    }
}