using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterAtTarget : MonoBehaviour
{
    public bool isPlayer;

    private void Update()
    {
        RotateToMousePosition();
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
        if (isPlayer)
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
