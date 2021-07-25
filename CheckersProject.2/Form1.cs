using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace CheckersProject._2
{
    

    
    public   partial class Form1 : Form
    {

        


        const int cellSize = 60;
        const int mapSize = 8;
        Button prevButton; // предыдущая нажатая  кнопка
        Button pressedButton; // нажатая кнопка 
        bool isMoving;// двигается шашка или нет 
        bool isContinue = false;// есть ли еще сЪедобные ходы, кроме 1 
        int countEatSteps = 0;// количество возможных съедобных ходов
        int currentPlayer;
        List<Button> simpleSteps = new List<Button>();
        bool botPlay;
        bool botStep;
        int record;

        InitialMenu StartMenu;
        Board board;
        Player player;
       
        Square[,] masSquare;
        ArtificialIntelligence computer;
        Option option;
        
      


        public Form1()
        {
           

            InitializeComponent();


            this.Text = "Шашки"; // название проекта

            board = new Board(this, cellSize);// объект класса доска

            Start();

            this.BackgroundImageLayout = ImageLayout.Stretch;
            MaximizeBox = false;
           
        }

        public void Start()
        {
            StartMenu = new InitialMenu(this);

            StartMenu.playButton.Click += PlayButtonClic;

            StartMenu.authorization.Click += Authorization_Click;

            StartMenu.entry.Click += Entry_Click;

            StartMenu.home.Click += Home_Click;

            StartMenu.tabelRecords.Click += TabelRecords_Click;

            StartMenu.exit.Click += Exit_Click;

            StartMenu.spravka.Click += Spravka_Click;
        }

        private void Spravka_Click(object sender, EventArgs e)
        {
            StartMenu.Spravka_Click(sender,e);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        public void Init()// инициализация игрового поля
        {

            board = new Board(this, cellSize);// объект класса доска

            if (StartMenu.textBox1.Text != "")//отображаем игрока и его рекорд
            {
                Label usName = new Label();
                usName.Text ="Игрок: " + StartMenu.textBox1.Text + "\nРекорд: 55";
                usName.Size = new Size(200, 40);
                usName.Location = new Point(60 * 8 + 60, 5);

                this.Controls.Add(usName);
            }

                botPlay = option.BotIsPlay();

            if (botPlay)
            {
                this.Controls.Add(option.panel);
                if (option.White.Checked == false && option.Black.Checked == false)
                {
                    option.White.Focus();
                }
            }

            else this.Controls.Remove(option.panel);

            option.optionButton.Click += OptionButtonClick;

            option.backButton.Click += ApplyButtonClick;

            player = new Player();

            masSquare = new Square[mapSize, mapSize]; // массив клеток 8 на 8

            board.InitMarkup(this);

          

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    Square square = new Square(this, i, j);//задаем координаты клеткам
                    
                    square.Button.Click += new EventHandler(OnFigurePress);
                    

                    masSquare[i, j] = square;

                    square.Button.BackColor = GetPrevButtonColor(square.Button);

                    //masSquare[i, j].Text = square.CoordinateY.ToString() + " " + square.CoordinateX.ToString();

                    if (board.map[i, j] == 1)
                    {
                        masSquare[i, j].Image = player.arrayCheckers[i].imagePlayer1;

                    }
                    if (board.map[i, j] == 2)
                    {
                        masSquare[i, j].Image = player.arrayCheckers[i].imagePlayer2;

                    }


                }
            }

            if (botStep)
            {
                currentPlayer = 2;
                SwitchPlayer();
            }
            else currentPlayer = 1;
            
            isMoving = false;
            prevButton = null;



        }

        public void ResetGame()
        {
            bool player1 = false;
            bool player2 = false;

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (board.map[i, j] == 1)
                        player1 = true;
                    if (board.map[i, j] == 2)
                        player2 = true;
                }
            }
            if (!player1)
            {
                MessageBox.Show("Черные победили!","Конец игры!");
                this.Controls.Clear();
                Start();
            }
            if (!player2)
            {
                MessageBox.Show("Белые победили!", "Конец игры!");
                this.Controls.Clear();
                Start();
            }
        }
        
        public void SwitchPlayer()
        {
            currentPlayer = currentPlayer == 1 ? 2 : 1;
            ResetGame();

            if (botPlay)
            {
                if (!botStep) botStep = true;


                if (botStep)
                {
                    computer = new ArtificialIntelligence(board.map, masSquare);
                    int[] mass = computer.ChooseChekers(currentPlayer); // какой ходим
                    int[] mass2;
                    if (masSquare[mass[0], mass[1]].Text == "D")
                    {
                        mass2 = computer.ChooseMove(mass[0], mass[1], false); // куда ходим
                    }
                    else
                    {
                         mass2 = computer.ChooseMove(mass[0], mass[1]); // куда ходим
                    }
                    computer.Replacement(masSquare[mass[0], mass[1]].Button, masSquare[mass2[0], mass2[1]].Button, cellSize); // функция для хода 
                    SwitchButtonToCheat(masSquare[mass2[0], mass2[1]].Button);
                    //while (computer.ChooseMove(mass[0], mass[1],true, false) != null)// если у компьютера есть еще съедобный ход, то он будет ходить пока они не закочатся
                    //{
                    //    mass2 = computer.ChooseMove(mass[0], mass[1]); // куда ходим
                    //    computer.Replacement(masSquare[mass[0], mass[1]].Button, masSquare[mass2[0], mass2[1]].Button, cellSize); // функция для хода 
                    //    SwitchButtonToCheat(masSquare[mass2[0], mass2[1]].Button);
                    //}
                    currentPlayer = currentPlayer == 1 ? 2 : 1;

                    botStep = false;
                }
                else
                {
                    botStep = true;
                    //currentPlayer = currentPlayer == 1 ? 2 : 1; // меняем текущего игрока 
                }
                
            }
            ResetGame();
            record++;


        }

        public void OnFigurePress(object sender, EventArgs e)
        {
            if (!botStep || !botPlay)
            {
                if (prevButton != null)
                    prevButton.BackColor = GetPrevButtonColor(prevButton);

                pressedButton = sender as Button;

                if (board.map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] != 0 && board.map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] == currentPlayer)
                {
                    CloseSteps();
                    pressedButton.BackColor = Color.Red;
                    DeactivateAllButtons();
                    pressedButton.Enabled = true;
                    countEatSteps = 0;
                    if (pressedButton.Text == "D")
                        ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize, false);
                    else ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize);

                    if (isMoving)
                    {
                        CloseSteps();
                        pressedButton.BackColor = GetPrevButtonColor(pressedButton);
                        ShowPossibleSteps();
                        isMoving = false;
                    }
                    else
                        isMoving = true;
                }
                else
                {
                    if (isMoving)
                    {
                        isContinue = false;
                        if (Math.Abs(pressedButton.Location.X / cellSize - prevButton.Location.X / cellSize) > 1)
                        {
                            isContinue = true;
                            DeleteEaten(pressedButton, prevButton);
                        }
                        int temp = board.map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
                        board.map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = board.map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize];
                        board.map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize] = temp;
                        pressedButton.Image = prevButton.Image;
                        prevButton.Image = null;
                        pressedButton.Text = prevButton.Text;
                        prevButton.Text = "";
                        SwitchButtonToCheat(pressedButton);
                        countEatSteps = 0;
                        isMoving = false;
                        CloseSteps();
                        DeactivateAllButtons();
                        if (pressedButton.Text == "D")
                            ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize, false);
                        else ShowSteps(pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize);
                        if (countEatSteps == 0 || !isContinue)
                        {
                            CloseSteps();
                            SwitchPlayer();
                            ShowPossibleSteps();
                            isContinue = false;
                        }
                        else if (isContinue)
                        {
                            pressedButton.BackColor = Color.Red;
                            pressedButton.Enabled = true;
                            isMoving = true;
                        }
                    }
                }

                prevButton = pressedButton;
            }
        } // нажатие игроком на кнопку

        public void ShowPossibleSteps()
        {
            bool isOneStep = true;
            bool isEatStep = false;
            DeactivateAllButtons();
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (board.map[i, j] == currentPlayer)
                    {
                        if (masSquare[i, j].Text == "D")
                            isOneStep = false;
                        else isOneStep = true;
                        if (IsButtonHasEatStep(i, j, isOneStep, new int[2] { 0, 0 }))
                        {
                            isEatStep = true;
                            masSquare[i,j].Enabled = true;
                        }
                    }
                }
            }
            if (!isEatStep)
                ActivateAllButtons();
        }

        public void DeleteEaten(Button endButton, Button startButton)
        {
            int count = Math.Abs(endButton.Location.Y / cellSize - startButton.Location.Y / cellSize);
            int startIndexX = endButton.Location.Y / cellSize - startButton.Location.Y / cellSize;
            int startIndexY = endButton.Location.X / cellSize - startButton.Location.X / cellSize;
            startIndexX = startIndexX < 0 ? -1 : 1;
            startIndexY = startIndexY < 0 ? -1 : 1;
            int currCount = 0;
            int i = startButton.Location.Y / cellSize + startIndexX;
            int j = startButton.Location.X / cellSize + startIndexY;
            while (currCount < count - 1)
            {
                board.map[i, j] = 0;
                masSquare[i, j].Image = null;
                masSquare[i, j].Text = "";
                i += startIndexX;
                j += startIndexY;
                currCount++;
            }

        }


        public void ShowSteps(int iCurrFigure, int jCurrFigure, bool isOnestep = true)
        {
            simpleSteps.Clear();
            ShowDiagonal(iCurrFigure, jCurrFigure, isOnestep);
            if (countEatSteps > 0)
                CloseSimpleSteps(simpleSteps);
        }

        public void ShowDiagonal(int IcurrFigure, int JcurrFigure, bool isOneStep = false)// проверка на следующий ход
        {
            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;
                if (board.IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;
                if (board.IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (board.IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure + 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (board.IsInsideBorders(i, j))
                {
                    if (!DeterminePath(i, j))
                        break;
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }
        }

        public void SwitchButtonToCheat(Button button)// проверка на дамку
        {
            if (board.map[button.Location.Y / cellSize, button.Location.X / cellSize] == 1 && button.Location.Y / cellSize == mapSize - 1)
            {
                button.Text = "D";

            }
            if (board.map[button.Location.Y / cellSize, button.Location.X / cellSize] == 2 && button.Location.Y / cellSize == 0)
            {
                button.Text = "D";
            }
        }


        public bool DeterminePath(int ti, int tj)// Определить путь 
        {

            if (board.map[ti, tj] == 0 && !isContinue)
            {
                masSquare[ti, tj].BackColor = Color.Yellow;
                masSquare[ti, tj].Enabled = true;
                simpleSteps.Add(masSquare[ti, tj].Button);
            }
            else
            {

                if (board.map[ti, tj] != currentPlayer)
                {
                    if (pressedButton.Text == "D")
                        ShowProceduralEat(ti, tj, false);
                    else ShowProceduralEat(ti, tj);
                }

                return false;
            }
            return true;
        }

        public void CloseSimpleSteps(List<Button> simpleSteps)// закрываем кнопки для хода которые передали 
        {
            if (simpleSteps.Count > 0)
            {
                for (int i = 0; i < simpleSteps.Count; i++)
                {
                    simpleSteps[i].BackColor = GetPrevButtonColor(simpleSteps[i]);
                    simpleSteps[i].Enabled = false;
                }
            }
        }

        public Color GetPrevButtonColor(Button prevButton)
        {
            if ((prevButton.Location.Y / cellSize % 2) != 0)
            {
                if ((prevButton.Location.X / cellSize % 2) == 0)
                {
                    return Color.FromArgb(128, 79, 45);
                }
            }
            if ((prevButton.Location.Y / cellSize) % 2 == 0)
            {
                if ((prevButton.Location.X / cellSize) % 2 != 0)
                {
                    return Color.FromArgb(128, 79, 45);
                }
            }
            return Color.PeachPuff;
        }

        public void ShowProceduralEat(int i, int j, bool isOneStep = true)// строит съедобные ходы
        {
            int dirX = i - pressedButton.Location.Y / cellSize;
            int dirY = j - pressedButton.Location.X / cellSize;
            dirX = dirX < 0 ? -1 : 1;
            dirY = dirY < 0 ? -1 : 1;
            int il = i;
            int jl = j;
            bool isEmpty = true;
            while (board.IsInsideBorders(il, jl))
            {
                if (board.map[il, jl] != 0 && board.map[il, jl] != currentPlayer)
                {
                    isEmpty = false;
                    break;
                }
                il += dirX;
                jl += dirY;

                if (isOneStep)
                    break;
            }
            if (isEmpty)
                return;
            List<Button> toClose = new List<Button>();
            bool closeSimple = false;
            int ik = il + dirX;
            int jk = jl + dirY;
            while (board.IsInsideBorders(ik, jk))
            {
                if (board.map[ik, jk] == 0)
                {
                    if (IsButtonHasEatStep(ik, jk, isOneStep, new int[2] { dirX, dirY }))
                    {
                        closeSimple = true;
                    }
                    else
                    {
                        toClose.Add(masSquare[ik, jk].Button);
                    }
                    masSquare[ik, jk].BackColor = Color.Yellow;
                    masSquare[ik, jk].Enabled = true;
                    countEatSteps++;
                }
                else break;
                if (isOneStep)
                    break;
                jk += dirY;
                ik += dirX;
            }
            if (closeSimple && toClose.Count > 0)
            {
                CloseSimpleSteps(toClose);
            }

        }
        public bool IsButtonHasEatStep(int IcurrFigure, int JcurrFigure, bool isOneStep, int[] dir)// есть ли у шашки съедобный ход
        {
            bool eatStep = false;
            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;// ограничение от хода вверх
                if (dir[0] == 1 && dir[1] == -1 && !isOneStep) break; // проверка клетки откуда шашка сходила не требуется 
                if (board.IsInsideBorders(i, j))
                {
                    if (board.map[i, j] != 0 && board.map[i, j] != currentPlayer) // если впереди не 0 и не шашка союзника, а шашка противника 
                    {
                        eatStep = true;
                        if (!board.IsInsideBorders(i - 1, j + 1))
                            eatStep = false;
                        else if (board.map[i - 1, j + 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }  
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;
                if (dir[0] == 1 && dir[1] == 1 && !isOneStep) break;
                if (board.IsInsideBorders(i, j))
                {
                    if (board.map[i, j] != 0 && board.map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!board.IsInsideBorders(i - 1, j - 1))
                            eatStep = false;
                        else if (board.map[i - 1, j - 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure - 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (dir[0] == -1 && dir[1] == 1 && !isOneStep) break;
                if (board.IsInsideBorders(i, j))
                {
                    if (board.map[i, j] != 0 && board.map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!board.IsInsideBorders(i + 1, j - 1))
                            eatStep = false;
                        else if (board.map[i + 1, j - 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j > 0)
                    j--;
                else break;

                if (isOneStep)
                    break;
            }

            j = JcurrFigure + 1;
            for (int i = IcurrFigure + 1; i < 8; i++)
            {
                if (currentPlayer == 2 && isOneStep && !isContinue) break;
                if (dir[0] == -1 && dir[1] == -1 && !isOneStep) break;
                if (board.IsInsideBorders(i, j))
                {
                    if (board.map[i, j] != 0 && board.map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!board.IsInsideBorders(i + 1, j + 1))
                            eatStep = false;
                        else if (board.map[i + 1, j + 1] != 0)
                            eatStep = false;
                        else return eatStep;
                    }
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }
            return eatStep;
        }

        public void CloseSteps()// закрываем шаги, которые были открыты 
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    masSquare[i, j].BackColor = GetPrevButtonColor(masSquare[i, j].Button);
                }
            }
        }

       

        public void ActivateAllButtons()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    masSquare[i, j].Enabled = true;
                }
            }
        }

        public void DeactivateAllButtons()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    masSquare[i, j].Enabled = false;
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }


        public void PlayButtonClic(object sender, EventArgs e)
        {
            this.Controls.Clear();
            option = new Option(this);
            
            this.Controls.Add(StartMenu.home);
            Init();
        }

        private void OptionButtonClick(object sender, EventArgs e)
        {
            //this.Controls.Clear();
           
            option.Init(this);
            option.BlacOrWhiteInit();
        }

        public void ApplyButtonClick(object sender, EventArgs e)// кнопка для применения настроек
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    this.Controls.Remove(masSquare[i, j].Button);

                }
            }

            board = new Board(this, cellSize);

            botStep = !option.BlacOrWhite();
            Init();
           
        }


        private void Authorization_Click(object sender, EventArgs e)// авторизация - инициализируется меню с регистраци
        {
            this.Controls.Clear();
            StartMenu.autorization();
        }


        public void Entry_Click(object sender, EventArgs e)
        {
            if (!StartMenu.User_Veriication())
            {
                MessageBox.Show("Пользователь не найден.\nПопробуйте еще раз или войдите как гость.");
            }
            else
            {
                this.Controls.Clear();
                option = new Option(this);
                this.Controls.Add(StartMenu.home);
                Init();

            }

        }

        public void Home_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            Start();
        }

        private void TabelRecords_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.Controls.Add(StartMenu.home);
            StartMenu.TabelRecords_Click();

        }


    }
}
