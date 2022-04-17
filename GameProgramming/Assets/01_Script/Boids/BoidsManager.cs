using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    public static readonly float Bound = 10f;
    public static Vector3 minPos;
    public static Vector3 maxPos;
    public BoidsMovement fishV1Prefab;
    public static List<BoidsMovement> boidsObjects = new List<BoidsMovement>();

    public BoidAdjustment boid;

    public int fishCount = 200;

    private void Start()
    {
        minPos = -Vector3.one * Bound;
        maxPos = Vector3.one * Bound;

        GenerateFishes(boidsObjects, fishV1Prefab, fishCount, boid);
    }

    public void GenerateFishes(List<BoidsMovement> fishes, BoidsMovement fish, int count, BoidAdjustment adjustment)
    {
        for (int i = 0; i < count; i++)
        {
            BoidsMovement obj = Instantiate(fish);

            Vector2 randomPosition = Random.insideUnitSphere * Bound;

            obj.transform.position = randomPosition;
            obj.transform.RandomDirection();
            obj.transform.localScale = Vector3.one * 2f;
            obj.Init(adjustment);

            fishes.Add(obj);
        }
    }
}
