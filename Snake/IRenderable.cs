using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    /// <summary>
    /// Classes using this interface needs to implement a char
    /// </summary>
    interface IRenderable
    {
        public char InstanceChar { get; set; }
    }
}
