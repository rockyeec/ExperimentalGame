using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderScript : MonoBehaviour
{
    [SerializeField] Animator animator = null;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.Play("Attack");
        }
    }
}
