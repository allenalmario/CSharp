using System;
using System.Collections.Generic;

namespace DeckOfCards
{
    public class Deck
    {
        public List<Card> Cards {get; set;} = new List<Card>();

        public Deck()
        {
            Reset();
        }
        
        public Card Deal() // works
        {
            Card lastCard = Cards[Cards.Count - 1];
            Cards.Remove(lastCard);
            return lastCard;
        }
        public void Reset()
        {
            Cards = new List<Card>();
            // Use loop to create the cards
            // loop 4 times for each suit
            string[] suits = new string[4]
            {
                "Hearts",
                "Diamonds",
                "Clubs",
                "Spades"
            };

            Dictionary<int, string> cardNamesTable = new Dictionary<int, string>()
            {
                {1, "Ace"},
                {11, "Jack"},
                {12, "Queen"},
                {13, "King"}
            };

            foreach (string suit in suits) // makes 52 card deck
            {
                for(int i = 1; i <= 13; i++)
                {
                    string cardName = i.ToString();

                    if(cardNamesTable.ContainsKey(i))
                    {
                        cardName = cardNamesTable[i];
                    }
                    Card newCard = new Card(cardName, suit, i);
                    Cards.Add(newCard);
                }
            }
        }

        public void Shuffle()
        {
            Random rand = new Random();
            for (int i = 0; i < Cards.Count; i++)
            {
                Card temp = Cards[i];
                Cards[i] = Cards[rand.Next(0, Cards.Count)];
                Cards[rand.Next(0, Cards.Count)] = temp;
            }
        }
    }

}