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
        private int timer;
        private int chance;
        private PacMan pacman;
        private int lastPacX;
        private int lastPacY;
        private Direction lastPacDir;
        private bool isVulnerable;

        public bool IsDead { get; private set; }

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
            this.pacman = pacman;
            this.direction = direction;

            animation = 0;
            animationTimer = 0;
            animationSpeed = 8;
            ghostNumber = number;
            speedTimer = 0;
            moveSpeed = 2;
            timer = 0;

            ghosts = new Dictionary<int, string[]> {
                [0] = gFrame1,
                [1] = gFrame2
            };

            rnd = new Random(ghostNumber ^ DateTime.Now.Millisecond);

            state = GhostState.LeavingSpawn;

            isVulnerable = false;
            IsDead = false;
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
            OnEatSpecialPoints();
            CheckCollision();
            UpdateState();
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
                    case Direction.None:
                        Console.Beep();
                        break;
                }
            }
            CheckToroidal();
        }

        private void UpdateDirection() {

            if (direction == Direction.Left || direction == Direction.Right) {

                if (!Level.WallCollider[x, y - 1] && !Level.WallCollider[x + 4, y - 1]) {

                    if (chance <= 40) {

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

                    if (IsDead) IsDead = false;
                    LeaveSpawn();

                    break;

                case GhostState.SearchPacMan:
                    Move();
                    UpdateDirection();
                    CheckPacMan();
                    if (isVulnerable || CheckPacMan()) state = GhostState.RunFromPacman;
                    break;

                case GhostState.FollowPacMan:
                    Move();
                    Follow();
                    if (isVulnerable || CheckPacMan()) state = GhostState.RunFromPacman;
                    break;

                case GhostState.RunFromPacman:
                    Move();
                    Run();
                    break;

                case GhostState.ReturnToSpawn:

                    ReturnToSpawn();
                    Run();
                    break;
            }
        }

        private void OnEatSpecialPoints() => pacman.EatSpecialPoints += Run;

        private void ReturnToSpawn() {
            if (IsDead) {
                IsDead = false;

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

            } else if (!IsDead) {

                //timer++;
            }
        }

        private void CheckToroidal() {
            if (x == 1 && y == 21 ||
                x == 101 && y == 21) {
                x = direction == Direction.Right ? 1 : 101;
            }
        }

        private void LeaveSpawn() {
            timer++;
            if (ghostNumber > 2 && timer < 40) return;
            if (x < 51 || x > 51) {
                x = ghostNumber > 2 ? x - 1 : x + 1;
            } else if (y > 17) {
                y--;
            } else {
                state = GhostState.SearchPacMan;
                timer = ghostNumber > 3 ? 0 : timer;
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

                //if (!pacman.CanEatGhosts) state = GhostState.FollowPacMan;

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

                //if (!pacman.CanEatGhosts) state = GhostState.FollowPacMan;

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

                //if (!pacman.CanEatGhosts) state = GhostState.FollowPacMan;

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

                //if (!pacman.CanEatGhosts) state = GhostState.FollowPacMan;

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

        private void Run() {

            timer++;

            if (!CheckPacMan()) {

                UpdateDirection();

            } else {

                if (y > pacman.Y) {

                    direction = Direction.Down;

                } else if (y < pacman.Y) {

                    direction = Direction.Up;

                } else if (x > pacman.X) {

                    direction = Direction.Right;

                } else if (x < pacman.X) {

                    direction = Direction.Left;
                }

                if (direction == Direction.Up || direction == Direction.Down) {

                    if (!Level.WallCollider[x - 1, y] && !Level.WallCollider[x - 1, y + 2] &&
                        !Level.WallCollider[x + 5, y] && !Level.WallCollider[x + 5, y + 2]) {

                        if (chance <= 50) {

                            direction = Direction.Left;

                        } else {

                            direction = Direction.Right;
                        }

                        return;

                    } else if (!Level.WallCollider[x - 1, y] && !Level.WallCollider[x - 1, y + 2]) {

                        direction = Direction.Left;
                        return;

                    } else if (!Level.WallCollider[x + 5, y] && !Level.WallCollider[x + 5, y + 2]) {

                        direction = Direction.Right;
                        return;
                    }
                }

                if (direction == Direction.Left || direction == Direction.Right) {

                    if (!Level.WallCollider[x, y - 1] && !Level.WallCollider[x + 4, y - 1] &&
                        !Level.WallCollider[x, y + 3] && !Level.WallCollider[x + 4, y + 3]) {

                        if (chance <= 50) {

                            direction = Direction.Up;

                        } else {

                            direction = Direction.Down;
                        }

                    } else if (!Level.WallCollider[x, y - 1] && !Level.WallCollider[x + 4, y - 1]) {

                        direction = Direction.Up;

                    } else if (!Level.WallCollider[x, y + 3] && !Level.WallCollider[x + 4, y + 3]) {

                        direction = Direction.Down;
                    }
                }
            }

            isVulnerable = true;
            color = ConsoleColor.DarkGray;
            moveSpeed = 3;

            if (timer > 120) {

                timer = 0;
                BackToNormal();
            }
        }

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
            state = GhostState.SearchPacMan;
        }

        private void CheckCollision() {

            if (x == pacman.X + 2 && y == pacman.Y || x == pacman.X && y == pacman.Y + 2 ||
                x == pacman.X && y + 2 == pacman.Y || x + 2 == pacman.X && y + 2 == pacman.Y + 2) {

                if (isVulnerable) {

                    isVulnerable = false;
                    PacMan.Points += 1500;
                    IsDead = true;
                    state = GhostState.ReturnToSpawn;

                } else {

                    //PACMAN DIES
                }
            }
        }
    }
}
