using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace _25._11
{
    class Virus : Cells
    {
        public int radius;

        public override int Age { get; set; }
        public override Color Color { get; set; }
        public override bool Life { get; set; }

        public Virus()
        {
            Age = 5;
            Color = Color.Black;
            radius = 2;
        }
    }
}
