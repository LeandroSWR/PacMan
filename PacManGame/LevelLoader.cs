using System.IO;

namespace PacManGame {
    class LevelLoader {
        private readonly string path = @"";
        private readonly string level = "PacMan_Level.txt";
        private readonly string points = "Points.txt";
        

        public string[] LevelSprite { get; private set; } = new string[49];

        public string[] LevelPoints { get; private set; } = new string[49];

        public LevelLoader() {
            LoadLevel(level);
            LoadLevel(points);
        }

        private void LoadLevel(string file) {
            using(StreamReader sr = new StreamReader(path + file)) {
                string line;
                if (LevelSprite[0] == null) {
                    for (int i = 0; (line = sr.ReadLine()) != null; i++) {
                        LevelSprite[i] = line;
                    }
                } else {
                    for (int i = 0; (line = sr.ReadLine()) != null; i++) {
                        LevelPoints[i] = line;
                    }
                }
                
            }
        }
    }
}
