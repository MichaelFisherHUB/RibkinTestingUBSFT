using UnityEngine;

[System.Serializable]
public class GravityEmitter
{
    [SerializeField]
    private float _mass = 15;
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

    public GravityEmitter(int newMass = 0)
    {
        if(newMass != 0)
        {
            Mass = newMass;
        }
    }

    [ContextMenu("Set random mass")]
    public void SetRandomMass()
    {
        SetRandomMass(5f, 40f);
    }

    public void SetRandomMass(float fromValue, float toValue)
    {
        Mass = Random.Range(fromValue, toValue);
    }
}
