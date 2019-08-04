using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBase : MonoBehaviour, IGravityEmitter
{
    public GravityEmitter gravityEmitter = new GravityEmitter();

    protected void Awake()
    {
        if (!GravityController.gravEmitters.ContainsKey(gameObject))
        {
            GravityController.gravEmitters.Add(gameObject, GetComponent<IGravityEmitter>());
        }
    }

    public float GetGravityValue()
    {
        return gravityEmitter.Mass;
    }
}
