using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Define;

public class ObjectPooling : Singletone<ObjectPooling>
{
    private readonly Dictionary<Define.PoolingKey, Queue<Pool>> _poolingDic = new Dictionary<Define.PoolingKey, Queue<Pool>>();

    [SerializeField] private Transform _enemyPool;
    [SerializeField] private Transform _projectilePool;
    [SerializeField] private Transform _coinPool;
    [SerializeField] private Transform _weaponPool;

    public void RegisterPooling(Define.PoolingKey key, Pool regObj, int count)
    {
        if (!_poolingDic.TryGetValue(key, out var objectQueue))
        {
            objectQueue = new Queue<Pool>();
            _poolingDic[key] = objectQueue;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(regObj.gameObject);
            SetPoolingParent(obj, regObj.ObjType);
            objectQueue.Enqueue(obj.GetComponent<Pool>());
            obj.SetActive(false);
        }
    }

    private void SetPoolingParent(GameObject poolObj, ObjectType type)
    {
        Transform parentTransform = type switch
        {
            ObjectType.Weapon => _weaponPool,
            ObjectType.Projectile => _projectilePool,
            ObjectType.Enemy => _enemyPool,
            ObjectType.Coin => _coinPool,
            _ => this.transform
        };

        poolObj.transform.SetParent(parentTransform);
    }

    public Pool Spawn(PoolingKey key)
    {
        if (_poolingDic.TryGetValue(key, out var objectQueue) && objectQueue.Count > 0)
        {
            Pool obj = objectQueue.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        return null;
    }

    public Pool SpawnWithParent(PoolingKey key, Transform parent)
    {
        Pool obj = Spawn(key);
        if (obj != null)
        {
            obj.transform.SetParent(parent);
        }
        return obj;
    }

    private Queue<Pool> GetOrCreatePoolQueue(PoolingKey key)
    {
        if (!_poolingDic.TryGetValue(key, out var queue))
        {
            queue = new Queue<Pool>();
            _poolingDic[key] = queue;
        }
        return queue;
    }

    public void DeSpawn(PoolingKey key, Pool obj)
    {
        if (_poolingDic.TryGetValue(key, out var objectQueue))
        {
            obj.gameObject.SetActive(false);
            SetPoolingParent(obj.gameObject, obj.ObjType);
            objectQueue.Enqueue(obj);
        }
        else
        {
            Queue<Pool> newQueue = new Queue<Pool>();
            newQueue.Enqueue(obj);
            _poolingDic[key] = newQueue;
            SetPoolingParent(obj.gameObject, obj.ObjType);
            obj.gameObject.SetActive(false);
        }
    }

}
