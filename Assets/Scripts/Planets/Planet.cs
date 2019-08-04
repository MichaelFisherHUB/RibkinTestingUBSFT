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
    [SerializeField]    private GameObject orbitingAround;
    [SerializeField]    private float orbitalSpeed = 40f;
    [SerializeField]    public bool isPlayer;

    private void Update()
    {
        RotateAroundStar();
        RotateToMousePosition();

        ShotControll();
    }

    private void ShotControll()
    {
        if(Input.GetMouseButton(0))
        {
            ShootManager.Shot();
        }
    }

    private void RotateAroundStar()
    {
        if (orbitingAround != null)
        {
            transform.RotateAround(orbitingAround.transform.position, transform.TransformDirection(Vector3.forward), orbitalSpeed * Time.deltaTime);
        }
        else
        { Debug.LogError("Can'find star object to orbit around"); }
    }

    private void RotateToMousePosition()
    {
        Vector3 lookAtPosition = GetDirection();

        Vector2 direction = new Vector2(
            lookAtPosition.x - transform.position.x,
            lookAtPosition.y - transform.position.y
            );

        transform.up = direction;
    }

    private Vector3 GetDirection()
    {
        if(isPlayer)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            return mousePos;
        }
        else
        {
            // TODO make AI
            return Vector3.zero;
        }
    }
}
