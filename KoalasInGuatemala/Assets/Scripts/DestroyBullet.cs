using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Object.Destroy(this.gameObject);
        // var collisionName = collision.gameObject.name;
        // if (collisionName == "Tree" || collisionName == "Branch")
        // {
        //     Object.Destroy(this.gameObject, 0f);
        // }
    }
}
