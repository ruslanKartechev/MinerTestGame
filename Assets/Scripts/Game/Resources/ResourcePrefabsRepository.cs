using System.Collections.Generic;
using UnityEngine;

namespace Game.Resources
{
    [CreateAssetMenu(fileName = nameof(ResourcePrefabsRepository), menuName = "SO/Repository/" + nameof(ResourcePrefabsRepository))]
    public class ResourcePrefabsRepository : ScriptableObject
    {
        [SerializeField] private List<PrefabData> _prefabs;
        private Dictionary<string, PrefabData> _prefabTable;

        private void OnEnable()
        {
            CreateTable();
        }

        public void CreateTable()
        {
            _prefabTable = new Dictionary<string, PrefabData>();
            foreach (var icon in _prefabs)
            {
                _prefabTable.Add(icon.ID, icon);
            }
        }

        public ResourceView GetViewPrefab(string id)
        {
            return _prefabTable[id].Prefab;
        }
        
        [System.Serializable]
        public class PrefabData
        {
            public string ID;
            public ResourceView Prefab;
        }
    }
}