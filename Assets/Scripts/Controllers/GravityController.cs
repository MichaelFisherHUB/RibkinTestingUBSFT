using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEnums;
using Extensions;

public class GravityController : MonoBehaviour
{
    public static Dictionary<GameObject, IGravityEmitter> gravEmitters = new Dictionary<GameObject, IGravityEmitter>();
    public static Dictionary<GameObject, IGravityAccepter> gravAccepters = new Dictionary<GameObject, IGravityAccepter>();
    private const float G = 6.674f;

    [SerializeField] private Threading gravityCalculationMode;

    private void Update()
    {
        switch(gravityCalculationMode)
        {
            default:
                {
                    foreach (GameObject gravAccepterObject in gravAccepters.Keys)
                    {
                        foreach (GameObject gravEmmiterObject in gravEmitters.Keys)
                        {
                            if(gravAccepterObject != gravEmmiterObject)
                            {
                                //Apply Vector2 as gravity force
                                gravAccepters[gravAccepterObject].AcceptGravity(
                                    //Calculate vector2 based on all gravity emittors
                                    SingleThreadCalculation(
                                        gravEmmiterObject.transform.position.ToVector2(),
                                        gravEmitters[gravEmmiterObject].GetGravityValue(),
                                        gravAccepterObject.transform.position.ToVector2(),
                                        gravAccepters[gravAccepterObject].GetMass()
                                        )
                                    );
                            }
                        }
                    }
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

    private Vector2 SingleThreadCalculation(Vector2 emitterPosition, float emitterMassValue, Vector2 accepterPosition, float accepterMassValue)
    {
        Vector2 direction = emitterPosition - accepterPosition;
        float distance = direction.magnitude;

        Vector2 force = Vector2.zero;

        if (distance != 0f)
        {
            float forceMagnitude = G * (emitterMassValue * accepterMassValue) / Mathf.Pow(distance, 2);
            force = direction.normalized * forceMagnitude;
        }

        return force;
    }
}
