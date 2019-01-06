using System;
using System.IO;

namespace PacManGame {
    class Menu {

        private readonly string path = @"";
        private readonly string menu = "PacMan_Menu.txt";

        private int selectionY = 17;

        private bool playSelected;
        private bool quitSelected;

        public string[] MenuSprite { get; private set; } = new string[49];

        KeyReader kR;
        Sprite sp;

        public Menu() {

            sp = new Sprite();
            kR = new KeyReader();

            selectionY = 17;
            playSelected = true;
            quitSelected = false;

            LoadMenu(menu);
            RenderMenu();
        }

        private void GetInput() {
            do {
                switch (kR.Input) {

                    case "Down":

                        if (playSelected) {

                            playSelected = false;
                            quitSelected = true;
                            selectionY = 25;
                            Console.Clear();
                            RenderMenu();

                        }

                        break;

                    case "Up":

                        if (quitSelected) {

                            quitSelected = false;
                            playSelected = true;
                            selectionY = 17;
                            Console.Clear();
                            RenderMenu();

                        }

                        break;

                    case "Enter":

                        if (playSelected) {

                            Game game = new Game(kR);
                            RenderMenu();

                        } else {

                            Environment.Exit(0);
                        }

                        break;
                }
            } while (true);
        }

        private void LoadMenu(string file) {
            using (StreamReader sr = new StreamReader(path + file)) {
                string line;
                if (MenuSprite[0] == null) {
                    for (int i = 0; (line = sr.ReadLine()) != null; i++) {
                        MenuSprite[i] = line;
                    }
                }
            }
        }

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
