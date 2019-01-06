using System;
using System.IO;

namespace PacManGame {
    /// <summary>
    /// This Class is responsible for Displaying the menu and letting the player sellect an option
    /// </summary>
    class Menu {
        /** \brief Creates a new empty string */
        private readonly string path = @"";
        /** \brief Creates a new string with the file name */
        private readonly string menu = "PacMan_Menu.txt";
        /** \brief Saves the Y position for the Selection sprite */
        private int selectionY;
        /** \brief Knows if have the play button selected */
        private bool playSelected;
        /** \brief Knows if have the play button selected */
        public string[] MenuSprite { get; private set; } = new string[49];
        /** \brief Declares a new "KeyReader" */
        KeyReader kR;
        /** \brief Declares a new "Sprite" */
        Sprite sp;

        /// <summary>
        /// Menu constructor
        /// </summary>
        public Menu() {

            sp = new Sprite(); // Iniciates the "Sprite" "sp"
            kR = new KeyReader(); // Iniciates the "KeyReader" "kR"

            selectionY = 17; // Set the value of "selectionY" to 17
            playSelected = true; // Set the value of "playSelected" to true

            LoadMenu(); // Calls the LoadMenu method
            RenderMenu(); // Calls the RenderMenu method
        }

        /// <summary>
        /// Reads the user input to select on of the options from the menu
        /// </summary>
        private void GetInput() {
            do { // Do while cycle
                switch (kR.Input) { // Reads the input
                    case "Down": // If it's "Down"
                        if (playSelected) { // Ask if "playSelected" is true
                            // If so...
                            playSelected = false; // Sets "playSelected" to false
                            selectionY = 25; // Set the value of "selectionY" to 25
                            Console.Clear(); // Clears the console
                            RenderMenu(); // Calls the RenderMenu method
                        }
                        break;
                    case "Up": // If it's "Up"
                        if (!playSelected) { // Ask if "playSelected" is false
                            // If so...
                            playSelected = true; // Sets "playSelected" to true
                            selectionY = 17; // Set the value of "selectionY" to 17
                            Console.Clear(); // Clears the console
                            RenderMenu(); // Calls the RenderMenu method
                        }
                        break;
                    case "Enter": // If it's "Enter"
                        if (playSelected) { // Ask if "playSelected" is true
                            // If so...
                            Game game = new Game(kR); // Creates a new Game and passes to it "kR"
                            RenderMenu(); // Calls the RenderMenu method
                        } else {
                            // Else...
                            Environment.Exit(0); // Exits the program
                        }
                        break;
                }
            } while (true); // Cycles while true
        }

        /// <summary>
        /// Loads the menu from a specific file
        /// </summary>
        private void LoadMenu() {
            // Iniciates a new StreamReader to read from the wanted file
            using (StreamReader sr = new StreamReader(path + menu)) {
                string line; // Creates a new string
                // Executes a for loop to pass all the information from the file...
                for (int i = 0; (line = sr.ReadLine()) != null; i++) {
                    MenuSprite[i] = line; // ...into a specific house of the array
                }
            }
        }

        /// <summary>
        /// Renders the Menu
        /// </summary>
        private void RenderMenu() {

            // Display Level Borders
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < MenuSprite.Length; i++) {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(MenuSprite[i]);
            }

            // Display PacMan Logo
            for (int i = 0; i < sp.packString.Length; i++) {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(0, 16 + i);
                Console.Write(sp.packString[i]);
                Console.SetCursorPosition(85, 16 + i);
                Console.Write(sp.manString[i]);
            }

            // Display Play
            for (int i = 0; i < sp.playString.Length; i++) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(45, 16 + i);
                Console.Write(sp.playString[i]);
            }

            // Display Quit
            for (int i = 0; i < sp.quitString.Length; i++) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(45, 24 + i);
                Console.Write(sp.quitString[i]);
            }

            // Display PacMan
            for (int i = 0; i < sp.lFrame2.Length; i++) {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(35, 33 + i);
                Console.Write(sp.lFrame2[i]);
            }

            // Display Ghosts
            for (int i = 0; i < sp.gFrame1.Length; i++) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(46, 33 + i);
                Console.Write(sp.gFrame1[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 33 + i);
                Console.Write(sp.gFrame1[i]);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(60, 33 + i);
                Console.Write(sp.gFrame1[i]);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(67, 33 + i);
                Console.Write(sp.gFrame1[i]);
            }

            // Display Selection Square
            for (int i = 0; i < sp.selectionString.Length; i++) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(34, selectionY + i);
                Console.Write(sp.selectionString[i]);
            }

            // Ask for input
            GetInput();
        }
    }
}
