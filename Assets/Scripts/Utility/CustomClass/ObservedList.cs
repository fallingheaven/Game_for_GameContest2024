using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utility.CustomClass
{
    [System.Serializable]
    public class ObservedList<T> : List<T>
    {
        public UnityAction onChanged;
        public UnityAction<T> onItemAdded;
        public UnityAction<T> onItemRemoved;

        #region 重载方法

            public new void Add(T item)
            {
                base.Add(item);
                OnChanged();
                OnItemAdded(item);
            }

            public new void Remove(T item)
            {
                base.Remove(item);
                OnChanged();
                OnItemRemoved(item);
            }

            public new void RemoveAt(int index)
            {
                base.RemoveAt(index);
                OnChanged();
            }

        #endregion    
        

        private void OnChanged()
        {
            onChanged?.Invoke();
        }

        private void OnItemAdded(T item)
        {
            onItemAdded?.Invoke(item);
        }
        
        private void OnItemRemoved(T item)
        {
            onItemRemoved?.Invoke(item);
        }

        public static ObservedList<T> ArrayToList(T[] array)
        {
            ObservedList<T> newList = new ObservedList<T>();
            foreach (var item in array)
            {
                newList.Add(item);
            }
            
            return newList;
        }
    }
}
