using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {   
        var otherName = other.gameObject.name;
        if (otherName == "Tree")
        {
            Object.Destroy(this.gameObject, 0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
