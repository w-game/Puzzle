using System;
using System.Collections.Generic;

namespace GameMode.LevelGame
{
    [Serializable]
    public class LevelConfigs
    {
        public List<LevelConfig> levels;
        public Dictionary<int, LevelNewBlockConfig> levelNewBlock;
    }

    [Serializable]
    public class LevelConfig
    {
        public List<LevelGoalConfig> goal;
        public int blockCount;
        public int roundCount;
        public Dictionary<string, string> blocks;
    }
    
    [Serializable]
    public class LevelGoalConfig
    {
        public string type;
        public int count;
    }

    [Serializable]
    public class LevelNewBlockConfig
    {
        public string type;
        public Dictionary<string, LevelNewBlockInfoConfig> info;
        public string path;
        public string color;
    }

    [Serializable]
    public class LevelNewBlockInfoConfig
    {
        public string blockName;
        public string des;
    }
}