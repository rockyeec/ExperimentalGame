using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorsePhysicsMove : MonoBehaviour
{
    enum Gait
    {
        Idle, Walk, Trot, Canter, Gallop
    }


    [SerializeField] Animator animator = null;
    [SerializeField] Rigidbody rb = null;
    [SerializeField] SphereCollider coll = null;

    [SerializeField] float speedIncrement = 0.8f;
    [SerializeField] float turnRate = 133.7f;
    [SerializeField] float topSpeed = 69.0f;

    float moveAmount = 0.0f;
    Vector3 moveDir = Vector3.forward;
    bool isGround = false;

    float turnAmount = 0.0f;

    bool isLeft = false;
    bool isRight = false;

    private void FixedUpdate()
    {
        if (isLeft || isRight)
        {
            if (isLeft)
            {
                transform.eulerAngles = transform.eulerAngles.With(y: transform.eulerAngles.y - turnRate);
                turnAmount -= turnRate;
            }
            if (isRight)
            {
                transform.eulerAngles = transform.eulerAngles.With(y: transform.eulerAngles.y + turnRate);
                turnAmount += turnRate;
            }
        }
        else
        {
            turnAmount = Mathf.MoveTowards(turnAmount, 0.0f, turnRate * 0.8f);
        }
        turnAmount = Mathf.Clamp(turnAmount, -1.0f, 1.0f);


        DetectGround();
        Vector3 move = transform.rotation * moveDir * moveAmount * topSpeed;
        rb.velocity = isGround ? move : move.With(y: rb.velocity.y);

        CameraTranslator.FollowTarget(transform.position);
        CameraRotator.Tick(Input.GetAxis("Mouse Y") * 6.9f, Input.GetAxis("Mouse X") * 6.9f);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            moveAmount += Time.deltaTime * speedIncrement;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveAmount -= Time.deltaTime * speedIncrement;
        }
        moveAmount = Mathf.Clamp01(moveAmount);
        animator.SetFloat("moveZ", moveAmount, 0.15f, Time.deltaTime);

        isLeft = Input.GetKey(KeyCode.A);
        isRight = Input.GetKey(KeyCode.D);

        

        animator.SetFloat("moveX", turnAmount, 0.25f, Time.deltaTime);
        animator.SetLayerWeight(animator.GetLayerIndex("Turn Layer"), Mathf.Lerp(0.0f, 0.5f, Mathf.Abs(animator.GetFloat("moveX"))));

        animator.SetLayerWeight(animator.GetLayerIndex("Idle Turn Layer"), 
            animator.GetFloat("moveZ") < 0.01f 
            ? Mathf.Lerp(0.0f, 0.5f, Mathf.Abs(animator.GetFloat("moveX"))) 
            : 0.0f);
    }

    void DetectGround()
    {
        //Debug.DrawRay(transform.TransformPoint(coll.center), Vector3.down * (coll.center.y + 0.02f));
        isGround = Physics.Raycast(transform.TransformPoint( coll.center ), Vector3.down, out RaycastHit hit, coll.center.y + 0.02f, 1 << 0);
        if (isGround)
        {
            transform.position = transform.position.With(y: hit.point.y);
            Debug.DrawRay(transform.position, hit.normal);
        }
    }
}
