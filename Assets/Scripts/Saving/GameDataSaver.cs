using System.Collections.Generic;
using System.IO;
using Game.Main;
using Game.Resources;
using Helpers;
using UnityEngine;

namespace Saving
{
    [CreateAssetMenu(fileName = nameof(GameDataSaver), menuName = "SO/" + nameof(GameDataSaver))]
    public class GameDataSaver : DataSaver
    {
        private const string FileName = "SavedGameData";
        
        public ResourcesInventory Inventory;
        public GlobalData GlobalData;

        private GameData _loadedData;
        public override GameData LoadedData => _loadedData;
        private string Path => Application.persistentDataPath + "/" + FileName;
        
        public override void SaveData()
        {
            var data = new GameData();
            data.CurrentLevelIndex = GlobalData.CurrentLevel;
            data.TotalLevelsPassed = GlobalData.TotalLevelsPassed;
            data.Resources = new List<GameData.ResourceData>();
            foreach (var res in Inventory.InventoryResources)
            {
                data.Resources.Add(new GameData.ResourceData(res.ID, res.Amount.Value));
            }
            var jsonString = JsonUtility.ToJson(data);
            File.WriteAllText(Path, jsonString);
        }

        public override void LoadData()
        {
            if (File.Exists(Path))
            {
                var fileContents = File.ReadAllText(Path);
                _loadedData = JsonUtility.FromJson<GameData>(fileContents);
                foreach (var data in _loadedData.Resources)
                    Inventory.SetAmount(data.ID, data.Count);
            }
            else
            {
                _loadedData = new GameData();
                foreach (var data in Inventory.InventoryResources)
                    Inventory.SetAmount(data.ID, 0);
            }
            GlobalData.CurrentLevel = _loadedData.CurrentLevelIndex;
            GlobalData.TotalLevelsPassed = _loadedData.TotalLevelsPassed;
        }

        #if UNITY_EDITOR
        public void DebugPath()
        {
            Dbg.LogGreen("Saved FILE:   " + Path);
            Dbg.LogGreen("PDT:  " + Application.persistentDataPath);
            
        }
        #endif
    }
}