using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CheckersProject._2
{
    class Board 
    {

        const int mapSize = 8;


        public Board(Form form, int size)
        {
            form.Width = (8) * size + 60 + 200;
            form.Height = (8) * size + 80;
            //form.BackColor = Color.FromArgb(103, 72, 70);

        }


        public int[,] map = new int[8, 8] {

                {0,1,0,1,0,1,0,1},
                {1,0,1,0,1,0,1,0},
                {0,1,0,1,0,1,0,1},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {2,0,2,0,2,0,2,0},
                {0,2,0,2,0,2,0,2},
                {2,0,2,0,2,0,2,0},

            };

        public bool IsInsideBorders(int ti, int tj)// Находится ли внутри поля
        {
            if (ti >= mapSize || tj >= mapSize || ti < 0 || tj < 0)
            {
                return false;
            }
            return true;
        }

        public void InitMarkup(Form form)//Инициализация разметки
        {
            Label top = new Label();
            top.Text = "   A                 B                 C                  D                  E                  F                  G                 H";
            top.Location = new Point(17, 0);
            top.Size = new Size(8*60 + 6, 17);
            form.Controls.Add(top);
            top.BackColor = Color.FromArgb(128, 79, 45);
            top.ForeColor = Color.White;
            top.TextAlign = ContentAlignment.MiddleCenter;

            Label bottom = new Label();
            bottom.Text = "   A                 B                 C                  D                  E                  F                  G                 H";
            bottom.Location = new Point(17, 60*8+23);
            bottom.Size = new Size(8 * 60 + 6, 17);
            form.Controls.Add(bottom);
            bottom.BackColor = Color.FromArgb(128, 79, 45);
            bottom.ForeColor = Color.White;
            bottom.TextAlign = ContentAlignment.MiddleCenter;

            Label l8 = new Label(), l7 = new Label(), l6 = new Label(), l5 = new Label(), l4 = new Label(), l3 = new Label(), l2 = new Label(), l1 = new Label();
            l8.Text = "                8";
            l8.Location = new Point(0, 0);
            l8.Size = new Size(17, 100);
            l8.BackColor = Color.FromArgb(128, 79, 45);
            l8.ForeColor = Color.White;
            l7.Text = " 7";
            l7.Location = new Point(0, 100);
            l7.Size = new Size(17, 70);
            l7.BackColor = Color.FromArgb(128, 79, 45);
            l7.ForeColor = Color.White;
            l6.Text = " 6";
            l6.Location = new Point(0, 160);
            l6.Size = new Size(17, 70);
            l6.BackColor = Color.FromArgb(128, 79, 45);
            l6.ForeColor = Color.White;
            l5.Text = " 5";
            l5.Location = new Point(0, 220);
            l5.Size = new Size(17, 70);
            l5.BackColor = Color.FromArgb(128, 79, 45);
            l5.ForeColor = Color.White;
            l4.Text = " 4";
            l4.Location = new Point(0, 280);
            l4.Size = new Size(17, 70);
            l4.BackColor = Color.FromArgb(128, 79, 45);
            l4.ForeColor = Color.White;
            l3.Text = " 3";
            l3.Location = new Point(0, 340);
            l3.Size = new Size(17, 70);
            l3.BackColor = Color.FromArgb(128, 79, 45);
            l3.ForeColor = Color.White;
            l2.Text = " 2";
            l2.Location = new Point(0, 400);
            l2.Size = new Size(17, 70);
            l2.BackColor = Color.FromArgb(128, 79, 45);
            l2.ForeColor = Color.White;
            l1.Text = " 1";
            l1.Location = new Point(0, 460);
            l1.Size = new Size(17, 60);
            l1.BackColor = Color.FromArgb(128, 79, 45);
            l1.ForeColor = Color.White;
            form.Controls.Add(l1);
            form.Controls.Add(l2);
            form.Controls.Add(l3);
            form.Controls.Add(l4);
            form.Controls.Add(l5);
            form.Controls.Add(l6);
            form.Controls.Add(l7);
            form.Controls.Add(l8);


            Label r8 = new Label(), r7 = new Label(),r6 = new Label(), r5 = new Label(), r4 = new Label(), r3 = new Label(), r2 = new Label(), r1 = new Label();
            r8.Text = "                8";
            r8.Location = new Point(8 * 60 + 23, 0);
            r8.Size = new Size(17, 100);
            r8.BackColor = Color.FromArgb(128, 79, 45);
            r8.ForeColor = Color.White;
            r7.Text = " 7";
            r7.Location = new Point(8 * 60 + 23, 100);
            r7.Size = new Size(17, 70);
            r7.BackColor = Color.FromArgb(128, 79, 45);
            r7.ForeColor = Color.White;
            r6.Text = " 6";
            r6.Location = new Point(8 * 60 + 23, 160);
            r6.Size = new Size(17, 70);
            r6.BackColor = Color.FromArgb(128, 79, 45);
            r6.ForeColor = Color.White;
            r5.Text = " 5";
            r5.Location = new Point(8 * 60 + 23, 220);
            r5.Size = new Size(17, 70);
            r5.BackColor = Color.FromArgb(128, 79, 45);
            r5.ForeColor = Color.White;
            r4.Text = " 4";
            r4.Location = new Point(8 * 60 + 23, 280);
            r4.Size = new Size(17, 70);
            r4.BackColor = Color.FromArgb(128, 79, 45);
            r4.ForeColor = Color.White;
            r3.Text = " 3";
            r3.Location = new Point(8 * 60 + 23, 340);
            r3.Size = new Size(17, 70);
            r3.BackColor = Color.FromArgb(128, 79, 45);
            r3.ForeColor = Color.White;
            r2.Text = " 2";
            r2.Location = new Point(8 * 60 + 23, 400);
            r2.Size = new Size(17, 70);
            r2.BackColor = Color.FromArgb(128, 79, 45);
            r2.ForeColor = Color.White;
            r1.Text = " 1";
            r1.Location = new Point(8 * 60 + 23, 460);
            r1.Size = new Size(17, 60);
            r1.BackColor = Color.FromArgb(128, 79, 45);
            r1.ForeColor = Color.White;
            form.Controls.Add(r1);
            form.Controls.Add(r2);
            form.Controls.Add(r3);
            form.Controls.Add(r4);
            form.Controls.Add(r5);
            form.Controls.Add(r6);
            form.Controls.Add(r7);
            form.Controls.Add(r8);

            Label lineTop = new Label();
            lineTop.Size = new Size(60 * 8 + 6, 3);
            lineTop.Location = new Point(17, 17);
            lineTop.BackColor = Color.FromArgb(64, 41, 24);
            form.Controls.Add(lineTop);

            Label lineBottom = new Label();
            lineBottom.Size = new Size(60 * 8 +6, 3);
            lineBottom.Location = new Point(17,60*8 +20);
            lineBottom.BackColor = Color.FromArgb(64, 41, 24);
            form.Controls.Add(lineBottom);

            Label lineLeft = new Label();
            lineLeft.Size = new Size(3, 60 * 8);
            lineLeft.Location = new Point(17,20);
            lineLeft.BackColor = Color.FromArgb(64, 41, 24);
            form.Controls.Add(lineLeft);

            Label lineRight = new Label();
            lineRight.Size = new Size(3, 60 * 8);
            lineRight.Location = new Point(60 * 8 + 20, 20);
            lineRight.BackColor = Color.FromArgb(64, 41, 24);
            form.Controls.Add(lineRight);


        }

    }
}
