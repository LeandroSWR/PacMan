using System;
using System.Collections.Concurrent;
using System.Threading;

namespace PacManGame {
    /// <summary>
    /// This Class is responsible for reading and regestering every keypress from the player
    /// </summary>
    class KeyReader {
        // Declares a new BlockingCollection of type "ConsoleKey"
        private BlockingCollection<ConsoleKey> keyQueue; 

        public string Input = ""; // Iniciates a new empty string
        private Thread thread1; // Declares a new thread
        private Thread thread2; // Declares a new thread

        /// <summary>
        /// KeyReader Constructor
        /// </summary>
        public KeyReader() {
            thread1 = new Thread(GetInput); // Initializes "thread1"
            thread2 = new Thread(ReadFromQueue); // Initializes "thread2"

            keyQueue = new BlockingCollection<ConsoleKey>(); // Initializes "keyQueue"
            thread1.Start(); // Stats "thread1"
            thread2.Start(); // Stats "thread2"
        }

        /// <summary>
        /// Get the input from the user
        /// </summary>
        private void GetInput() {
            ConsoleKey pressedKey;
            do {
                pressedKey = Console.ReadKey(true).Key;
                keyQueue.Add(pressedKey); // Adds the "pressedKey" into the "keyQueue"
            } while (true);
        }

        /// <summary>
        /// Reads the input from the BockingCollection "keyQueue"
        /// </summary>
        private void ReadFromQueue() {
            ConsoleKey currentKey; // Declares variable "currentKey"
            
            while (true) { // While "true" Do...
                // Sets the current key to be equal to the "pressedKey" taken from the "keyQueue"
                currentKey = keyQueue.Take(); 

                if (currentKey == ConsoleKey.Enter) // Asks if the "currentKey" is Enter
                    // If so...
                    Input = "Enter"; 
                // Asks if the "currentKey" is W or UpArrow
                if (currentKey == ConsoleKey.W || currentKey == ConsoleKey.UpArrow)
                    // If so...
                    Input = "Up";
                // Asks if the "currentKey" is A or LeftArrow
                if (currentKey == ConsoleKey.A || currentKey == ConsoleKey.LeftArrow)
                    // If so...
                    Input = "Left";
                // Asks if the "currentKey" is S or DownArrow
                if (currentKey == ConsoleKey.S || currentKey == ConsoleKey.DownArrow)
                    // If so...
                    Input = "Down";
                // Asks if the "currentKey" is D or RightArrow
                if (currentKey == ConsoleKey.D || currentKey == ConsoleKey.RightArrow)
                    // If so...
                    Input = "Right";
            }
        }
    }
}
