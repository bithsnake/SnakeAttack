using System;


namespace Snake
{
    class Food : GameObject, IRenderable
    {
        private char mychar;
        private Position foodPosition;
        private GameWorld world;
        public char InstanceChar { get => mychar; set => mychar = value; }
        public Position FoodPosition{ get => foodPosition; set => foodPosition = value; }
        public GameWorld World { get => world; set => world = value; }

        public Food(char foodSprite,GameWorld world)
        {
            InstanceChar = foodSprite;
            World = world;
        }

        bool canMoveFood = true;

        public Food(char foodSprite, int xPosition,int yPosition,GameWorld world)
            :this(foodSprite,world)
        {
            InstanceChar = foodSprite;
            FoodPosition = new Position(xPosition,yPosition);
        }
       
        /// <summary>
        /// Set a new position for the food
        /// </summary>
        public void ChangePosition(GameWorld world)
        {
            if(Program.currentRank != Difficulty.Easy)
            {
                this.Position = new Position(new Random().Next(4, world.windowWidth - 5), new Random().Next(4, world.windowHeight - 5));

                //Create the food in a X & Y coordinate that the snake is not initialized in
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
                }                
            }
        }
       
        public override void Update()
        {
            if(Program.currentRank != Difficulty.Easy)
            {                
                if((int)Program.globalTimer % 4 == 0 && canMoveFood)
                {
                    canMoveFood = false;
                    Console.Beep(80, 30);
                    ChangePosition(this.world);
                }
                if((int)Program.globalTimer % 3 == 0 && !canMoveFood)
                {
                    canMoveFood = true;
                }
            }
        }


        /// <summary>
        /// Not used in food
        /// </summary>
        public override void CheckBorder()
        {

        }
    }
}
