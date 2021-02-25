using System;

namespace Snake
{


    class ConsoleRenderer
    {
        private readonly GameWorld world;
        public ConsoleRenderer(GameWorld gameWorld)
        {
            world = gameWorld;
            Console.SetWindowSize(world.WindowWidth, world.WindowHeight);
            Console.SetBufferSize(world.WindowWidth, world.WindowHeight);
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.ResetColor();
        }
       
        //Set to false when all false have been drawn
        bool canDrawWall = true;

        /// <summary>
        /// GUI where you can see your current score, the game timer and the gamespeed
        /// </summary>
        public void RenderShowStatus()
        {
            Console.SetCursorPosition((int)(0 +world.WindowWidth*0.05), 0);
            string globalTimerFormatted = String.Format("{0:#}", world.globalTimer);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write($"Score: {world.score} Time: {globalTimerFormatted} Spd: {world.globalGameSpeed}");
            switch (Program.currentRank)
            {
                case Difficulty.Easy:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Difficulty.Medium:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case Difficulty.Hard:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case Difficulty.Extreme:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
            }
                

        }

        /// <summary>
        /// Render blank spaces to avoid blinking on the console window
        /// </summary>
        public void RenderBlank(GameWorld world)
        {
            try
            {
                foreach (var instance in world.gameObjects)
                {
                    if (instance is IRenderable)
                    {
                        Console.Write(' ');
                        Console.SetCursorPosition(instance.X, instance.Y);
                        Console.Write(' ');
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                world.windowWidth = 32;
                world.windowHeight = 16;
                throw new ArgumentOutOfRangeException("Width or height on console window is either to small or large");
            }
        }
        
        /// <summary>
        /// Draw sprites on the console window
        /// </summary>
        public void Render()
        {
            foreach (var instance in world.gameObjects)
            {
                //DRAW PLAYER
                if (instance is Player && instance is IRenderable)
                {
                    var instPlayer = instance as Player;
                    instPlayer.CheckBorder();
                    Console.SetCursorPosition(instPlayer.X, instPlayer.Y);
                    Console.Write(instPlayer.InstanceChar);
                }
                //DRAW FOOD
                if (instance is Food && instance is IRenderable)
                {
                    var instFood = instance as Food;
                    Console.SetCursorPosition(instFood.X, instFood.Y);
                    Console.Write(instFood.InstanceChar);
                }
                //DRAW ENEMY
                if (instance is Enemy && instance is IRenderable)
                {
                    var instEnemy = instance as Enemy;
                    
                    Console.SetCursorPosition(instEnemy.X, instEnemy.Y);
                    Console.Write(instEnemy.InstanceChar);
                }

                //DRAW WALL

                if (instance is Wall && instance is IRenderable && canDrawWall)
                {
                    var instWall = instance as Wall;
                    for (int i = 0; i < instWall.wallObjects.Count; i++)
                    {
                        Console.SetCursorPosition(instWall.wallObjects[i].X, instWall.wallObjects[i].Y);
                        Console.Write(instWall.wallObjects[i].InstanceChar);
                        if(i >= instWall.wallObjects.Count)
                        {
                            canDrawWall = false;
                        }
                    }
                }
            }
        }
    }
}
