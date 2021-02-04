using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static T GetRandom<T>(this T[] items)
    {
        return items[Random.Range(0, items.Length)];
    }

    public static T GetRandom<T>(this List<T> items)
    {
        return items[Random.Range(0, items.Count)];
    }

    public static Collider GetNearest(this Collider[] colliders, Transform transform)
    {
        return colliders.OrderBy(t => (transform.position - t.transform.position).sqrMagnitude).FirstOrDefault();
    }
    public static Collider GetNearestExcludingSelf(this Collider[] colliders, Collider self)
    {
        return colliders.Except(new Collider[1] { self }).OrderBy(t => (self.transform.position - t.transform.position).sqrMagnitude).FirstOrDefault();
    }

    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }

    public static Quaternion WithEuler(this Quaternion original, float? x = null, float? y = null, float? z = null)
    {
        Vector3 originalEuler = original.eulerAngles;
        return Quaternion.Euler(
            originalEuler.With(
                x ?? originalEuler.x,
                y ?? originalEuler.y,
                z ?? originalEuler.z)
            );
    }

    public static bool IsNextInterval(this ref float time, in float intervalDuration)
    {
        if (Time.time >= time)
        {
            time = Time.time + intervalDuration;
            return true;
        }
        return false;
    }

    public static bool IsWithinInterval(this ref float elapsed, in float duration, in float deltaTime)
    {
        if (elapsed < duration)
        {
            elapsed += deltaTime;
            return true;
        }
        return false;
    }

    public static T[] GetComponentsOnlyInChildren<T>(this Component component) where T:Component
    {
        T[] children = component.GetComponentsInChildren<T>();
        List<T> childList = new List<T>();
        childList.AddRange(children);
        childList.Remove(component.GetComponent<T>());
        return childList.ToArray();
    }

    public static int LoopIndex(this ref int index, int max)
    {
        while (index < 0)
        {
            index += max;
        }
        return index %= max;
    }

    public static bool IsLonger(this float value, Vector3 pointA, Vector3 pointB)
    {
        return value * value > (pointA - pointB).sqrMagnitude;
    }

    public static float SmootherStep(this float t)
    {
        return t * t * t * (t * (6.0f * t - 15.0f) + 10.0f);
    }

    public static Vector3 QuadraticCurve(this Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.LerpUnclamped(a, b, t);
        Vector3 bc = Vector3.LerpUnclamped(b, c, t);
        return Vector3.LerpUnclamped(ab, bc, t);
    }
}
