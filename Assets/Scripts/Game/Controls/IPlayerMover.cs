using System;

namespace Game.Controls
{
    public interface IPlayerMover
    {
        public event Action OnMovementStarted;
        public event Action OnMovementStopped;
        public bool AllowMovement { get; set; }
        public bool IsMoving { get; }
    }
}