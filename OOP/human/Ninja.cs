using System;
using System.Collections.Generic;

namespace human
{
    public class Ninja : Human
    {
        // base() means can directly invoke the parent class's constructor
        public Ninja(string name) : base(name)
        {
            Dexterity = 175;
        }
        public override int Attack(Human target)
        {
            Random rand = new Random();
            int chance = rand.Next(1,6);
            if (chance == 1)
            {
                int doubleDamage = (5 * Strength) + 10;
                target.Health = doubleDamage;
                Console.WriteLine("Critical Damage!!");
            }
            int damage = 5 * Strength;
            target.Health = damage;
            int lowerDexterity = 5 * Dexterity;
            target.Dexterity = target.Dexterity - lowerDexterity;
            Console.WriteLine($"{target.Name} was attacked! Their health is now {target.Health}");
            Console.WriteLine($"{target.Name}'s Dexterity went down by {lowerDexterity}!");
            Console.WriteLine($"{target.Name}'s Dexterity is now {target.Dexterity}");
            return target.Health;
        }

        public void Steal(Human target)
            {
                target.Health = 5;
                this.Health = 5;
            }
    }
}