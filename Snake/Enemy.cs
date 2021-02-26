using System;

namespace Snake
{
    /// <summary>
    /// Enemy Object, inherits from GameObject class
    /// </summary>
    class Enemy : GameObject , IRenderable , IMovable
    {
        private char myChar;
        private Direction myDirection;

        public char InstanceChar { get => myChar; set => myChar = '†'; }
        public Direction CurrentDirection { get => myDirection; set => myDirection = Direction.Down; }

        /// <summary>
        /// Init the enemy with a sprite and direction on creation.
        /// </summary>
        /// <param name="charSprite"></param>
        /// <param name="dir"></param>
        public Enemy(char charSprite, Direction dir)
        {
            InstanceChar = charSprite;
            CurrentDirection = dir;
        }

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
                Y = Console.WindowHeight - 2;
            }
            else if (Y > Console.WindowHeight - 2)
            {
                Y = Console.WindowTop + 2;
            }
        }

        /// <summary>
        /// Main Update Loop for the enemy.
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

    }
}
