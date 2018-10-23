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
                        case (13): return "King";
                        case (14): return "Ace";
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
                 return CardName;
             }
            public int CompareTo(Card obj)
            {
                return this.Number.CompareTo(obj.Number);
            }
        }
        static void Main(string[] args)
        {
            Card a = new Card(Colors.red, 2);
            Card b = new Card(Colors.red, 5);
            if (a.CompareTo(b)==0) Console.WriteLine("fgh");

        }
    }
}
