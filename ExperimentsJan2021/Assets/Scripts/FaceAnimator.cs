using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceAnimator : MonoBehaviour
{
    [SerializeField] Animator animator = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.Play("Angry Face");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.Play("Normal Face");
        }
    }
}
