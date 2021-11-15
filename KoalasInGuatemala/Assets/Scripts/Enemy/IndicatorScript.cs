using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask layer;
    Renderer rd;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(rd.isVisible == false)
        // {
        //     if(indicator.activeSelf == false)
        //     {
        //         indicator.SetActive(true);
        //     }
            Vector2 direction = player.transform.position - transform.position;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, 100, layer);
            if(ray.collider != null)
            {
                if (ray.distance < 8f)
                {
                    indicator.SetActive(false);
                }
                else
                {
                    indicator.SetActive(true);
                }
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                indicator.transform.rotation = Quaternion.Euler(180f, 180f, angle);
                indicator.transform.position = ray.point;
            }
        // }
        // else
        // {
        //     if(indicator.activeSelf == true)
        //     {
        //         indicator.SetActive(false);
        //     }
        // }
    }
}
