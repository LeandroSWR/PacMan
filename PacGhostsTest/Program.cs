using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacGhostsTest {
    class Program {
        static void Main(string[] args) {
            //Thread animations = new Thread();
            Console.CursorVisible = false;
            Console.Clear();
            Ghost ghostGreen = new Ghost(10, 10, ConsoleColor.Green, Direction.Right);
            Ghost ghostRed = new Ghost(10, 15, ConsoleColor.Red, Direction.Right);
            Ghost ghostBlue = new Ghost(10, 20, ConsoleColor.Cyan, Direction.Right);
            Ghost ghostPink = new Ghost(10, 25, ConsoleColor.Magenta, Direction.Right);
            


            while (true) {
                ghostGreen.Plot();
                ghostRed.Plot();
                ghostBlue.Plot();
                ghostPink.Plot();

                Thread.Sleep(30);

                ghostGreen.UnPlot();
                ghostRed.UnPlot();
                ghostBlue.UnPlot();
                ghostPink.UnPlot();

                ghostGreen.Move();
                ghostRed.Move();
                ghostBlue.Move();
                ghostPink.Move();
            }

            //Console.CursorVisible = true;
        }
    }
}
