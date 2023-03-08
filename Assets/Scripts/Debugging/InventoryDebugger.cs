using System;
using Game.Resources;
using UnityEngine;
using UnityEditor;
namespace Debugging
{
    public class InventoryDebugger : MonoBehaviour
    {
        public string _res;
        public int _addCount;
        public ResourcesInventory _inventory;

        public void RemoveAll()
        {
            var count = _inventory.GetAmount(_res);
            Debug.Log($"Removing: {_res}, count: {count}");
            _inventory.RemoveResource(_res, count);
        }

        public void AddRes()
        {
            _inventory.AddResource(_res, _addCount);

        }
    }


    #if UNITY_EDITOR
    [CustomEditor(typeof(InventoryDebugger))]
    public class InventoryDebuggerEditor : Editor
    {
        private InventoryDebugger me;

        private void OnEnable()
        {
            me = target as InventoryDebugger;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("RemoveRes"))
            {
                me.RemoveAll();   
            }
            if (GUILayout.Button("AddRes"))
            {
                me.AddRes();
            }
        }
    }
    #endif
}