using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    [SerializeField] private LayerMask layerToIgnore;

    void Start()
    {
        print(layerToIgnore.value);
        print((int)layerToIgnore);
        Physics2D.IgnoreLayerCollision(gameObject.layer, 7);
    }
}
