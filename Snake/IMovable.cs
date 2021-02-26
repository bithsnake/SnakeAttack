using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    /// <summary>
    /// Classes using this interface needs to implement a Direction
    /// </summary>
    interface IMovable
    {
        public Direction CurrentDirection { get; set; }
    }
}
