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


        public Ghost(int x, int y, ConsoleColor color, Direction direction) {
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

            speedTimer = 0;
            moveSpeed = 5;
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
                        y--;
                        break;
                    case Direction.Down:
                        y++;
                        break;
                    case Direction.Left:
                        x--;
                        break;
                    case Direction.Right:
                        x++;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
