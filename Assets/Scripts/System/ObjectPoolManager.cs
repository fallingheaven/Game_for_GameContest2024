using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Utility.CustomClass;
using Utility.Interface;
using Object = UnityEngine.Object;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public Dictionary<string, IObjectPool> poolDictionary = new ();

    /// <summary>
    /// 添加新类型的角色池
    /// </summary>
    /// <param name="newPool"></param>
    public void AddNewPool<T>(ObjectPool<T> newPool) where T: class, new()
    {
        if (poolDictionary.ContainsKey(newPool.tag))
        {
            Debug.LogWarning($"已存在{tag}类型对象池");
        }

        poolDictionary[newPool.tag] = newPool;
    }

    /// <summary>
    /// 从角色池中取物体
    /// </summary>
    /// <param name="objTag"></param>
    /// <param name="position"></param>
    /// <param name="quaternion"></param>
    /// <returns></returns>
    public object SpawnFromPool(string objTag, Vector3 position, Quaternion quaternion = default)
    {
        // 角色对象tag不存在
        if (!poolDictionary.ContainsKey(objTag))
        {
            Debug.LogWarning($"{objTag} 类型物体不存在于角色池中");
            return null;
        }

        var pool = poolDictionary[objTag];
        var newObj = pool.Dequeue();

        if (newObj is GameObject obj)
        {
            // 处理角色池对象数量不够的情况
            if (obj.activeSelf)
            {
                pool.Enqueue(newObj);
                
                pool.count++;
                obj = Object.Instantiate(obj, ObjectPoolManager.Instance.transform);
                obj.SetActive(false);
            }
            
            obj.transform.position = position;
            obj.transform.rotation = quaternion;
            obj.SetActive(true);

            return newObj;
        }

        return newObj;
    }
}

public class ObjectPool<T> : IObjectPool where T: class
{
    public Queue<T> pool = new();

    public override void Enqueue(object newObj)
    {
        if (newObj is T obj)
        {
            pool.Enqueue(obj);
        }
    }

    public override object Dequeue()
    {
        return pool.Dequeue();
    }
}