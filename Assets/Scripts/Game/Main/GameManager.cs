using Levels;
using Saving;
using UnityEngine;

namespace Game.Main
{
    [DefaultExecutionOrder(1)]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private DataSaver _dataSaver;
        [SerializeField] private LevelManager _levelManager;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _dataSaver.LoadData();
            Input.simulateMouseWithTouches = true;
            _levelManager.LoadLast();
        }

        private void OnApplicationQuit()
        {
            _dataSaver.SaveData();
        }
    }

}
