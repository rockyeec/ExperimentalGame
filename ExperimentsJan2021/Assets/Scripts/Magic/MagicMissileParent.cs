using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissileParent : MonoBehaviour
{
    [SerializeField] Transform testTarget = null;

    [SerializeField] MagicMissile missilePrefab = null;
    [SerializeField] int count = 13;
    Transform target = null;
    

    public void Shoot(in Transform target, Vector3 origin)
    {
        this.target = target;
        transform.position = target.position;
        transform.rotation = Quaternion.LookRotation(origin - target.position);

        for (int i = 0; i < count; i++)
        {
            MagicMissile missile = Instantiate(missilePrefab, origin, Quaternion.identity);
            missile.transform.SetParent(transform);

            float x = Mathf.Cos(Mathf.PI * i / count) * 20.0f;
            float y = Mathf.Sin(Mathf.PI * i / count) * 20.0f;
            float z = (transform.InverseTransformPoint(origin) * 0.5f).z;

            missile.Shoot(new Vector3(x, y, z));
        }
    }

    private void OnEnable()
    {
        Shoot(testTarget, transform.position);
    }
    private void Awake()
    {
        enabled = false;
    }
    private void Update()
    {
        transform.position = target.position;
    }
}
