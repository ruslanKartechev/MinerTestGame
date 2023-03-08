using UnityEngine;
using System.Collections.Generic;

namespace Game.Spots
{
    public abstract partial class Spot : MonoBehaviour
    {
        public abstract ICollection<string> NeededResources { get; protected set; }
        public abstract void Interact(SpotWorkingArgs args);
        public abstract void StopInteraction();
    }
    
}