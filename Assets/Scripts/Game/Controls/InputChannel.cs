using System;
using UnityEngine;

namespace Game.Controls
{
    [CreateAssetMenu(fileName = nameof(InputChannel), menuName = "SO/Channels/" + nameof(InputChannel))]
    public class InputChannel : ScriptableObject
    {
        public event Action<Vector2, float> Move;
        public event Action Stop;
        public event Action<Vector2> Tap;
        
        public void RaiseMove(Vector2 dir, float force)
        {
            Move.Invoke(dir, force);
        }
        
        public void RaiseTap(Vector2 pos)
        {
            Tap.Invoke(pos);
        }

        public void RaiseStop()
        {
            Stop.Invoke();
        }

        
    }
}