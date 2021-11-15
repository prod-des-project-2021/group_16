using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Cell
    {
        public int RowNumber { get; set; } = 0;
        public int ColomnNumber { get; set; } = 0;
        public bool CurrentlyOccupied { get; set; } = false;
        public bool LegalNextMove { get; set; } = false;
        public string Color { get; set; } = "None";

        public Cell()
        {
            /*
            RowNumber = (int)Math.Ceiling( (double) x / 8);
            ColomnNumber = x % 8;
       */
            }

        public static implicit operator Cell(int v)
        {
            throw new NotImplementedException();
        }
    }
}
