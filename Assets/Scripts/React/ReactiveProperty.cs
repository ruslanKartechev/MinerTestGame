using System;
using System.Collections.Generic;
using UnityEngine;

namespace React
{
    [System.Serializable]
    public class ReactiveProperty<T> where T : IEquatable<T>
    {
        [SerializeField] protected T val;
        protected List<Action<T>> _listeners = new List<Action<T>>();
        public bool UpdateChangeOnly;
        
        public T Value
        {
            get => val;
            set
            {
                if (UpdateChangeOnly)
                {
                    if (!val.Equals(value))
                    {
                        foreach (var listener in _listeners)
                        {
                            listener.Invoke(value);
                        }
                    }
                }
                else
                {
                    foreach (var listener in _listeners)
                    {
                        listener.Invoke(value);
                    }          
                }
                val = value;
            }
        }

        public ReactiveProperty()
        {
            Value = default;
            _listeners = new List<Action<T>>();
        }
        
        public ReactiveProperty(T start)
        {
            Value = start;
            _listeners = new List<Action<T>>();
        }

        public void SubOnChange(Action<T> listener)
        {
            _listeners.Add(listener);
        }
        
        public void UnsubOnChange(Action<T> listener)
        {
            _listeners.Remove(listener);
        }
        

    }
}