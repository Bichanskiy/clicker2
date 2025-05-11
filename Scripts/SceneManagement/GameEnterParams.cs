namespace SceneManagement
{
    public class GameEnterParams : SceneEnterParams
    {
        public int Location { get; }
        public int Level { get; }

        public GameEnterParams(int location, int level) : base("levelScene")
        {
            Location = location;
            Level = level;
        }
    }
}