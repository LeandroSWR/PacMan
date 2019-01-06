using System;

namespace PacManGame {
    /// <summary>
    /// This Class is responsible for displaying the level
    /// </summary>
    class Level {
        LevelLoader loader = new LevelLoader(); // Creates a new LevelLoader
        Sprite sprite = new Sprite(); // Creates a new Sprite
        // Creates a new string and set it's value to be equal to "loader.LevelSprite"
        private string[] LevelSprite => loader.LevelSprite;
        // Creates a new string and set it's value to be equal to "loader.LevelPoints"
        private string[] PointsSprite => loader.LevelPoints;

        // Four static variables that need to be acceced from other classes
        public static readonly int x = 107; // Creates a new static readonly int with the value 107
        public static readonly int y = 49; // Creates a new static readonly int with the value 49
        // Creates a new static bool array with a private set and size (107, 49)
        public static bool[,] WallCollider { get; private set; } = new bool[x, y];
        // Creates a new static bool array with a private set and size (107, 49)
        public static char[,] PointsCollider { get; private set; } = new char[x, y];

        private int otherFrame = 0; // Creates a new int with the value 0

        /// <summary>
        /// Level Constructor
        /// </summary>
        public Level() {
            GetCollider(); // Call the method "GetCollider"
            RenderLevel(); // Call the method "RenderLevel"
        }
        
        /// <summary>
        /// Gets the locations of all colliders and saves them into arrays
        /// </summary>
        public void GetCollider() {
            // Loops the level sprite to get the location of every wall
            for (int i = 0; i < LevelSprite.Length; i++) {
                for (int u = 0; u < LevelSprite[i].Length; u++) {
                    if (LevelSprite[i][u] != ' ') {
                        // Saves each wall location into the array "WallCollider"
                        WallCollider[u, i] = true;
                    }
                }
            }

            // Loops the level sprite to get the location of every point
            for (int i = 0; i < LevelSprite.Length; i++) {
                if (PointsSprite[i] != null) {
                    for (int u = 0; u < PointsSprite[i].Length; u++) {
                        if (PointsSprite[i][u] != ' ') {
                            // Saves each point location into the array "PointsCollider"
                            PointsCollider[u, i] = PointsSprite[i][u] == '▄' ? '▄' : '█';
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update method
        /// </summary>
        /// <param name="points">Current player points</param>
        /// <param name="lives">Current player lives</param>
        /// <param name="level">Current level the player's in</param>
        public void Update(int points, int lives, int level) {
            RenderUi(points, lives, level); // Calls the "RenderUi" method giving 3 attributes
            // Skips 9 frames rendering the points again on the 10th
            if (otherFrame == 0) {
                otherFrame += 10;
                RenderPoints(); // Calls the "RenderPoints" method
            }
            otherFrame--;
        }

        /// <summary>
        /// Renders the Ui
        /// </summary>
        /// <param name="points">Current player points</param>
        /// <param name="lives">Current player lives</param>
        /// <param name="level">Current level the player's in</param>
        public void RenderUi(int points, int lives, int level) {
            // Set the ForegroundColor to DarkYellow
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            // Create a new string and pass to it the number of points
            string score = Convert.ToString(points); 
            // Loops while the score length is different from 7
            while (score.Length != 7) {
                score = "0" + score; // Adds a 0 to the left at each loop
            }
            Console.SetCursorPosition(3, 25); // Sets the cursor position
            Console.Write("Score"); // Writes to the console 
            // Loops the string Score to display a "prettier" version of it to the screen
            for (int i = 0, u = 0; i < score.Length; i++, u += 2) {
                for (int a = 0; a < 3; a++) {
                    // Changes the cursor position as needed
                    Console.SetCursorPosition(3 + u, 26 + a);
                    // Checks witch number it needs to pint
                    switch (score[i]) {
                        case '0':
                            Console.Write(sprite.zero[a]); // Writes it to the console
                            break;
                        case '1':
                            Console.Write(sprite.one[a]); // Writes it to the console
                            break;
                        case '2':
                            Console.Write(sprite.two[a]); // Writes it to the console
                            break;
                        case '3':
                            Console.Write(sprite.tree[a]); // Writes it to the console
                            break;
                        case '4':
                            Console.Write(sprite.four[a]); // Writes it to the console
                            break;
                        case '5':
                            Console.Write(sprite.five[a]); // Writes it to the console
                            break;
                        case '6':
                            Console.Write(sprite.six[a]); // Writes it to the console
                            break;
                        case '7':
                            Console.Write(sprite.seven[a]); // Writes it to the console
                            break;
                        case '8':
                            Console.Write(sprite.eight[a]); // Writes it to the console
                            break;
                        case '9':
                            Console.Write(sprite.nine[a]); // Writes it to the console
                            break;
                    }
                }
            }

            Console.SetCursorPosition(87, 25); // Set the cursor possition
            Console.Write("Lives"); // Writes to the console
            Console.SetCursorPosition(96, 25); // Set the cursor possition
            Console.Write("Level"); // Writes to the console
            Console.SetCursorPosition(87, 26); // Set the cursor possition
            for (int i = 0; i < 3; i++) { // Loops 3 times to properlly display the wanted sprite
                Console.SetCursorPosition(89, 26 + i); // Changes the cursor position as needed
                CheckNumber(lives, i); // Calls the "CheckNumber" method passing 2 attributes
            }
            Console.SetCursorPosition(96, 26); // Set the cursor possition
            for (int i = 0; i < 3; i++) {// Loops 3 times to properlly display the wanted sprite
                Console.SetCursorPosition(98, 26 + i); // Changes the cursor position as needed
                CheckNumber(level, i); // Calls the "CheckNumber" method passing 2 attributes
            }
        }

        /// <summary>
        /// Checks a number to display a "prettier" version of it
        /// </summary>
        /// <param name="temp">Current number</param>
        /// <param name="i">Current loop number</param>
        private void CheckNumber(int temp, int i) {
            switch (temp) { // Check witch number it is
                // Depending on the number displays a better version of it
                case 0:
                    Console.Write(sprite.zero[i]); // Writes it to the console
                    break;
                case 1:
                    Console.Write(sprite.one[i]); // Writes it to the console
                    break;
                case 2:
                    Console.Write(sprite.two[i]); // Writes it to the console
                    break;
                case 3:
                    Console.Write(sprite.tree[i]); // Writes it to the console
                    break;
                case 4:
                    Console.Write(sprite.four[i]); // Writes it to the console
                    break;
                case 5:
                    Console.Write(sprite.five[i]); // Writes it to the console
                    break;
                case 6:
                    Console.Write(sprite.six[i]); // Writes it to the console
                    break;
                case 7:
                    Console.Write(sprite.seven[i]); // Writes it to the console
                    break;
                case 8:
                    Console.Write(sprite.eight[i]); // Writes it to the console
                    break;
                case 9:
                    Console.Write(sprite.nine[i]); // Writes it to the console
                    break;
            }
        }

        /// <summary>
        /// Renders all the points in the level
        /// </summary>
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
            // Display Level Walls
            Console.SetCursorPosition(0, 0);
            // Loops the required number of times so the sprite displays properlly
            for (int i = 0; i < LevelSprite.Length; i++) {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(LevelSprite[i]);
            }
            // Display PacMan Logo
            // Loops the required number of times so the sprite displays properlly
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
