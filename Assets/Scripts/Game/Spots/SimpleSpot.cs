using System.Collections;
using System.Collections.Generic;
using Game.Resources;
using UnityEngine;

namespace Game.Spots
{
    public class SimpleSpot : Spot
    {
        [SerializeField] private SimpleSpotSettings _settings;
        [Space(10)] 
        [SerializeField] private SpotEffects _effects;
        [SerializeField] private SpotStateView _stateView;
        [Space(10)]
        [SerializeField] private Transform _inputTo;
        [SerializeField] private Transform _outputFrom;
        [Space(10)]
        [SerializeField] private FlyingResPool _woodPool;
        [SerializeField] private FlyingResPool _metalPool;
        
        private bool _isInteracting;
        private SpotWorkingArgs _currentArgs;
        private int _neededResCount;
        private int _producedCount;
        private Coroutine _interactingCor;
        private float _elapsedProduction;

        private ESpotPhase Phase { get; set; }

        private void Start()
        {
            _stateView.UpdateResourceCount(_neededResCount, _settings.InputCount);
        }

        public override ICollection<string> NeededResources
        {
            get => new string[] { _settings.InputResource };
            protected set{}
        }
        
        public override void Interact(SpotWorkingArgs args)
        {
            _isInteracting = true;
            if(_interactingCor != null)
                StopCoroutine(_interactingCor);
            _interactingCor = StartCoroutine(Interacting(args));
            _effects.ShowStartInteraction();
        }

        public override void StopInteraction()
        {
            _isInteracting = false;
            if(_interactingCor != null)
                StopCoroutine(_interactingCor);
            _effects.ShowStopInteraction();
            switch (Phase)
            {
                case ESpotPhase.Production:
                    _stateView.UpdateResourceCount(_settings.InputCount, _settings.InputCount);
                    break;
                default:
                    _stateView.UpdateResourceCount(_neededResCount, _settings.InputCount);
                    break;
            }
        }

        private IEnumerator Interacting(SpotWorkingArgs args)
        {
            while (_isInteracting)
            {
                switch (Phase)
                {
                    case ESpotPhase.Idle:
                        Phase = ESpotPhase.Input;
                        break;
                    case ESpotPhase.Input:
                        yield return Inputting(args);
                        break;
                    case ESpotPhase.Production:
                        yield return Production();
                        break;
                    case ESpotPhase.Output:
                        yield return Outputting(args);
                        break;
                }
                yield return null;
            }
        }

        private IEnumerator Inputting(SpotWorkingArgs args)
        {
            _stateView.UpdateResourceCount(_neededResCount, _settings.InputCount);
            var inv = args.Inventory;
     
            var requested = _neededResCount;
            while (requested < _settings.InputCount)
            {
                while (inv.GetAmount(_settings.InputResource) <= 0)
                    yield return null;
                inv.RemoveResource(_settings.InputResource,1);
                var res = _woodPool.GetItem().Object;
                res.FlyFromTo(args.TakeFrom, _inputTo, () =>
                {
                    _neededResCount++;
                    _stateView.UpdateResourceCount(_neededResCount, _settings.InputCount);
                    res.Hide();
                });
                requested++;
                yield return new WaitForSeconds(_settings.SingleInputDelay);
            }
            while (_neededResCount < _settings.InputCount)
                yield return null;
            Phase = ESpotPhase.Production;
        }

        private IEnumerator Outputting(SpotWorkingArgs args)
        {
            while (_producedCount > 0)
            {
                _producedCount--;
                var res = _metalPool.GetItem().Object;
                res.FlyFromTo(_outputFrom, args.GiveTo, () =>
                {
                    args.Inventory.AddResource(_settings.OutputResource, 1);
                    res.Hide();
                });
                yield return new WaitForSeconds(_settings.SingleOutputDelay);
            }
            Phase = ESpotPhase.Input;
        }

        private IEnumerator Production()
        {
            _stateView.ShowProduction();
            _effects.ShowProduction(_settings.ProductionDuration);
            if (_neededResCount >= _settings.InputCount)
            {
                while (_neededResCount >= _settings.InputCount)
                {
                    _neededResCount -= _settings.InputCount;
                    _producedCount++;
                    yield return WaitProduction();
                }   
            }
            else
                yield return WaitProduction();
            Phase = ESpotPhase.Output;
            _effects.ShowOutput();
        }

        private IEnumerator WaitProduction()
        {
            while (_elapsedProduction <= _settings.ProductionDuration)
            {
                _elapsedProduction += Time.deltaTime;
                yield return null;
            }
            _elapsedProduction = 0;
        }
        
    }
}