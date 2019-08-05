using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpritesHolder
{
    public SpriteDataholderKey accessor;
    public List<Sprite> dataHolder = new List<Sprite>();

    public Sprite GetRandomSprite()
    {
        if (dataHolder.Count > 0)
        {
            return dataHolder[Random.Range(0, dataHolder.Count)];
        }
        Debug.LogError("SpritesHolder is empty");
        return null;
    }
}

public enum SpriteDataholderKey
{
    Planet,
    Star
}
