using System;
using System.Collections.Generic;

namespace GameMode.LevelGame
{
    [Serializable]
    public class LevelConfigs
    {
        public List<LevelConfig> levels;
        public List<LevelBoardConfig> levelBoards;
        public Dictionary<int, LevelNewBlockConfig> levelNewBlock;
    }

    [Serializable]
    public class LevelConfig
    {
        public List<LevelGoalConfig> goal;
        public int blockCount;
        public int boardIndex;
        public int roundCount;
    }
    
    [Serializable]
    public class LevelGoalConfig
    {
        public string type;
        public int count;
    }

    [Serializable]
    public class LevelBoardConfig
    {
        public Dictionary<string, string> blocks;
    }
    
    [Serializable]
    public class LevelNewBlockConfig
    {
        public string type;
        public string blockName;
        public string des;
        public string path;
        public string color;
    }
}