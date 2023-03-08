using Game.Resources;
using UnityEngine;

namespace Game.Spots
{
    public abstract partial class Spot
    {
        [System.Serializable]
        public class SpotWorkingArgs
        {
            public IResourceInventory Inventory;
            public Transform TakeFrom;
            public Transform GiveTo;
        }
    }
}