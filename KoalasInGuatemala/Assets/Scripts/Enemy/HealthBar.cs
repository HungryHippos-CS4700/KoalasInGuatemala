using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Color low;
    [SerializeField] private Color high;

    public void SetHealth(float health, float maxHealth)
    {
        if(health == maxHealth)
        {
            transform.localScale = new Vector3(0, .1f, 0f);
        }
        else
        {
            transform.localScale = new Vector3(2f*health/maxHealth, .5f, 0f);
        }
        GetComponent<SpriteRenderer>().color = Color.Lerp(low, high, health/maxHealth);
    }
}
