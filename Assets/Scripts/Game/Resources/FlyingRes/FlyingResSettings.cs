using UnityEngine;

namespace Game.Resources
{
    [CreateAssetMenu(fileName = nameof(FlyingResSettings), menuName = "SO/Resources/" + nameof(FlyingResSettings))]
    public class FlyingResSettings : ScriptableObject
    {
        public float SideOffset;
        public float FlyingTime;
        [Range(0f,1f)] public float CurveInflectionPos;
        public float CurveInflectionHeight;
        public int OscillationsSpeed;
        public float OscillationsMagnitude;

    }
}