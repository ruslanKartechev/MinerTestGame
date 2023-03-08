using System;
using Game.Resources;
using UnityEngine;

namespace Game.Collectables
{
    public class CollectableResource : MonoBehaviour, ICollectable
    {
        public event Action OnCollected;
        
        public string ID;
        public int Amount;
        [SerializeField] private ResourceCollectEffect _effect;
        private bool _isCollectable;

        public bool IsCollectable
        {
            get => _isCollectable;
        }

        public void Collect(IResourceInventory inventory)
        {
            inventory.AddResource(ID, Amount);
            _isCollectable = false;
            _effect.PlayOnCollected();
            OnCollected?.Invoke();
        }

        public void SetCollectable(bool isCollectable)
        {
            _isCollectable = isCollectable;
        }
        

    }
}