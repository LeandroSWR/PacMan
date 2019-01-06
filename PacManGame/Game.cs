using System;
using System.Threading;

namespace PacManGame {
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
            while (pacMan.Health != 0) { // Runes the loop untill PacMan is out of lives

                pacMan.Plot(); // "Plot" Draws PacMan into the game
                ghost1.Plot(); // "Plot" Draws the first Ghost into the game
                ghost2.Plot(); // "Plot" Draws the second Ghost into the game
                ghost3.Plot(); // "Plot" Draws the third Ghost into the game
                ghost4.Plot(); // "Plot" Draws the fourt Ghost into the game

                // Calls the Update method in Level
                level.Update(pacMan.Points, pacMan.Health, pacMan.NLevel);

                // Reads the input from the user to attribute a direction to PacMan
                ReadInput();

                Thread.Sleep(25); // Suspends the thread for 25 milliseconds

                pacMan.UnPlot(); // "UnPlot" Clear PacMan from the game
                ghost1.UnPlot(); // "UnPlot" Clear the first Ghost from the game
                ghost2.UnPlot(); // "UnPlot" Clear the second Ghost from the game
                ghost3.UnPlot(); // "UnPlot" Clear the third Ghost from the game
                ghost4.UnPlot(); // "UnPlot" Clear the fourt Ghost from the game

                pacMan.Update(); // Calls the Update method in PackMan
                ghost1.Update(); // Calls the Update method in Ghost
                ghost2.Update(); // Calls the Update method in Ghost
                ghost3.Update(); // Calls the Update method in Ghost
                ghost4.Update(); // Calls the Update method in Ghost
            }
        }

        private void ReadInput() {
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
        }
    }
}
