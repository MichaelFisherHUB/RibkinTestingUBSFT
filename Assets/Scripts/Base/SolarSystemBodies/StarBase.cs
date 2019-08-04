using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBase : MonoBehaviour, IGravityEmitter, ITakeDamagable
{
    public GravityEmitter gravityEmitter = new GravityEmitter();

    [SerializeField] private int totalDamageTaken = 0;

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

    public void TakeDamage(int damageValue)
    {
        totalDamageTaken += damageValue;
    }
}
