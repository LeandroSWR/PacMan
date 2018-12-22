using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldDrawTest {
    class LevelLoader {
        private string path = @"C:\Users\Shadow\Desktop\";
        private string level = "PacMan Level.txt";
        private string points = "Points.txt";
        

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
