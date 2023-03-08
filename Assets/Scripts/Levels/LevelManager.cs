using Game.Main;
using SceneSwitching;
using UnityEngine;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        public bool DebugMode;
        public int DebugLevel;
        [Space(10)]
        public int MaxLevel;
        [Space(10)]
        [SerializeField] private Level _currentLevel;
        [SerializeField] private GlobalData _globalData;
        [SerializeField] private SceneSwitcher _sceneSwitcher;
        
        public Level CurrentLevel => _currentLevel;
        
        public void LoadLast()
        {
            Clear();
            if (DebugMode)
            {
                LoadLevel(CorrectLevelNum(DebugLevel));   
            }
            else
            {
                LoadLevel(CorrectLevelNum(_globalData.CurrentLevel));
            }
        }
        
        public void NextLevel()
        {
            Clear();
            _globalData.CurrentLevel = CorrectLevelNum( _globalData.CurrentLevel + 1);
            _globalData.TotalLevelsPassed++;
            LoadLevel(_globalData.CurrentLevel);
        }

        public void Reload()
        {
            Clear();
        }

        private int CorrectLevelNum(int val)
        {
            return Mathf.Clamp(val, 1, MaxLevel);
        }
        
        private void LoadLevel(int level)
        {
            _sceneSwitcher.LoadLevel(level, OnSceneLoaded);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void OnSceneLoaded()
        {
            _currentLevel = FindObjectOfType<Level>();
            if (Application.isPlaying)
            { 
                _currentLevel.InitLevel();
            }
        }
        
        public void Clear()
        {
       
        }

        public void ClearData()
        {
            _globalData.TotalLevelsPassed = _globalData.CurrentLevel = 0;
        }
        
        
        #if UNITY_EDITOR
        public void DebugModeNext()
        {
            DebugLevel = CorrectLevelNum(DebugLevel + 1);
            Clear();
            LoadLevel(DebugLevel);   
        }

        public void DebugModePrev()
        {
            DebugLevel--;
            if (DebugLevel < 1)
                DebugLevel = 1;
            Clear();
            LoadLevel(DebugLevel);
        }
        #endif
        
    }
}