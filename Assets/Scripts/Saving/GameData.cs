using System.Collections.Generic;

namespace Saving
{
    [System.Serializable]
    public class GameData
    {
        public List<ResourceData> Resources;
        public int CurrentLevelIndex;
        public int TotalLevelsPassed;

        public GameData()
        {
            CurrentLevelIndex = TotalLevelsPassed = 0;
            Resources = new List<ResourceData>();
        }
        
        
        
        
        [System.Serializable]
        public struct ResourceData
        {
            public ResourceData(string id, float count)
            {
                ID = id;
                Count = count;
            }

            public string ID;
            public float Count;
        }
    }
}