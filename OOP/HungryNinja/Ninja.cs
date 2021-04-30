using System;
using System.Collections.Generic;

namespace HungryNinja 
{
    public abstract class Ninja 
    {
        protected int calorieIntake;
        public List<IConsumable> ConsumptionHistory;
        public Ninja()
        {
            calorieIntake = 0;
            ConsumptionHistory = new List<IConsumable>();
        }
        public abstract bool IsFull {get;}
        public abstract void Consume(IConsumable item);
        // private int calorieIntake;
        // public List<Food> FoodHistory;
        // // constructor
        // public Ninja()
        // {
        //     calorieIntake = 0;
        //     FoodHistory = new List<Food>();
        // }
        // public bool IsFull
        // {
        //     get
        //     {
        //         if(calorieIntake > 1200)
        //             return true;
        //         return false;
        //     }
        // }
        // public void Eat(Food Item)
        // {
        //     if(this.IsFull == true)
        //     {
        //         Console.WriteLine("This Ninja is full and can't eat anymore!");
        //     }
        //     else if(this.IsFull == false)
        //     {
        //         Console.WriteLine("-------------------------------------------------------");
        //         Console.WriteLine($"Name: {Item.Name}, Calories: {Item.Calories} Spicy: {Item.IsSpicy}, Sweet: {Item.IsSweet}");
        //         Console.WriteLine($"Adding {Item.Calories} to Calorie Intake!");
        //         this.calorieIntake += Item.Calories;
        //         FoodHistory.Add(Item);
        //         Console.WriteLine($"Calorie Intake: {calorieIntake}");
        //         Console.WriteLine(String.Join(", ", FoodHistory));
        //         Console.WriteLine("-------------------------------------------------------");
        //     }
        }
}