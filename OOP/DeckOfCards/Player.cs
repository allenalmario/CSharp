using System;
using System.Collections.Generic;

namespace DeckOfCards
{
    public class Player
    {
        public string Name {get; set;}
        public List<Card> Hand {get; set;} = new List<Card>();

        public Player(string name)
        {
            Name = name;
        }

        public Card Draw(Deck deck)
        {
            Card drawnCard = deck.Deal();
            Hand.Add(drawnCard);
            return drawnCard;
        }

        public Card Discard(List<Card> Hand, int idx)
        {
            if(Hand[idx] == null)
            {
                return null;
            }
            Hand.RemoveAt(idx);
            return Hand[idx];
        }
    }

}