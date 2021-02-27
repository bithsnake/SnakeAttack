using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class GameOver : GameObject, IRenderable , IMovable
    {
        private char mychar;
        private Direction mydirection;

        public char InstanceChar { get => mychar; set => mychar = value; }
        public Direction CurrentDirection { get => mydirection; set => mydirection = value; }


       internal void Move()
        {
            switch (CurrentDirection)
            {
                case Direction.Up:
                    Position.Y -= 1;
                    break;
                case Direction.Right:
                    Position.X += 1;
                    break;
                case Direction.Down:
                    Position.Y += 1;
                    break;
                case Direction.Left:
                    Position.X -= 1;
                    break;
                case Direction.None:
                    break;
            }
        }
        public override void Update()
        {
            Program.currentRank = Difficulty.Easy;
            string title        = "GAME OVER";
            string pressStart   = "PRESS S TO RETRY";
            string pressQuit    = "PRESS Q TO QUIT";
            int titleLength     = title.Length / 4;
            bool isCentered     = Position.Y == Console.WindowHeight / 2 - titleLength;

            if (Position.Y < Console.WindowHeight / 2 - titleLength)
            {
                CurrentDirection = Direction.Down;
            }
            else
            {
                CurrentDirection = Direction.None;
            }
            Move();
            if (!isCentered)
            {
                Console.Clear();
            }
            Console.SetCursorPosition(this.X + Console.WindowWidth / 2 - title.Length / 2, this.Y);
            Console.Write($"{title}\n        " +
                $"Your score was: {Program.currentScore}\n       " +
                $"Your highScore is: {Program.highScore}\n       " +
                $"{(isCentered ? "''" + pressStart + "''" : "")}\n       " +
                $"{(isCentered ? "''" + pressQuit + "''" : "")}");
        }

        // Not used
        public override void CheckBorder()
        {

        }
    }
}
