using System;
using System.Collections.Generic;
using Blocks;

namespace GameMode.LevelGame
{
    [Serializable]
    public class LevelConfigs
    {
        public List<LevelConfig> levels;
        public List<LevelBoardConfig> levelBoards;
    }

    [Serializable]
    public class LevelConfig
    {
        public List<LevelGoalConfig> goal;
        public int blockCount;
        public int boardIndex;
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
}