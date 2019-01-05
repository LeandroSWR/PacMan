using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Input;

namespace WorldDrawTest {
    class KeyReader {
        private BlockingCollection<ConsoleKey> keyQueue;

        public string Input = "";
        Thread thread1;
        Thread thread2;

        public KeyReader() {
            thread1 = new Thread(GetInput);
            thread2 = new Thread(ReadFromQueue);

            keyQueue = new BlockingCollection<ConsoleKey>();
            thread1.Start();
            thread2.Start();
        }

        private void GetInput() {
            ConsoleKey pressedKey;
            do {
                pressedKey = Console.ReadKey(true).Key;
                keyQueue.Add(pressedKey);
            } while (pressedKey != ConsoleKey.Escape);
        }

        private void ReadFromQueue() {
            ConsoleKey currentKey;
            
            while ((currentKey = keyQueue.Take()) != ConsoleKey.Escape) {

                if (currentKey == ConsoleKey.Enter)
                    Input = "Enter";
                if (currentKey == ConsoleKey.W || currentKey == ConsoleKey.UpArrow)
                    Input = "Up";
                if (currentKey == ConsoleKey.A || currentKey == ConsoleKey.LeftArrow)
                    Input = "Left";
                if (currentKey == ConsoleKey.S || currentKey == ConsoleKey.DownArrow)
                    Input = "Down";
                if (currentKey == ConsoleKey.D || currentKey == ConsoleKey.RightArrow)
                    Input = "Right";
            }
        }
    }
}
