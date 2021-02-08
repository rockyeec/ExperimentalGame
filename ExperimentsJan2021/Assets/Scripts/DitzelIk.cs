using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DitzelIk : MonoBehaviour
{
    [SerializeField] int chainLength = 3;

    [SerializeField] Transform target = null;
    [SerializeField] Transform pole = null;

    [SerializeField] int iterations = 10;

    public float delta = 0.001f;

    [Range(0.0f, 1.0f)]
    public float snapBackStrength = 1.0f;

    float[] boneLengths;
    float completeLength;
    Transform[] bones;
    Vector3[] positions;


    private void Awake()
    {
        bones = new Transform[chainLength + 1];
        positions = new Vector3[chainLength + 1];
        boneLengths = new float[chainLength];

        completeLength = 0.0f;

        Transform current = transform;
        for (int j = 0; j < chainLength + 1; j++)
        {
            int i = chainLength - j;
            bones[i] = current;

            if (i == chainLength)
            {

            }
            else
            {
                boneLengths[i] = (bones[i + 1].position - current.position).magnitude;
                completeLength += boneLengths[i];
            }

            current = current.parent;
        }
    }

    private void Update()
    {
        for (int i = 0; i < chainLength + 1; i++)
        {
            positions[i] = bones[i].position;
        }

        if (! completeLength.IsLonger(target.position, bones[0].position))
        {
            var direction = (target.position - positions[0]).normalized;

            for (int i = 1; i < chainLength + 1; i++)
            {
                positions[i] = positions[i - 1] + direction * boneLengths[i - 1];
            }
        }
        else
        {
            for (int i = 0; i < iterations; i++)
            {
                if (!delta.IsLonger(positions[positions.Length - 1], target.position))
                    break;

               // if (i == positions)
            }
        }

        for (int i = 0; i < chainLength + 1; i++)
        {
            bones[i].position = positions[i];
        }
    }
}
