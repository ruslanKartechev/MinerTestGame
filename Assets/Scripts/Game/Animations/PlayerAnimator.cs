using UnityEngine;

namespace Game.Animations
{
    public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
    {
        public enum Animation
        {
            Idle = 0,
            Run = 1
        }
        
        [SerializeField] private Animator _animator;
        private static readonly int Run = Animator.StringToHash("Run");
        private Animation _currentAnim;

        public void PlayIdle()
        {
            // if(_currentAnim != Animation.Idle)
                _animator.SetBool(Run, false);    
        }

        public void PlayRun()
        {
            // if(_currentAnim != Animation.Run)
                _animator.SetBool(Run, true);
        }
        
    }
}