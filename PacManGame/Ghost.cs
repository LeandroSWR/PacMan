﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace PacManGame {
    class Ghost {
        /** \brief Ghost X position */
        private int x;
        /** \brief Ghost Y position */
        private int y;
        /** \brief Knows what animation frame should be shown */
        private int animation;
        /** \brief Timer for the animation */
        private int animationTimer;
        /** \brief Animation speed */
        private int animationSpeed;
        /** \brief Movement speed */
        private int moveSpeed;
        /** \brief Timer for the movement speed */
        private int speedTimer;
        /** \brief Number of this Ghost */
        private int ghostNumber;
        /** \brief Multi uses timer */
        private int timer;
        /** \brief Random chance of changing direction */
        private int chance;
        /** \brief PacMan X when he was last seen */
        private int lastPacX;
        /** \brief PacMan Y when he was last seen */
        private int lastPacY;
        /** \brief Knows if the Ghost is currently vulnerable */
        private bool isVulnerable;
        /** \brief Knows if the Game is being rebooted */
        private bool rebooted;
        /** \brief Dictionari to hold 2 sprite frames */
        private Dictionary<int, string[]> ghosts;
        /** \brief Creates a new "Sprite" "sp" */
        private Sprite sp = new Sprite();
        /** \brief Declares a PacMan */
        private PacMan pacman;
        /** \brief Declares a Random */
        private Random rnd;
        /** \brief Current State */
        private GhostState state;
        /** \brief Last PacMan direction when seen */
        private Direction lastPacDir;
        /** \brief Current direction */
        private Direction direction;
        /** \brief This Ghost color */
        private ConsoleColor color;
        /** \brief True or false if this Ghost is dead */
        public bool IsDead { get; private set; }
        
        /// <summary>
        /// Ghost constructor
        /// </summary>
        /// <param name="number">Ghost number</param>
        /// <param name="x">Ghost starting x</param>
        /// <param name="y">Ghost starting y</param>
        /// <param name="color">Ghost color</param>
        /// <param name="direction">Ghost stating direction</param>
        /// <param name="pacman">PacMan Info</param>
        public Ghost(int number, int x, int y, ConsoleColor color, Direction direction, PacMan pacman) {
            this.x = x;
            this.y = y;
            this.color = color;
            this.pacman = pacman;
            this.direction = direction;

            animation = 0;
            animationTimer = 0;
            animationSpeed = 8;
            ghostNumber = number;
            speedTimer = 0;
            moveSpeed = 2;
            timer = 0;

            isVulnerable = false;
            rebooted = true;
            IsDead = false;

            ghosts = new Dictionary<int, string[]> {
                [0] = sp.gFrame1,
                [1] = sp.gFrame2
            };

            rnd = new Random(ghostNumber ^ DateTime.Now.Millisecond);

            state = GhostState.LeavingSpawn;

            pacman.EatSpecialPoints += Run;
            pacman.Died += Reboot;
        }

        /// <summary>
        /// Draws the Ghost
        /// </summary>
        public void Plot() {
            animationTimer++; // Increasse "animationTimer" by 1
            // Check if the "animationTimer" is equal to the "animationSpeed"
            if (animationTimer == animationSpeed) {
                // If so...
                animation = animation == 0 ? 1 : 0; // Switches the animation frame
                animationTimer = 0; // Resets the animation timer
            }

            // Switch the ForegroundColor
            Console.ForegroundColor = color;
            for (int i = 0; i < 3; i++) { // Loops 3 times to properlly display the wanted sprite
                Console.SetCursorPosition(x, y + i); // Sets the cursor position
                Console.WriteLine(ghosts[animation][i]); // Writes to the console
            }
            // Switch the ForegroundColor to White
            Console.ForegroundColor = ConsoleColor.White;

            // Displays a small animation when the player dies
            if (rebooted) {
                timer++; // Increasse the timer
                Thread.Sleep(125); // Suspends the thread for 125 milliseconds
                if (timer > 3) { // Checks if the timmer is bigger than 3
                    timer = 0; // Resets the timer
                    rebooted = false; // Changes rebooted to false
                }
            }
        }

        /// <summary>
        /// Clears the previous location the the Ghost
        /// </summary>
        public void UnPlot() {
            for (int i = 0; i < 3; i++) { // Loops 3 times to properlly clear the sprite
                Console.SetCursorPosition(x, y + i); // Sets the cursor position
                Console.WriteLine("     "); // Writes to the console
            }
            // Displays a small animation when the player dies
            if (rebooted) { // Checks if rebooted is true
                Thread.Sleep(125); // Suspends the thread for 125 milliseconds
            }
        }

        /// <summary>
        /// Update method
        /// </summary>
        public void Update() {
            
            chance = rnd.Next(1, 100);
            CheckCollision();
            UpdateState();
        }

        /// <summary>
        /// Moves the player each frame
        /// </summary>
        public void Move() {
            speedTimer++;// Increasse speedTimer by 1

            // Check if the speedTimer is equal to moveSpeed
            if (speedTimer >= moveSpeed) {
                // If So...
                speedTimer = 0; // reset speedTimer

                switch (direction) { // Checks the current direction
                    case Direction.Up: // If Up...
                        // Check if we're not colliding with the wall
                        if (!Level.WallCollider[x, y - 1] && !Level.WallCollider[x + 4, y - 1]) {
                            y--; // Decreasse Y by 1
                        } else {
                            direction = Direction.Down; // Change Direction to Down
                        }
                        break;
                    case Direction.Down:// If Down...
                        // Check if we're not colliding with the wall
                        if (!Level.WallCollider[x, y + 3] && !Level.WallCollider[x + 4, y + 3]) {
                            y++;// Increasse Y by 1
                        } else {
                            direction = Direction.Up; // Change Direction to Up
                        }
                        break;
                    case Direction.Left:
                        // Check if we're not colliding with the wall
                        if (!Level.WallCollider[x - 1, y] && !Level.WallCollider[x - 1, y + 2]) {
                            x--; // Decreasse X by 1
                        } else {
                            direction = Direction.Right; // Change Direction to Right
                        }
                        break;
                    case Direction.Right:
                        // Check if we're not colliding with the wall
                        if (!Level.WallCollider[x + 5, y] && !Level.WallCollider[x + 5, y + 2]) {
                            x++; // Increasse X by 1
                        } else {
                            direction = Direction.Left; // Change Direction to Left
                        }
                        break;
                    case Direction.None:
                        Console.Beep(); // Beeps
                        break;
                }
            }
            CheckToroidal();
        }
        
        /// <summary>
        /// Randomizes the Ghosts direction
        /// </summary>
        private void UpdateDirection() {
            // If the direction is Left or Right has a chance to change it to Up or Down
            if (direction == Direction.Left || direction == Direction.Right) {

                if (!Level.WallCollider[x, y - 1] && !Level.WallCollider[x + 4, y - 1]) {

                    if (chance <= 50) {

                        direction = Direction.Up;
                    }

                } else if (!Level.WallCollider[x, y + 3] && !Level.WallCollider[x + 4, y + 3]) {

                    if (chance > 50) {

                        direction = Direction.Down;
                    }
                }
                // If the direction is Up or Down has a chance to change it to Left or Right
            } else if (direction == Direction.Up || direction == Direction.Down) {

                if (!Level.WallCollider[x - 1, y] && !Level.WallCollider[x - 1, y + 2] && !Level.WallCollider[x - 1, y + 1]) {

                    if (chance <= 50) {

                        direction = Direction.Left;
                    }

                } else if (!Level.WallCollider[x + 5, y] && !Level.WallCollider[x + 5, y + 2] && !Level.WallCollider[x + 5, y + 1]) {

                    if (chance > 50) {

                        direction = Direction.Right;
                    }
                }
            }
        }

        /// <summary>
        /// Updates the Ghosts state
        /// </summary>
        private void UpdateState() {

            switch (state) { // Checks the current Stated

                case GhostState.LeavingSpawn:

                    if (IsDead) IsDead = false;
                    LeaveSpawn(); // Calls LeaveSpawn Method

                    break;

                case GhostState.SearchPacMan:
                    Move(); // Calls Move Method
                    UpdateDirection(); // Calls UpdateDirection Method
                    CheckPacMan(); // Calls CheckPacMan Method
                    // If vulnerable switch state to RunFromPacman
                    if (isVulnerable) state = GhostState.RunFromPacman; 
                    break;

                case GhostState.FollowPacMan:
                    Move(); // Calls Move Method
                    Follow(); // Calls Follow Method
                    // If vulnerable switch state to RunFromPacman
                    if (isVulnerable) state = GhostState.RunFromPacman;
                    break;

                case GhostState.RunFromPacman:
                    Move(); // Calls Move Method
                    Run(); // Calls Run Method
                    break;

                case GhostState.ReturnToSpawn:

                    ReturnToSpawn(); // Calls ReturnToSpawn Method
                    break;
            }
        }

        /// <summary>
        /// Returns the Ghost to spawn
        /// </summary>
        private void ReturnToSpawn() {
            if (IsDead) {
                BackToNormal();
                IsDead = false;

                x = 51;
                y = 21;

            } else if (!IsDead) {
                timer++;
                // Animate the Ghosts currently trapped in the spawn to move from side to side
                Move();
                if (timer > 140) {
                    state = GhostState.LeavingSpawn;
                    timer = 0;
                }
            }
        }

        /// <summary>
        /// Checks the location of the player for a possible toroidal movement
        /// </summary>
        private void CheckToroidal() {
            // Check if there's a toroidal movement
            if (x == 1 && y == 21 ||
                x == 101 && y == 21) {
                // If so..
                x = direction == Direction.Right ? 1 : 101; // Teleports to he oposite site
            }
        }

        /// <summary>
        /// Animation that moves the Ghost outside of the spawn
        /// </summary>
        private void LeaveSpawn() {
            timer++;
            if (x > 51 && timer < 40) return;
            if (x < 51 || x > 51) {
                x = x > 51 ? x - 1 : x + 1;
            } else if (y > 17) {
                y--;
            } else {
                state = GhostState.SearchPacMan;
                timer = ghostNumber > 2 ? 0 : timer;
            }
        }

        /// <summary>
        /// Checks if the Ghost can see PacMan
        /// </summary>
        /// <returns></returns>
        private bool CheckPacMan() {
            if (x == pacman.X && y > pacman.Y) {

                for (int i = pacman.Y; i <= y; i++) {

                    if (Level.WallCollider[x, i]) {

                        return false;

                    } else if (!Level.WallCollider[x, i]) {

                        continue;
                    }
                }

                if (!isVulnerable) state = GhostState.FollowPacMan;

                return true;
            }

            if (x == pacman.X && y < pacman.Y) {

                for (int i = y; i <= pacman.Y; i++) {

                    if (Level.WallCollider[x, i]) {

                        return false;

                    } else if (!Level.WallCollider[x, i]) {

                        continue;
                    }
                }

                if (!isVulnerable) state = GhostState.FollowPacMan;

                return true;
            }

            if (x > pacman.X && y == pacman.Y) {

                for (int i = pacman.X; i <= x; i++) {

                    if (Level.WallCollider[i, y]) {

                        return false;

                    } else if (!Level.WallCollider[i, y]) {

                        continue;
                    }
                }

                if (!isVulnerable) state = GhostState.FollowPacMan;

                return true;
            }

            if (x < pacman.X && y == pacman.Y) {

                for (int i = x; i <= pacman.X; i++) {

                    if (Level.WallCollider[i, y]) {

                        return false;

                    } else if (!Level.WallCollider[i, y]) {

                        continue;
                    }
                }

                if (!isVulnerable) state = GhostState.FollowPacMan;

                return true;
            }
            return false;
        }

        /// <summary>
        /// Tries to follow PacMan
        /// </summary>
        private void Follow() {

            if (!CheckPacMan()) {
                if (x == lastPacX && y == lastPacY) {
                    direction = lastPacDir;
                    state = GhostState.SearchPacMan;
                }
            } else {
                lastPacX = pacman.X;
                lastPacY = pacman.Y;

                if (y > pacman.Y) {
                    if (!Level.WallCollider[x, y - 1] && !Level.WallCollider[x + 4, y - 1])
                        direction = Direction.Up;

                } else if (y < pacman.Y) {
                    if (!Level.WallCollider[x, y + 3] && !Level.WallCollider[x + 4, y + 3])
                        direction = Direction.Down;

                } else if (x > pacman.X) {
                    if (!Level.WallCollider[x - 1, y] && !Level.WallCollider[x - 1, y + 2])
                        direction = Direction.Left;

                } else if (x < pacman.X) {
                    if (!Level.WallCollider[x + 5, y] && !Level.WallCollider[x + 5, y + 2])
                        direction = Direction.Right;
                }

                if (pacman.nextDirection != Direction.None) {
                    lastPacDir = pacman.nextDirection;
                } else {
                    lastPacDir = pacman.direction;
                }
            }
        }

        /// <summary>
        /// Runs away from PacMan
        /// </summary>
        private void Run() {
            if (!isVulnerable) {
                isVulnerable = true;
                color = ConsoleColor.DarkBlue;
                moveSpeed = 3;
                return;
            }
            timer++;


            UpdateDirection();

            if (y > pacman.Y && direction == Direction.Up && x == pacman.X) {

                direction = Direction.Down;

            } else if (y < pacman.Y && direction == Direction.Down && x == pacman.X) {

                direction = Direction.Up;

            } else if (x > pacman.X && direction == Direction.Left && y == pacman.Y) {

                direction = Direction.Right;

            } else if (x < pacman.X && direction == Direction.Right && y == pacman.Y) {

                direction = Direction.Left;
            }

            if (timer > 320) {

                timer = 0;
                BackToNormal();
            }
        }

        /// <summary>
        /// Returns the Ghost to normal
        /// </summary>
        private void BackToNormal() {
            
            if (ghostNumber == 1) {

                color = ConsoleColor.Red;

            } else if (ghostNumber == 2) {

                color = ConsoleColor.Green;

            } else if (ghostNumber == 3) {

                color = ConsoleColor.Cyan;

            } else if (ghostNumber == 4) {

                color = ConsoleColor.Magenta;

            }
            
            isVulnerable = false;
            moveSpeed = 2;
            if (!IsDead)
                state = GhostState.SearchPacMan;
        }

        /// <summary>
        /// Check for collisions againts PacMan
        /// </summary>
        private void CheckCollision() {

            if ((x >= pacman.X && x <= pacman.X + 4 && y == pacman.Y) ||
                (x + 4 >= pacman.X && x + 4 <= pacman.X + 4 && y == pacman.Y) ||
                (x == pacman.X && y <= pacman.Y && y >= pacman.Y + 2) ||
                (x == pacman.X && y + 2 >= pacman.Y && y <= pacman.Y + 2)) {
                
                if (isVulnerable) {

                    timer = 0;
                    isVulnerable = false;
                    pacman.Points += 1500;
                    IsDead = true;
                    state = GhostState.ReturnToSpawn;

                } else {

                    pacman.IsDead= true;
                    pacman.Respawn();
                }
            }
        }

        /// <summary>
        /// Reboots the level
        /// </summary>
        public void Reboot() {
            
            switch (ghostNumber) {
                case 1:
                    x = 39;
                    y = 21;
                    break;
                case 2:
                    x = 46;
                    y = 21;
                    break;
                case 3:
                    x = 56;
                    y = 21;
                    break;
                case 4:
                    x = 63;
                    y = 21;
                    break;
            }

            BackToNormal();
            state = GhostState.LeavingSpawn;
            rebooted = true;
        }
    }
}
