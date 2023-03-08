using Game.Cam;
using Game.Controls;
using GameUI;
using UnityEngine;
using UnityEngine.AI;

namespace Levels
{
    public class MinerLevel : Level
    {
        [SerializeField] private NavMeshData _navMesh;
        [Header("Player")]
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private PlayerMoveSettings _playerMoveSettings;
        [Header("Camera")] 
        [SerializeField] private CameraChannel _cameraChannel;
        [SerializeField] private UIChannel _uiChannel;
        
        public override void InitLevel()
        {
            NavMesh.RemoveAllNavMeshData();
            NavMesh.AddNavMeshData(_navMesh);
            SpawnPlayer();
            _uiChannel.ShowHUD?.Invoke();
        }

        public override void StartLevel()
        {
            Debug.Log("start level");
            
        }

        private void SpawnPlayer()
        {
            var instance = Instantiate(_player, _playerPosition.position, _playerPosition.rotation, _playerPosition);
            var mover = instance.GetComponent<PlayerMoveController>();
            mover.SetupMover(_playerMoveSettings);
            _cameraChannel.SetCameraTarget.Invoke(instance.transform);

        }
    }
}