using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WorldDrawTest {
    class Program {
        static void Main(string[] args) {
            Console.CursorVisible = false;
            Console.SetWindowSize(136, 60);
            Level level = new Level();
            PacMan pacMan = new PacMan(51, 25, Direction.Right);
            KeyReader kR = new KeyReader();
            string currentInput = "";

            while (true) {
                pacMan.Plot();

                if (currentInput != kR.Input) {
                    currentInput = kR.Input;
                    switch (currentInput) {
                        case "Up":
                            pacMan.previousDirection = pacMan.direction;
                            pacMan.direction = Direction.Up;
                            break;
                        case "Right":
                            pacMan.previousDirection = pacMan.direction;
                            pacMan.direction = Direction.Right;
                            break;
                        case "Left":
                            pacMan.previousDirection = pacMan.direction;
                            pacMan.direction = Direction.Left;
                            break;
                        case "Down":
                            pacMan.previousDirection = pacMan.direction;
                            pacMan.direction = Direction.Down;
                            break;
                    }
                }

                Thread.Sleep(30);

                pacMan.UnPlot();

                pacMan.Move();
            }
        }
    }
}
