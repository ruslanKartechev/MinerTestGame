using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace SceneSwitching
{
    public class SceneSwitcher : MonoBehaviour
    {
        private Coroutine _loading;
        private const string ScenesPath = "Assets/Scenes/";
        public void LoadGame(Action onDone)
        {
            DontDestroyOnLoad(gameObject);
            LoadScene(SceneNames.LoadingScene, onDone);
        }

        public void LoadLevel(int level, Action onLoaded)
        {
            var name = SceneNames.LevelPrefix + level.ToString();
            LoadScene(name, onLoaded);
        }
        
        public void LoadScene(string sceneName, Action onLoaded)
        {
            if (sceneName == SceneManager.GetActiveScene().name)
            {
                Debug.Log($"Already loaded: {sceneName}");
                onLoaded.Invoke();
                return;
            }

#if UNITY_EDITOR
            if (Application.isPlaying == false)
            {
                EditorSceneManager.OpenScene(ScenesPath + sceneName + ".unity");
                return;
            }            
#endif
            if(_loading != null)
                StopCoroutine(_loading);
            _loading = StartCoroutine(Loading(sceneName, onLoaded));
        }

        private IEnumerator Loading(string sceneName, Action onLoaded)
        {
            var loading = SceneManager.LoadSceneAsync(sceneName);
            loading.allowSceneActivation = true;
            while (loading.isDone == false)
            {
                yield return null;
            }
            onLoaded.Invoke();
        }
        
    }
}
