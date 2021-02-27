using System;
using System.Collections.Generic;
using System.Linq;


namespace Snake
{
   public class GameWorld
    {
        public int windowWidth;
        public int windowHeight;
        public int score = 0;
        public  double globalTimer  = 10;
        public  int globalGameSpeed;
        public GameWorld world;
        public List<GameObject> gameObjectList;

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
        public List<GameObject> GameObjectList { get => gameObjectList; set => gameObjectList = value; }
        
        /// <summary>
        /// Game world object constructor
        /// </summary>
        /// <param name="windowWidth">  Set window width in pixels.                 </param>
        /// <param name="windowHeight"> set window height in pixels                 </param>
        /// <param name="gamespeed">    Initialize the start game speed(in frames). </param>
        public GameWorld(int windowWidth,int windowHeight, int speed, List<GameObject> objectList)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            World = world;
            GlobalGameSpeed = speed;
            GameObjectList = objectList;
        }
        /// <summary>
        /// The game objects that the world is going to contain
        /// </summary>
        //public List<GameObject> gameInstanceList = new List<GameObject>();

        /// <summary>
        /// Create and initialise the snake and add it to the GameObject List.
        /// </summary>
        /// <param name="world">Input world this player is added to.</param>
        /// <param name="playerSprite">Input char to use as a sprite.</param>
        /// <param name="initDirection">Set initial direction.</param>
        public Player AddPlayer(char playerSprite,Direction initDirection)
        {
            Player snake = new Player(playerSprite, initDirection)
            {
                Position = new Position(new Random().Next(4, windowWidth - 1), new Random().Next(4, windowHeight - 1))
            };
            gameObjectList.Add(snake);
            return snake;
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
            foreach (var instance in gameObjectList)
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
            gameObjectList.Add(food);
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
                gameObjectList.Add(enemy);
            }

            // Create atype of enemy depending on difficulty
            switch (Program.currentRank)
            {
                case Difficulty.Easy:
                    break;
                case Difficulty.Medium:

                    if (score > 14)
                    {
                        if (Program.globalTimer % 4 == 0 && !gameObjectList.OfType<Enemy>().Any())
                        {
                            CreateAndSetEnemyPosition(new Random().Next(3,10));
                        }
                    }

                    break;
                case Difficulty.Hard:
                    if (Program.globalTimer % 2 == 0 && gameObjectList.OfType<Enemy>().Count() < 3)
                    {
                        CreateAndSetEnemyPosition(8);
                    }
                    if (Program.globalTimer % 5 == 0 && gameObjectList.OfType<Enemy>().Count() < 3)
                    {
                        CreateAndSetEnemyPosition(6);
                    }
                    break;
                case Difficulty.Extreme:

                    if (Program.globalTimer % 2 == 0 && gameObjectList.OfType<Enemy>().Count() < 4)
                    {
                        CreateAndSetEnemyPosition(3);
                    }
                    if (Program.globalTimer % 4 == 0 && gameObjectList.OfType<Enemy>().Count() < 4)
                    {
                        CreateAndSetEnemyPosition(4);
                    }
                    if (Program.globalTimer % 6 == 0 && gameObjectList.OfType<Enemy>().Count() < 4)
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
                    if (!gameObjectList.OfType<Wall>().Any())
                    {
                        foreach (var obj in gameObjectList)
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
                        gameObjectList.Add(wall);
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
            foreach (var instance in gameObjectList)
            {
                instance.Update();
            }

            //Create walls depending on difficulty
            CreateWall();

            //Enemy AI
            CreateEnemy();
            foreach (var instance in gameObjectList)
            {
                if (instance is Enemy)
                {
                    //instance.Update();
                    instance.CheckBorder();
                    var instEnemy = instance as Enemy;
                    if (instEnemy.Y >= Console.WindowHeight - 1)
                    {
                        gameObjectList.Remove(instEnemy);
                        break;
                    }
                    
                }
            }

            //PLAYER COLLISIONS
            foreach (var instance in gameObjectList)
            {
                if (instance is Player)
                {
                    var instPlayer = instance as Player;

                    //COLLISIONS WITH FOOD
                    foreach (var food in gameObjectList)
                    {
                        if (food is Food)
                        {
                            var instFood = food as Food;

                            if (instPlayer.Position.X == instFood.Position.X && instPlayer.Position.Y == instFood.Position.Y)
                            {
                                var currentWorld = instFood.World;
                                UpdateScoreAndDifficulty();
                                gameObjectList.Remove(instFood);
                                CreateFood(currentWorld);
                                break;
                            }
                        }
                    }

                    //PLAYER COLLISIONS WITH ENEMY
                    foreach (var enemy in gameObjectList)
                    {
                        if (enemy is Enemy)
                        {
                            var instEnemy = enemy as Enemy;

                            //If collide with player, take away score and reduce the timer
                            if (instEnemy.Position.X == instPlayer.Position.X && instEnemy.Position.Y == instPlayer.Position.Y)
                            {
                                Console.Beep(700, 20);
                                if (score > 0)
                                {
                                    score--;
                                    globalTimer--;
                                    gameObjectList.Remove(instEnemy);
                                }
                                break;
                            }
                        }
                    }

                    //PLAYER COLLISIONS WITH WALL OBJECT
                    foreach (var wall in gameObjectList)
                    {
                        if (wall is Wall)
                        {
                            var parentWall = wall as Wall;
                            foreach (var instWall in parentWall.wallObjects)
                            {
                                //If collide with player, take away score and reduce the timer
                                if (instPlayer.Position.X == instWall.Position.X && instPlayer.Position.Y == instWall.Position.Y)
                                {
                                    Console.Beep(666, 666);
                                    globalTimer = 0;
                                    instPlayer.Position = new Position(Console.WindowWidth / 2, Console.WindowHeight / 2);
                                    instPlayer.CurrentDirection = Direction.None;
                                    Program.runGame = false;

                                    break;
                                }
                            }
                        }
                    }
                }
                break;
            }
        }
    }
}
