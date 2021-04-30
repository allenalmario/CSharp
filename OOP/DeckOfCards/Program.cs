using System;

namespace DeckOfCards
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck myDeck = new Deck(); // makes a deck instance
            Console.WriteLine(myDeck.Deal());
            Console.WriteLine(myDeck.Cards.Count);
            myDeck.Reset();
            Console.WriteLine(myDeck.Cards.Count);
            myDeck.Shuffle();
            Player myPlayer = new Player("John");
            myPlayer.Draw(myDeck);
            Console.WriteLine(myDeck.Cards.Count);
            myPlayer.Draw(myDeck);
            myPlayer.Draw(myDeck);
            myPlayer.Draw(myDeck);
            myPlayer.Draw(myDeck);
            Console.WriteLine(myDeck.Cards.Count);
            myPlayer.Discard(myPlayer.Hand, 3);
            Console.WriteLine(myPlayer.Hand.Count);
        }
    }
}
