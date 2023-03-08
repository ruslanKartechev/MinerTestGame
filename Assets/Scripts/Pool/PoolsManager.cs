using System.Collections.Generic;
using Game.Resources;
using UnityEngine;

namespace Pool
{
    public class PoolsManager : MonoBehaviour
    {
        [SerializeField] private List<FlyingResPool> _resPools;
        [SerializeField] private List<DropResourcePool> _dropResourcePools;

        private void Start()
        {
            var p1 = GetNewParent(nameof(FlyingResPool) + "parent");
            foreach (var pool in _resPools)
            {
                pool.Parent = p1;
                pool.Init();
            }
            var p2 = GetNewParent(nameof(DropResourcePool) + "parent");
            foreach (var pool in _dropResourcePools)
            {
                pool.Parent = p2;
                pool.Init();
            }
        }

        private Transform GetNewParent(string name)
        {
            var go = new GameObject(name);
            go.transform.parent = transform;
            return go.transform;
        }
        
        
    }
}