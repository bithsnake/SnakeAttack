using System;


namespace Snake
{
    /// <summary>
    /// Food object, inherits from GameObject
    /// </summary>
    class Food : GameObject, IRenderable
    {
        
        private char mychar;
        private Position foodPosition;
        private GameWorld world;
        public char InstanceChar { get => mychar; set => mychar = value; }
        public Position FoodPosition{ get => foodPosition; set => foodPosition = value; }
        public GameWorld World { get => world; set => world = value; }

        /// <summary>
        /// Init the food object.
        /// </summary>
        /// <param name="foodSprite">Set a char to be used as a sprite.</param>
        /// <param name="world">Which world object to reference.</param>
        public Food(char foodSprite,GameWorld world)
        {
            InstanceChar = foodSprite;
            World = world;
        }

        //Set to true if you want to move around the food automatically in the Update method.
        bool canMoveFood = true;

        /// <summary>
        /// Init the food object + set a Position.
        /// </summary>
        /// <param name="foodSprite">Use a char to use as a sprite for the food.</param>
        /// <param name="xPosition">Set X Coordinate.</param>
        /// <param name="yPosition">Set Y Coodrindate.</param>
        /// <param name="world">Set reference to a world object.</param>
        public Food(char foodSprite, int xPosition,int yPosition,GameWorld world)
            :this(foodSprite,world)
        {
            InstanceChar = foodSprite;
            FoodPosition = new Position(xPosition,yPosition);
        }
       
        /// <summary>
        /// Sets a new position for the food.
        /// </summary>
        public void ChangePosition(GameWorld world)
        {
            if(Program.currentRank != Difficulty.Easy)
            {
                this.Position = new Position(new Random().Next(4, world.windowWidth - 5), new Random().Next(4, world.windowHeight - 5));

                //Check that the food is not colliding with the player or the wall, otherwise change position
                foreach (var instance in world.gameObjects)
                {
                    if (instance is Player)
                    {
                        while (this.X == instance.X && this.Y == instance.Y)
                        {
                            this.Position = new Position(new Random().Next(5, world.windowWidth - 5), new Random().Next(5, world.windowHeight - 5));
                            continue;
                        }
                    }
                    else if (instance is Wall)
                    {
                        while (this.X == instance.X && this.Y == instance.Y)
                        {
                            this.Position = new Position(new Random().Next(5, world.windowWidth - 5), new Random().Next(5, world.windowHeight - 5));
                            continue;
                        }
                    }
                }                
            }
        }

        /// <summary>
        /// Check if inside a wall
        /// </summary>
        public override void CheckBorder()
        {
            if(Program.currentRank != Difficulty.Easy)
            {
                foreach (var instance in world.gameObjects)
                {
                    if(instance is Wall)
                    {
                        var wall = instance as Wall;
                        if (Position.X > world.WindowWidth/2 && Position.X == wall.X)
                        {
                            Position.X--;
                        }else if (Position.X < world.WindowWidth/2 && Position.X == wall.X)
                        {
                            Position.X++;
                        }
                        if (Position.Y > world.WindowHeight/2 && Position.Y == wall.Y)
                        {
                            Position.Y--;
                        }else if (Position.X < world.WindowWidth/2 && Position.X == wall.X)
                        {
                            Position.Y++;
                        }
                    }

                }
            }

        }

        /// <summary>
        /// Main Loop for the food object.
        /// </summary>
        public override void Update()
        {
            //Check if colliding with wall
            CheckBorder();
            if (Program.currentRank != Difficulty.Easy)
            {
                if ((int)Program.globalTimer % 4 == 0 && canMoveFood)
                {
                    canMoveFood = false;
                    Console.Beep(80, 30);
                    ChangePosition(this.world);
                }
                if ((int)Program.globalTimer % 3 == 0 && !canMoveFood)
                {
                    canMoveFood = true;
                }
            }
        }


    }
}
