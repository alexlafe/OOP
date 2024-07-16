using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace _25._11
{
    class Cells : Cell
    {
        public override bool Life { get; set; }
        public override int Age { get; set; }
        public override Color Color { get; set; }

        public Cells()
        {
            Life = false;
            Color = Color.Pink;
            Age = 1;
        }

        public Color GetColor()
        {
            if (Age < 14)
            {
                return Color.LightCyan;
            }
            else if (Age >= 14 && Age < 18)
            {
                return Color.Cyan;
            }
            else if (Age >= 18 && Age < 55)
            {
                return Color.Blue;
            }
            else
            {
                return Color.DarkBlue;
            }
        }
    }
}
