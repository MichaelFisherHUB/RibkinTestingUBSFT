﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class AmmoBase : MonoBehaviour, IGravityAccepter , IPoolable
{
    [SerializeField]
    private Rigidbody2D _rigid;
    protected Rigidbody2D RigidBodyOfThis
    {
        get
        {
            return _rigid ?? (_rigid = GetComponent<Rigidbody2D>());
        }
    }

    [SerializeField]    protected bool useGravity = true;
    [SerializeField]    protected int damage = 50;
    [SerializeField]    protected float acceleration = 500f;
    [SerializeField]    protected float accelerationTime;
    [SerializeField]    protected float lifetime = 20;
    [SerializeField]    protected GameObject onDieParticles;

    public float reloadTime = 0.4f;

    private float accelerationTimer = 0;
    private float lifetimeTimer = 0;

    protected void Start()
    {
        PoolStart();
    }

    protected void FixedUpdate()
    {
        Accelerate();
    }

    protected void Update()
    {
        TikTokLifetime();
        RotateToMovement();
    }

    private void RotateToMovement()
    {
        //transform.rotation = Quaternion.
    }

    private void TikTokLifetime()
    {
        if (lifetimeTimer > lifetime)
        {
            PoolDestroy();
            return;
        }
        lifetimeTimer += Time.deltaTime;
    }

    private void Accelerate()
    {
        if (accelerationTimer < accelerationTime)
        {
            RigidBodyOfThis.AddRelativeForce(Vector2.up * acceleration);
            accelerationTimer += Time.fixedDeltaTime;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        ITakeDamagable damageInterface = collision.gameObject.GetComponent<ITakeDamagable>();
        if(damageInterface != null)
        {
            damageInterface.TakeDamage(damage);
            OnHit(collision);
            PoolDestroy();
        }
    }

    public virtual void OnHit(Collider2D collision)
    {
        GameObject tempGO = Instantiate(onDieParticles, transform.position, Quaternion.identity);
        ParticleSystemTemper tempPS = tempGO.GetComponent<ParticleSystemTemper>();
        if (tempPS != null)
        {
            tempPS.Play();
        }
    }

    #region interfaces

    public void AcceptGravity(Vector2 gravityVector)
    {
        RigidBodyOfThis.AddForce(gravityVector);
    }

    public float GetMass()
    {
        return RigidBodyOfThis.mass;
    }

    public void PoolStart()
    {
        if (useGravity && !GravityController.gravAccepters.ContainsKey(gameObject))
        {
            GravityController.gravAccepters.Add(gameObject, GetComponent<IGravityAccepter>());
        }
        accelerationTimer = 0;
        lifetimeTimer = 0;
    }

    public void PoolDestroy()
    {
        if(GravityController.gravAccepters.ContainsKey(gameObject))
        {
            GravityController.gravAccepters.Remove(gameObject);
        }
        GameObjectPool.Instance.ReturtToPool(gameObject);
        gameObject.SetActive(false);
    }
    #endregion
}
