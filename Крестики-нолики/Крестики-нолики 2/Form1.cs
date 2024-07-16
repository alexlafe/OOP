using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Крестики_нолики_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Init()
        {
            pers = false;
            pers_name = "0";
            pole = new byte[] { 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            label1.Text = "Сейчас ходит X";



            for (byte i = 1; i <= 10; i++)
            {
                if (i != 4)
                {
                    this.Controls[$"button{i}"].Text = "";
                }    
            }
        }

        bool pers = true;
        string pers_name = "X";
        byte[] pole = new byte[] { 2, 2, 2, 2, 2, 2, 2, 2, 2 };

        private void button1_Click(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            if (bt.Text == "")
            {
                pole[bt.TabIndex] = Convert.ToByte(pers);
                bt.Text = pers_name;

                check_win();

                pers = !pers;
                function();
                label1.Text = $"Сейчас ходит {pers_name}!";
            }
        } 

        public void check_win()
         {
                for (byte i = 0; i < 9; i++)
                {
                    if (pole[i] == 2)
                        break;
                    if (i == 8)
                    {
                        MessageBox.Show("Никто не победил!");
                        Init();
                    }
                }
            
            byte[,] Wins = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 }, { 0, 4, 8 }, { 2, 4, 6 } };
            for (byte i = 0; i < 8; i++)
            {
                if (pole[Wins[i, 0]] == pole[Wins[i, 1]] && pole[Wins[i, 1]] == pole[Wins[i, 2]] && pole[Wins[i, 0]] != 2)
                {
                    MessageBox.Show($"Победили {pers_name}");
                    Init();
                    break;
                }
            }
         }

        public void function()
        {
            if (pers == false)
            {
                pers_name = "0";
            }
            else
            {
                pers_name = "X";
            }
        }

        public void on_click()
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
    }
