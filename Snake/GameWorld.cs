using System;
using System.Collections.Generic;
using System.Linq;


namespace Snake
{
    class GameWorld
    {
        public int windowWidth;
        public int windowHeight;
        public int score = 0;
        public  double globalTimer  = 10;
        public  int globalGameSpeed;
        private GameWorld world;

        /// <summary>
        /// Set window width in pixels.
        /// </summary>
        public int WindowWidth { get => windowWidth; set => windowWidth = value; }
        /// <summary>
        /// Set window height in pixels.
        /// </summary>
        public int WindowHeight { get => windowHeight; set => windowHeight = value; }
        /// <summary>
        /// Initialise the game speed(frames).
        /// </summary>
        public int GlobalGameSpeed { get => globalGameSpeed; set => globalGameSpeed = value; }

        public GameWorld World { get => world; set => world = value; }
        
        /// <summary>
        /// Game world object constructor
        /// </summary>
        /// <param name="windowWidth">  Set window width in pixels.                 </param>
        /// <param name="windowHeight"> set window height in pixels                 </param>
        /// <param name="gamespeed">    Initialize the start game speed(in frames). </param>
        public GameWorld(int windowWidth,int windowHeight, int speed)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            World = world;
            GlobalGameSpeed = speed;
        }
        /// <summary>
        /// The game objects that thw world is going to contain
        /// </summary>
        public List<GameObject> gameObjects = new List<GameObject>();

        /// <summary>
        /// Create and initialise the snake and add it to the GameObject List.
        /// </summary>
        /// <returns></returns>
        public Player CreatePlayer()
        {
            Player name = new Player('☻', Direction.None)
            {
                Position = new Position(new Random().Next(4, windowWidth - 1), new Random().Next(4, windowHeight - 1))
            };
            gameObjects.Add(name);
            return name;
        }

        /// <summary>
        /// Create new food and add it to the GameObject List.
        /// </summary>
        public void CreateFood(GameWorld world)
        {
            //Create the first food
            Food food = new Food('♥', world);
            /*Test if the food changes place if it accidentaly get sthe same coordinations as the player*/
            //food.Position = new Position(snake.X, snake.Y);
            if(Program.currentRank == Difficulty.Easy)
            {
                food.Position = new Position(new Random().Next(1, windowWidth - 1), new Random().Next(4, windowHeight - 1));
            }
            else
            {
                food.Position = new Position(new Random().Next(5, windowWidth - 5), new Random().Next(5, windowHeight - 5));
            }

            //Create the food in a X & Y coordinate that the snake is not initialized in
            foreach (var instance in gameObjects)
            {
                if (instance is Player)
                {
                    while (food.X == instance.X && food.Y == instance.Y)
                    {
                        food.Position = new Position(new Random().Next(5, windowWidth - 1), new Random().Next(5, windowHeight - 1));
                        continue;
                    }
                }
            }
            gameObjects.Add(food);
        }
        
        /// <summary>
        /// Update score ,reset the timer cooldown and adjust difficulty according to score.
        /// </summary>
        void UpdateScoreAndDifficulty()
        {
            globalTimer = 10;
            score++;
            Program.highScore = Program.highScore < score ? score : Program.highScore;
            Program.SetRank(score);   //Set the rank depending on score
            globalGameSpeed = Program.SetDifficulty(score, globalGameSpeed);
            Program.SetColorAndSound();
        }

        /// <summary>
        /// Create an enemy depending on the difficulty
        /// </summary>
        void CreateEnemy()
        {
            void CreateAndSetEnemyPosition(int offset)
            {
                Enemy enemy = new Enemy('†', Direction.Down);
                int randomWidth = Convert.ToInt32(WindowWidth * 0.8);
                int newX, newY;
                Random r = new Random(Guid.NewGuid().GetHashCode());
                int random = r.Next(2, randomWidth - offset);
                newX = random;
                newY = 2;
                enemy.Position = new Position(newX, newY);
                gameObjects.Add(enemy);
            }
            var sec = DateTime.Now.Second;
            switch (Program.currentRank)
            {
                case Difficulty.Easy:
                    break;
                case Difficulty.Medium:

                    if (score > 14)
                    {
                        if (Program.globalTimer % 4 == 0 && !gameObjects.OfType<Enemy>().Any())
                        {
                            CreateAndSetEnemyPosition(new Random().Next(3,10));
                        }
                    }

                    break;
                case Difficulty.Hard:
                    if (Program.globalTimer % 2 == 0 && gameObjects.OfType<Enemy>().Count() < 3)
                    {
                        CreateAndSetEnemyPosition(8);
                    }
                    if (Program.globalTimer % 5 == 0 && gameObjects.OfType<Enemy>().Count() < 3)
                    {
                        CreateAndSetEnemyPosition(6);
                    }
                    break;
                case Difficulty.Extreme:

                    if (Program.globalTimer % 2 == 0 && gameObjects.OfType<Enemy>().Count() < 4)
                    {
                        CreateAndSetEnemyPosition(3);
                    }
                    if (Program.globalTimer % 4 == 0 && gameObjects.OfType<Enemy>().Count() < 4)
                    {
                        CreateAndSetEnemyPosition(4);
                    }
                    if (Program.globalTimer % 6 == 0 && gameObjects.OfType<Enemy>().Count() < 4)
                    {
                        CreateAndSetEnemyPosition(6);
                    }

                    break;
            }
        }

        /// <summary>
        /// Creates walls when Difficulty is higher than Easy.
        /// </summary>
        void CreateWall()
        {
            switch (Program.currentRank)
            {
                case Difficulty.Easy:

                    break;
                case Difficulty.Medium:
                    //Create a main/parent instance of the wall if there is not any Instance of type Wall
                    if (!gameObjects.OfType<Wall>().Any())
                    {
                        foreach (var obj in gameObjects)
                        {
                            if(obj is Player)
                            {
                                var instPlayer = obj as Player;
                                instPlayer.CurrentDirection = Direction.None;
                                instPlayer.Position = new Position(Console.WindowWidth / 2, Console.WindowHeight / 2);

                            }
                            if(obj is Food)
                            {
                                var instFood  = obj as Food;
                                instFood.ChangePosition(instFood.World);
                            }
                        }
                        Wall wall = new Wall(' ');
                        gameObjects.Add(wall);
                        wall.wallObjects = new List<Wall>();
                        wall.CreateWalls();
                    }
                    break;
                case Difficulty.Hard:
                    break;
                case Difficulty.Extreme:
                    break;
            }
        }
        
        /// <summary>
        /// Game Worlds Main Loop / Step event
        /// </summary>
        public void Update()
        {
            //Run update on all instances first
            foreach (var instance in gameObjects)
            {
                instance.Update();
            }

            //Create walls depending on difficulty
            CreateWall();

            //Enemy AI
            CreateEnemy();
            foreach (var instance in gameObjects)
            {
                if (instance is Enemy)
                {
                    //instance.Update();
                    instance.CheckBorder();
                    var instEnemy = instance as Enemy;
                    if (instEnemy.Y >= Console.WindowHeight - 1)
                    {
                        gameObjects.Remove(instEnemy);
                        break;
                    }
                    
                }
            }

            //Player Collisions
            foreach (var instance in gameObjects)
            {
                //Somebody in the list is a Player object
                if (instance is Player)
                {
                    var instPlayer = instance as Player;

                    //If somebody in the list is a Food object
                    foreach (var food in gameObjects)
                    {
                        if (food is Food)
                        {
                            var instFood = food as Food;

                            if (instPlayer.Position.X == instFood.Position.X && instPlayer.Position.Y == instFood.Position.Y)
                            {
                                var currentWorld = instFood.World;
                                UpdateScoreAndDifficulty();
                                gameObjects.Remove(instFood);
                                CreateFood(currentWorld);
                                break;
                            }
                        }
                    }
                    //If somebody in the list is a Wall object
                    foreach (var walls in gameObjects)
                    {
                        if (walls is Wall)
                        {
                            var parentWall = walls as Wall;

                            foreach (var wallInstance in parentWall.wallObjects)
                            {
                                if (instPlayer.Position.X == wallInstance.Position.X && instPlayer.Position.Y == wallInstance.Position.Y)
                                {
                                    globalTimer = 0;
                                    Program.runGame = false;
                                    Console.Beep(1500, 400);
                                    instPlayer.Position = new Position(Console.WindowWidth / 2, Console.WindowHeight / 2);
                                    break;
                                }
                            }

                        }
                    }

                    
                    //If somebody in the list is a Enemy object
                    foreach (var enemy in gameObjects)
                    {
                        if (enemy is Enemy)
                        {
                            var instEnemy = enemy as Enemy;

                            //If collide with player, take away score and reduce the timer
                            if (instEnemy.Position.X == instPlayer.Position.X && instEnemy.Position.Y == instPlayer.Position.Y)
                            {
                                Console.Beep(1200, 20);
                                if (score > 0)
                                {
                                    score--;
                                    globalTimer--;
                                    gameObjects.Remove(instEnemy);
                                }
                                break;
                            }
                        }
                    }
                }

               
                //instance.Update();
                break;
            }
        }
    }
}
