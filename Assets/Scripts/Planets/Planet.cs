using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

[RequireComponent(typeof(ShootingManager))]
[RequireComponent(typeof(CircleCollider2D))]
public class Planet : SolarSystemBodyBase
{
    [SerializeField]    private ShootingManager _shootingManager;
    [SerializeField]    private ShootingManager ShootManager
    {
        get
        {
            return _shootingManager ?? (_shootingManager = GetComponent<ShootingManager>());
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ShootManager.Shot();
        }
        //TODO Add AI
    }
}
