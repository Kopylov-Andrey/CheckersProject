using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;


namespace CheckersProject._2
{
    
    class ArtificialIntelligence
    {
        Square[,] masSquare;
        private int[,] map;
        List<int[]> checkersWithMove = new List<int[]>();//Список с координатами шашек которыми мы будем ходить
        List<int[]> possibleMoves = new List<int[]>();
        int countEatSteps = 0;
        int currentPlayer;
        bool isContinue = false;// есть ли еще сЪедобные ходы, кроме 1 

        public ArtificialIntelligence(int[,] map, Square[,] masSquar) 
        {
            this.map = map;
            masSquare = masSquar;
        }
        public void Replacement(Button prevButton, Button pressedButton, int cellSize)// перезапись шашки при ходе 
        {
            int temp = map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize];
            map[pressedButton.Location.Y / cellSize, pressedButton.Location.X / cellSize] = map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize];
            map[prevButton.Location.Y / cellSize, prevButton.Location.X / cellSize] = temp;
            pressedButton.Image = prevButton.Image;
            prevButton.Image = null;
            prevButton.BackColor = Color.Gray;
            pressedButton.Text = prevButton.Text;
            prevButton.Text = "";
        }

        public int[] ChooseChekers(int Player)// какой ходим
        {
            currentPlayer = Player;


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (currentPlayer == map[i, j])
                    {
                        if (EdibleMove(i, j))
                        {
                            return new  int[] { i,j };
                        }
                    }

                }

            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(currentPlayer == map[i,j])
                    {
                        if(masSquare[i,j].Text == "D")
                            PossibleCheckers(i, j, currentPlayer, false);
                        else PossibleCheckers(i, j, currentPlayer);
                    }

                }
            }
            int count = checkersWithMove.Count();
            Random rand = new Random();
            int[] endmass = checkersWithMove[rand.Next(0, count - 1)];



            return endmass;
        }
        public int[] ChooseMove(int IcurrFigure, int JcurrFigure, bool isOneStep = true, bool firstMove = true) // куда ходим
        {
            int[] mass = new int[2];

            bool eatStep = false;

            if (EdibleMove(IcurrFigure, JcurrFigure))
            {



                int j = JcurrFigure + 1;
                for (int i = IcurrFigure - 1; i >= 0; i--)
                {
                    if (currentPlayer == 1 && isOneStep && !isContinue) break;// ограничение от хода вверх
                    /* if (dir[0] == 1 && dir[1] == -1 && !isOneStep) break;*/ // проверка клетки откуда шашка сходила не требуется 
                    if (IsInsideBorders(i, j))
                    {
                        if (map[i, j] != 0 && map[i, j] != currentPlayer) // если впереди не 0 и не шашка союзника, а шашка противника 
                        {
                            eatStep = true;
                            if (!IsInsideBorders(i - 1, j + 1))
                                eatStep = false;
                            else if (map[i - 1, j + 1] != 0)
                                eatStep = false;
                            else
                            {
                                DeleteEaten(i - 1, j + 1, IcurrFigure, JcurrFigure);

                                return new int[] { i - 1, j + 1 };
                            }
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
                    //if (dir[0] == 1 && dir[1] == 1 && !isOneStep) break;
                    if (IsInsideBorders(i, j))
                    {
                        if (map[i, j] != 0 && map[i, j] != currentPlayer)
                        {
                            eatStep = true;
                            if (!IsInsideBorders(i - 1, j - 1))
                                eatStep = false;
                            else if (map[i - 1, j - 1] != 0)
                                eatStep = false;
                            else
                            {
                                DeleteEaten(i - 1, j - 1, IcurrFigure, JcurrFigure);
                                return new int[] { i - 1, j - 1 };
                            }

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
                    //if (dir[0] == -1 && dir[1] == 1 && !isOneStep) break;
                    if (IsInsideBorders(i, j))
                    {
                        if (map[i, j] != 0 && map[i, j] != currentPlayer)
                        {
                            eatStep = true;
                            if (!IsInsideBorders(i + 1, j - 1))
                                eatStep = false;
                            else if (map[i + 1, j - 1] != 0)
                                eatStep = false;
                            else
                            {
                                DeleteEaten(i + 1, j - 1, IcurrFigure, JcurrFigure);
                                return new int[] { i + 1, j - 1 };
                            }

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
                    //if (dir[0] == -1 && dir[1] == -1 && !isOneStep) break;
                    if (IsInsideBorders(i, j))
                    {
                        if (map[i, j] != 0 && map[i, j] != currentPlayer)
                        {
                            eatStep = true;
                            if (!IsInsideBorders(i + 1, j + 1))
                                eatStep = false;
                            else if (map[i + 1, j + 1] != 0)
                                eatStep = false;
                            else
                            {
                                DeleteEaten(i + 1, j + 1, IcurrFigure, JcurrFigure);
                                return new int[] { i + 1, j + 1 };
                            }
                        }
                    }
                    if (j < 7)
                        j++;
                    else break;

                    if (isOneStep)
                        break;
                }
                return new int[] { 0, 0 };
            }

            else
            {

                if (IsInsideBorders(IcurrFigure + 1, JcurrFigure - 1))
                {
                    if ((currentPlayer == 1 && map[IcurrFigure + 1, JcurrFigure - 1] == 0))
                    {
                        mass[0] = IcurrFigure + 1;
                        mass[1] = JcurrFigure - 1;

                        return mass;
                    }
                }
                if (IsInsideBorders(IcurrFigure + 1, JcurrFigure + 1))
                {
                    if ((currentPlayer == 1 && map[IcurrFigure + 1, JcurrFigure + 1] == 0))
                    {
                        mass[0] = IcurrFigure + 1;
                        mass[1] = JcurrFigure + 1;

                        return mass;
                    }
                }
                if (IsInsideBorders(IcurrFigure - 1, JcurrFigure - 1))
                {
                    if ((currentPlayer == 2 && map[IcurrFigure - 1, JcurrFigure - 1] == 0))
                    {
                        mass[0] = IcurrFigure - 1;
                        mass[1] = JcurrFigure - 1;

                        return mass;
                    }
                }
                if (IsInsideBorders(IcurrFigure - 1, JcurrFigure + 1))
                {
                    if ((currentPlayer == 2 && map[IcurrFigure - 1, JcurrFigure + 1] == 0))
                    {
                        mass[0] = IcurrFigure - 1;
                        mass[1] = JcurrFigure + 1;

                        return mass;
                    }
                }
                return mass;
            }



            //if (!isOneStep)// если дамка 
            //{
            //    if (EdibleMove(i, j))// если есть съедобный ход
            //    {
            //        AddMovesToList(i, j, isOneStep, true);
            //    }
            //    else
            //    {

            //    }
            //}
            //else
            //{
            //    if (EdibleMove(i, j))
            //    {

            //    }
            //    else
            //    {

            //    }
            //}


            //return null; 
        }


        private void  PossibleCheckers(int i, int j, int currentPlayer, bool isOnestep = true)// определяем шашку которой будем ходить
        {
            
            //if(masSquare[i,j].Text == "D")
            //{
            //    int jm = j + 1;
            //    for(int im = i - 1; i >= 0; i--)
            //    {

            //    }
            //}


            if(IsInsideBorders(i+1, j - 1))
            {

                if ((currentPlayer == 1 && map[i + 1, j - 1] == 0)|| masSquare[i, j].Text == "D")
                {
                    int[] mass = new int[2];
                    mass[0] = i;
                    mass[1] = j;

                    checkersWithMove.Add(mass);
                }

            }
            if (IsInsideBorders(i + 1, j + 1))
            {
                if ((currentPlayer == 1 && map[i + 1, j + 1] == 0)|| masSquare[i, j].Text == "D")
                {
                    int[] mass = new int[2];
                    mass[0] = i;
                    mass[1] = j;

                    checkersWithMove.Add(mass);
                }
            }
            if (IsInsideBorders(i - 1, j - 1))
            {
                if ((currentPlayer == 2 && map[i - 1, j - 1] == 0)|| masSquare[i, j].Text == "D")
                {
                    int[] mass = new int[2];
                    mass[0] = i;
                    mass[1] = j;

                    checkersWithMove.Add(mass);
                }
            }
            if (IsInsideBorders(i - 1, j + 1))
            {
                if ((currentPlayer == 2 && map[i - 1, j + 1] == 0)|| masSquare[i, j].Text == "D")
                {
                    int[] mass = new int[2];
                    mass[0] = i;
                    mass[1] = j;

                    checkersWithMove.Add(mass);
                }

            }

           
            
        }


        public bool IsButtonHasEatStep(int IcurrFigure, int JcurrFigure, bool isOneStep = true)// есть ли у шашки съедобный ход
        {
            bool eatStep = false;


            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;// ограничение от хода вверх
               
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer) // если впереди не 0 и не шашка союзника, а шашка противника 
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i - 1, j + 1))
                            eatStep = false;
                        else if (map[i - 1, j + 1] != 0)
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
               
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i - 1, j - 1))
                            eatStep = false;
                        else if (map[i - 1, j - 1] != 0)
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
                
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i + 1, j - 1))
                            eatStep = false;
                        else if (map[i + 1, j - 1] != 0)
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
               
                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer)
                    {
                        eatStep = true;
                        if (!IsInsideBorders(i + 1, j + 1))
                            eatStep = false;
                        else if (map[i + 1, j + 1] != 0)
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

        public void DeleteEaten(int ie,int je,int ist, int jst )// Удаление элемента
        {
            int count = Math.Abs(ie - ist);
            int startIndexX = ie - ist;
            int startIndexY = je - jst;
            startIndexX = startIndexX < 0 ? -1 : 1;
            startIndexY = startIndexY < 0 ? -1 : 1;
            int currCount = 0;
            int i = ist + startIndexX;
            int j = jst + startIndexY;
            while (currCount < count - 1)
            {
                map[i, j] = 0;
                masSquare[i, j].Image = null;
                masSquare[i, j].Text = "";
                i += startIndexX;
                j += startIndexY;
                currCount++;
            }

        }
       
        public bool IsInsideBorders(int ti, int tj)// Находится ли внутри поля
        {
            if (ti >= 8 || tj >= 8 || ti < 0 || tj < 0)
            {
                return false;
            }
            return true;
        }

        public bool EdibleMove(int ti, int tj)// есть ли съедобный ход 
        {

            //if (board.map[ti, tj] == 0 && !isContinue)
            //{
            //    masSquare[ti, tj].BackColor = Color.Yellow;
            //    masSquare[ti, tj].Enabled = true;
            //    simpleSteps.Add(masSquare[ti, tj].Button);
            //}
            //else
            

                if (map[ti, tj] == currentPlayer)
                {
                    if (masSquare[ti,tj].Text == "D") 
                    {
                        if (IsButtonHasEatStep(ti, tj, false))
                        {
                        return true;
                         }
                    }

                    else
                    {
                    if (IsButtonHasEatStep(ti, tj))
                        return true;
                    }
                   


                }

                return false;
            
        }

        //public void SwitchButtonToCheat(Button button)// проверка на дамку
        //{
        //    if (map[button.Location.Y / 60, button.Location.X / 60] == 1 && button.Location.Y / 60 == 8 - 1)
        //    {
        //        button.Text = "D";

        //    }
        //    if (map[button.Location.Y / 60, button.Location.X / 60] == 2 && button.Location.Y / 60 == 0)
        //    {
        //        button.Text = "D";
        //    }
        //}


        private void AddMovesToList(int IcurrFigure, int JcurrFigure , bool isOneStep, bool needEat)
        {

            int j = JcurrFigure + 1;
            for (int i = IcurrFigure - 1; i >= 0; i--)
            {
                if (currentPlayer == 1 && isOneStep && !isContinue) break;// ограничение от хода вверх

                if (IsInsideBorders(i, j))
                {
                    if (map[i, j] != 0 && map[i, j] != currentPlayer) // если впереди не 0 и не шашка союзника, а шашка противника 
                    {

                        if (!IsInsideBorders(i - 1, j + 1))
                            break;
                        else if (map[i - 1, j + 1] != 0)
                            break;
                        else;
                    }
                }
                if (j < 7)
                    j++;
                else break;

                if (isOneStep)
                    break;
            }


        }
    }
}
