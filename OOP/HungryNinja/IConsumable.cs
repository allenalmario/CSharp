namespace HungryNinja
{
    public interface IConsumable
    {
        // Properties
        string Name {get;set;}
        int Calories {get;set;}
        bool IsSpicy {get;set;}
        bool IsSweet {get;set;}
        // Method Signature
        string getInfo();
    }
}