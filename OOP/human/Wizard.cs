using System;

namespace human
{
    public class Wizard : Human
    {
        public Wizard(string name) : base(name)
        {
            Intelligence = 25;
            Health = 50;
        }

        public override int Attack(Human target)
        {
            int damage = 5 * Strength;
            target.Health = damage;
            int lowerIntelligence = 5 * Intelligence;
            target.Intelligence = target.Intelligence - lowerIntelligence;
            Console.WriteLine($"{target.Name} was attacked! Their health is now {target.Health}");
            Console.WriteLine($"{target.Name}'s Intelligence went down by {lowerIntelligence}!");
            Console.WriteLine($"{target.Name}'s Intelligence is now {target.Intelligence}");
            Console.WriteLine($"{this.Name} in return absorbs those points to their health. Their health grew by {lowerIntelligence} points.");
            return target.Health;
        }
        public void Heal(Human target)
        {
            int healBy = 10 * this.Intelligence;
            target.Health = healBy;
            Console.WriteLine($"{target.Name}'s health went up {healBy} points!");
        }
    }
}