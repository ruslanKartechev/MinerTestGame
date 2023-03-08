using System.Collections.Generic;
using UnityEngine;

namespace Game.Resources
{
    [DefaultExecutionOrder(-100)]
    [CreateAssetMenu(fileName = nameof(ResourcesInventory), menuName = "SO/Repository/" + nameof(ResourcesInventory))]
    public class ResourcesInventory : ScriptableObject, IResourceInventory
    {
        [SerializeField] private List<Resource> _startInventory;
        
        private Dictionary<string, Resource> _inventoryTable;
        public Dictionary<string, Resource>.ValueCollection InventoryResources => _inventoryTable.Values;
        
        public void SetResource(Resource res)
        {
            if (_inventoryTable.ContainsKey(res.ID) == false)
            {
                _inventoryTable.Add(res.ID, res);
                return;
            }
            _inventoryTable[res.ID].Amount = res.Amount;
        }
        
        public void AddResource(string resID, int count)
        {
            if (_inventoryTable.ContainsKey(resID) == false)
            {
                _inventoryTable.Add(resID, new Resource(resID, count));
                return;
            }
            _inventoryTable[resID].Amount.Value += count;
        }

        public void RemoveResource(string resID, float amount)
        {
            if (_inventoryTable.ContainsKey(resID) == false)
            {
                // throw new System.Exception($"No resource with id: {resID} was stored in inventory");
                return;
            }
            var val = _inventoryTable[resID].Amount.Value;
            val -= amount;
            if (val < 0)
                val = 0;
            _inventoryTable[resID].Amount.Value = val;
        }

        public void SetAmount(string resID, float amount)
        {
            if (amount < 0)
                amount = 0;
            if (_inventoryTable.ContainsKey(resID) == false)
            {
                _inventoryTable.Add(resID, new Resource(resID, amount));
                return;
            }
            _inventoryTable[resID].Amount.Value = amount;
        }

        public float GetAmount(string id)
        {
            if (_inventoryTable.ContainsKey(id) == false)
                return 0;
            
            return _inventoryTable[id].Amount.Value;
        }
        
        private void OnEnable()
        {
            _inventoryTable = new Dictionary<string, Resource>();
            foreach (var inv in _startInventory)
            {
                _inventoryTable.Add(inv.ID, inv);
            }   
        }

        public void DebugInventory()
        {
            foreach (var res in _inventoryTable)
            {
                Debug.Log($"ID: {res.Value.ID}, Amount: {res.Value.Amount.Value}");
            }
        }
    }
}