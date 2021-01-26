using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseMove : MonoBehaviour
{
    [SerializeField] Animator animator = null;

    [SerializeField] float speedIncrement = 0.05f;
    [SerializeField] float turnRate = 0.1f;
    [SerializeField] float topSpeed = 69.0f;
    [SerializeField] float walkSpeed = 30.0f;

    float moveAmount = 0.0f;
    Vector3 moveDir = Vector3.forward;

    float turnAmount = 0.0f;

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

        if (Input.GetKey(KeyCode.A))
        {
            transform.eulerAngles = transform.eulerAngles.With(y: transform.eulerAngles.y - turnRate * Time.deltaTime);
            turnAmount -= turnRate * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.eulerAngles = transform.eulerAngles.With(y: transform.eulerAngles.y + turnRate * Time.deltaTime);
            turnAmount += turnRate * Time.deltaTime;
        }
        turnAmount = Mathf.Clamp(turnAmount, -1.0f, 1.0f);
        animator.SetFloat("moveX", turnAmount, 0.15f, Time.deltaTime);
        turnAmount = Mathf.MoveTowards(turnAmount, 0.0f, turnRate * 0.8f * Time.deltaTime);

        transform.position += transform.rotation * moveDir * moveAmount * topSpeed * Time.deltaTime;

        CameraTranslator.FollowTarget(transform.position);
        CameraRotator.Tick(Input.GetAxis("Mouse Y") * 6.9f, Input.GetAxis("Mouse X") * 6.9f);
    }
}
