using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Tail : Player
    {
        private char myChar;

        private Position position;

        public char InstanceChar { get => myChar; set => myChar = value; }

        public Position CurrentPosition { get => position; set => position = value; }

        public Tail(char ch,Position position)
        {
            InstanceChar = ch;
            CurrentPosition = position;
        }
        public override void Update()
        {
           
        }
        public override void CheckBorder()
        {
            base.CheckBorder();
        }
    }
}
