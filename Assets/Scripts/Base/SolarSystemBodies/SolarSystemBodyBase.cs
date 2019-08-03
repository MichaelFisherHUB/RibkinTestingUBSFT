using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemBodyBase : MonoBehaviour
{
    public GravityEmitter gravityEmitter = new GravityEmitter();
    public HealthControll healthControll = new HealthControll();

    private void Awake()
    {
        healthControll.AddListeners(HealthChangeHandler, OnDieHandler);
    }

    public virtual void HealthChangeHandler(int newHealth)
    {

    }

    public virtual void OnDieHandler()
    {

    }
}
