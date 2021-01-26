using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderMove : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;

    [SerializeField] Animator animator = null;

    [SerializeField] TrailRenderer trail = null;

    bool IsMove => Input.GetKey(KeyCode.W) 
        || Input.GetKey(KeyCode.A)
        || Input.GetKey(KeyCode.S)
        || Input.GetKey(KeyCode.D);

    int attackCount = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.CrossFade("Parry", 0.15f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.CrossFade("Roll", 0.15f);
        }


        if (Input.GetMouseButtonDown(0))
        {
            animator.CrossFade( attackCount == 0 ? "Attack" : "Attack 2", 0.01f);
            attackCount++;
            attackCount %= 2;
        }
        trail.emitting = animator.GetBool("isAttack");

        Vector3 moveDir = Vector3.zero;
        if (IsMove)
        {
            moveDir = CameraRotator.CameraRotation * new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;

            if (moveDir != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), Time.deltaTime * 6.9f);
        }
        animator.SetFloat("moveZ", moveDir.sqrMagnitude, 0.05f, Time.deltaTime);

        transform.position += moveDir * Time.deltaTime * speed;

        CameraTranslator.FollowTarget(transform.position);
        CameraRotator.Tick(Input.GetAxis("Mouse Y") * 6.9f, Input.GetAxis("Mouse X") * 6.9f);
    }

    public void OnTrail()
    {
        trail.emitting = true;
    }
    public void OffTrail()
    {
        trail.emitting = false;
    }
}
