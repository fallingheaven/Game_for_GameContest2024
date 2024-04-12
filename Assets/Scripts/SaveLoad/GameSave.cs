using System.Collections.Generic;
using Utility.CustomClass;

namespace SaveLoad
{
    [System.Serializable]
    public class GameSaveArray
    {
        public ObservedList<GameSave> saves = new ObservedList<GameSave>();
    }
    
    [System.Serializable]
    public class GameSave
    {
        public int levelIndex;
        public float playTime;
    
        public GameSave()
        {
            levelIndex = 0;
            playTime = 0f;
        }
        
        public void ResetSave()
        {
            levelIndex = 0;
            playTime = 0f;
        }
    }
}

