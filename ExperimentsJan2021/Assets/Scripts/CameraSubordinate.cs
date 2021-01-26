using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraManager))]
public class CameraSubordinate : MonoBehaviour
{
    protected CameraManager Manager { get; private set; }
    private void Reset()
    {
        Manager = GetComponent<CameraManager>();
    }
    virtual protected void Awake()
    {
        if (Manager == null) Reset();
    }
}
