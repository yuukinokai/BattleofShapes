using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;

    private Rigidbody2D rb;
    private Vector3 _Velocity = Vector3.zero;
    private Vector2 _zero = Vector2.zero;

    private Vector3 targetVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetVelocity.z = 0;
        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Math.Abs(rb.velocity.x) < 0.5f || Math.Abs(rb.velocity.y) < 0.5f)
        {
            ChangeDirection();
        }*/
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref _Velocity, movementSmoothing);
    }

    private void ChangeDirection()
    {
        Debug.Log("Changing direction");
        targetVelocity.x = UnityEngine.Random.Range(-5.0f, 5.0f);
        targetVelocity.y = UnityEngine.Random.Range(-5.0f, 5.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //ChangeDirection();
    }
}
