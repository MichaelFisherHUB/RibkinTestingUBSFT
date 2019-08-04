using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolElement : MonoBehaviour
{
    public string Key { get; private set; }
    public Transform parrent { get; private set; }
    [SerializeField] private Queue<GameObject> poolElements = new Queue<GameObject>();

    public PoolElement(GameObject prefab, int size, Transform parrent)
    {
        this.parrent = parrent;
        Key = prefab.name;
        for (int i = 0; i < size; i++)
        {
            GameObject tempObj = Instantiate(prefab, parrent);
            poolElements.Enqueue(tempObj);
            tempObj.SetActive(false);
        }
    }

    public GameObject GetFromPool()
    {
        GameObject tmpObject = poolElements.Dequeue();
        if (tmpObject.activeInHierarchy)
        {
            tmpObject.GetComponent<IPoolable>().PoolDestroy();
            tmpObject.SetActive(false);
        }
        poolElements.Enqueue(tmpObject);
        tmpObject.transform.parent = null;

        //"|" - means how many times this gameobjectwas used from objectpool
        tmpObject.name = tmpObject.name + "|";
        return tmpObject;
    }
}