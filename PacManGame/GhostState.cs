namespace PacManGame {
    /// <summary>
    /// Enumerator to track the Ghost State
    /// </summary>
    public enum GhostState {
        LeavingSpawn = 1,
        SearchPacMan = 2,
        FollowPacMan = 3,
        RunFromPacman = 4,
        ReturnToSpawn = 5
    }
}
