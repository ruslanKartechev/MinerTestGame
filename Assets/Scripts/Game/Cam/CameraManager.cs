using UnityEngine;

namespace Game.Cam
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CameraChannel _cameraChannel;
        [SerializeField] private CameraFollower _follower;
        [SerializeField] private CameraShaker _shaker;
        
        private bool _isActive;

        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                _follower.IsActive = value;
            }
        }

        private void OnEnable()
        {
            _cameraChannel.Shake = Shake;
            _cameraChannel.SetCameraTarget = SetTarget;
            IsActive = true;
        }
        

        public void SetTarget(Transform target)
        {
            _follower.SetTarget(target);
        }

        public void Shake(CameraShakeArg args)
        {
            Debug.Log($"Called to shake");
        }
        
        
    }
}