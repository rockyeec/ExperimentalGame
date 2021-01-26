using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Vector3 distFromPlayer = new Vector3(0.0f, 4.20f, 6.9f);

    public Transform Pivot { get { return pivot; } }
    public Transform Rail { get { return rail; } }
    public Transform RailRef { get { return railRef; } }
    public Transform Shaker { get { return shaker; } }

    Transform pivot;
    Transform rail;
    Transform railRef;
    Transform shaker;

    Camera cam;

    public void ApplyChanges()
    {
        if (pivot == null) Reset();

        pivot.localPosition = Vector3.up * distFromPlayer.y;
        rail.localPosition
            = railRef.localPosition
            = Vector3.back * distFromPlayer.z;
    }

    private void Reset()
    {
        InitTransform(out pivot, transform, "Pivot");
        InitTransform(out rail, pivot, "Rail");
        InitTransform(out railRef, pivot, "Rail Ref");
        InitTransform(out shaker, rail, "Shaker");
    }

    private void Awake()
    {
        if (pivot == null) Reset();

        cam = Camera.main;
        cam.transform.SetParent(shaker);
        cam.transform.localPosition = Vector3.zero;
    }

    void InitTransform(out Transform toBeInit, in Transform parent, in string toBeInitName)
    {
        Transform targetChild = parent.Find(toBeInitName);
        if (targetChild != null)
        {
            toBeInit = targetChild;
        }
        else
        {
            toBeInit = new GameObject().transform;
            toBeInit.SetParent(parent);
            toBeInit.localPosition = Vector3.zero;
            toBeInit.localRotation = Quaternion.identity;
            toBeInit.name = toBeInitName;
        }
    }
}
