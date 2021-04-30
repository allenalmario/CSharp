using System;

namespace HungryNinja
{
public class Food : IConsumable
{
    public string Name {get;set;}
    public int Calories {get;set;}
    // foods can be spicy and/or sweet
    public bool IsSpicy {get;set;}
    public bool IsSweet {get;set;}
    public string getInfo()
    {
        return $"{Name} Food. Calories: {Calories}. Spicy?: {IsSpicy}, Sweet?: {IsSweet}";
    }
    // add a constructor that takes in all four parameters: Name, Calories, IsSpicy, IsSweet
    public Food(string name, int calories, bool spicy, bool sweet)
    {
        Name = name;
        Calories = calories;
        IsSpicy = spicy;
        IsSweet = sweet;
    }
}
}