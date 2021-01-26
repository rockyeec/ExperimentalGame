using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    [SerializeField] Animator animator = null;

    [SerializeField] Transform target = null;

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetLookAtPosition(target.position);
        animator.SetLookAtWeight(1.0f, 0.25f, 0.25f, 0.5f);
    }
}
