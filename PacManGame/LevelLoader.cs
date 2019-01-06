using System.IO;

namespace PacManGame {
    /// <summary>
    /// This Class is responsible for loading the level using the given files
    /// </summary>
    class LevelLoader {
        /** \brief Creates a new empty string */
        private readonly string path = @"";
        /** \brief Creates a new string with the file name */
        private readonly string level = "PacMan_Level.txt";
        /** \brief Creates a new string with the file name */
        private readonly string points = "Points.txt";
        /** \brief TCreates a new string array that'll save the information about the level Walls */
        public string[] LevelSprite { get; private set; } = new string[49];
        /** \brief Creates a new string array that'll save the information about the level Points */
        public string[] LevelPoints { get; private set; } = new string[49];

        /// <summary>
        /// LevelLoader Constructor executes some needed methods when called
        /// </summary>
        public LevelLoader() {

            LoadLevel(level); // Loads the  level using the level string as a path
            LoadLevel(points); // Loads the  level using the points string as a path
        }

        /// <summary>
        /// Loads stuff into the string arrays depending on witch file was passed
        /// </summary>
        /// <param name="file">Represents witch fille to read from</param>
        private void LoadLevel(string file) {
            // Iniciates a new StreamReader to read from the wanted file
            using(StreamReader sr = new StreamReader(path + file)) {
                string line; // Creates a new string
                // Ask if the first house on the LevelSprite array is null
                if (LevelSprite[0] == null) { 
                    // If so, executes a for loop to pass all the information from the file...
                    for (int i = 0; (line = sr.ReadLine()) != null; i++) {
                        LevelSprite[i] = line; // ...into a specific house of the array
                    }
                } else {
                    // If not, does the same thing but now saves all the info...
                    for (int i = 0; (line = sr.ReadLine()) != null; i++) {
                        LevelPoints[i] = line; // ...into the LevelPoints array
                    }
                }
            }
        }
    }
}
