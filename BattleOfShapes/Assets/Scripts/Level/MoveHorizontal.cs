using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontal : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    private Rigidbody2D rb;
    private Vector3 _Velocity = Vector3.zero;
    private Vector2 _zero = Vector2.zero;

    private Vector3 targetVelocity = new Vector2(1, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, movementSmoothing);
    }

    private void ChangeDirection()
    {
        targetVelocity.x *= -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Side")
        {
            ChangeDirection();
        }      
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Side")
        {
            ChangeDirection();
        }      
    }


}
