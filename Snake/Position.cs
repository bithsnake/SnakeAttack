using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    /// <summary>
    /// A Position struct
    /// </summary>
    struct Position
    {
        public int X;
        public int Y;

        public Position(int xx, int yy)
        {
            X = xx;
            Y = yy;
        }

        public static Position operator +(Position left, Position right)
        {
            return new Position(left.X + right.X, left.Y + right.Y);
        }
        public static Position operator -(Position left, Position right)
        {
            return new Position(left.X - right.X, left.Y - right.Y);
        }
        public static bool operator ==(Position left, Position right)
        {
            return left.X == right.X && left.Y == right.Y;
        }
        public static bool operator !=(Position left, Position right)
        {
            return left.X != right.X || left.Y != right.Y;
        }
    }
}
