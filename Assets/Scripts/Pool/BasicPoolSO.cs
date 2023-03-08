using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pool
{
    public class BasicPoolSO<T> : ScriptableObject, IPool<T> where T : MonoBehaviour
    {
        public Transform Parent;
        [SerializeField] protected int _count;
        [SerializeField] protected T Prefab;

        protected Dictionary<IPooledObject<T>, bool> _table = new Dictionary<IPooledObject<T>, bool>();

        public int Count
        {
            get => _count;
            set => _count = value;
        }
        
        public void Init()
        {
            for (int i = 0; i < _count; i++)
            {
                var go = Instantiate(Prefab, Parent);
                go.name = go.name + $"_i";
                var pooledObject = go.GetComponent<IPooledObject<T>>();
                pooledObject.Init(this);
                _table.Add(pooledObject, true);
            }
        }

        public IPooledObject<T> GetItem()
        {
            var pair = _table.First(t => t.Value);
            _table[pair.Key] = false;
            return pair.Key;
        }

        public void ReturnItem(IPooledObject<T> item)
        {
            _table[item] = true;
        }

        public void CollectAllBack()
        {
            foreach (var pair in _table)
            {
                pair.Key.CollectBack();
                _table[pair.Key] = true;
            }
        }
    }
}