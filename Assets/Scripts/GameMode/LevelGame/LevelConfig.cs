using System;
using System.Collections.Generic;

namespace GameMode.LevelGame
{
    [Serializable]
    public class LevelConfigs
    {
        public List<LevelConfig> levels;
    }

    [Serializable]
    public class LevelConfig
    {
        public List<LevelGoalConfig> goal;
        public int blockCount;
        public float time;
    }
    
    [Serializable]
    public class LevelGoalConfig
    {
        public string type;
        public int count;
    }
}