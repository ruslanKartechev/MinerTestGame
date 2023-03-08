using Cinemachine;
using UnityEngine;

namespace Game.Cam
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        // [SerializeField]
        
        public void SetTarget(Transform target)
        {
            _virtualCamera.LookAt = target;
            _virtualCamera.Follow = target;
        }

        public bool IsActive
        {
            get => _virtualCamera.enabled;
            set
            {
                _virtualCamera.enabled = value;
            }
        }



    }
}