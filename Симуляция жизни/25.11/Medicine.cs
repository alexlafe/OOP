using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace _25._11
{
    class Medicine : Cells
    {
        public int radius;

        public override int Age { get; set; }
        public override Color Color { get; set; }
        public override bool Life { get; set; }

        public Medicine()
        {
            Age = 5;
            radius = 2;
            Color = Color.Green;
        }
    }
}
