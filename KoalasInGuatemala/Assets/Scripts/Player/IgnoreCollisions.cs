using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    [SerializeField] private GameObject[] things;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject thing in things)
        {
            Collider2D thingCollider = thing.GetComponent<Collider2D>();
            Collider2D playerCollider = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(thingCollider, playerCollider);
        }
    }
}
