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
        private GhostState state;
        private int setTime = DateTime.Now.Second;
        private int timer;
        private int chance;
        private PacMan pacman;
        private int lastPacX;
        private int lastPacY;
        private Direction lastPacDir;

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


        public Ghost(int number, int x, int y, ConsoleColor color, Direction direction, PacMan pacman) {
            this.x = x;
            this.y = y;
            this.color = color;
            ghosts = new Dictionary<int, string[]> {
                [0] = gFrame1,
                [1] = gFrame2
            };
            animation = 0;
            animationTimer = 0;
            animationSpeed = 8;
            this.direction = direction;
            ghostNumber = number;
            this.pacman = pacman;

            speedTimer = 0;
            moveSpeed = 2;

            rnd = new Random(ghostNumber ^ DateTime.Now.Millisecond);

            state = GhostState.LeavingSpawn;
            timer = setTime;
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

        public void Update() {

            chance = rnd.Next(1, 100);

            Move();
            UpdateState();
        }

        public void Move() {
            speedTimer++;
            timer++;

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
                    case Direction.None:
                        break;
                }
            }
            CheckToroidal();
        }

        private void UpdateDirection() {

            if (direction == Direction.Left || direction == Direction.Right) {

                if (!Level.WallCollider[x, y - 1] && !Level.WallCollider[x + 4, y - 1]) {
                    
                    if(chance <= 40) {

                        direction = Direction.Up;
                    }

                } else if (!Level.WallCollider[x, y + 3] && !Level.WallCollider[x + 4, y + 3]) {

                    if (chance >= 60) {

                        direction = Direction.Down;
                    }
                }
            } else if (direction == Direction.Up || direction == Direction.Down) {

                if (!Level.WallCollider[x - 1, y] && !Level.WallCollider[x - 1, y + 2] && !Level.WallCollider[x - 1, y + 1]) {

                    if (chance <= 40) {

                        direction = Direction.Left;
                    }

                } else if (!Level.WallCollider[x + 5, y] && !Level.WallCollider[x + 5, y + 2] && !Level.WallCollider[x + 5, y + 1]) {

                    if (chance >= 60) {

                        direction = Direction.Right;
                    }
                }
            }
        }

        private void UpdateState() {

            switch (state) {

                case GhostState.LeavingSpawn:

                    LeaveSpawn();

                    break;

                case GhostState.SearchPacMan:

                    UpdateDirection();
                    CheckPacMan();
                    break;

                case GhostState.FollowPacMan:

                    Follow();
                    break;

                case GhostState.RunFromPacman:

                    //run from Pacman
                    break;

                case GhostState.ReturnToSpawn:

                    //return to Spawn
                    break;
            }
        }

        private void CheckToroidal() {
            if (x == 1 && y == 21 ||
                x == 101 && y == 21) {
                x = direction == Direction.Right ? 1 : 101;
            }
        }

        private void LeaveSpawn() {

            if (direction == Direction.Right || direction == Direction.Left) {

                if (!Level.WallCollider[x, y - 1] && !Level.WallCollider[x + 4, y - 1]) {

                    direction = Direction.Up;
                }

            } else if (direction == Direction.None) {

                if (timer.Equals(setTime + 40)) {

                    direction = Direction.Left;
                    timer = 0;
                }

            } else if (direction == Direction.Up) {

                if (Level.WallCollider[x, y - 1] && Level.WallCollider[x + 4, y - 1]) {

                    if (chance <= 50) {

                        direction = Direction.Left;

                    } else {

                        direction = Direction.Right;
                    }

                    state = GhostState.SearchPacMan;
                }
            }
        }

        private bool CheckPacMan() {
            if (x == pacman.X && y > pacman.Y) {

                for (int i = pacman.Y; i <= y; i++) {

                    if (Level.WallCollider[x, i]) {

                        return false;

                    } else if (!Level.WallCollider[x, i]) {

                        continue;
                    }
                }

                state = GhostState.FollowPacMan;
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

                state = GhostState.FollowPacMan;
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

                state = GhostState.FollowPacMan;
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

                state = GhostState.FollowPacMan;
                return true;
            }
            return false;
        }

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
    }
}
