using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public static GameObjectPool Instance { get; private set; }

    private  List<PoolElement> pools = new List<PoolElement>();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void CreatePool(GameObject poolAbleObject, int poolSize = 20)
    {
        #region Protectors

        IPoolable objInterface = poolAbleObject.GetComponent<IPoolable>();
        if (objInterface == null)
        { Debug.LogWarningFormat("Can't create objectPool to object: {0}. There is no 'IPoolable' interface", poolAbleObject.name); return; }

        if (poolSize < 0)
        { poolSize = 20; }
        #endregion

        if (pools.Find(x => x.Key.Equals(poolAbleObject.name)) == null)
        {
            GameObject tempParrent = new GameObject(string.Format("{0}_Pool", poolAbleObject.name));
            tempParrent.transform.parent = gameObject.transform;
            pools.Add(new PoolElement(poolAbleObject.gameObject, poolSize, tempParrent.transform));
        }
    }

    public GameObject GetGameObjectFromPool(string nameOfPrefab)
    {
        PoolElement foundedPool = pools.Find(x => x.name == nameOfPrefab);

        if (foundedPool != null)
        {
            return foundedPool.GetFromPool();
        }
        Debug.LogError("Can't fond ObjectPool for this object: " + nameOfPrefab);
        return null;
    }

    public void ReturtToPool(GameObject prefab)
    {
        PoolElement foundedPool = pools.Find(x => x.name == prefab.name);

        if (foundedPool != null)
        {
            prefab.transform.parent = foundedPool.parrent;
        }
        Debug.LogError("Can't fond ObjectPool for this object: " + prefab.name);
    }
}
