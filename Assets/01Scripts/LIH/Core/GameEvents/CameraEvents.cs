using UnityEngine;

public static class CameraEvents
{
    public static CamDistanceChange CamDistanceChangeEvent = new CamDistanceChange();
    public static CamShake CamShakeEvent = new CamShake();
}

public class CamDistanceChange : GameEvent
{
    public float distance;
    public float speed;
}

public class CamShake : GameEvent
{
    public float intensity;
}