using UnityEngine;

namespace Game.Main
{
    [CreateAssetMenu(fileName = nameof(GlobalData), menuName = "SO/" + nameof(GlobalData))]
    public class GlobalData : ScriptableObject
    {
        public float DataSavePeriod = 10;
        [Header("UI")] 
        public float UIJoystickFadeDuration = 0.3f;
        public float UIResourceMoveDuration = 0.5f;
        public float UIResourceScaleDuration = 0.5f;
        [Space(20)]
        public int CurrentLevel;
        public int TotalLevelsPassed;
    }
}