using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public static DataController instance;

    public List<SpritesHolder> spritesHolder = new List<SpritesHolder>();

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public Sprite GetRandomSpriteFromHolder(SpriteDataholderKey accessor)
    {
        return spritesHolder.Find(x => x.accessor == accessor).GetRandomSprite();
    }
}
