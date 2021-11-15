using System;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board myboard = new Board();

            printBoard(myboard);
          //  printInfo(myboard);
        }

        private static void printInfo(Board myboard)
        {
            for (int i = 0; i < 64; i++)
            {
                Cell c = myboard.cell[i];
                Console.WriteLine(c.ColomnNumber + " " + c.Color);
            }
        }

        private static void printBoard(Board myboard)
        {

            /*
            for (int i = 0; i < 8; i++)
            {

                Cell c = myboard.cell[i];
                if (c.RowNumber % 2 == 0)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        //Console.Write("□");
                        if (j % 3 == 0)
                        {
                          //  Console.WriteLine();
                        }
                    }

                }

            }*/

            for (int i = 0; i < 64; i++)
            {

                Cell c = myboard.cell[i];

                if (c.CurrentlyOccupied == true)
                {
                    Console.Write("X");
                }
                else if (c.LegalNextMove == true)
                {
                    Console.Write("+");
                }
                else if (c.Color == "B")
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("   ");
                }
                else if (c.Color == "W")
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("   ");
                }


                if (i > 0 && i % 8 == 7)
                {
                    Console.WriteLine();
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
} 
