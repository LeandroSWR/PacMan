using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WorldDrawTest {
    class Program {
        static void Main(string[] args) {
            Console.CursorVisible = false;
            Console.SetWindowSize(136, 60);
            Level level = new Level();
            PacMan pacMan = new PacMan(51, 25, Direction.Right);
            Ghost ghost1 = new Ghost(1, 39, 21, ConsoleColor.Red, Direction.Right);
            Ghost ghost2 = new Ghost(2, 46, 21, ConsoleColor.Green, Direction.Right);
            Ghost ghost3 = new Ghost(3, 56, 21, ConsoleColor.Cyan, Direction.Left);
            Ghost ghost4 = new Ghost(4, 63, 21, ConsoleColor.Magenta, Direction.Left);
            KeyReader kR = new KeyReader();

            while (true) {

                pacMan.Plot();
                level.RenderPoints();
                ghost1.Plot();
                ghost2.Plot();
                ghost3.Plot();
                ghost4.Plot();
                pacMan.CheckPointsCollision();

                level.RenderScore();

                switch (kR.Input) {
                    case "Up":
                        if (pacMan.direction == null || pacMan.direction == Direction.Down)
                            pacMan.direction = Direction.Up;
                        pacMan.nextDirection = Direction.Up;
                        break;
                    case "Right":
                        if (pacMan.direction == null || pacMan.direction == Direction.Left)
                            pacMan.direction = Direction.Right;
                        pacMan.nextDirection = Direction.Right;
                        break;
                    case "Left":
                        if (pacMan.direction == null || pacMan.direction == Direction.Right)
                            pacMan.direction = Direction.Left;
                        pacMan.nextDirection = Direction.Left;
                        break;
                    case "Down":
                        if (pacMan.direction == null || pacMan.direction == Direction.Up)
                            pacMan.direction = Direction.Down;
                        pacMan.nextDirection = Direction.Down;
                        break;
                }

                Thread.Sleep(30);

                pacMan.UnPlot();
                ghost1.UnPlot();
                ghost2.UnPlot();
                ghost3.UnPlot();
                ghost4.UnPlot();

                pacMan.Move();
                ghost1.Move();
                ghost2.Move();
                ghost3.Move();
                ghost4.Move();
            }
        }
    }
}
