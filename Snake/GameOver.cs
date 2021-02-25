using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class GameOver: Player
    {
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
            base.Update();
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

        public override void CheckBorder()
        {
            base.CheckBorder();
        }
    }
}
