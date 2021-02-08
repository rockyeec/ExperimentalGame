using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseIk : MonoBehaviour
{
    [SerializeField] Animator animator = null;

    [SerializeField] Transform hips = null;
    [SerializeField] Transform upperChest = null;

    [SerializeField] Transform[] legs = null;
    [SerializeField] Transform[] arms = null;

    float hipsHeight = 0.0f;
    float upperChestHeight = 0.0f;
    float bodyLength = 0.0f;

    Vector3 hipsOriLocPos;
    Vector3 hipsOriLocEul;
    readonly Vector3[] armsOriLocEul = new Vector3[2];
    readonly Vector3[] legsOriLocEul = new Vector3[2];


    private void Awake()
    {
        hipsHeight = transform.InverseTransformPoint(hips.position).y;
        upperChestHeight = transform.InverseTransformPoint(upperChest.position).y;
        bodyLength = Vector3.Distance(transform.InverseTransformPoint(hips.position), transform.InverseTransformPoint(upperChest.position));

        hipsOriLocPos = hips.localPosition;
        hipsOriLocEul = hips.localEulerAngles;
        for (int i = 0; i < 2; i++)
        {
            armsOriLocEul[i] = arms[i].localEulerAngles;
            legsOriLocEul[i] = legs[i].localEulerAngles;
        }
    }

    void LateUpdate()
    {
        HandleHips();
    }


    void HandleHips()
    {
        if (Physics.Raycast(
                hips.position + 0.5f * hipsHeight * Vector3.up,
                Vector3.down,
                out RaycastHit hipsHit,
                hipsHeight * 2.0f,
                1 << 0))
        {
            Vector3 hipsDeltaLocPos = hips.localPosition - hipsOriLocPos;
            Vector3 hipsCorrectedLocPos = hips.parent.InverseTransformPoint(hips.position.With(y: (hipsHit.point + Vector3.up * hipsHeight).y));
            hips.localPosition = hipsCorrectedLocPos + hipsDeltaLocPos;

            //Vector3 predictedChestPos = 

           // if (Physics.Raycast(
           //     upperChest.position + 0.5f * upperChestHeight * Vector3.up,
           //     Vector3.down,
           //     out RaycastHit chestHit,
           //     upperChestHeight * 2.0f,
           //     1 << 0))
           // {
           //     Vector3 targetDir = 

                Vector3 hipsDeltaEul = hips.localEulerAngles - hipsOriLocEul;
                Vector3 hipsCorrectedEul = Quaternion.LookRotation(-hipsHit.normal, transform.forward).eulerAngles;


                hips.eulerAngles = hipsCorrectedEul + hipsDeltaEul;

           // }


            //foreach (var item in legs)
            //{
            //    item.rotation = Quaternion.LookRotation(-transform.forward, Vector3.down);
            //}
        }
    }
}
