using System.Collections.Generic;
using UnityEngine;

namespace Game.Resources
{
    [CreateAssetMenu(fileName = nameof(ResourceNameRepository), menuName = "SO/Repository/" + nameof(ResourceNameRepository))]
    public class ResourceNameRepository : ScriptableObject
    {
        [SerializeField]
        private List<NameData> _data;
        private Dictionary<string, NameData> _namesTable;

        public void CreateTable()
        {
            _namesTable = new Dictionary<string, NameData>();
            foreach (var icon in _data)
            {
                _namesTable.Add(icon.ID, icon);
            }
        }
        
        public string GetName(string id)
        {
            return _namesTable[id].Name;
        }        

        [System.Serializable]
        public class NameData
        {
            public string ID;
            public string Name;
        }
    }
}