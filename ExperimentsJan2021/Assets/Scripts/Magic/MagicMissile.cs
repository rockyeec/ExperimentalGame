using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissile : MonoBehaviour
{
    public void Shoot(in Vector3 pole)
    {
        a = transform.localPosition;
        b = pole;
        c = Vector3.zero;

        StartLerp();
    }

    [SerializeField] AnimationCurve curve = null;
    [SerializeField] float duration = 2.5f;
    float elapsed = 0.0f;
    Vector3 a, b, c;

    void Awake()
    {
        enabled = false;        
    }

    void StartLerp()
    {
        enabled = true;
        elapsed = 0.0f;
    }
    void EndLerp()
    {
        enabled = false;
    }

    void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = curve.Evaluate(elapsed / duration);

            transform.localPosition = a.QuadraticCurve(b, c, t);
        }
        else
        {
            EndLerp();
        }
    }
}
