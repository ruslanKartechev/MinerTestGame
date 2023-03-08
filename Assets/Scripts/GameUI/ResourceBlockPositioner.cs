using System.Collections.Generic;
using DG.Tweening;
using Game.Main;
using UnityEngine;

namespace GameUI
{
    public class ResourceBlockPositioner : MonoBehaviour
    {
        public List<Transform> _spots;
        public List<ResourceUIBlock> _blocks;
        public GlobalData _data;
        private Dictionary<ResourceUIBlock, int> _blocksPosIndices;
        private int _activeCount;

        public void RepositionAll()
        {
            _blocksPosIndices = new Dictionary<ResourceUIBlock, int>();
            for(var i =0; i < _blocks.Count; i++)
            {
                _blocks[i].Positioner = this;
                if (_blocks[i].IsActive)
                {
                    _blocks[i].transform.localPosition = _spots[_activeCount].localPosition;
                    _blocksPosIndices.Add(_blocks[i], _activeCount);
                    _activeCount++;
                }
                else
                {
                    _blocksPosIndices.Add(_blocks[i], -1);
                }
            }
        }

        public void AddToGrid(ResourceUIBlock block)
        {
            var nextBlockInd = _blocks.IndexOf(block) + 1;
            var placeInd = 0;
            var isLast = true;
            for (var i = nextBlockInd; i < _blocks.Count; i++)
            {
                var b = _blocks[i];
                if(b.IsActive == false)
                    continue;
                isLast = false;
                var itsInd = _blocksPosIndices[b];
                if (nextBlockInd == i)
                    placeInd = itsInd;
                Debug.Log($"i: {i}, itsInd before: {itsInd}");
                itsInd++;
                _blocksPosIndices[b] = itsInd;
                b.transform.DOLocalMove(_spots[itsInd].localPosition, _data.UIResourceMoveDuration);
            }
            if (isLast)
                placeInd = _activeCount;
            _activeCount++;
            _blocksPosIndices[block] = placeInd;
            block.transform.localPosition = _spots[placeInd].localPosition;
        }

        public void RemoveFromGrid(ResourceUIBlock block)
        {
            var nextBlockInd = _blocks.IndexOf(block) + 1;
            _blocksPosIndices[block] = -1;
            _activeCount--;
            for (var i = nextBlockInd; i < _blocks.Count; i++)
            {
                var b = _blocks[i];
                if(b.IsActive == false)
                    continue;
                var itsInd = _blocksPosIndices[b];
                itsInd--;
                _blocksPosIndices[b] = itsInd;
                b.transform.DOLocalMove(_spots[itsInd].localPosition, _data.UIResourceMoveDuration);
            }
        }


    }
}