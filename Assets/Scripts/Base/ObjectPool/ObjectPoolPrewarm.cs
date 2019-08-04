using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolPrewarm : MonoBehaviour
{
    [SerializeField]    private List<GameObject> objectsInPoolInitiate = new List<GameObject>();
    
    private void Awake()
    {
        objectsInPoolInitiate.ForEach(x =>
        {
            GameObjectPool.Instance.CreatePool(x);
        });
    }
}
