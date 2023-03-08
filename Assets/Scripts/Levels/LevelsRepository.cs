using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelsRepository", menuName = "SO/Levels/LevelsRepository")]
    public class LevelsRepository : BaseLevelRepository
    {
        [SerializeField] private List<Level> _levels;

        public override int GetTotalCount() => _levels.Count;
        
        public override Level GetLevel(int index)
        {
            if (index >= _levels.Count)
            {
                Debug.Log($"Index > levels count. Returning last level");
                return _levels[_levels.Count - 1];
            }
            return _levels[index];
        }

    }
}