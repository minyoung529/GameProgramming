using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsMovement : MonoBehaviour
{
    public BoidsObjectType boidsType;
    private Vector3 forward;
    private Vector3 direction;
    private float fieldOfViewAngle;

    BoidAdjustment adjustment;

    public void Init(BoidAdjustment adjustment)
    {
        this.adjustment = adjustment;
    }

    void Update()
    {
        forward = -transform.right;
        direction = CohesionDirection() * adjustment.cohesionSpeed;
        direction += AlignmentDirection() * adjustment.alignmentSpeed;
        direction += SeparationDirection() * adjustment.seperationSpeed;
        direction.Normalize();

        if (direction == Vector3.zero)
        {
            Debug.Log("Sdf");
        }
        else
        {
            Debug.Log("fff");
        }

        transform.right -= Vector3.Lerp(forward, direction, Time.deltaTime * adjustment.rotationSpeed).normalized;
        transform.position += forward * adjustment.speed * Time.deltaTime;
    }

    /// <summary>
    /// 이웃들의 중심값으로 향하는 방향을 반환하는 함수
    /// </summary>
    private Vector3 CohesionDirection()
    {
        List<BoidsMovement> objs = SetBoidsObjects(adjustment.cohesionDistance);
        if (objs.Count == 0) return Vector3.zero;

        Vector3 direction = Vector3.zero;

        for (int i = 0; i < objs.Count; i++)
        {
            direction += objs[i].transform.position;
        }

        direction /= objs.Count;
        direction -= transform.position;

        return direction.normalized;
    }

    /// <summary>
    /// 이웃들이 향하는 평균 방향을 반환하는 함수
    /// </summary>
    private Vector3 AlignmentDirection()
    {
        List<BoidsMovement> objs = SetBoidsObjects(adjustment.alignmentDistance);
        Vector3 direction = Vector3.zero;

        for (int i = 0; i < objs.Count; i++)
            direction += objs[i].forward;

        direction /= objs.Count;
        return direction.normalized;
    }

    private Vector3 SeparationDirection()
    {
        List<BoidsMovement> objs = SetBoidsObjects(adjustment.seperationDistance);
        Vector3 direction = Vector3.zero;

        for (int i = 0; i < objs.Count; i++)
            direction += (transform.position - objs[i].transform.position);

        direction /= objs.Count;
        return direction.normalized;
    }

    /// <summary>
    /// 같은 종류의 가까이 있는 이웃들을 반환하는 함수
    /// </summary>
    private List<BoidsMovement> SetBoidsObjects(float distance)
    {
        List<BoidsMovement> objs = new List<BoidsMovement>();

        for (int i = 0; i < BoidsManager.boidsObjects.Count; i++)
        {
            BoidsMovement obj = BoidsManager.boidsObjects[i];

            if (obj == this || obj.boidsType != boidsType) continue;
            if (Mathf.Abs((transform.position - obj.transform.position).sqrMagnitude) >= Mathf.Pow(distance, 2)) continue;
            if (!IsInFieldOfView(obj.transform.position)) continue;

            objs.Add(obj);
        }

        return objs;
    }

    private bool IsInFieldOfView(Vector3 position)
    {
        //Vector3.Dot(transform.position, position) > Mathf.Cos(fieldOfViewAngle * 0.5f * Mathf.Deg2Rad)
        //Vector3.Angle(transform.position, position - transform.position) <= fieldOfViewAngle
        bool sdf = Vector3.Angle(transform.position, position - transform.position) <= fieldOfViewAngle;
        if (sdf) Debug.Log("true");
        return sdf;
    }
}

public enum BoidsObjectType
{
    FishV1
}

[System.Serializable]
public class BoidAdjustment
{
    [Header("Basic Setup")]
    [Range(0f, 10f)]
    public float speed = 1.5f;
    [Range(0f, 10f)]
    public float rotationSpeed = 0.7f;

    [Header("Speed")]
    [Range(0f, 10f)]
    public float cohesionSpeed = 1f;
    [Range(0f, 10f)]
    public float alignmentSpeed = 1f;
    [Range(0f, 10f)]
    public float seperationSpeed = 1f;

    [Header("Distance")]
    [Range(0f, 10f)]
    public float cohesionDistance = 1f;
    [Range(0f, 10f)]
    public float alignmentDistance = 1f;
    [Range(0f, 10f)]
    public float seperationDistance = 1f;

    public BoidAdjustment(float speed, float rotationSpeed,
                          float cohesionSpeed, float alignmentSpeed, float seperationSpeed,
                          float cohesionDistance, float alignmentDistance, float seperationDistance)
    {
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;

        this.cohesionSpeed = cohesionSpeed;
        this.alignmentSpeed = alignmentSpeed;
        this.seperationSpeed = seperationSpeed;

        this.cohesionDistance = cohesionDistance;
        this.alignmentDistance = alignmentDistance;
        this.seperationDistance = seperationDistance;
    }
}