using System;

namespace Snake
{
    /// <summary>
    /// Main Player Class
    /// </summary>
    public class Player : GameObject, IRenderable, IMovable
    {
        private char mychar;
        private Direction mydirection;
        private GameWorld world;
        public char InstanceChar { get => mychar; set => mychar = value; }
        public Direction CurrentDirection { get => mydirection; set => mydirection = value; }
        public GameWorld World { get => world; set => world = value; }

        /// <summary>
        /// Initialize player
        /// </summary>
        /// <param name="playerSprite"></param>
        /// <param name="dir"></param>
        public Player(char sprite,Direction direction)
        {
            InstanceChar = sprite;
            CurrentDirection = direction;
        }

        /// <summary>
        /// Players main update, Checks direction and moves according to directions.
        /// </summary>
        public override void Update()
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
        /// <summary>
        /// Check console window borders and wrap X and Y position accordingly if coordinates exceeds the borders.
        /// </summary>
        /// <param name="position"></param>
        public override void CheckBorder()
        {
            if (X < Console.WindowLeft)
            {
                X = Console.WindowWidth - 1;
            }
            else if (X > Console.WindowWidth - 1)
            {
                X = 0;
            }
            else if (Y < Console.WindowTop + 2)
            {
                Y = Console.WindowHeight - 1;
            }
            else if (Y > Console.WindowHeight - 1)
            {
                Y = Console.WindowTop+2;
            }
        }
    }
}
