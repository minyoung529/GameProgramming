using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
    public static void RandomPosition(this Transform transform, float minX, float maxX, float minY, float maxY)
    {
        transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    public static void RandomPosition(this Transform transform, float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
    }

    public static void RandomDirection(this Transform transform)
    {
        transform.forward = new Vector3(Random.Range(-1f, 1f), /*Random.Range(-1f, 1f)*/0f, Random.Range(-1f, 1f));
    }

    public static void RandomRotation(this Transform transform, bool isX, bool isY, bool isZ)
    {
        transform.rotation = Quaternion.Euler(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));
    }
}
