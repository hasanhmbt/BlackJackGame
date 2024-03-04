




using System.Reflection;

namespace BlackJack
{

    class Program
    {

        static void Main()
        {
            Console.WriteLine("Welcome to BlackJack have fun !\n");

            var deck = new Deck();
            deck.Shuffle();

            var playerHand = new Hand();
            var dealerHand = new Hand();

            playerHand.AddCard(deck.Deal());
            dealerHand.AddCard(deck.Deal());
            playerHand.AddCard(deck.Deal());
            dealerHand.AddCard(deck.Deal());


            Console.WriteLine($"Your hand: {playerHand}");
            Console.WriteLine($"Dealer's hand: {dealerHand}\n");



            while (true)
            {
                Console.WriteLine("Would you like to hit or stand? (h/s)");
                var input = Console.ReadLine();

                if (input == "h")
                {

                    playerHand.AddCard(deck.Deal());
                    Console.WriteLine($"Your hand {playerHand}");
                    Console.WriteLine($"Dealer's hand: {dealerHand}\n");

                    if (playerHand.Value > 21)
                    {

                        Console.WriteLine("Bust ! YOU Lose !");
                        break;
                    }


                }
                else if (input == "s")
                {
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid input. Please enter 'h' or 's'.");

                }



            }


            if (playerHand.Value <= 21)
            {
                while (dealerHand.Value < 17)
                {
                    dealerHand.AddCard(deck.Deal());
                }


                Console.WriteLine($"Dealer's hand: {dealerHand}\n");

                if (playerHand.Value <= 21 || playerHand.Value < dealerHand.Value)

                    Console.WriteLine("You win !");

                else if (dealerHand.Value == playerHand.Value)

                    Console.WriteLine("Tie !");

                else
                    Console.WriteLine("Dealer wins !");
            }

            Console.WriteLine("Would you like to play again? (y/n)");
            var playAgainInput = Console.ReadLine();

            if (playAgainInput.ToLower() == "y")
                Main();
            else
                Console.WriteLine("Thanks for playing !");

        }



    }




    class Card
    {
        public string Rank { get; }
        public string Suit { get; }


        public Card(string rank, string suit)
        {
            Rank = rank;
            Suit = suit;
        }


        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }


    }




    class Deck
    {
        private List<Card> cards = new List<Card>();

        public Deck()
        {

            var ranks = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "A", "K", "J", "Q", };
            var suits = new string[] { "Hearts", "Diamonds", "Clubs", "Spades" };

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {

                    cards.Add(new Card(rank, suit));
                }
            }



        }


        public void Shuffle()
        {

            var random = new Random();

            cards = cards.OrderBy(c => random.Next()).ToList();


        }


        public Card Deal()
        {


            var card = cards[0];
            cards.RemoveAt(0);
            return card;


        }


    }



    class Hand
    {
        private List<Card> cards = new List<Card>();

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int Value
        {

            get
            {
                var value = 0;
                var aces = 0;

                foreach (var card in cards)
                {
                    if (card.Rank == "A")
                    {
                        aces++;
                    }

                    else if (card.Rank == "J" || card.Rank == "K" || card.Rank == "Q")
                    {
                        value += 10;
                    }
                    else
                    {
                        value += int.Parse(card.Rank);

                    }

                }

                for (int i = 0; i < aces; i++)
                {

                    if (value + 11 <= 21)
                        value += 11;
                    else
                        value += 1;

                }

                return value;

            }

        }

        public override string ToString()
        {
            return $"{string.Join(", ", cards)} (Total: {Value})";

        }

    }






}

