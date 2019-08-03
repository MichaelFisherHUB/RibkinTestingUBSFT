using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEnums;

public class GravityController : MonoBehaviour
{
    public static Dictionary<GameObject, IGravityEmitter> gravEmitters = new Dictionary<GameObject, IGravityEmitter>();
    public static Dictionary<Rigidbody2D, IGravityAccepter> gravAccepters = new Dictionary<Rigidbody2D, IGravityAccepter>();
    private const float G = 667.4f;

    [SerializeField] private Threading gravityCalculationMode;

    private void Update()
    {
        switch(gravityCalculationMode)
        {
            default:
                {
                    SingleThreadCalculation();
                    break;
                }
            case (Threading.MultyThread):
                {
                    break;
                }
            case (Threading.JobSystem):
                {
                    break;
                }
        }
    }

    private Vector2 SingleThreadCalculation(Vector2 emitterPosition, Rigidbody2D accepter, float massValue)
    {
        Vector2 direction = emitterPosition - accepter.position;
        float distance = direction.magnitude;

        Vector2 force = Vector2.zero;

        if (distance != 0f)
        {
            float forceMagnitude = G * (massValue * accepter.mass) / Mathf.Pow(distance, 2);
            force = direction.normalized * forceMagnitude;
        }

        return force;
        //rbToAttract.AddForce(force);
    }
}
