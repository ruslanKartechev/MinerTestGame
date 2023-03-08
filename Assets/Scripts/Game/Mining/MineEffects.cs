using UnityEngine;

namespace Game.Mining
{
    public class MineEffects : MonoBehaviour
    {
        [SerializeField] private Material _passiveMat;
        [SerializeField] private Material _activeMat;
        [SerializeField] private Renderer _renderer;

        public void ShowActive()
        {
            _renderer.sharedMaterial = _activeMat;
        }

        public void ShowPassive()
        {
            _renderer.sharedMaterial = _passiveMat;
        }
    }
}