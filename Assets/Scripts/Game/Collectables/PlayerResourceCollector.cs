using Data.DTypes;
using Game.Resources;
using UnityEngine;

namespace Game.Collectables
{
    public class PlayerResourceCollector : MonoBehaviour
    {
        [SerializeField] private ResourcesInventory _inventory;

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case Tags.Resource:
                    var collectable = other.gameObject.GetComponent<ICollectable>();
                    if(collectable.IsCollectable)
                        collectable.Collect(_inventory);
                    break;
            }
        }
        
    }
}