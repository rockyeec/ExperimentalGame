using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using System.Diagnostics;
#endif

public class FootIk : MonoBehaviour
{
    [SerializeField] Animator animator = null;
    [SerializeField] float footRangeHeight = 3.6f;

    Transform[] feet;
    AvatarIKGoal[] footGoals = new AvatarIKGoal[2] { AvatarIKGoal.LeftFoot, AvatarIKGoal.RightFoot };
    float footHeight = 0.25f;
    float currWeight = 0.0f;

    private void Awake()
    {
        feet = new Transform[2]
        {
            animator.GetBoneTransform(HumanBodyBones.LeftFoot),
            animator.GetBoneTransform(HumanBodyBones.RightFoot)
        };

        footHeight = transform.InverseTransformPoint(feet[0].position).y;
    }

    private void OnAnimatorIK(int layerIndex)
    {
#if UNITY_EDITOR
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
#endif

        bool isMoving = animator.GetFloat("moveZ") > 0.02f;

        currWeight = Mathf.MoveTowards(currWeight, isMoving ? 0.0f : 1.0f, Time.deltaTime * 6.9f);

        for (int i = 0; i < 2; i++)
        {
            animator.SetIKPositionWeight(footGoals[i], currWeight);
            animator.SetIKRotationWeight(footGoals[i], currWeight);

            if (Mathf.Approximately( currWeight, 0.0f ))
                continue;

            if (Physics.Raycast(feet[i].position + Vector3.up * footRangeHeight, Vector3.down, out RaycastHit hit, footRangeHeight * 2.0f, 1 << 0))
            {
                animator.SetIKPosition(footGoals[i], hit.point.With(y: hit.point.y + footHeight));
                animator.SetIKRotation(footGoals[i],  Quaternion.LookRotation(feet[i].up, hit.normal) * Quaternion.Euler(-30.0f, 0.0f, 0.0f));
            }
        }

#if UNITY_EDITOR
        stopWatch.Stop();
        // Get the elapsed time as a TimeSpan value.
        System.TimeSpan ts = stopWatch.Elapsed;
        print(ts);
#endif
    }
}
