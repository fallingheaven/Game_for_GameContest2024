using System.Collections.Generic;
using UnityEngine;
using Utility.CustomClass;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject objectPrefab;
    public int count;
}

public class ObjectPool : Singleton<ObjectPool>
{
    public ObservedList<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    private void OnEnable()
    {
        pools.onItemAdded += AddNewPool;
        
        InitObjectPool();
    }

    /// <summary>
    /// 初始化预设的角色池
    /// </summary>
    private void InitObjectPool()
    {
        foreach (var pool in pools)
        {
            AddNewPool(pool);
        }
    }

    /// <summary>
    /// 添加新类型的角色池
    /// </summary>
    /// <param name="newPool"></param>
    public void AddNewPool(Pool newPool)
    {
        if (poolDictionary.ContainsKey(newPool.tag))
        {
            Debug.LogWarning($"已存在{tag}类型对象池");
        }
        
        var objectPool = new Queue<GameObject>();

        for (var i = 1; i <= newPool.count; i++)
        {
            var newObject = Instantiate(newPool.objectPrefab, transform);
            newObject.SetActive(false);
            objectPool.Enqueue(newObject);
        }

        poolDictionary[newPool.tag] = objectPool;
    }

    /// <summary>
    /// 从角色池中取物体
    /// </summary>
    /// <param name="objTag"></param>
    /// <param name="position"></param>
    /// <param name="quaternion"></param>
    /// <returns></returns>
    public GameObject SpawnFromPool(string objTag, Vector3 position, Quaternion quaternion = default)
    {
        // 角色对象tag不存在
        if (!poolDictionary.ContainsKey(objTag))
        {
            Debug.LogWarning($"{objTag} 类型物体不存在于角色池中");
            return null;
        }

        var newObj = poolDictionary[objTag].Dequeue();

        // 处理角色池对象数量不够的情况
        if (newObj.activeSelf)
        {
            poolDictionary[objTag].Enqueue(newObj);
            
            var pool = pools.Find(p => p.tag == objTag);
            pool.count++;
            newObj = Instantiate(pool.objectPrefab, transform);
            newObj.SetActive(false);
        }

        newObj.transform.position = position;
        newObj.transform.rotation = quaternion;
        newObj.SetActive(true);

        poolDictionary[objTag].Enqueue(newObj);

        return newObj;
    }
}
