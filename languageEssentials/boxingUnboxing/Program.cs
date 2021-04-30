using System;
using System.Collections.Generic;

namespace boxingUnboxing
{
    class Program
    {
        static void Main(string[] args)
        {
            List <object> BoxedData = new List<object>();
            BoxedData.Add(7);
            BoxedData.Add(28);
            BoxedData.Add(-1);
            BoxedData.Add(true);
            BoxedData.Add("chair");

            for (int i = 0; i < BoxedData.Count; i++) 
            {
                Console.WriteLine(BoxedData[i]);
            }

            int count = 0;

            for (int j = 0; j < BoxedData.Count; j++) 
            {
                if (BoxedData[j] is int) 
                {
                    int num = (int)BoxedData[j];
                    count = count + num;
                }
            }
            Console.WriteLine(count);
        }
    }
}
