using System;


namespace Snake
{
    /// <summary>
    /// A Class all game objects inherits from
    /// </summary>
    public abstract class GameObject
    {
        public Position Position;
        public int timer;

        public int X    { get => Position.X; set => Position.X = value; }
        public int Y    { get => Position.Y; set => Position.Y = value; }
        public int Timer { get => timer; set => timer = value; }
        
        public abstract void Update();
        public abstract void CheckBorder();
    }
}
