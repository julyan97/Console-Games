using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame2._0
{

    public class SnakeEngine
    {
        //Prop
        private int BoardSize { get; set; }
        private List<Point> SnakeBody { get; set; }
        private int[][] Board { get; set; }
        private int Snake { get; set; }
        private int Wall { get; set; }
        private int Food { get; set; }
        public int Speed { get; set; }
        private int Row { get; set; }
        private bool IsFoodReady { get; set; } = false;
        public int CopySpeed { get; set; }
        private ConsoleKey Key { get; set; } = ConsoleKey.RightArrow;
        private Random rand { get; set; } = new Random();

        //Ctors
        public SnakeEngine()
        {
            var p = new Point(1, 2);
            Row = 2;
            CopySpeed = Speed;
            Snake = 1;
            Food = 2;
            Wall = 3;
            BoardSize = 17;
            Board = new int[BoardSize / Row][].Select(x => new int[BoardSize]).ToArray();
            SnakeBody = new List<Point>() { new Point(1, 1) };
            GenerateWalls();
        }
        public SnakeEngine(int size, int speed) : this()
        {
            Speed = speed;
            CopySpeed = speed;
            BoardSize = size;
            Board = new int[BoardSize / Row][].Select(x => new int[BoardSize]).ToArray();
            SnakeBody = new List<Point>() { new Point(1, 1) };
            GenerateWalls();
        }

        //Functions
        private void GenerateWalls()
        {
            Board[0] = Board[0].Select(x => 3).ToArray();
            Board[BoardSize / Row - 1] = Board[BoardSize / Row - 1].Select(x => 3).ToArray();

            for (int i = 0; i < BoardSize / 2; i++)
            {
                Board[i][0] = 3;
                Board[i][BoardSize - 1] = 3;
            }

        }
        private void DrawBoard()
        {
            for (int i = 0; i < BoardSize / Row; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Board[i][j] == Wall) Console.Write("#");
                    else if (Board[i][j] == Snake)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("o");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (Board[i][j] == Food)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("@");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        private void MovebyKey()
        {

            switch (Key)
            {
                case ConsoleKey.UpArrow:
                    Movement(-1, 0);
                    break;
                case ConsoleKey.DownArrow:
                    Movement(1, 0);
                    break;
                case ConsoleKey.LeftArrow:
                    Movement(0, -1);
                    break;
                case ConsoleKey.RightArrow:
                    Movement(0, 1);
                    break;
                default:
                    break;
            }
        }
        private void Movement(int x, int y)
        {

            if (Board[SnakeBody.Last().X + x][SnakeBody.Last().Y + y] == Food)
            {
                IsFoodReady = false;
            }
            SnakeBody.Add(new Point(SnakeBody.Last().X + x, SnakeBody.Last().Y + y));
            SnakeBody.ForEach(x =>
            {
                Board[x.X][x.Y] = Snake;
            });


            if (!(Board[SnakeBody.Last().X + x][SnakeBody.Last().Y + y] == Food))
            {
                Board[SnakeBody.First().X][SnakeBody.First().Y] = 0;
                SnakeBody.RemoveAt(0);
            }
        }
        private void GenerateFood()
        {
            //if (IsFoodReady == true) return;
            //int i = rand.Next(1, BoardSize / 2 - 2);
            //int j = rand.Next(1, BoardSize - 2);
            //if (Board[i][j] != 0) return;

            //Board[i][j] = Food;
            //IsFoodReady = true;
            //while (true)
            //{
            //    if (IsFoodReady == true) break;
            //    else
            //    {
            //        int i = rand.Next(3, BoardSize / 2 - 3);
            //        int j = rand.Next(3, BoardSize - 3);
            //        if (Board[i][j] == 0)
            //        {
            //            IsFoodReady = true;
            //            Board[i][j] = Food;
            //        }
            //    }
            //}

            int i = rand.Next(1, BoardSize / 2 - 2);
            int j = rand.Next(1, BoardSize - 2);
            if (Board[i][j] != 0) GenerateFood();

            Board[i][j] = Food;
            IsFoodReady = true;

        }
        public void Run()
        {
            //var player = new SoundPlayer();
            //player.SoundLocation = @"ameno.wav";
            //player.Play();
            while (true)
            {
                Console.WriteLine("To PAUSE PRESS ANY KEY OTHER THEN THE ARROWS");
                //Task.Run(() =>
                //{
                //    Key = Console.ReadKey(true).Key;
                //});
                if (Console.KeyAvailable) Key = Console.ReadKey(true).Key;

                try
                {
                    DrawBoard();
                    MovebyKey();
                    if (IsFoodReady != true)
                        GenerateFood();

                    Thread.Sleep(Speed);
                    Console.Clear();
                }
                catch
                {
                    Console.WriteLine("Game over LOOSER");
                    break;
                }
            }


        }

    }
}
