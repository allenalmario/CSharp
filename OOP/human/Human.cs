using System;
using System.Collections.Generic;

namespace human
{
    public class Human
    {
        public string Name;
        public int Strength;
        public int Intelligence;
        public int Dexterity;
        private int health;
        public int Health
        {
            get {return health;}
            set {health = health - value;}
        }

        public Human(string name)
        {
            Name = name;
            Strength = 3;
            Intelligence = 3;
            Dexterity = 3;
            health = 100;
        }
        public Human(string name, int strength, int intelligence, int dexterity, int healthh)
        {
            Name = name;
            Strength = strength;
            Intelligence = intelligence;
            Dexterity = dexterity;
            health = healthh;
        }
        public virtual int Attack(Human target)
        {
            int damage = 5 * Strength;
            target.Health = damage;
            Console.WriteLine($"{target.Name} was attacked! Their health is now {target.health}");
            return target.Health;
        }
    }
}