using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace _25._11
{
    class Player_1 : Cell
    {
        public override int Age { get; set; }
        public override bool Life { get; set; }
        public override Color Color { get; set; }

        public Player_1()
        {
            Life = false;
            Age = 0;
            Color = Color.Purple;
        }

        public Color GetColor()
        {
            return Color;
        }
    }
}
