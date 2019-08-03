using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AmmoBase : MonoBehaviour, IGravityAccepter
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

    private void Awake()
    {
        GravityController.gravAccepters.Add(gameObject, GetComponent<IGravityAccepter>());
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
    #endregion
}
