#if UNITY_EDITOR
using Game.Resources;
using UnityEditor;
using UnityEngine;

namespace CustomEditors
{
    [CustomEditor(typeof(ResourcesInventory))]
    public class ResourcesInventoryEditor : Editor
    {
        private ResourcesInventory me;

        private void OnEnable()
        {
            me = target as ResourcesInventory;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("DebugInventory"))
            {
                me.DebugInventory();
            }
        }
    }
}
#endif