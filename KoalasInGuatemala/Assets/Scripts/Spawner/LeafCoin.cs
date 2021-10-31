using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafCoin : MonoBehaviour
{
    [SerializeField] private Score score;

    [SerializeField] private float rotationsPerMinute;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            score.addScore(1000);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);

    }
}
