using UnityEngine;

public static class SoundEvents
{
    public static PlaySFXEvent PlaySfxEvent = new PlaySFXEvent();
    public static PlayBGMEvent PlayBGMEvent = new PlayBGMEvent();
    public static StopBGMEvent StopBGMEvent = new StopBGMEvent();
}

public class PlaySFXEvent : GameEvent
{
    public SoundSO clipData;
    public Vector3 position;
}

public class PlayBGMEvent : GameEvent
{
    public SoundSO clipData;
}

public class StopBGMEvent : GameEvent
{
    
}
