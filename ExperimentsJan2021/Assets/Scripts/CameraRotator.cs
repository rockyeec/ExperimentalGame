using UnityEngine;

public class CameraRotator : CameraSubordinate
{
    [SerializeField] float minPitchDegrees = -69.0f;
    [SerializeField] float maxPitchDegrees = 69.0f;
    public static void Tick(in float pitchInput, in float yawInput)
    {
        if (instance == null) return;
        instance.Rotate(pitchInput, yawInput);
    }

    public static Quaternion CameraRotation 
    { 
        get 
        {
            if (instance == null)
                return Quaternion.identity;
            return instance.Manager.transform.localRotation; 
        } 
    }

    private void Rotate(in float pitchInput, in float yawInput)
    {
        pitch -= pitchInput;
        pitch = Mathf.Clamp(pitch, minPitchDegrees, maxPitchDegrees);

        yaw += yawInput;

        Manager.Pivot.localEulerAngles = Vector3.right * pitch;
        Manager.transform.localEulerAngles = Vector3.up * yaw;
    }


    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    static CameraRotator instance;
    float pitch = 0.0f;
    float yaw = 0.0f;
}
