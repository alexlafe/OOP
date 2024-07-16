using System;
using System.Collections.Generic;
using System.Text;

namespace _25._11
{
    class Game
    {
        private Random rand = new Random();
        private int col_cell;

        private int rows;
        private int cols;

        private Cell[,] pole2;

        public int year { get; private set; }
        private int max_age;
        public Game(int cols, int rows, int col_cell, int max_numeric)
        {
            this.cols = cols;
            this.rows = rows;
            this.col_cell = col_cell;
            this.max_age = max_numeric;
            year = 0;
            Generation2();
        }

        private void Generation2()
        {
            pole2 = new Cell[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    pole2[x, y] = new Cells();
                    pole2[x, y].Life = false;
                    pole2[x, y].Age = rand.Next(max_age); //Рандомный стартовый возраст клетки с учетом максимального возраста
                }
            }
        }

        
        public bool IsEndGame(out int nPlayer1, out int nPlayer2)
        {
            nPlayer1 = nPlayer2 = 0;
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (pole2[x, y].Life)
                    {
                        if (pole2[x, y] is Player_1)
                            nPlayer1++;
                        if (pole2[x, y] is Player_2)
                            nPlayer2++;
                    }
                }
            }

            // Нет клеток у одного из игроков или нет совободных клеток
            if (nPlayer1 == 0 || nPlayer2 == 0 || nPlayer1 + nPlayer2 == cols * rows)
                return true;
            else
                return false;
        }

        public void NextPole2()
        {
            Cell[,] newpole = new Cell[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (pole2[x, y] is Virus)
                    {
                        newpole[x, y] = new Virus();
                    }
                    else if (pole2[x, y] is Medicine)
                    {
                        newpole[x, y] = new Medicine();
                    }
                    else if (pole2[x, y] is Player_1)
                    {
                        newpole[x, y] = new Player_1();
                    }
                    else if (pole2[x, y] is Player_2)
                    {
                        newpole[x, y] = new Player_2();
                    }
                    else
                    {
                        newpole[x, y] = new Cells();
                    }

                    int[] hh1 = CountSos2<Player_1>(x, y); // соседние клетки игрока 1
                    int[] hh2 = CountSos2<Player_2>(x, y); // соседние клетки игрока 2
                    newpole[x, y].Age = pole2[x, y].Age + 1;

                    if (pole2[x, y].Life && hh1[0] >= 5 && pole2[x, y].Age > 18 && pole2[x, y].Age < 65)
                    {
                        newpole[x, y] = new Player_1();
                        newpole[x, y].Life = false;
                    }
                    else if (!pole2[x, y].Life && hh1[1] > 3 && hh1[1] > 2)
                    {
                        newpole[x, y] = new Player_1();
                        newpole[x, y].Life = true;
                        newpole[x, y].Age = 0;
                    }
                    else if (pole2[x, y].Life && pole2[x, y].Age > 55 &&
                        pole2[x, y].Age < max_age && pole2[x, y].Age == rand.Next(56, max_age))
                    {
                        newpole[x, y] = new Player_1();
                        newpole[x, y].Life = false;
                    }
                    else if (pole2[x, y].Life && hh2[0] >= 5 && pole2[x, y].Age > 18 && pole2[x, y].Age < 65)
                    {
                        newpole[x, y] = new Player_2();
                        newpole[x, y].Life = false;
                    }
                    else if (!pole2[x, y].Life && hh2[1] > 3 && hh2[1] > 2)
                    {
                        newpole[x, y] = new Player_2();
                        newpole[x, y].Life = true;
                        newpole[x, y].Age = 0;
                    }
                    else if (pole2[x, y].Life && pole2[x, y].Age > 55 &&
                        pole2[x, y].Age < max_age && pole2[x, y].Age == rand.Next(56, max_age))
                    {
                        newpole[x, y] = new Player_2();
                        newpole[x, y].Life = false;
                    }
                    else if (pole2[x, y].Life && pole2[x, y].Age > max_age) // Если клетка живет больше чем положено, то убиваем ее
                    {
                        newpole[x, y] = new Cells();
                        newpole[x, y].Life = false;
                    }
                    else
                    {
                        newpole[x, y].Life = pole2[x, y].Life;
                    }

                    // Если клетка игрока - выполняем проверку количества соседей
                    if (pole2[x, y] is Player_1 || pole2[x, y] is Player_2)
                    {
                        int p1 = GetNumberIdenticalCellsNearby<Player_1>(x, y, pole2);
                        int p2 = GetNumberIdenticalCellsNearby<Player_2>(x, y, pole2);
                        if (p1 > p2 && pole2[x, y] is Player_2)
                        {
                            newpole[x, y] = new Player_1();
                            newpole[x, y].Life = true;
                            newpole[x, y].Age = 0;
                        }
                        else if (p1 < p2 && pole2[x, y] is Player_1)
                        {
                            newpole[x, y] = new Player_2();
                            newpole[x, y].Life = true;
                            newpole[x, y].Age = 0;
                        }
                    }
                }
            }

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (pole2[x, y] is Virus && x > 0 && x < cols - 1 && y > 0 && y < rows - 1)
                    {
                        newpole[x - 1, y].Life = false;
                        newpole[x + 1, y].Life = false;
                        newpole[x + 1, y + 1].Life = false;
                        newpole[x, y + 1].Life = false;
                        newpole[x, y - 1].Life = false;
                        newpole[x + 1, y - 1].Life = false;
                        newpole[x - 1, y + 1].Life = false;
                        newpole[x - 1, y - 1].Life = false;
                    }

                    if (pole2[x, y] is Virus && x == 0 && y > 0 && x < cols - 1 && y < rows - 1)
                    {
                        newpole[x + 1, y].Life = false;
                        newpole[x + 1, y + 1].Life = false;
                        newpole[x, y + 1].Life = false;
                        newpole[x, y - 1].Life = false;
                        newpole[x + 1, y - 1].Life = false;
                    }

                    if (pole2[x, y] is Virus && x > 0 && y == 0 && x < cols - 1 && y < rows - 1)
                    {
                        newpole[x - 1, y].Life = false;
                        newpole[x + 1, y].Life = false;
                        newpole[x + 1, y + 1].Life = false;
                        newpole[x, y + 1].Life = false;
                        newpole[x - 1, y + 1].Life = false;
                    }

                    if (pole2[x, y] is Virus && x == 0 && y == 0 && x < cols - 1 && y < rows - 1)
                    {
                        newpole[x + 1, y].Life = false;
                        newpole[x + 1, y + 1].Life = false;
                        newpole[x, y + 1].Life = false;
                    }

                    if (pole2[x, y] is Virus && x > 0 && y > 0 && x == cols - 1 && y < rows - 1)
                    {
                        newpole[x - 1, y].Life = false;
                        newpole[x, y + 1].Life = false;
                        newpole[x, y - 1].Life = false;
                        newpole[x - 1, y - 1].Life = false;
                        newpole[x - 1, y + 1].Life = false;
                    }
                    if (pole2[x, y] is Virus && x > 0 && y > 0 && x < cols - 1 && y == rows - 1)
                    {
                        newpole[x - 1, y].Life = false;
                        newpole[x + 1, y].Life = false;
                        newpole[x, y - 1].Life = false;
                        newpole[x - 1, y - 1].Life = false;
                        newpole[x + 1, y - 1].Life = false;
                    }
                    if (pole2[x, y] is Virus && x > 0 && y > 0 && x == cols - 1 && y == rows - 1)
                    {
                        newpole[x - 1, y].Life = false;
                        newpole[x, y - 1].Life = false;
                        newpole[x - 1, y - 1].Life = false;
                    }

                    if (pole2[x, y] is Virus && pole2[x, y].Age > 20 && x > 0 && y > 0 && x < cols - 1 && y < rows - 1)
                    {
                        newpole[x, y].Life = false;
                        newpole[x, y] = new Cells();

                        newpole[x - 1, y] = new Cells();
                        newpole[x + 1, y] = new Cells();
                        newpole[x + 1, y + 1] = new Cells();
                        newpole[x, y + 1] = new Cells();
                        newpole[x, y - 1] = new Cells();
                        newpole[x - 1, y - 1] = new Cells();
                        newpole[x + 1, y - 1] = new Cells();
                        newpole[x - 1, y + 1] = new Cells();
                    }
                    if (pole2[x, y] is Medicine && x > 0 && y > 0 && x > 0 && y > 0 && x < cols - 1 && y < rows - 1)
                    {
                        newpole[x - 1, y].Life = true;
                        newpole[x + 1, y].Life = true;
                        newpole[x + 1, y + 1].Life = true;
                        newpole[x, y + 1].Life = true;
                        newpole[x, y - 1].Life = true;
                        newpole[x - 1, y - 1].Life = true;
                        newpole[x + 1, y - 1].Life = true;
                        newpole[x - 1, y + 1].Life = true;
                    }

                    if (pole2[x, y] is Medicine && x == 0 && y > 0 && x > 0 && y > 0 && x < cols - 1 && y < rows - 1)
                    {
                        newpole[x + 1, y].Life = true;
                        newpole[x + 1, y + 1].Life = true;
                        newpole[x, y + 1].Life = true;
                        newpole[x, y - 1].Life = true;
                        newpole[x + 1, y - 1].Life = true;
                    }
                    if (pole2[x, y] is Medicine && x > 0 && y == 0 && x > 0 && y > 0 && x < cols - 1 && y < rows - 1)
                    {
                        newpole[x - 1, y].Life = true;
                        newpole[x + 1, y].Life = true;
                        newpole[x + 1, y + 1].Life = true;
                        newpole[x, y + 1].Life = true;
                        newpole[x - 1, y + 1].Life = true;
                    }
                    if (pole2[x, y] is Medicine && x == 0 && y == 0 && x > 0 && y > 0 && x < cols - 1 && y < rows - 1)
                    {
                        newpole[x + 1, y].Life = true;
                        newpole[x + 1, y + 1].Life = true;
                        newpole[x, y + 1].Life = true;
                    }
                    if (pole2[x, y] is Medicine && x > 0 && y > 0 && x > 0 && y > 0 && x == cols - 1 && y < rows - 1)
                    {
                        newpole[x - 1, y].Life = true;
                        newpole[x, y + 1].Life = true;
                        newpole[x, y - 1].Life = true;
                        newpole[x - 1, y - 1].Life = true;
                        newpole[x - 1, y + 1].Life = true;
                    }
                    if (pole2[x, y] is Medicine && x > 0 && y > 0 && x > 0 && y > 0 && x < cols - 1 && y == rows - 1)
                    {
                        newpole[x - 1, y].Life = true;
                        newpole[x + 1, y].Life = true;
                        newpole[x, y - 1].Life = true;
                        newpole[x - 1, y - 1].Life = true;
                        newpole[x + 1, y - 1].Life = true;
                    }
                    if (pole2[x, y] is Medicine && x > 0 && y > 0 && x > 0 && y > 0 && x == cols - 1 && y == rows - 1)
                    {
                        newpole[x - 1, y].Life = true;
                        newpole[x, y - 1].Life = true;
                        newpole[x - 1, y - 1].Life = true;
                    }

                    if (pole2[x, y] is Medicine && pole2[x, y].Age > 15 && x > 0 && y > 0 && x < cols - 1 && y < rows - 1)
                    {
                        newpole[x, y].Life = false;
                        newpole[x, y] = new Cells();
                    }
                }
            }

            pole2 = newpole;
            year++;
        }

        

        public Cell[,] GetPole2()
        {
            return pole2;
        }

        private int[] CountSos2<T>(int x, int y) 
        {
            int count = 0;
            int count18 = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i != x || j != y)
                    {
                        var x1 = Math.Abs(i % cols);
                        var y1 = Math.Abs(j % rows);

                        if (pole2[x1, y1].Life && pole2[x1, y1] is T && pole2[x, y] is Cells)
                        {
                            if (pole2[x1, y1].Age >= 18)
                            {
                                count18++;
                            }
                            count++;
                        }
                    }
                }
            }
            int[] arr = { count, count18 };
            return arr;
        }

        public void UpdateCell(int x, int y, bool status, int count)
        {
            if (x < cols && y < rows && x >= 0 && y >= 0)
            {
                if (status == true)
                {
                    pole2[x, y] = new Virus();
                    pole2[x, y].Life = true;

                    // сбрасываем клетки вокруг
                    pole2[x - 1, y] = new Cells();
                    if (x > 0) // left
                        pole2[x - 1, y] = new Cells();
                    if (y > 0) // top
                        pole2[x, y - 1] = new Cells();
                    if (x > 0 && y > 0) // top left
                        pole2[x - 1, y - 1] = new Cells();

                    if (x < cols - 1) // right
                        pole2[x + 1, y] = new Cells();
                    if (y < rows - 1) // bottom
                        pole2[x, y + 1] = new Cells();
                    if (x < cols - 1 && y < rows - 1) // bottom right
                        pole2[x + 1, y + 1] = new Cells();

                    if (x < cols - 1 && y > 0) // top right
                        pole2[x + 1, y - 1] = new Cells();
                    if (y < rows - 1 && x > 0) // bottom left
                        pole2[x - 1, y + 1] = new Cells();
                }
                if (status == false)
                {
                    pole2[x, y] = new Medicine();
                    pole2[x, y].Life = true;

                    if (count == 0)
                    {
                        // присваиваем клетки вокруг игроку
                        if (x > 0) // left
                            pole2[x - 1, y] = new Player_1();
                        if (y > 0) // top
                            pole2[x, y - 1] = new Player_1();
                        if (x > 0 && y > 0) // top left
                            pole2[x - 1, y - 1] = new Player_1();

                        if (x < cols - 1) // right
                            pole2[x + 1, y] = new Player_1();
                        if (y < rows - 1) // bottom
                            pole2[x, y + 1] = new Player_1();
                        if (x < cols - 1 && y < rows - 1) // bottom right
                            pole2[x + 1, y + 1] = new Player_1();

                        if (x < cols - 1 && y > 0) // top right
                            pole2[x + 1, y - 1] = new Player_1();
                        if (y < rows - 1 && x > 0) // bottom left
                            pole2[x - 1, y + 1] = new Player_1();
                    }
                    else if (count == 1)
                    {
                        // присваиваем клетки вокруг игроку
                        if (x > 0) // left
                            pole2[x - 1, y] = new Player_2();
                        if (y > 0) // top
                            pole2[x, y - 1] = new Player_2();
                        if (x > 0 && y > 0) // top left
                            pole2[x - 1, y - 1] = new Player_2();

                        if (x < cols - 1) // right
                            pole2[x + 1, y] = new Player_2();
                        if (y < rows - 1) // bottom
                            pole2[x, y + 1] = new Player_2();
                        if (x < cols - 1 && y < rows - 1) // bottom right
                            pole2[x + 1, y + 1] = new Player_2();

                        if (x < cols - 1 && y > 0) // top right
                            pole2[x + 1, y - 1] = new Player_2();
                        if (y < rows - 1 && x > 0) // bottom left
                            pole2[x - 1, y + 1] = new Player_2();
                    }

                    // оживляем их
                    if (x > 0) // left
                        pole2[x - 1, y].Life = true;
                    if (y > 0) // top
                        pole2[x, y - 1].Life = true;
                    if (x > 0 && y > 0) // top left
                        pole2[x - 1, y - 1].Life = true;

                    if (x < cols - 1) // right
                        pole2[x + 1, y].Life = true;
                    if (y < rows - 1) // bottom
                        pole2[x, y + 1].Life = true;
                    if (x < cols - 1 && y < rows - 1) // bottom right
                        pole2[x + 1, y + 1].Life = true;

                    if (x < cols - 1 && y > 0) // top right
                        pole2[x + 1, y - 1].Life = true;
                    if (y < rows - 1 && x > 0) // bottom left
                        pole2[x - 1, y + 1].Life = true;
                }
            }
        }

        public bool Cell_Check(int x, int y, int count, int start_col_cell)
        {
            if (x < cols && y < rows && x >= 0 && y >= 0)
            {
                // Проверям что клетка не занята
                if (pole2[x, y] is Player_1 || pole2[x, y] is Player_2)
                    return false;

                if (count >= 0 && count < start_col_cell)
                {
                    pole2[x, y] = new Player_1();
                    pole2[x, y].Life = true;
                    return true;
                }
                else if (count >= start_col_cell && count < 2 * start_col_cell)
                {
                    pole2[x, y] = new Player_2();
                    pole2[x, y].Life = true;
                    return true;
                }
            }
            return false;
        }

        private int GetNumberIdenticalCellsNearby<T>(int x, int y, Cell[,] field)
        {
            int number = 0;

            if (x > 0) // left
                number += field[x - 1, y] is T && field[x - 1, y].Life ? 1 : 0;
            if (y > 0) // top
                number += field[x, y - 1] is T && field[x, y - 1].Life ? 1 : 0;
            if (x > 0 && y > 0) // top left
                number += field[x - 1, y - 1] is T && field[x - 1, y - 1].Life ? 1 : 0;

            if (x < cols - 1) // right
                number += field[x + 1, y] is T && field[x + 1, y].Life ? 1 : 0;
            if (y < rows - 1) // bottom
                number += field[x, y + 1] is T && field[x, y + 1].Life ? 1 : 0;
            if (x < cols - 1 && y < rows - 1) // bottom right
                number += field[x + 1, y + 1] is T && field[x + 1, y + 1].Life ? 1 : 0;

            if (x < cols - 1 && y > 0) // top right
                number += field[x + 1, y - 1] is T && field[x + 1, y - 1].Life ? 1 : 0;
            if (y < rows - 1 && x > 0) // bottom left
                number += field[x - 1, y + 1] is T && field[x - 1, y + 1].Life ? 1 : 0;

            return number;
        }
    }
}
