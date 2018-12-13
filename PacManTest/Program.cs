using System;
using System.Threading;

namespace PacManTest {
    class Program {
        static void Main(string[] args) {
            Console.CursorVisible = false;
            Console.Clear();
            PacMan pacMan = new PacMan(10, 10, Direction.Right);
            ConsoleKeyInfo key = default(ConsoleKeyInfo);


            while (true) {
                pacMan.Plot();

                Thread.Sleep(30);

                pacMan.UnPlot();

                switch (key.Key) {
                    case ConsoleKey.A:
                        pacMan.direction = Direction.Left;
                        break;
                    case ConsoleKey.D:
                        pacMan.direction = Direction.Right;
                        break;
                    default:
                        break;
                }

                pacMan.Move();
            }
        }
    }
}
