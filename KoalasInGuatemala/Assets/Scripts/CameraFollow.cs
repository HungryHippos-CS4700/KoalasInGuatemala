using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float dampTime;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update () 
    {
        Vector3 destination = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}
