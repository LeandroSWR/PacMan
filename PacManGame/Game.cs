using System;
using System.Threading;

namespace PacManGame {
    /// <summary>
    /// This Class is responsible for looping the game, making it play
    /// </summary>
    class Game {
        /** \brief Declares a new Level */
        private Level level;
        /** \brief Declares a new PacMan */
        private PacMan pacMan;
        /** \brief Declares a new Ghost */
        private Ghost ghost1;
        /** \brief Declares a new Ghost */
        private Ghost ghost2;
        /** \brief Declares a new Ghost */
        private Ghost ghost3;
        /** \brief Declares a new Ghost */
        private Ghost ghost4;
        /** \brief Declares a new KeyReader */
        private KeyReader kR;

        /// <summary>
        /// Public Game constructor
        /// </summary>
        /// <param name="kr">Key Reader</param>
        public Game(KeyReader kr) {
            // Inicializes the variable level
            level = new Level(); 
            // Inicializes pacMan passing in the given attributes
            pacMan = new PacMan(51, 25, Direction.Right);
            // Inicializes ghost1 using in the given attributes
            ghost1 = new Ghost(1, 39, 21, ConsoleColor.Red, Direction.Right, pacMan);
            // Inicializes ghost2 using in the given attributes
            ghost2 = new Ghost(2, 46, 21, ConsoleColor.Green, Direction.Left, pacMan);
            // Inicializes ghost3 using in the given attributes
            ghost3 = new Ghost(3, 56, 21, ConsoleColor.Cyan, Direction.Right, pacMan);
            // Inicializes ghost4 using in the given attributes
            ghost4 = new Ghost(4, 63, 21, ConsoleColor.Magenta, Direction.Left, pacMan);
            // Sets kR to be equal to the recieved as an argument
            kR = kr;

            // Calls the GameLoop method
            GameLoop();
        }

        /// <summary>
        /// Loops the game
        /// </summary>
        private void GameLoop() {
            while (pacMan.Health != 0) { // Runs the loop untill PacMan is out of lives

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
                ghost4.UnPlot(); // "UnPlot" Clear the fourth Ghost from the game

                pacMan.Update(); // Calls the Update method in PackMan
                ghost1.Update(); // Calls the Update method in Ghost
                ghost2.Update(); // Calls the Update method in Ghost
                ghost3.Update(); // Calls the Update method in Ghost
                ghost4.Update(); // Calls the Update method in Ghost


                if (pacMan.WinCondition()) { // If PacMan wins...

                    // ...reset Ghosts' position...
                    ghost1.Reboot();
                    ghost2.Reboot();
                    ghost3.Reboot();
                    ghost4.Reboot();

                    // ...and reset PacMan position...
                    pacMan.Respawn();

                    // ...and reset eatable points
                    level.GetCollider();
                }
            }
            Console.Clear(); // Clears the console
        }

        /// <summary>
        /// Reads the input from the user to attribute a direction to PacMan.
        /// </summary>
        private void ReadInput() {
            switch (kR.Input) { // Reads the input
                case "Up": // If it's "Up" ask if pacMan's direcction is None or Down
                    if (pacMan.direction == Direction.None || pacMan.direction == Direction.Down)
                        pacMan.direction = Direction.Up; // If so switch it to up
                    pacMan.nextDirection = Direction.Up; // Switch nextDirection to Up
                    break;
                case "Right": // If it's "Right" ask if pacMan's direcction is None or Left
                    if (pacMan.direction == Direction.None || pacMan.direction == Direction.Left)
                        pacMan.direction = Direction.Right; // If so switch it to Right
                    pacMan.nextDirection = Direction.Right; // Switch nextDirection to Right
                    break;
                case "Left": // If it's "Left" ask if pacMan's direcction is None or Right
                    if (pacMan.direction == Direction.None || pacMan.direction == Direction.Right)
                        pacMan.direction = Direction.Left; // If so switch it to Left
                    pacMan.nextDirection = Direction.Left; // Switch nextDirection to Left
                    break;
                case "Down": // If it's "Down" ask if pacMan's direcction is None or Up
                    if (pacMan.direction == Direction.None || pacMan.direction == Direction.Up)
                        pacMan.direction = Direction.Down; // If so switch it to Left
                    pacMan.nextDirection = Direction.Down; // Switch nextDirection to Down
                    break;
            }
        }
    }
}
