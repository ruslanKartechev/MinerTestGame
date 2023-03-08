using Game.Resources;
using System;
namespace Game.Collectables
{
    public interface ICollectable
    {
        public event Action OnCollected;
        void Collect(IResourceInventory inventory);
        void SetCollectable(bool isCollectable);
        bool IsCollectable { get; }
        
    }
}