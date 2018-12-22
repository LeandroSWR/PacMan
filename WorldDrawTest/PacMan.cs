using System;
using System.Collections.Generic;

namespace WorldDrawTest {
    class PacMan {
        // Ascii ▄ ▀ █

        private int x;
        private int y;
        private Dictionary<int, string[]> ghosts;
        private int animation;
        private int animationTimer;
        private int animationSpeed;
        public Direction direction;
        public Direction previousDirection;
        private int moveSpeed;
        private int speedTimer;

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



        public PacMan(int x, int y, Direction direction) {
            this.x = x;
            this.y = y;
            ghosts = new Dictionary<int, string[]> {
                [0] = rFrame1,
                [1] = rFrame2
            };
            animation = 0;
            animationTimer = 0;
            animationSpeed = 5;
            this.direction = direction;

            speedTimer = 0;
            moveSpeed = 2;
        }

        public void Plot() {
            animationTimer++;
            if (animationTimer == animationSpeed) {
                animation = animation == 0 ? 1 : 0;
                animationTimer = 0;
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < 3; i++) {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(ghosts[animation][i]);
            }
        }

        public void UnPlot() {
            for (int i = 0; i < 3; i++) {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine("     ");
            }
        }

        public void Move() {
            speedTimer++;

            if (speedTimer == moveSpeed) {
                speedTimer = 0;

                switch (direction) {
                    case Direction.Up:
                        if (!Level.WallCollider[x, y-1] && !Level.WallCollider[x + 4, y - 1]) {
                            y--;
                            ghosts[0] = uFrame1;
                            ghosts[1] = uFrame2;
                        } else {
                            SmoothMove();
                        }
                        break;
                    case Direction.Down:
                        if (!Level.WallCollider[x, y + 3] && !Level.WallCollider[x + 4, y + 3]) {
                            y++;
                            ghosts[0] = dFrame1;
                            ghosts[1] = dFrame2;
                        } else {
                            SmoothMove();
                        }
                        break;
                    case Direction.Left:
                        if (!Level.WallCollider[x - 1, y] && !Level.WallCollider[x -1, y + 2]) {
                            x--;
                            ghosts[0] = lFrame1;
                            ghosts[1] = lFrame2;
                        } else {
                            SmoothMove();
                        }
                        break;
                    case Direction.Right:
                        if (!Level.WallCollider[x + 5, y] && !Level.WallCollider[x + 5, y + 2]) {
                            x++;
                            ghosts[0] = rFrame1;
                            ghosts[1] = rFrame2;
                        } else {
                            SmoothMove();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void SmoothMove() {
            switch (previousDirection) {
                case Direction.Up:
                    if (!Level.WallCollider[x, y - 1] && !Level.WallCollider[x + 4, y - 1]) {
                        y--;
                        ghosts[0] = uFrame1;
                        ghosts[1] = uFrame2;
                    }
                    break;
                case Direction.Down:
                    if (!Level.WallCollider[x, y + 3] && !Level.WallCollider[x + 4, y + 3]) {
                        y++;
                        ghosts[0] = dFrame1;
                        ghosts[1] = dFrame2;
                    }
                    break;
                case Direction.Left:
                    if (!Level.WallCollider[x - 1, y] && !Level.WallCollider[x - 1, y + 2]) {
                        x--;
                        ghosts[0] = lFrame1;
                        ghosts[1] = lFrame2;
                    }
                    break;
                case Direction.Right:
                    if (!Level.WallCollider[x + 5, y] && !Level.WallCollider[x + 5, y + 2]) {
                        x++;
                        ghosts[0] = rFrame1;
                        ghosts[1] = rFrame2;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
