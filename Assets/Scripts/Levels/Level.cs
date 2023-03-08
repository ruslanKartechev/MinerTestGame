using UnityEngine;

namespace Levels
{
    public abstract class Level : MonoBehaviour
    {
        public abstract void InitLevel();
        public abstract void StartLevel();
        
    }
}