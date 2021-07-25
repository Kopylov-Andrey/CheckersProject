using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CheckersProject._2
{
    class InitialMenu // Инициализация начального меню
    {
        public Button playButton;

        public Button authorization;

        public Button entry;

        public Button tabelRecords;


        

        public Label label1;
        Label label2;
        Label label3;
        Label label4;
        Label label5;
        Label label6;
        Form form1;
        public TextBox textBox1;
        TextBox textBox2;

        DataGridView dataGridView1;
        Button button1;
        Button button2;
        public Button exit;
        public Button spravka;
        //Button button3;
        TextBox text1;
        TextBox text2;
        //TextBox text3;

        public Button home;


        Query controller;

        



        public InitialMenu(Form form)
        {
            form1 = form;
            controller = new Query(CollectionString.ConnStr);

            label1 = new Label();
            label1.Text = ("ШАШКИ");
            label1.Location = new Point(250, 25);
            form.Controls.Add(label1);
            label1.Font = new Font(label1.Font.FontFamily, 40, label1.Font.Style);
            label1.Size = new Size(250, 70);

            playButton = new Button();
            playButton.Size = new Size(200, 50);
            playButton.Location = new Point(250, 100);
            form.Controls.Add(playButton);
            playButton.Text = ("Играть как гость");

            label2 = new Label();
            label2.Text = ("Никнейм");
            label2.Location = new Point(250, 165);
            form.Controls.Add(label2);

            textBox1 = new TextBox();
            textBox1.Location = new Point(250, 190);
            form.Controls.Add(textBox1);
            textBox1.Size = new Size(200, 30);

            label3 = new Label();
            label3.Text = ("Пароль");
            label3.Location = new Point(250, 225);

            form.Controls.Add(label3);

            textBox2 = new TextBox();
            textBox2.Location = new Point(250, 250);
            textBox2.UseSystemPasswordChar = true;
            form.Controls.Add(textBox2);
            textBox2.Size = new Size(200, 30);

            entry = new Button();
            form.Controls.Add(entry);
            entry.Text = ("Вход");
            entry.Size = new Size(200, 50);
            entry.Location = new Point(250, 280);
           
            authorization = new Button();
            form.Controls.Add(authorization);
            authorization.Text = ("Регистрация");
            authorization.Size = new Size(200, 50);
            authorization.Location = new Point(250, 350);

            tabelRecords = new Button();
            tabelRecords.Size = new Size(200, 50);
            tabelRecords.Location = new Point(250, 420);
            form.Controls.Add(tabelRecords);
            tabelRecords.Text = ("Таблица Рекордов");

            home = new Button();
            home.Location = new Point(640, 10);
            
            home.Size = new Size(30, 30);


            exit = new Button();
            exit.Size = new Size(70, 20);
            exit.Location = new Point(600, 480);
            form.Controls.Add(exit);
            exit.Text = ("Выход");

            spravka = new Button();
            spravka.Size = new Size(70, 20);
            spravka.Location = new Point(50, 480);
            form.Controls.Add(spravka);
            spravka.Text = ("Справка");

            //form.BackgroundImage = Image.FromFile(@"fon2.jpg");
        }
        

        public void autorization()
        {
           
            // отображает таблицу из бд.

            //button1 = new Button();
            //button1.Location = new Point(200, 200);
            //form1.Controls.Add(button1);

            // кнопка обновления таблицы


            button2 = new Button();
            button2.Location = new Point(320, 280);
            button2.Text = ("Зарегистрироваться");
            form1.Controls.Add(button2);

            //button3 = new Button();
            //button3.Location = new Point(200, 260);
            //form1.Controls.Add(button3);

            label4 = new Label();
            label4.Text = ("Введите никнейм");
            label4.Location = new Point(200, 170);
            form1.Controls.Add(label4);

            text1 = new TextBox();
            text1.Location = new Point(200, 200);
            form1.Controls.Add(text1);

            label5 = new Label();
            label5.Text = ("Введите пароль");
            label5.Location = new Point(200, 250);
            form1.Controls.Add(label5);

            text2 = new TextBox();
            text2.Location = new Point(200, 280);
            text2.UseSystemPasswordChar = true;
            form1.Controls.Add(text2);
            


            //text3 = new TextBox();
            //text3.Location = new Point(295, 260);
            //form1.Controls.Add(text3);

            label6 = new Label();
            label6.Text = ("Заполните все поля");
            label6.Location = new Point(200, 320);
            label6.ForeColor = Color.Red;
            label6.Size = new Size(120, 20);
            


            //button1.Click += button1_Click;
            button2.Click += button2_Click_User_Registration;
            //button3.Click += button3_Click;


            form1.Controls.Add(home);

        }


        public void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = controller.UpdatePerson();
        }

        public void button2_Click_User_Registration(object sender, EventArgs e)
        {
            if(text1.Text == "" || text2.Text == "")
            {
                form1.Controls.Add(label6);
            }
            else {
                controller.Add(text1.Text, text2.Text);
                MessageBox.Show("Пользователь успешно зарегистрирован");
                label6.Visible = false;
            }

            
        }
        //public void button3_Click(object sender, EventArgs e)
        //{
        //    controller.
        //}

        public bool User_Veriication()
        {
            string[] name = controller.Read_name();
            string[] password = controller.Read_password();


            for (int i = 0; i < controller.FieldCount(); i++)
            {
                if(name[i] == textBox1.Text && password[i] == textBox2.Text)
                {
                    return true;
                }
            }

           
            

            return false;
        }

        internal void TabelRecords_Click()
        {

            dataGridView1 = new DataGridView();
            dataGridView1.Location = new Point(170, 170);
            dataGridView1.DataSource = controller.UpdatePerson();
            form1.Controls.Add(dataGridView1);
        }


        public void Exit_Click(object sender, EventArgs e)
        {
            form1.Close();
        }

        public void Spravka_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Все шашки, участвующие в партии, выставляются перед началом игры на доску. Далее они передвигаются по полям доски и могут быть сняты с нее в случае боя шашкой противника \n\n\n Копылов Андрей","Справка" );
        }





    }
}
