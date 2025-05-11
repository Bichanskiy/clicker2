using System;
using System.Collections.Generic;

namespace Configs.LevelConfig {
    [Serializable]
    public struct LevelData {
        public int Location;
        public int LevelNumber;
        public List<EnemySpawnData> Enemies;
        public int Reward;
    }
}