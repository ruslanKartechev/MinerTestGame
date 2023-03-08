using System.Collections.Generic;
using Game.Resources;
using UnityEngine;

namespace GameUI
{
    public class HUDPage : UIPage
    {
        [SerializeField] private ResourcesInventory _inventory;
        [SerializeField] private List<ResourceUIBlock> _blocks;
        [SerializeField] private ResourceBlockPositioner _resourceBlockPositioner;
        
        
        public override void Show(bool animated = true)
        {
            IsOpen = true;
            UpdateBlocks();
        }

        public override void Hide(bool animated = true)
        {
            IsOpen = false;
        }

        public void UpdateBlocks()
        {
            var i = 0;
            foreach (var res in _inventory.InventoryResources)
            {
                if(_blocks.Count <= i)
                    return;
                var block = _blocks[i];
                block.InitFor(res);
                i++;
            }
            
            _resourceBlockPositioner.RepositionAll();
        }
        
    }
}