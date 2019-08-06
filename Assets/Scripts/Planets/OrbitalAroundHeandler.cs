using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalAroundHeandler : MonoBehaviour {

    public GameObject orbitingAround;
    public float orbitalSpeed = 40f;

    private void RotateAroundStar()
    {
        if (orbitingAround != null)
        {
            transform.RotateAround(orbitingAround.transform.position, transform.TransformDirection(Vector3.forward), orbitalSpeed * Time.deltaTime);
        }
        else
        { Debug.LogError("Can'find star object to orbit around"); }
    }

    private void Update()
    {
        if(orbitingAround != null)
        {
            RotateAroundStar();
        }
    }
}
