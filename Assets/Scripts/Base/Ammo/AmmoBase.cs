using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
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
    [SerializeField]    protected float acceleration;
    [SerializeField]    protected float accelerationTime;
    [SerializeField]    protected float lifetime = 20;

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
            RigidBodyOfThis.AddRelativeForce(Vector2.up * acceleration * Time.fixedDeltaTime);
            accelerationTimer += Time.fixedDeltaTime;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        ITakeDamagable damageInterface = collision.gameObject.GetComponent<ITakeDamagable>();
        if(damageInterface != null)
        {
            damageInterface.TakeDamage(damage);
        }
        OnHit(collision);
        PoolDestroy();
    }

    public virtual void OnHit(Collider2D collision)
    {

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
