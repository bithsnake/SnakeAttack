using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class TEST
    {

        Position myposition = new Position(8, 8);

        Player player = new Player('O',Direction.None);

        public static Position ChangePosition(Position position)
        {
           return new Position(position.X + 3, position.Y + 2);
        }
        static void ChangePlayerDirection(Player player, Direction direction)
        {
            Random rand = new Random();
            
            int val = rand.Next(0, 3);
            switch (val)
            {
                case 0:
                    player.CurrentDirection = Direction.Up;
                    break;
                case 1:
                    player.CurrentDirection = Direction.Right;
                    break;
                case 2:
                    player.CurrentDirection = Direction.Down;
                    break;
                case 3:
                    player.CurrentDirection = Direction.Left;
                    break;
            }
        }


        static GameWorld CreateGameWorld()
        {
            return new GameWorld(32,16,8);
        }

        static Food CreateFoodObject(GameWorld world)
        {
            return new Food('F', world);
        }
        static void AddToGameWorld(List<object> list, object obj)
        {
            list.Add(obj);
        }
        static void TestMain()
        {
            //Position test
            Position pos = new Position(2, 2);


            GameWorld newWorld = CreateGameWorld();
            ChangePosition(pos);
        }


    }
}
