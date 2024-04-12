using UnityEngine;

namespace Utility.CustomClass
{
    public class Singleton<T> : MonoBehaviour where T: Component 
    {
        private static T _instance;
    
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject($"{typeof(T).Name}");
                    _instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);
                    
                    Debug.Log($"{typeof(T).Name}单例生成完成");
                }

                return _instance;
            }
            set => _instance = value;
        }
    }
}
