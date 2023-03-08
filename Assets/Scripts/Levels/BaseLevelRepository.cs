using UnityEngine;

namespace Levels
{
    public abstract class BaseLevelRepository : ScriptableObject
    {
        public abstract Level GetLevel(int index);
        public abstract int GetTotalCount();
    }
}