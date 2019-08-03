using UnityEngine;

[System.Serializable]
public struct GravityEmitter
{
    [SerializeField]
    private float _mass;
    public float Mass
    {
        get
        {
            return _mass > 0 ? _mass : Mathf.Abs(_mass);
        }
        private set
        {
            _mass = value;
        }
    }

    public void SetRandomMass()
    {
        SetRandomMass(1000f, 10000f);
    }

    public void SetRandomMass(float fromValue, float toValue)
    {
        Mass = Random.Range(fromValue, toValue);
    }
}
