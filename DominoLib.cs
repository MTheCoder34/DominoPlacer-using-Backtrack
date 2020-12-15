using System;
using System.Linq;

namespace Domino
{
    public struct DominoRecord
    {
        public int foreface;
        public int backface;
        public DominoRecord(int fore, int back)
        {
            foreface = fore;
            backface = back;
        }
    } //Domino record

    class DominoSequence
    {
        private static int startpoint = 1; /*
                                            This variable's purpose is if the backtrack
                                            enters a false route and comes out of it, it
                                            starts from the next point to the wrong point,
                                            so it keeps track of the previous position, therefore
                                            the algorithm does not fall into infinite iteration */

        private static DominoRecord[] records = new DominoRecord[10]; //The domino records

        private static int len = 0; // Total length

        public static int[] Sequence; // The sequence which we store the dominos in

        public static bool IsRoutePossible; // Tells whether the game is possible to solve with using up all dominos

        private void SwapDomino(int i)
        {
            int temp = records[i].foreface;
            records[i].foreface = records[i].backface;
            records[i].backface = temp;
        } // Swaps the domino

        public void Append(DominoRecord d)
        {
            if(len != records.Length)
            {
                records[len] = d;
                len++;
            }
            else
            {
                Array.Resize(ref records, records.Length + 10);
                records[len] = d;
                len++;
            }
        } // Append domino to the records

        public DominoSequence()
        {

        } //Constructor

        private bool BadSequence(int i, int j)
        {
            if(i == 0)
            {
                return false;
            }
            if(Sequence[i] == j)
            {
                return true;
            }
            else if(!Sequence.Contains(j))
            {
                if (records[j].foreface == records[Sequence[i-1]].backface)
                {
                    return false;
                }
                else if (records[j].backface == records[Sequence[i-1]].backface)
                {
                    SwapDomino(j);
                    return false;
                }
                if (records[j].backface == records[Sequence[i - 1]].foreface)
                {
                    SwapDomino(j);
                    return false;
                }
                else if(i - 1 == 0 && records[Sequence[i - 1]].foreface == records[j].foreface)
                {
                    SwapDomino(Sequence[i - 1]);
                    return false;
                }
                else if (i - 1 == 0 && records[Sequence[i - 1]].foreface == records[j].backface)
                {
                    SwapDomino(Sequence[i - 1]);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        } //Bad sequence search

        private void GoodSequence(int i, ref int j, ref bool Good)
        {
            j = startpoint;
            startpoint = 1;
            while(j < len && BadSequence(i, j))
            {
                j++;
            }
            if (j < len)
            {
                Good = true;
                if(Sequence[0] > 0)
                {
                    startpoint = 0;
                }
            }
            else Good = false;
        } //Good sequence search

        public void CreateSequence()
        {
            Array.Resize(ref records, len);
            Sequence = new int[len];
            Random rnd = new Random();
            Sequence[0] = 0;
            for (int f = 1; f < len; f++)
            {
                Sequence[f] = -1;
            }

            int i = 1;
            
            while(i >= 0 && i < len) //The head of the backtrack algorithm
            {
                int j = 0;
                bool Good = false;

                if(i > 0)
                {
                    GoodSequence(i, ref j, ref Good);
                }
                else
                {
                    GoodSequence(0, ref j, ref Good);
                }

                if (Good)
                {
                    Sequence[i] = j;
                    i++;
                }
                else if(i == 0)
                {
                    i = len + 1;
                }
                else
                {
                    i--;
                    startpoint = Sequence[i] + 1;
                    Sequence[i] = -1;
                }
            }
            IsRoutePossible = i <= len;
            if(IsRoutePossible == true)
            {
                for (int e = 0; e < len; e++)
                {
                    Console.WriteLine(Sequence[e]);
                }
            }
            else
            {
                Console.WriteLine("Game cannot be solved with using up all dominos");
            }
        } //Search sequence
    } //Domino sequence
}
