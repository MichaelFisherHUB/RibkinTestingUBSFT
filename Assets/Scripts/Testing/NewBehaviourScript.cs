using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public SolarSystemBodyBase solar;

    [ContextMenu("Testing")]
	public void Testing()
    {
        ITakeDamagable damageInterface = solar.GetComponent<ITakeDamagable>();
        Debug.Log(damageInterface == null ? "NUll" : "There");
    }
}
