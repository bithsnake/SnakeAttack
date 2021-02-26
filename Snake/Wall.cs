using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Snake
{
    /// <summary>
    /// Wall Class
    /// </summary>
    class Wall : GameObject, IRenderable
    {
        private char myChar;
        public char InstanceChar { get => myChar; set => myChar = value; }

        //The wall child instances of this parent
        public List<Wall> wallObjects = null;

        public Wall(char ch = '█')
        {
            InstanceChar = ch;
            
        }
        /// <summary>
        /// Empty Check border method
        /// </summary>
        public override void CheckBorder()
        {
            //Nothing to check or to here
        }
        /// <summary>
        /// Empty Update loop so far.
        /// </summary>
        public override void Update()
        {
            //nothing here
        }
        /// <summary>
        /// Create walls around the stage.
        /// </summary>
        public void CreateWalls()
        {
            var ww = Console.WindowWidth - 2;
            var wh = Console.WindowHeight - 2;

            int wallAmount = ww * 2 + wh;
            try
            {
                if (wallObjects.Count < wallAmount)
                {
                    for (int i = 1; i <= ww; i++)
                    {
                        for (int j = 2; j <= wh; j++)
                        {
                            Position previousHor = new Position(i, j);
                            if (i == 1 || i == ww)
                            {
                                var newWall = new Wall
                                {
                                    //newWall.Position = new Position(i, j);
                                    Position = new Position(i != previousHor.X ? i : previousHor.X, j != previousHor.Y ? j : previousHor.Y)
                                };
                                wallObjects.Add(newWall);
                            }
                            if (j == 2 || j == wh)
                            {
                                var newWall = new Wall
                                {
                                    Position = new Position(i != previousHor.X ? i : previousHor.X, j != previousHor.Y ? j : previousHor.Y)
                                };
                                wallObjects.Add(newWall);
                            }
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Window size too big or small");
            }

        }
    }
}
