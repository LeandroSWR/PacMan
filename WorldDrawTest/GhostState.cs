using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldDrawTest {
    public enum GhostState {

        LeavingSpawn = 1,
        SearchPacMan = 2,
        FollowPacMan = 3,
        RunFromPacman = 4,
        ReturnToSpawn = 5
    }
}
