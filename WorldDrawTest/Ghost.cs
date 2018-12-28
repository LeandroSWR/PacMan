using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldDrawTest {
    class Ghost {

        // Ascii ▄ ▀ █

        private int x;
        private int y;
        private ConsoleColor color;
        private Dictionary<int, string[]> ghosts;
        private int animation;
        private int animationTimer;
        private int animationSpeed;
        private Direction direction;
        private int moveSpeed;
        private int speedTimer;
        private Random rnd;
        private int ghostNumber;

        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        private readonly string[] gFrame1 = new string[3] {
            "▄███▄",
            "█@█@█",
            "█▀█▀█"
        };
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        private readonly string[] gFrame2 = new string[3] {
            "▄███▄",
            "█@█@█",
            "▀█▀█▀"
        };


        public Ghost(int number, int x, int y, ConsoleColor color, Direction direction) {
            this.x = x;
            this.y = y;
            this.color = color;
            ghosts = new Dictionary<int, string[]> {
                [0] = gFrame1,
                [1] = gFrame2
            };
            animation = 0;
            animationTimer = 0;
            animationSpeed = 10;
            this.direction = direction;
            ghostNumber = number;

            speedTimer = 0;
            moveSpeed = 1;

            rnd = new Random(ghostNumber ^ DateTime.Now.Millisecond);
        }

        public void Plot() {
            animationTimer++;
            if (animationTimer == animationSpeed) {
                animation = animation == 0 ? 1 : 0;
                animationTimer = 0;
            }
            Console.ForegroundColor = color;
            for (int i = 0; i < 3; i++) {
                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(ghosts[animation][i]);
            }
            Console.ForegroundColor = ConsoleColor.White;
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
                        if (!Level.WallCollider[x, y - 1] && !Level.WallCollider[x + 4, y - 1]) {
                            y--;
                        } else {
                            direction = Direction.Down;
                        }
                        break;
                    case Direction.Down:
                        if (!Level.WallCollider[x, y + 3] && !Level.WallCollider[x + 4, y + 3]) {
                            y++;
                        } else {
                            direction = Direction.Up;
                        }
                        break;
                    case Direction.Left:
                        if (!Level.WallCollider[x - 1, y] && !Level.WallCollider[x - 1, y + 2]) {
                            x--;
                        } else {
                            direction = Direction.Right;
                        }
                        break;
                    case Direction.Right:
                        if (!Level.WallCollider[x + 5, y] && !Level.WallCollider[x + 5, y + 2]) {
                            x++;
                        } else {
                            direction = Direction.Left;
                        }
                        break;
                }
            }
            UpdateDirection();
            CheckToroidal();
        }

        private void UpdateDirection() {

            int chance = rnd.Next(1, 100);

            if (direction == Direction.Left || direction == Direction.Right) {

                if (!Level.WallCollider[x, y - 1] && !Level.WallCollider[x + 4, y - 1]) {
                    
                    if(chance <= 30) {

                        direction = Direction.Up;
                    }

                } else if (!Level.WallCollider[x, y + 3] && !Level.WallCollider[x + 4, y + 3]) {

                    if (chance >= 70) {

                        direction = Direction.Down;
                    }
                }
            } else if (direction == Direction.Up || direction == Direction.Down) {

                if (!Level.WallCollider[x - 1, y] && !Level.WallCollider[x - 1, y + 2] && !Level.WallCollider[x - 1, y + 1]) {

                    if (chance <= 30) {

                        direction = Direction.Left;
                    }

                } else if (!Level.WallCollider[x + 5, y] && !Level.WallCollider[x + 5, y + 2] && !Level.WallCollider[x + 5, y + 1]) {

                    if (chance >= 70) {

                        direction = Direction.Right;
                    }
                }
            }
        }

        private void CheckToroidal() {
            if (x == 1 && y == 21 ||
                x == 101 && y == 21) {
                x = direction == Direction.Right ? 1 : 101;
            }
        }
    }
}
