using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingObstacle : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, rotateSpeed, 0.0f);
    }
}
