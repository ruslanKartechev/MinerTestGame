#if UNITY_EDITOR
using Levels;
using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    [CustomEditor(typeof(LevelManager))]
    public class LevelManagerEditor : Editor
    {
        private LevelManager me;
        private int _switchButtonWidth = 100;
        private int _bigButtonWidth = 200;
        
        private void OnEnable()
        {
            me = target as LevelManager;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("<<", GUILayout.Width(_switchButtonWidth)))
            {
                me.DebugModePrev();
            }
            if (GUILayout.Button(">>", GUILayout.Width(_switchButtonWidth)))
            {
                me.DebugModeNext();
            }
            GUILayout.EndHorizontal();
 
            
        }
    }
}
#endif