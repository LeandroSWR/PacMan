using System;
using System.Collections.Generic;

namespace PacManGame {
    class PacMan {

        public int X { get; private set; }
        public int Y { get; private set; }
        private Dictionary<int, string[]> ghosts;
        private Sprite sp = new Sprite();
        private int animation;
        private int animationTimer;
        private int speedTimer;
        public Direction direction;
        public Direction nextDirection;
        private readonly int animationSpeed;
        private readonly int moveSpeed;
        private int setTime = DateTime.Now.Second;
        private int timer;
        private int totalPoints;

        public bool IsDead { get; set; }

        public int Health { get; private set; }

        public int NLevel { get; private set; }

        public event Action EatSpecialPoints;

        public event Action Died;

        public int Points { get; set; }

        public PacMan(int X, int Y, Direction direction) {
            this.X = X;
            this.Y = Y;
            ghosts = new Dictionary<int, string[]> {
                [0] = sp.rFrame1,
                [1] = sp.rFrame2
            };
            animation = 0;
            animationTimer = 0;
            animationSpeed = 4;
            this.direction = direction;

            speedTimer = 0;
            moveSpeed = 2;

            timer = setTime;

            Health = 3;

            totalPoints = 207;

            NLevel = 1;
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

        public void Update() {

            CheckPointsCollision();
            Move();
        }

        private void CheckPointsCollision() {
            for (int i = 0; i < Level.y; i++) {
                for (int u = 0; u < Level.x; u++) {
                    if (Level.PointsCollider[u, i] != default(char)) {
                        if (((X + 1 == u || X + 3 == u) && Y + 1 == i) ||
                            ((Y == i || Y + 2 == i) && X + 2 == u)) {
                            if (Level.PointsCollider[u, i] == '█') {
                                
                                EatSpecialPoints();
                            }
                            Points += 10;
                            totalPoints--;
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
            speedTimer++;
            FixDirection();
            CheckToroidal();

            if (speedTimer == moveSpeed) {
                speedTimer = 0;

                switch (direction) {
                    case Direction.Up:
                        if (!Level.WallCollider[X, Y - 1] && !Level.WallCollider[X + 4, Y - 1]) {
                            Y--;
                            ghosts[0] = sp.uFrame1;
                            ghosts[1] = sp.uFrame2;
                        } else
                            direction = Direction.None;
                        break;
                    case Direction.Down:
                        if (!Level.WallCollider[X, Y + 3] && !Level.WallCollider[X + 4, Y + 3]) {
                            Y++;
                            ghosts[0] = sp.dFrame1;
                            ghosts[1] = sp.dFrame2;
                        } else
                            direction = Direction.None;
                        break;
                    case Direction.Left:
                        if (!Level.WallCollider[X - 1, Y] && !Level.WallCollider[X - 1, Y + 2]) {
                            X--;
                            ghosts[0] = sp.lFrame1;
                            ghosts[1] = sp.lFrame2;
                        } else
                            direction = Direction.None;
                        break;
                    case Direction.Right:
                        if (!Level.WallCollider[X + 5, Y] && !Level.WallCollider[X + 5, Y + 2]) {
                            X++;
                            ghosts[0] = sp.rFrame1;
                            ghosts[1] = sp.rFrame2;
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

        public void Respawn() {

            if (IsDead) {

                IsDead = false;
                Health--;
                X = 51;
                Y = 25;
                direction = Direction.Right;
                Died();

            } else {

                X = 51;
                Y = 25;
                direction = Direction.Right;
            }
        }

        public bool WinCondition() {

            if (totalPoints == 0) {
                
                NLevel++;
                Health = 3;
                Points += 10000;
                totalPoints = 207;
                Respawn();

                return true;
            }

            return false;
        }
    }
}
