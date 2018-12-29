using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WorldDrawTest {
    class Menu {

        private readonly string path = @"";
        private readonly string menu = "PacMan_Menu.txt";

        private int selectionY = 17;

        private bool playSelected;
        private bool quitSelected;

        public string[] MenuSprite { get; private set; } = new string[49];

        MenuSymbols ms;

        public Menu() {

            ms = new MenuSymbols();

            selectionY = 17;
            playSelected = true;
            quitSelected = false;

            LoadMenu(menu);
            RenderMenu();
        }

        /* No necessity for the Menu to have another Thread run on the background
         * since there's nothing else happening than the 'waiting for input' */
        private void GetInput() {

            switch(Console.ReadKey(true).Key) {

                case ConsoleKey.DownArrow:

                    if (playSelected) {

                        playSelected = false;
                        quitSelected = true;
                        selectionY = 25;
                        Console.Clear();
                        RenderMenu();

                    } else {

                        RenderMenu();
                        
                    }

                    break;

                case ConsoleKey.UpArrow:

                    if (quitSelected) {

                        quitSelected = false;
                        playSelected = true;
                        selectionY = 17;
                        Console.Clear();
                        RenderMenu();

                    } else {

                        RenderMenu();
                    }

                    break;

                case ConsoleKey.Enter:

                    if (playSelected) {

                        Game game = new Game();
                        
                    } else {

                        Environment.Exit(0);
                    }

                    break;

                default:

                    RenderMenu();
                    break;
            }
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
            for (int i = 0; i < ms.packString.Length; i++) {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(0, 16 + i);
                Console.Write(ms.packString[i]);
                Console.SetCursorPosition(85, 16 + i);
                Console.Write(ms.manString[i]);
            }

            // Display Play
            for (int i = 0; i < ms.playString.Length; i++) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(45, 16 + i);
                Console.Write(ms.playString[i]);
            }

            // Display Quit
            for (int i = 0; i < ms.quitString.Length; i++) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(45, 24 + i);
                Console.Write(ms.quitString[i]);
            }

            // Display PacMan
            for (int i = 0; i < ms.pacManString.Length; i++) {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(35, 33 + i);
                Console.Write(ms.pacManString[i]);
            }

            // Display Ghosts
            for (int i = 0; i < ms.ghost1String.Length; i++) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(46, 33 + i);
                Console.Write(ms.ghost1String[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 33 + i);
                Console.Write(ms.ghost2String[i]);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(60, 33 + i);
                Console.Write(ms.ghost3String[i]);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.SetCursorPosition(67, 33 + i);
                Console.Write(ms.ghost4String[i]);
            }

            // Display Selection Square
            for (int i = 0; i < ms.selectionString.Length; i++) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(34, selectionY + i);
                Console.Write(ms.selectionString[i]);
            }

            // Ask for input
            GetInput();
        }
    }
}
