using UnityEngine;

namespace Game.Mining
{
    public abstract class BaseMine : MonoBehaviour
    {
        public abstract string Resource { get; set; }
        
        public abstract bool IsAvailable { get; }
        
        public abstract float MiningDuration { get; }
        
        public abstract void StartMining();
        public abstract void StopMining();
    }
}