using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    interface IMovable
    {
        public Direction CurrentDirection { get; set; }
    }
}
