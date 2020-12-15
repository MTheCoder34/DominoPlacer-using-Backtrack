using System;

namespace Domino
{
    class Program
    {
        static void Main(string[] args)
        {
            DominoSequence ds = new DominoSequence();
            ds.Append(new DominoRecord(2, 3));
            ds.Append(new DominoRecord(1, 4));
            ds.Append(new DominoRecord(3, 4));
            ds.Append(new DominoRecord(5, 2));
            ds.Append(new DominoRecord(1, 9));
            ds.Append(new DominoRecord(9, 6));
            ds.CreateSequence();

            Console.ReadKey();
        }
    }
}
