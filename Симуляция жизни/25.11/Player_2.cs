using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace _25._11
{
    class Player_2 : Cell
    {
        public override int Age { get; set; }
        public override bool Life { get; set; }
        public override Color Color { get; set; }

        public Player_2()
        {
            Life = false;
            Age = 0;
            Color = Color.Orange;
        }

        public Color GetColor()
        {
            return Color;
        }
    }
}
