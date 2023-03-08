using UnityEngine;

namespace Game.Resources
{
    public abstract class ResourceView : MonoBehaviour
    {
        public abstract void Drop(Vector3 toPosition); 
    }
}