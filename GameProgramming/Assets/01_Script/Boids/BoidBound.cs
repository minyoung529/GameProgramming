using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBound : MonoBehaviour
{
    void Start()
    {
        transform.localScale = Vector3.one * BoidsManager.Bound * 2f;
    }
}
