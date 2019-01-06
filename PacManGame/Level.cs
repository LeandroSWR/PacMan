﻿using System;

namespace PacManGame {
    class Level {
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

        public void RenderUi(int points, int lives, int level) {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string score = Convert.ToString(points);
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

            Console.SetCursorPosition(87, 25);
            Console.Write("Lives");
            Console.SetCursorPosition(96, 25);
            Console.Write("Level");
            Console.SetCursorPosition(87, 26);
            for (int i = 0; i < 3; i++) {
                Console.SetCursorPosition(89, 26 + i);
                CheckNumber(lives, i);
            }
            Console.SetCursorPosition(96, 26);
            for (int i = 0; i < 3; i++) {
                Console.SetCursorPosition(98, 26 + i);
                CheckNumber(level, i);
            }
        }

        private void CheckNumber(int temp, int i) {
            switch (temp) {
                case 0:
                    Console.Write(sprite.zero[i]);
                    break;
                case 1:
                    Console.Write(sprite.one[i]);
                    break;
                case 2:
                    Console.Write(sprite.two[i]);
                    break;
                case 3:
                    Console.Write(sprite.tree[i]);
                    break;
                case 4:
                    Console.Write(sprite.four[i]);
                    break;
                case 5:
                    Console.Write(sprite.five[i]);
                    break;
                case 6:
                    Console.Write(sprite.six[i]);
                    break;
                case 7:
                    Console.Write(sprite.seven[i]);
                    break;
                case 8:
                    Console.Write(sprite.eight[i]);
                    break;
                case 9:
                    Console.Write(sprite.nine[i]);
                    break;
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

        public void RenderLevel() {
            // Display Level Borders
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < LevelSprite.Length; i++) {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(LevelSprite[i]);
            }
            // Display PacMan Logo
            for (int i = 0; i < sprite.packString.Length; i++) {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(0, 16 + i);
                Console.Write(sprite.packString[i]);
                Console.SetCursorPosition(85, 16 + i);
                Console.Write(sprite.manString[i]);
            }
        }
    }
}
