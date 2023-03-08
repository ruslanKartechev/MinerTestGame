using UnityEngine;

namespace GameUI
{
    [CreateAssetMenu(fileName = nameof(JoystickSettings), menuName = "SO/Settings/" + nameof(JoystickSettings))]
    public class JoystickSettings : ScriptableObject
    {
        [SerializeField] private float _range;
        [SerializeField] private float _sensitivity;

        public float Range => _range;
        public float Sensitivity => _sensitivity;
    }
}