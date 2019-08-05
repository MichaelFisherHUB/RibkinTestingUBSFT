using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteUpdate : MonoBehaviour {

    [SerializeField]    private SpriteRenderer _spriteToUpdate;
    private SpriteRenderer SpriteToUpdate
    {
        get
        {
            return _spriteToUpdate ?? (_spriteToUpdate = GetComponent<SpriteRenderer>());
        }
    }
    [SerializeField]    private SpriteDataholderKey dataholderKey;

    void Start ()
    {
        if (SpriteToUpdate != null)
        {
            SpriteToUpdate.sprite = DataController.instance.GetRandomSpriteFromHolder(dataholderKey);
        }
    }
}
