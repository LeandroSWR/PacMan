using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WorldDrawTest {
    class Game {

        private Level level;
        private PacMan pacMan;
        private Ghost ghost1;
        private Ghost ghost2;
        private Ghost ghost3;
        private Ghost ghost4;
        private KeyReader kR;

        public Game(KeyReader kr) {

            level = new Level();
            pacMan = new PacMan(51, 25, Direction.Right);
            ghost1 = new Ghost(1, 39, 21, ConsoleColor.Red, Direction.Right, pacMan);
            ghost2 = new Ghost(2, 46, 21, ConsoleColor.Green, Direction.Left, pacMan);
            ghost3 = new Ghost(3, 56, 21, ConsoleColor.Cyan, Direction.Right, pacMan);
            ghost4 = new Ghost(4, 63, 21, ConsoleColor.Magenta, Direction.Left, pacMan);
            kR = kr;

            GameLoop();
        }

        private void GameLoop() {
            while (pacMan.Health != 0) {

                pacMan.Plot();
                level.RenderPoints();
                ghost1.Plot();
                ghost2.Plot();
                ghost3.Plot();
                ghost4.Plot();

                level.RenderUi(pacMan.Points, pacMan.Health, pacMan.NLevel);

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

                pacMan.Update();
                ghost1.Update();
                ghost2.Update();
                ghost3.Update();
                ghost4.Update();
            }

            ResetLevel();
        }

        private void ResetLevel() {

            level = new Level();
            pacMan = new PacMan(51, 25, Direction.Right);
            ghost1 = new Ghost(1, 39, 21, ConsoleColor.Red, Direction.Right, pacMan);
            ghost2 = new Ghost(2, 46, 21, ConsoleColor.Green, Direction.Left, pacMan);
            ghost3 = new Ghost(3, 56, 21, ConsoleColor.Cyan, Direction.Right, pacMan);
            ghost4 = new Ghost(4, 63, 21, ConsoleColor.Magenta, Direction.Left, pacMan);

            GameLoop();
        }
    }
}
