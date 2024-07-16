using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace _25._11
{
    abstract class Cell
    {
        public abstract bool Life { get; set; }
        public abstract int Age { get; set; }
        public abstract Color Color { get; set; }
    }
}
