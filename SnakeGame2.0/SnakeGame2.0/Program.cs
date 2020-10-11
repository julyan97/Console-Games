using System;

namespace SnakeGame2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            SnakeEngine eng = new SnakeEngine(50, 50);
            eng.Run();
        }
    }
}
