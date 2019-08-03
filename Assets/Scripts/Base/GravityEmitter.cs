using UnityEngine;

[SerializeField]
public class GravityEmitter
{
    [SerializeField]
    private float _mass;
    public float Mass
    {
        get
        {
            return _mass > 0 ? _mass : Mathf.Abs(_mass);
        }
    }
}
