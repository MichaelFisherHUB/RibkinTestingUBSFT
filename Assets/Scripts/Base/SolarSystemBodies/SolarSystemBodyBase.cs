using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemBodyBase : MonoBehaviour, ITakeDamagable, IDeadable
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

    #region Constructors

    public SolarSystemBodyBase(int startHealth = 0)
    {
        if (startHealth != 0)
        {
            _health = startHealth;
        }
    }

    public SolarSystemBodyBase(System.Action<int> onHealthChange, System.Action onDie, int startHealth = 0)
    {
        onHealthValueChange += onHealthChange;
        onDieAction += onDie;
        if (startHealth != 0)
        {
            _health = startHealth;
        }
    }
    #endregion

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

    public void Die()
    {
        if (onDieAction != null)
        {
            onDieAction.Invoke();
        }
    }
    #endregion
}
