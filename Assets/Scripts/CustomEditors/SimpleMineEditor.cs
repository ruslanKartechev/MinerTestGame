using System;
using Game.Mining;
using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    [CustomEditor(typeof(SimpleMine))]
    public class SimpleMineEditor : Editor
    {
        private SimpleMine me;

        private void OnEnable()
        {
            me = target as SimpleMine;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("StartMining"))
            {
                me.StartMining();
            }
            if (GUILayout.Button("Stop"))
            {
                me.StopMining();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("StartRecovery"))
            {
                me.StartRecovery();
            }
            if (GUILayout.Button("FullRecover"))
            {
                me.FullRecover();
            }
            GUILayout.EndHorizontal();
            
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Hit"))
            {
                me.Hit();
            }
            if (GUILayout.Button("Reset"))
            {
                me.Reset();
            }

            GUILayout.EndHorizontal();

        }
    }
}