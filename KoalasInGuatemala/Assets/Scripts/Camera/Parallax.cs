using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startposx, startposy;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;

    void Start()
    {
        startposx = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        startposy = transform.position.y;
    }

    void Update()
    {
        float distancex = cam.transform.position.x * parallaxEffect;
        float distancey = cam.transform.position.y * parallaxEffect;
        transform.position = new Vector3(startposx + distancex, startposy + distancey, transform.position.z);
    }
}
