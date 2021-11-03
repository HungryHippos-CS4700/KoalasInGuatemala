using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    [SerializeField] private LayerMask layerToIgnore;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, 7);
    }
}