using System;
using System.Collections.Generic;

namespace WorldDrawTest {
    class PacMan {
        // Ascii ▄ ▀ █

        public int X { get; private set; }
        public int Y { get; private set; }
        private Dictionary<int, string[]> ghosts;
        private int animation;
        private int animationTimer;
        private int speedTimer;
        public Direction direction;
        public Direction nextDirection;
        private readonly int animationSpeed;
        private readonly int moveSpeed;
        private int setTime = DateTime.Now.Second;
        private int timer;
        public bool CanEatGhosts { get; private set; }

        public static int Points { get; set; }

        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        private readonly string[] rFrame1 = new string[3] {
            "▄██@▄",
            "█████",
            "▀███▀"
        };
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        private readonly string[] rFrame2 = new string[3] {
            "▄█@█▀",
            "███  ",
            "▀███▄"
        };
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        private readonly string[] lFrame1 = new string[3] {
            "▄@██▄",
            "█████",
            "▀███▀"
        };
        private readonly string[] lFrame2 = new string[3] {
            "▀█@█▄",
            "  ███",
            "▄███▀"
        };
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        private readonly string[] uFrame1 = new string[3] {
            "@███▄",
            "█████",
            "▀███▀"
        };
        private readonly string[] uFrame2 = new string[3] {
            "▄   ▄",
            "@█▄██",
            "▀███▀"
        };
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        private readonly string[] dFrame1 = new string[3] {
            "▄███▄",
            "█████",
            "@███▀"
        };
        private readonly string[] dFrame2 = new string[3] {
            "▄███▄",
            "@█▀██",
            "▀   ▀"
        };



        public PacMan(int X, int Y, Direction direction) {
            this.X = X;
            this.Y = Y;
            ghosts = new Dictionary<int, string[]> {
                [0] = rFrame1,
                [1] = rFrame2
            };
            animation = 0;
            animationTimer = 0;
            animationSpeed = 4;
            this.direction = direction;

            speedTimer = 0;
            moveSpeed = 2;

            timer = setTime;

            CanEatGhosts = false;
        }

        public void Plot() {
            animationTimer++;
            if (animationTimer == animationSpeed) {
                animation = animation == 0 ? 1 : 0;
                animationTimer = 0;
            }
            
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < 3; i++) {
                Console.SetCursorPosition(X, Y + i);
                Console.WriteLine(ghosts[animation][i]);
            }
        }

        public void UnPlot() {
            for (int i = 0; i < 3; i++) {
                Console.SetCursorPosition(X, Y + i);
                Console.WriteLine("     ");
            }
        }

        /// <summary>
        /// Check's if theres a movement available to where the player wants to move
        /// </summary>
        private void FixDirection() {
            switch (nextDirection) {
                case Direction.Up:
                    if (!Level.WallCollider[X, Y - 1] && !Level.WallCollider[X + 4, Y - 1]) {
                        direction = nextDirection;
                        nextDirection = Direction.None;
                    }
                    break;
                case Direction.Down:
                    if (!Level.WallCollider[X, Y + 3] && !Level.WallCollider[X + 4, Y + 3]) {
                        direction = nextDirection;
                        nextDirection = Direction.None;
                    }
                    break;
                case Direction.Left:
                    if (!Level.WallCollider[X - 1, Y] && !Level.WallCollider[X - 1, Y + 2]) {
                        direction = nextDirection;
                        nextDirection = Direction.None;
                    }
                    break;
                case Direction.Right:
                    if (!Level.WallCollider[X + 5, Y] && !Level.WallCollider[X + 5, Y + 2]) {
                        direction = nextDirection;
                        nextDirection = Direction.None;
                    }
                    break;
            }
        }


        public void CheckPointsCollision() {
            for (int i = 0; i < Level.y; i++) {
                for (int u = 0; u < Level.x; u++) {
                    if (Level.PointsCollider[u, i] != default(char)) {
                        if (((X + 1 == u || X + 3 == u) && Y + 1 == i) ||
                            ((Y == i || Y + 2 == i) && X + 2 == u)) {
                            if (Level.PointsCollider[u, i] == '█') {

                                CanEatGhosts = true;
                            }
                            Points += 10;
                            Level.PointsCollider[u, i] = default(char);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Moves the player each frame
        /// </summary>
        public void Move() {
            speedTimer++;
            FixDirection();
            CheckToroidal();
            UpdateGhostsState();

            if (speedTimer == moveSpeed) {
                speedTimer = 0;

                switch (direction) {
                    case Direction.Up:
                        if (!Level.WallCollider[X, Y - 1] && !Level.WallCollider[X + 4, Y - 1]) {
                            Y--;
                            ghosts[0] = uFrame1;
                            ghosts[1] = uFrame2;
                        } else
                            direction = Direction.None;
                        break;
                    case Direction.Down:
                        if (!Level.WallCollider[X, Y + 3] && !Level.WallCollider[X + 4, Y + 3]) {
                            Y++;
                            ghosts[0] = dFrame1;
                            ghosts[1] = dFrame2;
                        } else
                            direction = Direction.None;
                        break;
                    case Direction.Left:
                        if (!Level.WallCollider[X - 1, Y] && !Level.WallCollider[X - 1, Y + 2]) {
                            X--;
                            ghosts[0] = lFrame1;
                            ghosts[1] = lFrame2;
                        } else
                            direction = Direction.None;
                        break;
                    case Direction.Right:
                        if (!Level.WallCollider[X + 5, Y] && !Level.WallCollider[X + 5, Y + 2]) {
                            X++;
                            ghosts[0] = rFrame1;
                            ghosts[1] = rFrame2;
                        } else
                            direction = Direction.None;
                        break;
                }
            }
        }

        private void CheckToroidal() {
            if (X == 1 && Y == 21 ||
                X == 101 && Y == 21) {
                X = direction == Direction.Right ? 1 : 101;
            }
        }

        private void UpdateGhostsState() {

            if (CanEatGhosts) {

                timer++;

                if (timer.Equals(setTime + 120)) {

                    CanEatGhosts = false;
                }

            } else {

                timer = 0;
            }
        }
    }
}
