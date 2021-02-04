using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseIk : MonoBehaviour
{
    [SerializeField] float weight = 0.5f;
    [SerializeField] Animator animator = null;

    [SerializeField] Transform hips = null;
    [SerializeField] Transform upperChest = null;

    [SerializeField] Transform[] legs = null;
    [SerializeField] Transform[] arms = null;

    float hipsHeight = 0.0f;
    float upperChestHeight = 0.0f;
    float bodyLength = 0.0f;

    Vector3 hipsOriPos;
    Vector3 chestOriPos;
    Quaternion hipsOriRot;
    Quaternion chestOriRot;


    private void Awake()
    {
        hipsHeight = transform.InverseTransformPoint(hips.position).y;
        upperChestHeight = transform.InverseTransformPoint(upperChest.position).y;
        bodyLength = Vector3.Distance(transform.InverseTransformPoint(hips.position), transform.InverseTransformPoint(upperChest.position));

        
    }

    private void LateUpdate()
    {
        if (animator.GetFloat("moveZ") < 0.02f)
        {
            HandleHips();
            //HandleUpperChest();
        }
    }


    void HandleHips()
    {
        Ray ray = new Ray(hips.position + 0.5f * hipsHeight * Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, hipsHeight * 2.0f, 1 << 0))
        {
            hips.position = Vector3.LerpUnclamped(hips.position, hips.position.With(y: (hit.point + Vector3.up * hipsHeight).y), weight);

            hips.rotation = Quaternion.LookRotation(-hit.normal, transform.forward);

            foreach (var item in legs)
            {
                item.rotation = Quaternion.LookRotation(-transform.forward, Vector3.down);
            }
        }
    }
    void HandleUpperChest()
    {
        Ray ray = new Ray(upperChest.position + 0.5f * upperChestHeight * Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, upperChestHeight * 2.0f, 1 << 0))
        {
            upperChest.position = Vector3.LerpUnclamped(upperChest.position, upperChest.position.With(y: (hit.point + Vector3.up * upperChestHeight).y), weight);

            upperChest.rotation = Quaternion.LookRotation(-hit.normal, transform.forward);

            foreach (var item in arms)
            {
                item.rotation = Quaternion.LookRotation(-transform.forward, Vector3.down);
            }
        }
    }
}
