using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaddleLegIk : MonoBehaviour
{
    [SerializeField] Transform leftStirrup = null;
    [SerializeField] Transform rightStirrup = null;

    [SerializeField] Animator riderAnimator = null;

    private void OnAnimatorIK(int layerIndex)
    {
        riderAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1.0f);
        riderAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1.0f);

        riderAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, leftStirrup.position);
        riderAnimator.SetIKPosition(AvatarIKGoal.RightFoot, rightStirrup.position);
    }
}
