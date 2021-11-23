using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelBehavior : MonoBehaviour
{
    // private bool onRight;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject acorn;
    [SerializeField] private bool throwAcorn;
    
    private IEnumerator ThrowAcorn(int side)
    {
        throwAcorn = false;
        GameObject acornClone = Instantiate(acorn, transform.position, Quaternion.identity);
        Rigidbody2D acornRB = acornClone.GetComponent<Rigidbody2D>();
        acornRB.velocity = new Vector2(side * 12f, 8f);
        acornRB.angularVelocity = Random.Range(-720, 720);
        yield return new WaitForSeconds(1.5f);
        throwAcorn = true;
    }

    //Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        throwAcorn = true;
    }
    void FixedUpdate()
    {
        if (transform.position.x < 0f)
        {
            rb.velocity = new Vector2(2f, rb.velocity.y);
            if (transform.position.x > -8) {
                rb.velocity = new Vector2(0, 0);
                if (throwAcorn)
                    StartCoroutine(ThrowAcorn(1));
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            rb.velocity = new Vector2(-2f, rb.velocity.y);
            if (transform.position.x < 8) {
                rb.velocity = new Vector2(0, 0);
                if (throwAcorn)
                    StartCoroutine(ThrowAcorn(-1));
            }
        }
    }
}
