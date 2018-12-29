using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldDrawTest {
    class MenuSymbols {

        public MenuSymbols() { }

        public readonly string[] packString = new string[4] {
            "╔══════╗╔═════╗╔═════╗",
            "║   ═  ║║  ═  ║║  ╔══╝",
            "║  ╔═══╝║ ╔═╗ ║║  ╚══╗",
            "╚══╝    ╚═╝ ╚═╝╚═════╝",
        };

        public readonly string[] manString = new string[4] {
            "╔═╗  ╔═╗╔═════╗╔═╗ ╔═╗",
            "║ ╚╗╔╝ ║║  ═  ║║ ╚╗╣ ║",
            "║ ╠╚╝╣ ║║ ╔═╗ ║║ ╠╚╗ ║",
            "╚═╝  ╚═╝╚═╝ ╚═╝╚═╝ ╚═╝",
        };

        public readonly string[] playString = new string[4] {
            "╔══════╗╔══╗  ╔═════╗╔╗   ╔╗",
           @"║   ═  ║║  ║  ║  ═  ║╚╗\ /╔╝",
            "║  ╔═══╝║  ╚═╗║ ╔═╗ ║ ╚╗ ╔╝",
            "╚══╝    ╚════╝╚═╝ ╚═╝  ╚═╝",
        };

        public readonly string[] quitString = new string[5] {
            "╔═════╗╔═╗ ╔═╗╔══╗╔═════╗",
            "║ ╔═╗ ║║ ║ ║ ║║  ║╚═╗ ╔═╝",
            "║ ╚═╝ ║║ ╚═╝ ║║  ║  ║ ║",
            "╚═══╗ ║╚═════╝╚══╝  ╚═╝",
            "    ╚═╝"
        };

        public readonly string[] selectionString = new string[2] {
            "████",
            "████"
        };

        public readonly string[] pacManString = new string[3] {
            "▀█@█▄",
            "  ███",
            "▄███▀"
        };

        public readonly string[] ghost1String = new string[3] {
            "▄███▄",
            "█@█@█",
            "█▀█▀█"
        };

        public readonly string[] ghost2String = new string[3] {
            "▄███▄",
            "█@█@█",
            "█▀█▀█"
        };

        public readonly string[] ghost3String = new string[3] {
            "▄███▄",
            "█@█@█",
            "█▀█▀█"
        };

        public readonly string[] ghost4String = new string[3] {
            "▄███▄",
            "█@█@█",
            "█▀█▀█"
        };
    }
}
