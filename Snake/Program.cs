using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace Snake
{
    /// <summary>
    /// Objects Directions.
    /// </summary>
    enum Direction
    {
        Up,
        Right,
        Left,
        Down,
        None
    };
    /// <summary>
    /// Difficulty.
    /// </summary>
    enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Extreme
    }
    /// <summary>
    /// Game States.
    /// </summary>
    enum CurrentGameState
    {
        StartMenu,
        InGame,
        GameOver
    }
    /// <summary>
    /// The Main Program
    /// </summary>
    class Program
    {
        //Init Public values
        public static bool runGame = false;
        // Game Ranks
        public static int lowRank = 6;
        public static int mediumRank = 18;
        public static int highRank = 25;
        //initial speed at start
        public static int startSpeed = 8;
        public static Difficulty currentRank = Difficulty.Easy;
        public static int globalTimer = 0;
        //Public scores
        public static int currentScore = 0;
        public static int highScore = 0;
        //Game initial state
        public static CurrentGameState gameState = CurrentGameState.StartMenu;

        //Use this to deisable screen resizing
        public static bool DisableWindowResize = false;

        /*Settings for disable screen resizing*/
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]

        private static extern IntPtr GetConsoleWindow();
        /// <summary>
        /// Gets the current rank.
        /// </summary>
        /// <returns>Returns the Current rank.</returns>
        /// 
        public static Difficulty GetRank()
        {
            //Set difficulty
            return currentRank;
        }
        /// <summary>
        /// Sets the rank depending on the current score.
        /// </summary>
        /// <param name="currentscore">Input current score.</param>
        public static void SetRank(int currentscore)
        {
            //Set difficulty
            var s = currentscore;
            if (s <= lowRank)
            {
                currentRank = Difficulty.Easy;
            }
            else
            if (s > lowRank && s <= mediumRank)
            {
                currentRank = Difficulty.Medium;
            }
            else
            if (s > mediumRank && s <= highRank)
            {
                currentRank = Difficulty.Hard;
            }
            else
            {
                currentRank = Difficulty.Extreme;
            }
        }

        /// <summary>
        /// Checks Console to see if a keyboard key has been pressed, if so returns it as lowercase, otherwise returns '\0'.
        /// </summary>
        static char ReadKeyIfExists() => Console.KeyAvailable ? Console.ReadKey(intercept: true).Key.ToString().ToLower()[0] : '\0';

        /// <summary>
        /// Move the players diretion according to keyboard inputs
        /// </summary>
        /// <param name="key"></param>
        /// <param name="snake"></param>
        static void Move(char key,Player snake)
        {
            switch (key)
            {
                case 'w':
                    snake.CurrentDirection = (snake.CurrentDirection != Direction.Down) ? Direction.Up : Direction.Down;
                    break;
                case 'a':
                    snake.CurrentDirection = (snake.CurrentDirection != Direction.Right) ? Direction.Left : Direction.Right;
                    break;
                case 's':
                    snake.CurrentDirection = (snake.CurrentDirection != Direction.Up) ? Direction.Down : Direction.Up;
                    break;
                case 'd':
                    snake.CurrentDirection = (snake.CurrentDirection != Direction.Left) ? Direction.Right : Direction.Left;
                    break;
                default:
                    break;
            }            
        }

        /// <summary>
        /// Sets the background and foreground color depending on the difficulty.
        /// </summary>
        /// <param name="score">Input current score.</param>
        /// <param name="low">Input low rank score.</param>
        /// <param name="medium">Input medium rank score.</param>
        /// <param name="high">Input high rank score.</param>
        public static void SetColorAndSound()
        {
            //var bgc = Console.BackgroundColor;
            //Console.Clear();
            switch (Program.currentRank)
            {
                case Difficulty.Easy:
                    Console.Beep(2000, 20);
                    Console.Beep(1000, 20);
                    break;
                case Difficulty.Medium:
                    Console.Beep(1500, 20);
                    Console.Beep(750, 20);
                    break;
                case Difficulty.Hard:
                    Console.Beep(1200, 20);
                    Console.Beep(500, 20);
                    break;
                case Difficulty.Extreme:
                    Console.Beep(600, 20);
                    Console.Beep(250, 20);
                    break;
            }
        }
        /// <summary>
        /// Sets the difficulty speed depending on your score and rank.
        /// </summary>
        /// <param name="score"></param>
        /// <param name="low"></param>
        /// <param name="medium"></param>
        /// <param name="high"></param>
        /// <param name="currentrank"></param>
        /// <returns>New framerate speed.</returns>
        public static int SetDifficulty(int score,int currentspeed)
        {
            int low, medium, high,extreme;
            low     = Program.lowRank;
            medium  = Program.mediumRank;
            high    = Program.highRank;
            extreme = (int)(high * 1.4);
            int gameSpeedBuffer = currentspeed;


            switch (Program.currentRank)
            {
                case Difficulty.Easy:
                    if (score <= low-1)
                    {
                        gameSpeedBuffer = 8 + (1 * (int)Math.Round(score * 0.08));
                    }
                    break;
                case Difficulty.Medium:
                    if (score >= medium-1)
                    {
                        gameSpeedBuffer = 10 + (1 * (int)Math.Round(score * 0.08));
                    }
                    break;
                case Difficulty.Hard:
                    if (score >= high-1)
                    {
                        gameSpeedBuffer = 12 + (1 * (int)Math.Round(score * 0.08));
                    }
                    break;
                case Difficulty.Extreme:
                    if (score > extreme)
                    {
                        gameSpeedBuffer = 14 + (1 * (int)Math.Round(score * 0.08));
                    }
                    break;
            }
            //In case something
            return gameSpeedBuffer;
        }
        /// <summary>
        /// Start Menu
        /// </summary>
        static void GameStart()
        {
            #region CREATE OBJECTS AND INIT VALUES

            //Create objects
            GameWorld world = new GameWorld(32, 16, lowRank);          //The World, width,height,gamespeed
            ConsoleRenderer renderer = new ConsoleRenderer(world);       //Create the renderer
            GameStart intro = new GameStart();
            world.gameObjects.Add(intro);

            //Init values
            bool pressedStart = false;
            bool stopSoundtrack = false;
            runGame = true;
            //Set Color
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            //Sound settings
            int playbackRate = 60;
            int playedNotes = 0;
            int notesTotal;
            double[] soundTrack = new double[]
            {
                /*Notes*/
                523.251,    //c5
                783.991,    //g5
                1046.50,    //c6
                987.767,    //b5
                1046.50     //c6
            };
            notesTotal = soundTrack.Length;
            
            #endregion

            while (!pressedStart)
            {
                
                DateTime before = DateTime.Now;

                double playbackTime = Math.Ceiling((1000.0 / playbackRate) - (DateTime.Now - before).TotalMilliseconds);

                //Play the soundtrack once
                if (playbackTime > 0 && !stopSoundtrack)
                {
                    if (playedNotes < notesTotal - 1)
                    {

                        Console.Beep((int)soundTrack[playedNotes], playbackRate * 2);
                        playedNotes++;
                    }
                    else
                    {
                        playedNotes = notesTotal;
                        Console.Beep((int)soundTrack[notesTotal-1], 800);
                        stopSoundtrack = true;
                    }
                    Thread.Sleep((int)playbackTime);
                }

                char key = ReadKeyIfExists();

                if (key == 's')
                {
                    pressedStart = true;
                    Console.Beep((int)523.251, 50);
                    Console.Beep((int)659.255, 50);
                    //gameState = CurrentGameState.InGame;
                }

                renderer.RenderBlank(world);
                world.Update();
            }
            gameState = CurrentGameState.InGame;
        }
        /// <summary>
        /// Game over, shows points and seconds you have played
        /// </summary>
        static bool Gameover()
        {
            #region CREATE OBJECTS AND INIT VALUES
            GameWorld world = new GameWorld(32, 16, lowRank);       //The World, width,height,gamespeed
            ConsoleRenderer renderer = new ConsoleRenderer(world);  //Create the renderer
            GameOver gameover = new GameOver();                     //Create the gameover object
            world.gameObjects.Add(gameover);

            //Init values
            bool choiceDone = false;
            bool choice = false;
            bool stopSoundtrack = false;

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Clear();

            //Sound settings
            int notesTotal;
            int playedNotes = 0;
            double[] soundTrack = new double[]
            {
                /*The Melody*/
                523.251,    //c5
                659.255,    //e5
                783.991,    //g5
                659.255,    //e5

                493.883,    //b4
                587.330,    //d5
                783.991,    //g5
                587.330,    //d5

                440.000,    //a4
                523.251,    //c5
                659.255,    //e5
                493.883,    //b4

                523.251,    //c5
            };
            notesTotal = soundTrack.Length;
            #endregion
            while (!choiceDone)
            {
                int playbackRate = 60;
                DateTime before = DateTime.Now;

                

                double playbackTime = Math.Ceiling((1000.0 / playbackRate) - (DateTime.Now - before).TotalMilliseconds);
                if (playbackTime > 0 && !stopSoundtrack)
                {
                    if (playedNotes < notesTotal-1)
                    {
                        Console.Beep((int)soundTrack[playedNotes], 100);
                        playedNotes++;
                    }
                    else
                    {
                        playedNotes = notesTotal-1;
                        Console.Beep((int)soundTrack[playedNotes], 300);
                        stopSoundtrack = true;
                    }
                    Thread.Sleep((int)playbackTime);
                }
                char key = ReadKeyIfExists();

                if (key == 's')
                {
                    gameState = CurrentGameState.InGame;
                    choice = true;
                    choiceDone = true;
                }
                if (key == 'q')
                {
                    choice = false;
                    choiceDone = true;
                }
                renderer.RenderBlank(world);
                world.Update();
            }
            return choice;
        }
        /// <summary>
        /// Main game loop
        /// </summary>
        static void GameLoop()
        {
            #region INIT
            //Create And initialise the global game objects
            GameWorld world             = new GameWorld(32,16, startSpeed); //The World, width,height,gamespeed      
            Player snake                = world.CreatePlayer();             //Snake
            world.CreateFood(world);                                        //The food
            ConsoleRenderer renderer    = new ConsoleRenderer(world);       //Create the renderer

            //Init global game settings
            SetDifficulty(world.score, world.globalGameSpeed);         //Set Start difficulty (sets game speed)
            bool canResetseconds        = true;
            //int frameRate               = world.globalGameSpeed;
            //int framRateBuffer          = frameRate;          //Not used yet
            //int frameHalfSpeed          = framRateBuffer / 2; //Not used yet
            runGame                     = true;
            Console.ResetColor();
            Console.Clear();
            
            #endregion
            while (runGame == true)
            {
                //Set globalframerate
                int frameRate = world.globalGameSpeed;
                //framRateBuffer = frameRate;             //Not used yet
                //frameHalfSpeed = framRateBuffer / 2;    //not used yet



                // Set the time from the current time
                DateTime before = DateTime.Now;
                //Use the current second
                int currentSeconds = before.Second;
                double newTimer = Math.Sign(currentSeconds) * 0.1;
                world.globalTimer -= newTimer;
                if (canResetseconds)
                {
                    canResetseconds = false;
                }

                // Handle and react to inputs from the user
                char key = ReadKeyIfExists();
                Move(key, snake);

                //Main Update
                renderer.RenderShowStatus();
                renderer.RenderBlank(world);         //Pre draw event
                world.Update();                      //Step event
                if(runGame == false)
                {
                    break;
                }
                renderer.Render();                  //Post draw event

                // Measure time
                double frameTime = Math.Ceiling((1000.0 / frameRate) - (DateTime.Now - before).TotalMilliseconds);

                if (frameTime > 0)
                {
                    // Vänta rätt antal millisekunder innan loopens nästa varv
                    Thread.Sleep((int)frameTime);
                }

                if (world.globalTimer < 1)
                {
                    runGame = false;
                }
                //Using this timer in SetDifficulty Method
                globalTimer = (int)world.globalTimer;
                //Save current score to a global int
                currentScore = world.score;
            }
            gameState = CurrentGameState.GameOver;
        }


        /// <summary>
        /// The Main Program
        /// </summary>
        /// <param name="args"></param>
        static void Main()
        {
            if (DisableWindowResize)
            {
                IntPtr handle = GetConsoleWindow();
                IntPtr sysMenu = GetSystemMenu(handle, false);

                if (handle != IntPtr.Zero)
                {
                    DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                    DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
                }
            }

           bool runGame = true;

           while (runGame)
           {
                switch (gameState)
                {
                    case CurrentGameState.StartMenu:
                        GameStart();
                        break;
                    case CurrentGameState.InGame:
                        GameLoop();
                        break;
                    case CurrentGameState.GameOver:
                        runGame = Gameover();
                        break;
                }
            }
            Console.Clear();
            Console.ResetColor();
            Console.SetCursorPosition((int)(Console.WindowWidth * 0.2), (int)Console.WindowHeight / 2);
            Console.Write("Thank you for playing!");
            Console.ReadKey();
       }
    }
}
