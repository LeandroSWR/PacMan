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
            KeyReader kR = new KeyReader();

            while (true) {
                pacMan.Plot();

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

                pacMan.Move();
            }
        }
    }
}
