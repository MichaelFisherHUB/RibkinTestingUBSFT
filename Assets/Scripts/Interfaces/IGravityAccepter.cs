using UnityEngine;

public interface IGravityAccepter
{
    void AcceptGravity(Vector2 gravityValue);
    float GetMass();
}
