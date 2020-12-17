using System;
using System.Linq;

namespace SnakeGame2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            SnakeEngine eng = new SnakeEngine(30, 50);
            eng.Run();
        }
    }
}
