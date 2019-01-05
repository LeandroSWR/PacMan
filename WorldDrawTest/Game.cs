using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WorldDrawTest {
    class Game {

        public Game() {

            Level level = new Level();
            PacMan pacMan = new PacMan(51, 25, Direction.Right);
            Ghost ghost1 = new Ghost(1, 39, 21, ConsoleColor.Red, Direction.Right, pacMan);
            Ghost ghost2 = new Ghost(2, 46, 21, ConsoleColor.Green, Direction.Left, pacMan);
            Ghost ghost3 = new Ghost(3, 56, 21, ConsoleColor.Cyan, Direction.Right, pacMan);
            Ghost ghost4 = new Ghost(4, 63, 21, ConsoleColor.Magenta, Direction.Left, pacMan);
            KeyReader kR = new KeyReader();

            while (pacMan.Health != 0) {

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
                        if (pacMan.direction == Direction.None || pacMan.direction == Direction.Down)
                            pacMan.direction = Direction.Up;
                        pacMan.nextDirection = Direction.Up;
                        break;
                    case "Right":
                        if (pacMan.direction == Direction.None || pacMan.direction == Direction.Left)
                            pacMan.direction = Direction.Right;
                        pacMan.nextDirection = Direction.Right;
                        break;
                    case "Left":
                        if (pacMan.direction == Direction.None || pacMan.direction == Direction.Right)
                            pacMan.direction = Direction.Left;
                        pacMan.nextDirection = Direction.Left;
                        break;
                    case "Down":
                        if (pacMan.direction == Direction.None || pacMan.direction == Direction.Up)
                            pacMan.direction = Direction.Down;
                        pacMan.nextDirection = Direction.Down;
                        break;
                }

                Thread.Sleep(20);

                pacMan.UnPlot();
                ghost1.UnPlot();
                ghost2.UnPlot();
                ghost3.UnPlot();
                ghost4.UnPlot();

                pacMan.Move();
                ghost1.Update();
                ghost2.Update();
                ghost3.Update();
                ghost4.Update();
            }
        }
    }
}
