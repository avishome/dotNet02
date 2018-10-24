using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    
    class Program
    {
        public enum Colors { red, black }
        public static Random rng = new Random();
        private static object program;

        public class CardStock{
            List<Card> Cards = new List<Card>();

            public CardStock() {
                for (int i = 2; i <= 14; i++) {
                    Cards.Add(new Card(Colors.red, i));
                    Cards.Add(new Card(Colors.black, i));
                }
             }
            public bool this[string namecard] {
                get { return Cards.Exists(e => (e.ToString().StartsWith(namecard))); }
            }
            public Card this[int namecard]
            {
                get { return Cards[namecard]; }
            }
            public void mixing() {
                int n = Cards.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    Card value = Cards[k];
                    Cards[k] = Cards[n];
                    Cards[n] = value;
                }
            }
            public void distrib(params Player[] players) {
                List<Card> tempCards = Cards;
                while(tempCards.Count>=players.Length)
                    foreach (Player p in players) {
                            p.addCard(tempCards.First());
                            tempCards.RemoveAt(0);
                    }
            }
            public void addCard(Card item) { Cards.Insert(0,item); }
            public void removeCard(Card item) { Cards.RemoveAt(0); }
            public List<Card>.Enumerator GetEnumerator() { return Cards.GetEnumerator(); }
            public override string ToString()
            {
                string li = "";
                for (int i = 0; i < Cards.Count; i++) {
                    li +="(" + Cards[i] + ")";
                }
                return li;
            }

        }
        public class Card : IComparable<Card>
        {
            private Colors color { get; set; }
            private int number;
            private int Number { get { return number; } set { if (value <= 14 && value >= 2) number = value; else throw new ArgumentException("num dont good"); } }
            public string CardName {
                get {
                    switch (Number)
                    {
                        case (11): return "jack";
                        case (12): return "queen";
                        case (13): return "king";
                        case (14): return "ace";
                        default:
                            return Number.ToString();
                    }
                }
            }
            public Card(Colors _color, int num) {
                Number = num;
                color = _color;
            }
             public override string ToString() {
                 return CardName+" "+color;
             }
            public int CompareTo(Card obj)
            {
                return this.Number.CompareTo(obj.Number);
            }
        }
        public class Player {
            public string Name { get; set; }
            private Queue<Card> Cards = new Queue<Card>();
            public bool Lose() { return (Cards.Count == 0); }
            public void addCard(params Card[] args) { foreach (Card x in args) { Cards.Enqueue(x); } }
            public override string ToString()
            {
                string temp = "";
                foreach (Card x in Cards) { temp += "(" + x + ")"; }
                return Name + " " + Cards.Count + " \n " + temp;
            }
            public string diteles() { return Name + " " + Cards.Count; }
            public Card pop() { return Cards.Dequeue(); }
        }
        public class Game {
            private CardStock Stock;
            private Player a, b;
            public Game() {
                Stock = new CardStock();
                a = new Player();
                b = new Player();
                a.Name = "ani";
                b.Name = "ata";
            }
            public override string ToString() {
                return a.diteles()+" "+b.diteles();
            }
            public void init() {
                Stock.mixing();
                Stock.distrib(a, b);
            }
            public string iswin() { if (a.Lose()) return b.Name; if (b.Lose()) return a.Name; return ""; }
            public bool gameover() { if (a.Lose() || b.Lose()) return true; return false; }
            public int play() {
                if (!a.Lose() && !b.Lose())
                {
                    Card x = a.pop();
                    Card y = b.pop();
                    Console.WriteLine(x + " " + y);
                    int nextgame = 10;
                    if (x.CompareTo(y) == 0) { Console.WriteLine("boooooom"); nextgame = play(); }
                    if (x.CompareTo(y) > 0 || nextgame == 0) { a.addCard(x, y); return 0; }
                    if (x.CompareTo(y) < 0 || nextgame == 1) { b.addCard(y, x); return 1; }
                }
                return 10;
            }
        }
        static int getnum() { try { return Convert.ToInt32(Console.ReadLine()); } catch(Exception e) { return 1; } }
        static void Main(string[] args)
        {
            
            Game x = new Game();
            x.init();
            int chose = 2;
            Console.WriteLine("to step step game :1, to show all game until the end:0");
            chose = getnum() ;
            while (!x.gameover()) { Console.WriteLine(x); x.play(); if(chose!=0) chose = getnum(); }
            Console.WriteLine(x.iswin()+" is the winner");
        }
    }
}
