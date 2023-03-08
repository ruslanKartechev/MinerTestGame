using System.Collections.Generic;
using UnityEngine;

namespace Game.Resources
{
    [CreateAssetMenu(fileName = nameof(ResourceIconsRepository), menuName = "SO/Repository/" + nameof(ResourceIconsRepository))]
    public class ResourceIconsRepository : ScriptableObject
    {
        [SerializeField] private List<IconData> _icons;
        private Dictionary<string, IconData> _iconsTable;

        private void OnEnable()
        {
            CreateTable();
        }

        public void CreateTable()
        {
            _iconsTable = new Dictionary<string, IconData>();
            foreach (var icon in _icons)
            {
                _iconsTable.Add(icon.ID, icon);
            }
        }
        
        public Sprite GetIcon(string id)
        {
            return _iconsTable[id].Icon;
        }


        [System.Serializable]
        public class IconData
        {
            public string ID;
            public Sprite Icon;
        }
    }
}