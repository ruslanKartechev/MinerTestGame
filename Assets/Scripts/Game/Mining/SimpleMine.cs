using Effects;
using UnityEngine;

namespace Game.Mining
{
    public class SimpleMine : BaseMine
    {
        [SerializeField] private bool _recoverOnStart;
        [SerializeField] private string _resourceID;
        [SerializeField] private SimpleMineResourceDropper _dropper;
        [SerializeField] private SimpleMineSettings _settings;
        [SerializeField] private MineClock _mineClock;
        [SerializeField] private ShakerOneDim _shakerOneDim;
        [SerializeField] private MineEffects _mineEffects;
        
        private float _mineCountdown;
        private float _recoverCountdown;
        private bool _isMining;
        private int _hits;

        private bool _isAvailable;
        private bool _duration;
        
        public override bool IsAvailable => _isAvailable;
        public override float MiningDuration => _settings.MiningDuration;

        public override string Resource
        {
            get => _resourceID;
            set => _resourceID = value;
        }

        private void Start()
        {
            if(_recoverOnStart)
                FullRecover();
            _mineClock.Show();
            _dropper.ResourceID = _resourceID;
        }

        public override void StartMining()
        {
            if (!_isAvailable) 
                return;
            _mineClock.SetMiningMode();
            _mineClock.Show();
            _mineCountdown = _settings.MiningDuration;
            _isMining = true;
            _mineEffects.ShowActive();
        }

        public override void StopMining()
        {
            _isMining = false;
            _mineCountdown = 0;
            _mineClock.UpdateTime(0f);
            if (_isAvailable)
                _mineClock.SetAvailableMode();
            else
                _mineClock.SetRecoveryMode();
            _mineClock.Hide();
            _mineEffects.ShowPassive();
        }

        private void Update()
        {
            if (_isAvailable)
            {
                if (!_isMining) 
                    return;
                _mineCountdown -= Time.deltaTime;
                if (_mineCountdown <= 0)
                {
                    _mineCountdown = 0;
                    _mineClock.UpdateTime(_mineCountdown);
                    Hit();
                }
                else
                    _mineClock.UpdateTime(_mineCountdown);
            }
            else
            {
                _recoverCountdown -= Time.deltaTime;
                _mineClock.UpdateTime(_recoverCountdown);
                if (_recoverCountdown < 0)
                {
                    FullRecover();
                }
            }
        }

        public void Hit()
        {
            _shakerOneDim.StartShake1D();
            _dropper.Drop(_settings.DropCount);
            _hits++;
            _mineCountdown = _settings.MiningDuration;
            if (_hits >= _settings.HitsToStop)
            {
                StartRecovery();
            }
        }
        
        public void StartRecovery()
        {
            _mineClock.SetRecoveryMode();
            _isAvailable = false;
            _recoverCountdown = _settings.RecoveringTime;
        }

        public void FullRecover()
        {
            // Debug.Log($"mine: {gameObject.name} fully recovered");
            _hits = 0;
            _recoverCountdown = 0f;
            _isAvailable = true;
            if(_isMining)
                _mineClock.SetMiningMode();
            else
                _mineClock.SetAvailableMode();
        }
        
        public void Reset()
        {
            _hits = 0;
            _mineCountdown = _recoverCountdown = 0;
            _isAvailable = true;
            _isMining = false;
        }
    }
}