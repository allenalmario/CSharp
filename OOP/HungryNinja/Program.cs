// dotnet watch run
using System;

namespace HungryNinja
{
    class Program
    {
        static void Main(string[] args)
        {
            Buffet TheBuffet = new Buffet();
            Ninja TedTheNinja = new Ninja();
            TedTheNinja.Eat(TheBuffet.Serve());
            TedTheNinja.Eat(TheBuffet.Serve());
            TedTheNinja.Eat(TheBuffet.Serve());
            TedTheNinja.Eat(TheBuffet.Serve());
            TedTheNinja.Eat(TheBuffet.Serve());
            TedTheNinja.Eat(TheBuffet.Serve());
        }
    }
}
