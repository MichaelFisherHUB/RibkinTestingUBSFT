using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : SolarSystemBodyBase
{
    [SerializeField]    private GameObject orbitingAround;
    [SerializeField]    private float orbitalSpeed = 40f;

    private void Update()
    {
        transform.RotateAround(orbitingAround.transform.position, transform.TransformDirection(Vector3.forward), orbitalSpeed * Time.deltaTime);
    }
}
