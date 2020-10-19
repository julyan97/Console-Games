using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TronGame
{
    public class TronEngine
    {
        //PROP
        private int BoardSize { get; set; }
        private List<Point> Player1 { get; set; }
        private List<Point> Player2 { get; set; }
        private ConsoleKey Key1 { get; set; } = ConsoleKey.A;
        private ConsoleKey Key2 { get; set; } = ConsoleKey.RightArrow;
        private int Player1Symb { get; set; }
        private int Player2Symb { get; set; }
        private int Wall { get; set; }
        private int Row { get; set; }
        public int Speed { get; set; } = 50;
        private int[][] Board { get; set; }
        public bool IsGameOver { get; set; } = false;


        //CTORS
        public TronEngine()
        {
            Row = 2;
            BoardSize = 17;
            Board = new int[BoardSize / Row][].Select(x => new int[BoardSize]).ToArray();
            Player1 = new List<Point>() { new Point(1, 1) };
            Player2 = new List<Point>() { new Point(1, BoardSize - 2) };
            Player1Symb = 1;
            Player2Symb = 2;
            Wall = 3;
            GenerateWalls();
        }
        public TronEngine(int size) : this()
        {

            BoardSize = size;
            Board = new int[BoardSize / Row][].Select(x => new int[BoardSize]).ToArray();
            Player1 = new List<Point>() { new Point(1, 1) };
            Player2 = new List<Point>() { new Point(1, BoardSize - 2) };
            GenerateWalls();
        }

        //FUNC
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
        private void PrintBoard()
        {
            for (int i = 0; i < BoardSize / Row; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (Board[i][j] == Wall)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("#");
                    }
                    else if (Board[i][j] == Player1Symb)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("o");
                    }
                    else if (Board[i][j] == Player2Symb)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("o");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
        private void MovebyKey()
        {

            switch (Key2)
            {
                case ConsoleKey.UpArrow:
                    Movement(-1, 0, Player1Symb);
                    break;
                case ConsoleKey.DownArrow:
                    Movement(1, 0, Player1Symb);
                    break;
                case ConsoleKey.LeftArrow:
                    Movement(0, -1, Player1Symb);
                    break;
                case ConsoleKey.RightArrow:
                    Movement(0, 1, Player1Symb);
                    break;
                default:
                    break;
            }

            switch (Key1)
            {
                case ConsoleKey.W:
                    Movement(-1, 0, Player2Symb);
                    break;
                case ConsoleKey.S:
                    Movement(1, 0, Player2Symb);
                    break;
                case ConsoleKey.A:
                    Movement(0, -1, Player2Symb);
                    break;
                case ConsoleKey.D:
                    Movement(0, 1, Player2Symb);
                    break;
                default:
                    break;
            }
        }
        
        private void Movement(int x, int y, int player)
        {

            if (player == Player1Symb)
            {
                Player1.Add(new Point(Player1.Last().X + x, Player1.Last().Y + y));
                Player1.ForEach(x =>
                {
                    Board[x.X][x.Y] = Player1Symb;
                });
                if (Board[Player1.Last().X + x][Player1.Last().Y + y] != 0)
                {
                    IsGameOver = true;
                }
            }
            if (player == Player2Symb)
            {
                Player2.Add(new Point(Player2.Last().X + x, Player2.Last().Y + y));
                Player2.ForEach(x =>
                {
                    Board[x.X][x.Y] = Player2Symb;
                });
                if (Board[Player2.Last().X + x][Player2.Last().Y + y] != 0)
                {
                    IsGameOver = true;
                }
            }
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


                try
                {
                    if (Console.KeyAvailable)
                    {
                        var tempKey = Console.ReadKey(true).Key;
                        if (tempKey == ConsoleKey.W || tempKey == ConsoleKey.A || tempKey == ConsoleKey.S || tempKey == ConsoleKey.D)
                        {
                            Key1 = tempKey;
                        }
                        if (tempKey == ConsoleKey.UpArrow || tempKey == ConsoleKey.LeftArrow || tempKey == ConsoleKey.DownArrow || tempKey == ConsoleKey.RightArrow)
                        {
                            Key2 = tempKey;
                        }


                    }
                    PrintBoard();
                    MovebyKey();
                    if (IsGameOver) break;
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
