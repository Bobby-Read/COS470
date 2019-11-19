using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    public class Vector
    {
        public Vector(int x, int y)
        {
            y = Y;
            x = X;
        }

        public int X;
        public int Y;

        public static Vector operator +( Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

    }
}
