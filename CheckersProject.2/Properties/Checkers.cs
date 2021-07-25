using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CheckersProject._2
{
    class Checkers 
    {

        private Image whiteFigure = new Bitmap(new Bitmap(@"Sprites\w.png"), new Size(50, 50));
            

        private Image blackFigure = new Bitmap(new Bitmap(@"Sprites\b.png"), new Size(50, 50));


     







        //public Color CheckerWhite
        //{
        //    get
        //    {
        //        return Color.White;
        //    }
        //    set
        //    {

        //    }
        //}





        public Image imagePlayer1 // свойство картинки 
        {
            get
            {
                return whiteFigure;
            }
        }

        public Image imagePlayer2 // свойство картинки 
        {
            get
            {
                return blackFigure;
            }
        }


    }
}
