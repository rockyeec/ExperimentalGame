using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField] float speed = 25.0f;

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        transform.Translate(hor * speed * Time.deltaTime, 0.0f, ver * speed * Time.deltaTime);
    }
}
