using System;

namespace Snake
{
    class GameStart : Player
    {
        /// <summary>
        /// Render blank spaces to avoid blinking on the console window
        /// </summary>
        public void RenderBlank()
        {
            Console.Write(' ');
            Console.SetCursorPosition(this.X, this.Y);
            Console.Write(' ');
        }
        public override void Update()
        {
            try
            {
                string title = "SNAKE ATTACK";
                string pressStart = "PRESS S TO PLAY";
                int titleLength = title.Length / 4;
                bool isCentered = Position.Y == Console.WindowHeight / 2 - titleLength;
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
                Console.Write($"{title}\n      {(isCentered ? "''" + pressStart + "''" : "")}");
            }
            catch (System.IO.IOException)
            {
                Console.WindowWidth = 32;
                Console.WindowHeight = 16;
                Console.WriteLine("something wernt wrong here..dont adjust the screen please.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WindowWidth = 32;
                Console.WindowHeight = 16;
                Console.WriteLine("Dont adjust the window please, its meant to stay the size it start in.");
            }

        }


        public override void CheckBorder()
        {
            base.CheckBorder();
        }
    }
}
