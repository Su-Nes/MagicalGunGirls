using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new();

    private static List<GameObject> AllObjects = new();

    private GameObject _objectPoolEmptyHolder;

    private static GameObject _gameObjectsEmpty, _particleSystemsEmpty, _audioSourceEmpty;

    public enum PoolType
    {
        GameObject,
        ParticleSystem_NOT_IMPLEMENTED,
        AudioSource,
        None
    }

    private void Awake()
    {
        SetupEmpties();
    }

    private void SetupEmpties()
    {
        _gameObjectsEmpty = new GameObject("GameObjects");
        _gameObjectsEmpty.transform.SetParent(transform);

        _particleSystemsEmpty = new GameObject("PooledParticleSystems");
        _particleSystemsEmpty.transform.SetParent(transform);

        _audioSourceEmpty = new GameObject("AudioSources");
        _audioSourceEmpty.transform.SetParent(transform);
    }

    public static GameObject SpawnObject(GameObject objToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objToSpawn.name);

        if (pool == null) // if nonexistent pool, create one
        {
            pool = new PooledObjectInfo() { LookupString = objToSpawn.name };
            ObjectPools.Add(pool);
        }

        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        if (spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(poolType);
            
            spawnableObj = Instantiate(objToSpawn, spawnPosition, spawnRotation);
            AllObjects.Add(spawnableObj);

            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }
    
    public static GameObject SpawnObject(GameObject objToSpawn, Transform parent)
    {
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == objToSpawn.name);

        if (pool == null) // if nonexistent pool, create one
        {
            pool = new PooledObjectInfo() { LookupString = objToSpawn.name };
            ObjectPools.Add(pool);
        }

        GameObject spawnableObj = pool.InactiveObjects.FirstOrDefault();

        if (spawnableObj == null)
        {
            spawnableObj = Instantiate(objToSpawn, parent);
            AllObjects.Add(spawnableObj);
        }
        else
        {
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name[..^7]; // remove "(Clone)" from obj name, so the lookupString matches

        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if (pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled: " + obj.name);
            //Destroy(obj);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    public static bool IsInPool(GameObject obj)
    {
        string goName = obj.name[..^7];
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        return pool is not null;
    }
    
    public static void ReturnAllPooledObjectsToPool()
    {
        for (int i = 0; i < AllObjects.Count; i++)
        {
            ReturnObjectToPool(AllObjects[i].gameObject);
        }
        AllObjects.Clear();
    }

    private static GameObject SetParentObject(PoolType poolType)
    {
        switch (poolType)
        {
            case PoolType.GameObject:
                return _gameObjectsEmpty;
            
            case PoolType.ParticleSystem_NOT_IMPLEMENTED:
                return _particleSystemsEmpty;
            
            case PoolType.AudioSource:
                return _audioSourceEmpty;
            
            case PoolType.None:
                return null;
            
            default:
                return null;
        }
    }
}

public class PooledObjectInfo
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new();
}
