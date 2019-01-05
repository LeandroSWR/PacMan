using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldDrawTest {
    class Level {
        // ╣ ║ ╗ ╝ ╚ ╔ ╩ ╦ ╠ ═ ╬
        // 107x49

        // PreMade Sprites
        readonly string[] packString = new string[4] {
            "╔══════╗╔═════╗╔═════╗",
            "║   ═  ║║  ═  ║║  ╔══╝",
            "║  ╔═══╝║ ╔═╗ ║║  ╚══╗",
            "╚══╝    ╚═╝ ╚═╝╚═════╝",
            };
        readonly string[] manString = new string[4] {
            "╔═╗  ╔═╗╔═════╗╔═╗ ╔═╗",
            "║ ╚╗╔╝ ║║  ═  ║║ ╚╗╣ ║",
            "║ ╠╚╝╣ ║║ ╔═╗ ║║ ╠╚╗ ║",
            "╚═╝  ╚═╝╚═╝ ╚═╝╚═╝ ╚═╝",
            };

        LevelLoader loader = new LevelLoader();
        Sprite sprite = new Sprite();
        private string[] LevelSprite => loader.LevelSprite;
        private string[] PointsSprite => loader.LevelPoints;

        public static readonly int x = 107;
        public static readonly int y = 49;
        public static bool[,] WallCollider { get; private set; } = new bool[107, 49];
        public static char[,] PointsCollider { get; private set; } = new char[107, 49];

        public Level() {
            GetCollider();
            RenderLevel();
        }
        //3x25
        private void GetCollider() {
            for (int i = 0; i < LevelSprite.Length; i++) {
                for (int u = 0; u < LevelSprite[i].Length; u++) {
                    if (LevelSprite[i][u] != ' ') {
                        WallCollider[u, i] = true;
                    }
                }
            }

            for (int i = 0; i < LevelSprite.Length; i++) {
                if (PointsSprite[i] != null) {
                    for (int u = 0; u < PointsSprite[i].Length; u++) {
                        if (PointsSprite[i][u] != ' ') {
                            PointsCollider[u, i] = PointsSprite[i][u] == '▄' ? '▄' : '█';
                        }
                    }
                }
            }
        }

        public void RenderScore() {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string score = Convert.ToString(PacMan.Points);
            while (score.Length != 7) {
                score = "0" + score;
            }
            Console.SetCursorPosition(3, 25);
            Console.Write("Score");
            for (int i = 0, u = 0; i < score.Length; i++, u += 2) {
                for (int a = 0; a < 3; a++) {
                    Console.SetCursorPosition(3 + u, 26 + a);
                    switch (score[i]) {
                        case '0':
                            Console.Write(sprite.zero[a]);
                            break;
                        case '1':
                            Console.Write(sprite.one[a]);
                            break;
                        case '2':
                            Console.Write(sprite.two[a]);
                            break;
                        case '3':
                            Console.Write(sprite.tree[a]);
                            break;
                        case '4':
                            Console.Write(sprite.four[a]);
                            break;
                        case '5':
                            Console.Write(sprite.five[a]);
                            break;
                        case '6':
                            Console.Write(sprite.six[a]);
                            break;
                        case '7':
                            Console.Write(sprite.seven[a]);
                            break;
                        case '8':
                            Console.Write(sprite.eight[a]);
                            break;
                        case '9':
                            Console.Write(sprite.nine[a]);
                            break;
                    }
                }
            }
        }

        public void RenderPoints() {
            for (int i = 0; i < LevelSprite.Length; i++) {
                if (PointsSprite[i] != null) {
                    for (int u = 0; u < PointsSprite[i].Length; u++) {
                        if (PointsCollider[u, i] != default(char)) {
                            if (PointsCollider[u, i] == '█') {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.SetCursorPosition(u, i);
                                Console.Write(PointsCollider[u, i]);
                            } else {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.SetCursorPosition(u, i);
                                Console.Write(PointsCollider[u, i]);
                            }
                        }
                    }
                }
            }
        }

        private void RenderLevel() {
            // Display Level Borders
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < LevelSprite.Length; i++) {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(LevelSprite[i]);
            }
            // Display Points
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < LevelSprite.Length; i++) {
                if (PointsSprite[i] != null) {
                    for (int u = 0; u < PointsSprite[i].Length; u++) {
                        if (PointsSprite[i][u] != ' ') {
                            if (PointsSprite[i][u] == '█') {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.SetCursorPosition(u, i);
                                Console.Write(PointsSprite[i][u]);
                                Console.ForegroundColor = ConsoleColor.White;
                            } else {
                                Console.SetCursorPosition(u, i);
                                Console.Write(PointsSprite[i][u]);
                            }
                        }
                    }
                }
            }
            // Display PacMan Logo
            for (int i = 0; i < packString.Length; i++) {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(0, 16 + i);
                Console.Write(packString[i]);
                Console.SetCursorPosition(85, 16 + i);
                Console.Write(manString[i]);
            }
        }
    }
}
