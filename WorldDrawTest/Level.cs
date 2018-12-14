using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldDrawTest {
    class Level {
        // ╣ ║ ╗ ╝ ╚ ╔ ╩ ╦ ╠ ═ ╬
        // 3 DE ALTURA!!!!
        // 111x49
        // Ascii for borders
        private readonly string bar = "══";
        private readonly string side = "║";

        // PreMade Sprites
        string[] packString = new string[4] {
            "╔══════╗╔═════╗╔═════╗",
            "║   ═  ║║  ═  ║║  ╔══╝",
            "║  ╔═══╝║ ╔═╗ ║║  ╚══╗",
            "╚══╝    ╚═╝ ╚═╝╚═════╝",
            };
        string[] manString = new string[4] {
            "╔═╗  ╔═╗╔═════╗╔═╗ ╔═╗",
            "║ ╚╗╔╝ ║║  ═  ║║ ╚╗╣ ║",
            "║ ╠╚╝╣ ║║ ╔═╗ ║║ ╠╚╗ ║",
            "╚═╝  ╚═╝╚═╝ ╚═╝╚═╝ ╚═╝",
            };
        string[] leftThickDivision = new string[6] {
            "═════════════════════╗",
            "║",
            "║",
            "║",
            "║",
            "═════════════════════╝",
            };
        string[] rightThickDivision = new string[6] {
            "╔═════════════════════",
            "║",
            "║",
            "║",
            "║",
            "╚═════════════════════",
            };

        public void Plot() {

            Console.ForegroundColor = ConsoleColor.Blue;
            //
            // Draws the hole level
            //
            for (int i = 0; i < 111; i++) {
                // Draw Top
                Console.SetCursorPosition(2 + i, 2);
                switch (i) {
                    case 0:
                        Console.Write("╔");
                        break;
                    case 54:
                        Console.Write("╦");
                        break;
                    case 58:
                        Console.Write("╦");
                        break;
                    case 110:
                        Console.Write("╗");
                        break;
                    default:
                        Console.Write(bar);
                        break;
                }
                // Draw Bottom
                Console.SetCursorPosition(2 + i, 50);
                switch (i) {
                    case 0:
                        Console.Write("╚");
                        break;
                    case 110:
                        Console.Write("╝");
                        break;
                    default:
                        Console.Write(bar);
                        break;
                }

                // Draw bottom side
                if (i < 19) {
                    Console.SetCursorPosition(2, 31 + i);
                    Console.Write(i == 0 ? "╔" : side);
                    Console.SetCursorPosition(112, 31 + i);
                    Console.Write(i == 0 ? "╗" : side);
                }

                // Draw Top Side
                if (i < 15) {
                    Console.SetCursorPosition(2, 3 + i);
                    Console.Write(i != 14 ? side : "╚");
                    Console.SetCursorPosition(112, 3 + i);
                    Console.Write(i != 14 ? side : "╝");

                    if (i < 6) {
                        // Draw left big borders
                        if (i > 0 && i < 5) {
                            Console.SetCursorPosition(24, 17 + i);
                            Console.Write(leftThickDivision[i]);
                            Console.SetCursorPosition(24, 26 + i);
                            Console.Write(leftThickDivision[i]);
                        } else if (i == 0 || i == 5) {
                            Console.SetCursorPosition(3, 17 + i);
                            Console.Write(leftThickDivision[i]);
                            Console.SetCursorPosition(3, 26 + i);
                            Console.Write(leftThickDivision[i]);
                        }
                        // Draw right big border
                        Console.SetCursorPosition(90, 17 + i);
                        Console.Write(rightThickDivision[i]);
                        Console.SetCursorPosition(90, 26 + i);
                        Console.Write(rightThickDivision[i]);
                    }

                    // Draw PackMan Title
                    if (i < 4) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(2, 18 + i);
                        Console.Write(packString[i]);
                        Console.SetCursorPosition(91, 18 + i);
                        Console.Write(manString[i]);
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
