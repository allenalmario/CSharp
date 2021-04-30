using System;
using System.Collections.Generic;
// dotnet watch run

namespace puzzles
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(RandomArray());
            // Console.WriteLine($"The TossCoin function returned: {TossCoin()}");
            // Console.WriteLine(TossMultipleCoins(5));
            Console.WriteLine(Names());
        }

        // Random Array
        public static int[] RandomArray()
        {
            int[] randArr = new int[10];
            int min = int.MaxValue;
            int max = int.MinValue;
            int sum = 0;
            Random rand = new Random();
            for (int i = 0; i < 10; i++) 
            {
                randArr[i] = rand.Next(5, 26);
                if (randArr[i] < min)
                {
                    min = randArr[i];
                }
                else if (randArr[i] > max)
                {
                    max = randArr[i];
                }
                sum += randArr[i];
            }
            Console.WriteLine(String.Join(", ", randArr));
            Console.WriteLine(min);
            Console.WriteLine(max);
            Console.WriteLine(sum);
            return randArr;
        }

        // Coin Flip
        public static string TossCoin()
        {
            Random rand = new Random();
            string result = "";
            int flip = rand.Next(0,2);
            if (flip == 0) 
            {
                result = "Tails";
            }
            else if (flip == 1)
            {
                result = "Heads";
            }
            Console.WriteLine("Tossing a Coin!");
            Console.WriteLine(result);
            return result;
        }

        public static Double TossMultipleCoins(int num)
        {
            double headsCount = 0;
            for(int i =0; i < num; i++) {
                string tossResult = TossCoin();
                if (tossResult == "Heads")
                {
                    headsCount++;
                }
            }
            return headsCount / num;
        }

        //Names
        public static List<string> Names()
        {
            Random rand = new Random();
            List<string> people = new List<string>{
                "Todd", "Tiffany", "Charlie", "Geneva", "Sydney"
            };

            List<string> longerList = new List<string>();
            
            for (int i = 0; i < people.Count; i++)
            {
                int num = rand.Next(0, people.Count);
                string temp = people[i];
                people[i] = people[num];
                people[num] = temp;
            }
            Console.WriteLine(String.Join(", ", people));
            for (int i = 0; i < people.Count; i++)
            {
                if(people[i].Length > 5)
                {
                    longerList.Add(people[i]);
                }
            }
            Console.WriteLine(String.Join(", ", longerList));
            return(longerList);
        }
    }
}
