using System;

namespace TronGame
{
    class Program
    {
        static void Main(string[] args)
        {
            TronEngine eng = new TronEngine(50);
            eng.Run();
        }
    }
}
