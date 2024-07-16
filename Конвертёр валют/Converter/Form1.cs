using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string stat1, stat2;

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text == "")
                {
                    textBox2.Text = $"{float.Parse(textBox1.Text) / 72}";
                }
                else if (textBox1.Text == "" && textBox2.Text != "")
                {
                    textBox1.Text = $"{float.Parse(textBox2.Text) * 72}";
                }
            }
            catch(FormatException)
            {
                if (textBox1.Text != "")
                {
                    MessageBox.Show("Ошибка. Вводите число");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text != "" && comboBox1.Text != "" && textBox3.Text != "")
                {
                    if (stat1 == stat2)
                    {
                        label10.Text = textBox3.Text;
                    }
                    if (stat1 == "Рубли")
                    {
                        switch (stat2)
                        {
                            case "Доллары":
                                label10.Text = $"{float.Parse(textBox3.Text) * 0.014}";
                                break;
                            case "Евро":
                                label10.Text = $"{float.Parse(textBox3.Text) * 0.012}";
                                break;
                            case "Злоты":
                                label10.Text = $"{float.Parse(textBox3.Text) * 0.055}";
                                break;
                        }
                    }
                    if (stat1 == "Доллары")
                    {
                        switch (stat2)
                        {
                            case "Рубли":
                                label10.Text = $"{float.Parse(textBox3.Text) * 72.06}";
                                break;
                            case "Евро":
                                label10.Text = $"{float.Parse(textBox3.Text) * 0.86}";
                                break;
                            case "Злоты":
                                label10.Text = $"{float.Parse(textBox3.Text) * 3.96}";
                                break;
                        }
                    }
                    if (stat1 == "Евро")
                    {
                        switch (stat2)
                        {
                            case "Рубли":
                                label10.Text = $"{float.Parse(textBox3.Text) * 83.52}";
                                break;
                            case "Доллары":
                                label10.Text = $"{float.Parse(textBox3.Text) * 1.16}";
                                break;
                            case "Злоты":
                                label10.Text = $"{float.Parse(textBox3.Text) * 4.6}";
                                break;
                        }
                    }
                    if (stat1 == "Злоты")
                    {
                        switch (stat2)
                        {
                            case "Доллары":
                                label10.Text = $"{float.Parse(textBox3.Text) / 4}";
                                break;
                            case "Евро":
                                label10.Text = $"{float.Parse(textBox3.Text) * 0.22}";
                                break;
                            case "Рубли":
                                label10.Text = $"{float.Parse(textBox3.Text) * 18.18}";
                                break;
                        }
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка. Введите число");
            }
        }
      

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            stat2 = comboBox2.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            stat1 = comboBox1.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
