using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    public class TEST
    {
        public static Position ChangePosition(Position position)
        {
           return new Position(position.X + 3, position.Y + 2);
        }
        public static Position MovePlayer(Player player,Direction newDirection)
        {
            player.CurrentDirection = newDirection;
            player.Update();
            return player.Position;
        }


        public static GameWorld CreateGameWorld()
        {
            List<GameObject> gameObjectList = new List<GameObject>();
            return new GameWorld(32, 16, 8, gameObjectList);
        }


        static void TestMain()
        {

        }


    }
}
