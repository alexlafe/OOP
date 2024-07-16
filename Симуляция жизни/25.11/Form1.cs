using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _25._11
{
    public partial class Form1 : Form
    {
        Graphics abc;
        int size;
        Game game;
        private Bitmap bitmap;
        int count;
        int pause, numberPause;
        int start_col_cell;
        int interval;

        private bool stageBeforeStart;
        private bool stagePause;
        private int countActionInPause;

        public Form1()
        {
            InitializeComponent();
        }

        public void init()
        {
            count = 0;
            size = (int)numeric.Value;
            start_col_cell = (int)numericUpDown1.Value;
            interval = (int)numericUpDown2.Value;
            pause = interval;
            numberPause = 1;

            game = new Game(pictureBox1.Width / size, pictureBox1.Height / size,
                (int)col_clet.Value, (int)max_numeric.Value);

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            abc = Graphics.FromImage(bitmap);
            abc.Clear(Color.White);

            stageBeforeStart = true;
            stagePause = false;
            countActionInPause = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrowPole();
            pictureBox1.Image = bitmap;
            label7.Text = $"Год: {game.year}-й.";

            if (game.year == pause)
            {
                timer1.Enabled = false;
                stagePause = true;
                pause += interval;
                numberPause++;
                countActionInPause = 0;
            }

            // Проверка на конец игры
            bool result = game.IsEndGame(out int nPlayer1, out int nPlayer2);
            if (nPlayer1 == 0 || nPlayer2 == 0 || result)
            {
                label3.Text = $"Счет: И1 {nPlayer1} : И2 {nPlayer2}";
                timer1.Enabled = false;
                init();
            }
        }

        public void DrowPole()
        {
            game.NextPole2();
            Cell[,] pole = game.GetPole2();
            abc.Clear(Color.White);

            for (int x = 0; x < pole.GetLength(0); x++)
            {
                for (int y = 0; y < pole.GetLength(1); y++)
                {
                    if (pole[x, y].Life)
                    {
                        SolidBrush solidBrush = new SolidBrush(pole[x, y].Color);
                        abc.FillRectangle(solidBrush, x * size, y * size, size, size);
                    }
                }
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            stageBeforeStart = stagePause = false;
            timer1.Start();
        }

        private void stop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Инициализация игры после первого клика игрока по полю
            if (numeric.Enabled)
            {
                init();
                numeric.Enabled = false; // Отключение возможности изменить размер клетки
            }

            if (!timer1.Enabled)
            {
                size = (int)numeric.Value;
                start_col_cell = (int)numericUpDown1.Value;
                interval = (int)numericUpDown2.Value;
                pause = interval * numberPause;
            }

            var x = e.Location.X / size;
            var y = e.Location.Y / size;

            if (!timer1.Enabled)
            {
                Color color_cell = Color.White;
                Player_1 player_1 = new Player_1();
                Player_2 player_2 = new Player_2();
                Medicine med = new Medicine();
                Virus virus = new Virus();

                // Перед началом игры
                if (stageBeforeStart)
                {
                    bool result = game.Cell_Check(x, y, count, start_col_cell);
                    if (result)
                    {
                        if (count >= 0 && count < start_col_cell)
                        {
                            color_cell = player_1.GetColor();
                            label3.Text = "Расставляет 1-й игрок";
                        }
                        else if (count >= start_col_cell && count < 2 * start_col_cell)
                        {
                            color_cell = player_2.GetColor();
                            label3.Text = "Расставляет 2-й игрок";
                        }

                        SolidBrush solidBrush = new SolidBrush(color_cell);
                        abc.FillRectangle(solidBrush, x * size, y * size, size, size);

                        count++;
                    }

                    if (start_col_cell * 2 == count)
                    {
                        start.Enabled = true;
                        label3.Text = "Нажмите Начать";
                    }
                }

                // Во время паузы
                if (stagePause && countActionInPause < 2)
                {
                    if (countActionInPause == 0)
                    {
                        color_cell = player_1.GetColor();
                    }
                    else if (countActionInPause == 1)
                    {
                        color_cell = player_2.GetColor();
                    }

                    if (e.Button == MouseButtons.Left) // Если вирус
                    {
                        game.UpdateCell(x, y, true, countActionInPause);

                        SolidBrush solidBrush = new SolidBrush(Color.White);
                        abc.FillRectangle(solidBrush, x * size - size, y * size - size, size * 3, size * 3);

                        solidBrush = new SolidBrush(virus.Color);
                        abc.FillRectangle(solidBrush, x * size, y * size, size, size);
                    }
                    else if (e.Button == MouseButtons.Right) // Если лекарство
                    {
                        game.UpdateCell(x, y, false, countActionInPause);

                        SolidBrush solidBrush = new SolidBrush(color_cell);
                        abc.FillRectangle(solidBrush, x * size - size, y * size - size, size * 3, size * 3);

                        solidBrush = new SolidBrush(med.Color);
                        abc.FillRectangle(solidBrush, x * size, y * size, size, size);
                    }
                    countActionInPause++;
                }

                pictureBox1.Image = bitmap;
                return;
            }
        }
    }
}
