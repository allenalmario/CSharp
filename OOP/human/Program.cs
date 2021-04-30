using System;

namespace human
{
    class Program
    {
        static void Main(string[] args)
        {
            Human Wizard = new Human("Wizard");
            Human Knight = new Human("Knight");
            Knight.Attack(Wizard);
            Knight.Attack(Wizard);
            Ninja myNinja = new Ninja("Jim");
            Console.WriteLine(myNinja.Dexterity);
            Wizard myWizard = new Wizard("Tim");
            Console.WriteLine(myWizard.Health);
            Console.WriteLine(myWizard.Intelligence);
            Samurai mySamurai = new Samurai("Kim");
            Console.WriteLine(mySamurai.Health);
            myWizard.Attack(myNinja);
            myNinja.Attack(myWizard);
            mySamurai.Attack(myWizard);
            myWizard.Heal(myWizard);
        }
    }
}
