using System.Collections.Generic;
using UnityEngine.Events;

namespace Utility.CustomClass
{
    [System.Serializable]
    public class ObservedList<T> : List<T>
    {
        public UnityAction onChanged;
        
        public new void Add(T item)
        {
            base.Add(item);
            OnChanged();
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            OnChanged();
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            OnChanged();
        }

        private void OnChanged()
        {
            onChanged?.Invoke();
        }
    }
}
