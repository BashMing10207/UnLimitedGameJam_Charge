using UnityEngine;

public class BashUtils
{
    public static Vector2 LimitedSpeed(Vector2 currentSpeed, Vector2 addSpeed,float maxSpeed)
    {
        Vector2 dir = addSpeed.normalized * Mathf.Lerp(1, 0, ((Vector2)Vector3.Project(addSpeed, currentSpeed) + currentSpeed).magnitude / maxSpeed);
        return dir;
    }
}
