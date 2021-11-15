using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Board
    {
        //        public Cell[] myCell = new Cell[64];


        public Cell[] cell = new Cell[64];

        public Board()
        {

            for (int i = 0; i < 64; i++)
            {
                cell[i] = new Cell { RowNumber = 0, ColomnNumber = 0, CurrentlyOccupied = false, LegalNextMove = false, Color = "None" };
            }

            bool oddRow = true;

            for (int i = 0; i < cell.Length; i += 8)
            {
                if (oddRow)
                {
                    for (int j = i; j <= i + 6; j += 2)
                    {
                        cell[j].Color = "B";
                    }

                    for (int j = i + 1; j <= i + 6; j += 2)
                    {
                        cell[j].Color = "W";
                    }

                    oddRow = false;
                }
                else
                {
                    for (int j = i; j <= i + 6; j += 2)
                    {
                        cell[j].Color = "W";
                    }

                    for (int j = i + 1; j <= i + 6; j += 2)
                    {
                        cell[j].Color = "B";
                    }

                    oddRow = true;
                }
            }



            /*
            if (i % 2 != 0)
            {
                cell[i].Color = "W";
            }
            else
            {
                cell[i].Color = "B";
            }
            if (i != 0 && i % 8 == 0 && i < 64)
            {
                cell[i].Color = cell[i-1].Color;
            }*/
        }



        public void LegalMove(Cell currentCell, string chessPiece)
        {
            switch (chessPiece)
            {
                case "Pawn":
                    break;

                case "Rook":
                    break;

                case "Knight":
                    break;

                case "Bishop":
                    break;

                case "King":
                    break;

                case "Queen":
                    break;

                default:
                    break;
            }

        }

    }
}
