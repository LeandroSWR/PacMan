using System;
using System.Collections.Generic;

namespace PacManGame {
    /// <summary>
    /// This Class is responsible for everything related to PacMan
    /// </summary>
    class PacMan {
        /** \brief Knows what animation frame should be shown */
        private int animation;
        /** \brief Timer for the movement speed */
        private int speedTimer;
        /** \brief Number of points left in the map */
        private int totalPoints;
        /** \brief Timer for the animation */
        private int animationTimer;
        /** \brief Current direction */
        public Direction direction;
        /** \brief Next direction */
        public Direction nextDirection;
        /** \brief Movement speed */
        private readonly int moveSpeed;
        /** \brief Animation speed */
        private readonly int animationSpeed;
        /** \brief True or false if PacMan is dead */
        public bool IsDead { get; set; }
        /** \brief Number of points PacMan has */
        public int Points { get; set; }
        /** \brief PacMan X position */
        public int X { get; private set; }
        /** \brief PacMan Y position */
        public int Y { get; private set; }
        /** \brief Number of lives PacMan has */
        public int Health { get; private set; }
        /** \brief Current level we're in */
        public int NLevel { get; private set; }
        /** \brief Dictionari to hold 2 sprite frames */
        private Dictionary<int, string[]> pac;
        /** \brief Creates a new "Sprite" "sp" */
        private Sprite sp = new Sprite();
        /** \brief Declares a new Action */
        public event Action EatSpecialPoints;
        /** \brief Declares a new Action */
        public event Action Died;

        /// <summary>
        /// PacMan constructor
        /// </summary>
        /// <param name="X">PacMan starting X</param>
        /// <param name="Y">PacMan starting Y</param>
        /// <param name="direction">PacMan starting Direction</param>
        public PacMan(int X, int Y, Direction direction) {
            this.X = X; // Sets my X equal to the recieved X
            this.Y = Y; // Sets my Y equal to the recieved Y
            // Adds 2 sprites to the Dictionary
            pac = new Dictionary<int, string[]> {
                [0] = sp.rFrame1,
                [1] = sp.rFrame2
            };
            animation = 0; // Set "animation" to 0
            animationTimer = 0; // Set "animation" to 0
            animationSpeed = 4; // Set "animation" to 0 
            this.direction = direction; // Set my "direction" equal to the recieved direction

            speedTimer = 0; // Set "speedTimer" to 0
            moveSpeed = 2; // Set "moveSpeed" to 2

            Health = 3; // Set "Health" to 3

            totalPoints = 207; // Set "totalPoints" to 207

            NLevel = 1; // Set "NLevel" to 1
        }

        /// <summary>
        /// Draws PacMan
        /// </summary>
        public void Plot() {
            animationTimer++; // Increasse "animationTimer" by 1
            // Check if the "animationTimer" is equal to the "animationSpeed"
            if (animationTimer == animationSpeed) { 
                // If so...
                animation = animation == 0 ? 1 : 0; // Switches the animation frame
                animationTimer = 0; // Resets the animation timer
            }

            // Switch ForegroundColor to DarkYellow
            Console.ForegroundColor = ConsoleColor.DarkYellow; 
            for (int i = 0; i < 3; i++) { // Loops 3 times to properlly display the wanted sprite
                Console.SetCursorPosition(X, Y + i); // Sets the cursor position
                Console.WriteLine(pac[animation][i]); // Writes to the console
            }
        }

        /// <summary>
        /// Clears the previous location of PacMan
        /// </summary>
        public void UnPlot() {
            for (int i = 0; i < 3; i++) { // Loops 3 times to properlly clear the sprite
                Console.SetCursorPosition(X, Y + i); // Sets the cursor position
                Console.WriteLine("     "); // Writes to the console
            }
        }

        /// <summary>
        /// Check's if theres a movement available to where the player wants to move
        /// </summary>
        private void FixDirection() {
            switch (nextDirection) { // Checks the next wanted direction
                case Direction.Up: // If Up...
                    // Checks if there's no wall on top
                    if (!Level.WallCollider[X, Y - 1] && !Level.WallCollider[X + 4, Y - 1]) {
                        direction = nextDirection; // Switches the direction
                        nextDirection = Direction.None; // Set the nextDirection to None
                    }
                    break;
                case Direction.Down: // If Down...
                    // Checks if there's no wall bellow
                    if (!Level.WallCollider[X, Y + 3] && !Level.WallCollider[X + 4, Y + 3]) {
                        direction = nextDirection; // Switches the direction
                        nextDirection = Direction.None; // Set the nextDirection to None
                    }
                    break;
                case Direction.Left: // If Left...
                    // Checks if there's no wall to the left
                    if (!Level.WallCollider[X - 1, Y] && !Level.WallCollider[X - 1, Y + 2]) {
                        direction = nextDirection; // Switches the direction
                        nextDirection = Direction.None; // Set the nextDirection to None
                    }
                    break;
                case Direction.Right: // If Right...
                    // Checks if there's no wall to the right
                    if (!Level.WallCollider[X + 5, Y] && !Level.WallCollider[X + 5, Y + 2]) {
                        direction = nextDirection; // Switches the direction
                        nextDirection = Direction.None; // Set the nextDirection to None
                    }
                    break;
            }
        }

        /// <summary>
        /// Update method
        /// </summary>
        public void Update() {
            CheckPointsCollision(); // Calls the CheckPointsCollision method
            Move(); // Calls the Move method
        }

        /// <summary>
        /// Checks if there's a collision with any points
        /// </summary>
        private void CheckPointsCollision() {
            // Loops trought the PointsCollider to check if we collided with one
            for (int i = 0; i < Level.y; i++) {
                for (int u = 0; u < Level.x; u++) {
                    if (Level.PointsCollider[u, i] != default(char)) {
                        if (((X + 1 == u || X + 3 == u) && Y + 1 == i) ||
                            ((Y == i || Y + 2 == i) && X + 2 == u)) {
                            // Check if the point we collided with is a special point
                            if (Level.PointsCollider[u, i] == '█') {
                                EatSpecialPoints(); // Executes the EatSpecialPoints Event
                            }
                            Points += 10; // Increases the score
                            totalPoints--; // Reduces the number of points left
                            // Removes the "ate" point from the array
                            Level.PointsCollider[u, i] = default(char);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Moves the player each frame
        /// </summary>
        private void Move() {
            speedTimer++; // Increasse speedTimer by 1
            FixDirection(); // Call the FixDirection method
            CheckToroidal(); // Call the CheckToroidal method

            // Check if the speedTimer is equal to moveSpeed
            if (speedTimer == moveSpeed) {
                // If So...
                speedTimer = 0; // reset speedTimer

                switch (direction) { // Checks the current direction
                    case Direction.Up: // If Up...
                        // Check if we're not colliding with the wall
                        if (!Level.WallCollider[X, Y - 1] && !Level.WallCollider[X + 4, Y - 1]) {
                            // If so...
                            Y--; // Decreasse Y by 1
                            // Switch the sprite on the dictionary to the moving Up sprite
                            pac[0] = sp.uFrame1; 
                            pac[1] = sp.uFrame2;
                        } else
                            // Else...
                            direction = Direction.None; // Change Direction to None
                        break;
                    case Direction.Down: // If Down...
                        // Check if we're not colliding with the wall
                        if (!Level.WallCollider[X, Y + 3] && !Level.WallCollider[X + 4, Y + 3]) {
                            Y++; // Increasse Y by 1
                            // Switch the sprite on the dictionary to the moving Down sprite
                            pac[0] = sp.dFrame1;
                            pac[1] = sp.dFrame2;
                        } else
                            // Else...
                            direction = Direction.None; // Change Direction to None
                        break;
                    case Direction.Left: // If Left...
                        // Check if we're not colliding with the wall
                        if (!Level.WallCollider[X - 1, Y] && !Level.WallCollider[X - 1, Y + 2]) {
                            X--; // Decreasse X by 1
                            // Switch the sprite on the dictionary to the moving Left sprite
                            pac[0] = sp.lFrame1;
                            pac[1] = sp.lFrame2;
                        } else
                            // Else...
                            direction = Direction.None; // Change Direction to None
                        break;
                    case Direction.Right: // If Right...
                        // Check if we're not colliding with the wall
                        if (!Level.WallCollider[X + 5, Y] && !Level.WallCollider[X + 5, Y + 2]) {
                            X++; // Increasse X by 1
                            // Switch the sprite on the dictionary to the moving Right sprite
                            pac[0] = sp.rFrame1;
                            pac[1] = sp.rFrame2;
                        } else
                            // Else...
                            direction = Direction.None; // Change Direction to None
                        break;
                }
            }
        }

        /// <summary>
        /// Checks the location of the player for a possible toroidal movement
        /// </summary>
        private void CheckToroidal() {
            // Check if there's a toroidal movement
            if (X == 1 && Y == 21 ||
                X == 101 && Y == 21) {
                // If so..
                X = direction == Direction.Right ? 1 : 101; // Teleports to he oposite site
            }
        }

        /// <summary>
        /// Respawns the Player
        /// </summary>
        public void Respawn() {
            // Check if isDead is true
            if (IsDead) {
                IsDead = false; // Change isDead to false
                Health--; // Decreasses the number of lives
                X = 51; // Resets the X position
                Y = 25; // Resets the Y position
                direction = Direction.Right; // Resets the direction
                nextDirection = Direction.None;  // Resets the nextDirection
                Died(); // Executes the Died Event
            } else {
                X = 51; // Resets the X position
                Y = 25; // Resets the Y position
                direction = Direction.Right; // Resets the direction
                nextDirection = Direction.None;  // Resets the nextDirection
            }
        }

        /// <summary>
        /// Checks for win condition
        /// </summary>
        /// <returns>Returns true of False</returns>
        public bool WinCondition() {
            if (totalPoints == 0) { // Checks if pacMan as ate all the points
                // If so...
                NLevel++; // Increasse the level
                Health = 3; // Reset the number of Lives
                Points += 10000; // Add 10000 points to score
                totalPoints = 207; // Resets the total number of points on the map
                Respawn(); // Respawns PacMan

                return true; // Returns true
            }
            return false; // Returns False
        }
    }
}
