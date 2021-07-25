using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace CheckersProject._2
{
    class Option
    {


        RadioButton radioButton1;
        RadioButton radioButton2;
        public RadioButton White;
        public RadioButton Black;
        public Panel panel;
        public Button backButton;
        public Button optionButton;
        Form form;
        Label ottention;

        public Option(Form form)
        {
            optionButton = new Button();
            optionButton.Size = new Size(170, 50);
            optionButton.Location = new Point(60 * 8 + 60, 70);
            form.Controls.Add(optionButton);
            optionButton.Text = ("Настройки");
       


            radioButton1 = new RadioButton();
            radioButton1.Text = "Играть вдвоем";
            radioButton1.Location = new Point(60 * 8 + 60, 120);
            

            radioButton2 = new RadioButton();
            radioButton2.Size = new Size(160, 30);
            radioButton2.Text = "Играть с компьютером";
            radioButton2.Location = new Point(60*8 + 60, 140);
            

            backButton = new Button();
            backButton.Location = new Point(60 * 8 + 60, 270);
            backButton.Size = new Size(70, 20);
            backButton.Text = "применить";

            panel = new Panel();
            panel.Size = new Size(180, 100);
            panel.Location = new Point(60 * 8 + 60, 190);

            White = new RadioButton();
            White.Size = new Size(60, 20);
            
            White.Text = "Белые";
            White.Location = new Point(0, 0);

            Black = new RadioButton();
            Black.Text = "Черные";
            Black.Size = new Size(70, 20);
            Black.Location = new Point(60, 0);

            //ottention = new Label();
            //ottention.Text = "(Выбор доступен только при \nигре с компьютером!)";
            //ottention.Location = new Point(0, 20);
            //ottention.Size = new Size(180, 30);
            //ottention.ForeColor = Color.Red; 
        }

        public void Init(Form form1)
        {
            form = form1;
           
            form.Controls.Add(radioButton1);
            form.Controls.Add(radioButton2);
            form.Controls.Add(backButton);
            radioButton1.Focus();
            
            //form.Controls.Add(panel);

            //radioButton1.Checked = true
            


        }

       public void BlacOrWhiteInit()
        {
            panel.Controls.Add(White);
            
            panel.Controls.Add(Black);
            //panel.Controls.Add(ottention);
           
        }



        public bool BotIsPlay()
        {
            
            if (radioButton1.Checked == true)
            {
                return false;  
            }
            if (radioButton2.Checked == true)
            {
                return true;
            }
            return  false;

        }

        public bool BlacOrWhite()
        {
            if (White.Checked == true)
            {
                return true;
            }
            if (Black.Checked == true)
            {
                return false;
            }
            return true;
        }



    }
}
