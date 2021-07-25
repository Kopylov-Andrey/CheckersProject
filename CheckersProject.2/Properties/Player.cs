using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CheckersProject._2
{
    class Player 
    {
       public Checkers[] arrayCheckers;


      
        public Player()// конструктор
        {
            arrayCheckers = new Checkers[12];

            for (int i = 0; i < 12; i++)
            {
                Checkers checkers = new Checkers();

                arrayCheckers[i] = checkers;

            }
        }
   
       
    
    
    }
}
