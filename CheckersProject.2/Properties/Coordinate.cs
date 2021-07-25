using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersProject._2
{
    class Coordinate
    {

        private int x;

        private int y;
        public Coordinate(int i, int j)
        {
            x = j;
            y = i;
        }






        public int X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

    }
}
