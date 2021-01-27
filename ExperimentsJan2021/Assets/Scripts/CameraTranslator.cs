using UnityEngine;

public class CameraTranslator : CameraSubordinate
{
    public static void FollowTarget(in Vector3 targetPosition)
    {
        if (instance == null) return;
        instance.Manager.transform.position = targetPosition;
    }

    static CameraTranslator instance;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }
}
