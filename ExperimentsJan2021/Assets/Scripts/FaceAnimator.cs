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
            animator.CrossFade("Angry Face", 3.75f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.CrossFade("Normal Face", 3.75f);
        }
    }
}
