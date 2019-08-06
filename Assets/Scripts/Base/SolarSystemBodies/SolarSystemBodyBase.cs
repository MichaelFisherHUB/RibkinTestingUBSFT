using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SolarSystemBodyBase : MonoBehaviour, ITakeDamagable, IDeadable, IGravityEmitter
{
    public GravityEmitter gravityEmitter = new GravityEmitter();

    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }

        private set
        {
            _health = value;
            if (_health <= 0)
            {
                _health = 0;
                if (onHealthValueChange != null)
                {
                    onHealthValueChange.Invoke(Health);
                }
                Die();
            }
        }
    }

    private System.Action<int> onHealthValueChange;
    private System.Action onDieAction;

    [SerializeField] private GameObject onDieParticles;

    protected void Awake()
    {
        if (!GravityController.gravEmitters.ContainsKey(gameObject))
        {
            GravityController.gravEmitters.Add(gameObject, GetComponent<IGravityEmitter>());
        }
    }

    #region AddListeners

    public void AddListeners(System.Action<int> onHealthChange, System.Action onDieHeandler)
    {
        AddHealthListener(onHealthChange);
        AddDeadListener(onDieHeandler);
    }

    public void AddHealthListener(System.Action<int> onHealthChange)
    {
        if (onHealthChange != null)
        {
            onHealthValueChange += onHealthChange;
        }
    }

    public void AddDeadListener(System.Action onDieHeandler)
    {
        if (onDieHeandler != null)
        {
            onDieAction += onDieHeandler;
        }
    }
    #endregion

    #region interfaces

    public virtual void TakeDamage(int damageValue)
    {
        Health -= damageValue;
    }

    public virtual void Die()
    {
        GameObject tempGO = Instantiate(onDieParticles, transform.position, Quaternion.identity);
        ParticleSystemTemper tempPS = tempGO.GetComponent<ParticleSystemTemper>();
        if(tempPS != null)
        {
            tempPS.Play();
        }

        if (onDieAction != null)
        {
            onDieAction.Invoke();
        }
        if (GravityController.gravEmitters.ContainsKey(gameObject))
        {
            GravityController.gravEmitters.Remove(gameObject);
        }
        Destroy(gameObject);
    }

    public float GetGravityValue()
    {
        return gravityEmitter.Mass;
    }
    #endregion
}
