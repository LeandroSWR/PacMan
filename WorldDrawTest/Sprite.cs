using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldDrawTest {
    class Sprite {
        // PreMade PacMan Sprites
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
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        public readonly string[] gFrame1 = new string[3] {
            "▄███▄",
            "█@█@█",
            "█▀█▀█"
        };
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        public readonly string[] gFrame2 = new string[3] {
            "▄███▄",
            "█@█@█",
            "▀█▀█▀"
        };
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        public readonly string[] rFrame1 = new string[3] {
            "▄██@▄",
            "█████",
            "▀███▀"
        };
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        public readonly string[] rFrame2 = new string[3] {
            "▄█@█▀",
            "███  ",
            "▀███▄"
        };
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        public readonly string[] lFrame1 = new string[3] {
            "▄@██▄",
            "█████",
            "▀███▀"
        };
        public readonly string[] lFrame2 = new string[3] {
            "▀█@█▄",
            "  ███",
            "▄███▀"
        };
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        public readonly string[] uFrame1 = new string[3] {
            "@███▄",
            "█████",
            "▀███▀"
        };
        public readonly string[] uFrame2 = new string[3] {
            "▄   ▄",
            "@█▄██",
            "▀███▀"
        };
        // Create a new read only string array that contains what we need to
        //draw on the first animated frame of the ghost
        public readonly string[] dFrame1 = new string[3] {
            "▄███▄",
            "█████",
            "@███▀"
        };
        public readonly string[] dFrame2 = new string[3] {
            "▄███▄",
            "@█▀██",
            "▀   ▀"
        };
        public readonly string[] zero = new string[3] {
            "╔╗",
            "║║",
            "╚╝"
        };
        public readonly string[] one = new string[3] {
            " ╗",
            " ║",
            " ╩"
        };
        public readonly string[] two = new string[3] {
            "╔╗",
            "╔╝",
            "╚╝"
        };
        public readonly string[] tree = new string[3] {
            "╔╗",
            " ╣",
            "╚╝"
        };
        public readonly string[] four = new string[3] {
            "╗╗",
            "╚╣",
            " ╩"
        };
        public readonly string[] five = new string[3] {
            "╔╗",
            "╚╗",
            "╚╝"
        };
        public readonly string[] six = new string[3] {
            "╔╗",
            "╠╗",
            "╚╝"
        };
        public readonly string[] seven = new string[3] {
            "╔╗",
            " ║",
            " ╩"
        };
        public readonly string[] eight = new string[3] {
            "╔╗",
            "╠╣",
            "╚╝"
        };
        public readonly string[] nine = new string[3] {
            "╔╗",
            "╚╣",
            " ╩"
        };
    }
}
