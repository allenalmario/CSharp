using System;

namespace Fundamentals
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 1; i <= 255; i++)
            {
                Console.WriteLine(i);
            }

            for (int z = 1; z <= 100; z++)
            {
                if (z % 5 == 0 && z % 3 == 0) 
                {
                    Console.WriteLine("BuzzFizz");
                    Console.WriteLine(z);
                }
                if (z % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                    Console.WriteLine(z);
                }
                if (z % 3 == 0) 
                {
                    Console.WriteLine("Fizz");
                    Console.WriteLine(z);
                }
            }

        }
    }
}
