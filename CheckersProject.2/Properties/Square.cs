using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Management;

namespace CheckersProject._2
{
    
    class Square
    {
        
        int cellSize = 60;


        Button button;
        Coordinate coordinate;

        //private void Show_oneFig(Deleg deleg)
        //{
        //    deleg.Invoke(button);
        //}

        public Square(Form form, int y, int x/*, FunDelegate Deleg*/) // конструктор 
        {
            
            button = new Button();
            button.Size = new Size(cellSize, cellSize);
            button.Location = new Point(20+x * cellSize,20 + y * cellSize);
            form.Controls.Add(button);
            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = FlatStyle.Flat;



            coordinate = new Coordinate(y, x);
        }
        ~Square()
        {

        }


        private void Button_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public Color BackColor  // свойство цвета клетки 
        {
            set
            {
                button.BackColor = value;
            }

        }

        public int CoordinateX
        {
            get
            {
                return coordinate.X;
            }
        }

        public int CoordinateY
        {
            get
            {
                return coordinate.Y;
            }
        }

        

        public string Text
        {
            get
            {
                return button.Text;
            }
            set
            {
                button.Text = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return button.Enabled;
            }
            set
            {
                button.Enabled = value;
            }
        }

        public Image Image
        {
            get
            {
                return button.Image;
            }
            set
            {
                button.Image = value;
            }
        }

        public Button Button
        {
            get
            {
                return button;
            }
            set
            {
                button = value;
            }
        }


       

    }
}
