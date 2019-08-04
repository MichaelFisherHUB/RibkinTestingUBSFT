using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public static GameObjectPool Instance { get; private set; }

    [SerializeField]    private GameObject poolContainer;

    [SerializeField]
    private List<PoolElement> pools = new List<PoolElement>();

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
            GameObject tempParrent = Instantiate(poolContainer, gameObject.transform);
            tempParrent.name = string.Format("{0}_Pool", poolAbleObject.name);
            PoolElement tmpPoolScript = tempParrent.GetComponent<PoolElement>();
            if(tmpPoolScript != null)
            {
                tmpPoolScript.CreatePool(poolAbleObject.gameObject, poolSize, tempParrent.transform);
                pools.Add(tmpPoolScript);
            }
        }
    }

    public GameObject GetGameObjectFromPool(string nameOfPrefab)
    {
        PoolElement foundedPool = pools.Find(x => x.Key == nameOfPrefab);

        if (foundedPool != null)
        {
            return foundedPool.GetFromPool();
        }
        else
        {
            Debug.LogError("Can't fond ObjectPool for this object: " + nameOfPrefab);
        }
        return null;
    }

    public void ReturtToPool(GameObject prefab)
    {
        PoolElement foundedPool = pools.Find(x => x.Key == prefab.name);

        if (foundedPool != null)
        {
            prefab.transform.SetParent(foundedPool.parrent);
        }
        else
        {
            Debug.LogError("Can't fond ObjectPool for this object: " + prefab.name);
        }
    }
}
