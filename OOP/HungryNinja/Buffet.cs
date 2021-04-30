using System;
using System.Collections.Generic;

namespace HungryNinja
{
public class Buffet
{
    // access level, field data type, field name
    public List<Food> Menu;

    // constructor
    public Buffet()
    {
        Menu = new List<Food>()
        {
            new Food("Cheeseburger", 343, false, false),
            new Food("Pizza", 237, false, false),
            new Food("Orange Chicken", 660, false, false),
            new Food("Turkey Sandwich", 308, false, false),
            new Food("Spaghetti", 220, false, false),
            new Food("Ice Cream", 136, false, true),
            new Food("Brownies", 114, false, true)
        };
    }
    public Food Serve()
    {
        Random rand = new Random();
        int randNum = rand.Next(0, Menu.Count);
        return Menu[randNum];
    }
}
}