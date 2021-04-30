using System;
using System.Collections.Generic;

namespace CollectionsPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numArray = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

            string[] nameArray = {"Tim", "Martin", "Nikki", "Sara"};

            bool[] randArray = new bool[10];
            for (int i = 0; i < randArray.Length; i++) 
            {
                if (i % 2 == 0) 
                {
                    randArray[i] = true;
                }
                else
                {
                    randArray[i] = false;
                }
            }
            for (int j = 0; j < randArray.Length; j++) 
            {
                Console.WriteLine(randArray[j]);
            }

            List<string> flavors = new List<string>();
            flavors.Add("Vanilla");
            flavors.Add("Strawberry");
            flavors.Add("Chocolate");
            flavors.Add("Sherbert");
            flavors.Add("Mint");
            Console.WriteLine(flavors[2]);
            flavors.RemoveAt(2);
            Console.WriteLine(flavors.Count);

            Random rand = new Random();
            Dictionary<string,string> myDict = new Dictionary<string,string>();
            for (int i = 0; i < nameArray.Length; i++) 
            {
                myDict.Add(nameArray[i], flavors[i]);
            }
            foreach (KeyValuePair<string,string> person in myDict) 
            {
                Console.WriteLine(person.Key + " " + person.Value);
            }
        }
    }
}
