using System;
using UnityEngine;

namespace Game.Cam
{
    [CreateAssetMenu(fileName = nameof(CameraChannel), menuName = "SO/Channels/" + nameof(CameraChannel))]
    public class CameraChannel : ScriptableObject
    {
        public Action<Transform> SetCameraTarget;
        public Action<CameraShakeArg> Shake;
    }
}