using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlEnemy : Enemy
{
    void Start()
    {
        offset = new Vector3(0f, -1.05f, 0f);
        maxHealth = 5f;
        health = 5f;
    }
}
