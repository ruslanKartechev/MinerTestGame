using System.Collections.Generic;
using Game.Resources;
using UnityEngine;

namespace Game.Spots
{
    public class SpotsDebugger : MonoBehaviour
    {
        public ResourcesInventory Inventory;
        public Transform From;
        public Transform To;
        [Space(20)]
        public bool DoCheck = true;
        public float MinDistance;
        
        private Spot[] _all;
        private HashSet<Spot> _active = new HashSet<Spot>();
        
        private void Start()
        {
            _all = FindObjectsOfType<Spot>();
        }

        public void Update()
        {
            if (!DoCheck || _all == null || _all.Length == 0)
                return;
            var max2 = MinDistance * MinDistance;
            foreach (var spot in _all)
            {
                if(spot == null)
                    continue;
                var d2 = (transform.position - spot.transform.position).sqrMagnitude;
                if (d2 <= max2)
                {
                    if (_active.Contains(spot) == false)
                    {
                        _active.Add(spot);
                        spot.Interact(new Spot.SpotWorkingArgs()
                        {
                            Inventory = Inventory,
                            TakeFrom =  From,
                            GiveTo = To
                        });
                    }
                }
                else
                {
                    if (_active.Contains(spot))
                    {
                        _active.Remove(spot);
                        spot.StopInteraction();
                    }
                }
                
            }
            
        }
    }
}