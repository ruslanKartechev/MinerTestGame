using UnityEngine;

namespace Game.Spots
{
    [CreateAssetMenu(fileName = nameof(SimpleSpotSettings), menuName = "SO/Spot/" + nameof(SimpleSpotSettings))]
    public class SimpleSpotSettings : ScriptableObject
    {
        public string InputResource;
        public int InputCount;
        public float SingleInputDelay;
        [Space(10)] 
        public float ProductionDuration;
        [Space(10)]
        public string OutputResource;
        public int OutputCount;
        public float SingleOutputDelay;   
    }
}