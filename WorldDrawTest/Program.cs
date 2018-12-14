using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldDrawTest {
    class Program {
        static void Main(string[] args) {
            Console.SetWindowSize(136, 60);
            Level level = new Level();
            level.Plot();
        }
    }
}
