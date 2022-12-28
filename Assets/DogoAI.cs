using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogoAI : MonoBehaviour
{
    public Enemy enemy;
    public SpriteRenderer sp;
    public Rigidbody2D rb;
    public BoxCollider2D collider;
    public float gravity = -10;
    public float speed = 1;
    public LayerMask ground;
    public Transform target;
    public Transform feet;
    public Transform leftCollider;
    public Transform rightCollider;

    public Vector2 lookVector;
    public float distanceLimiter;
    public Vector2 delta;
    public float absoluteX;
    public bool grounded;
    public Vector2 velocity;
    public Vector2 movement;
    public Vector2 snapVector;
    public float movementVector;
    public float speedUp;
    public float collisionSide;
    public float yHeight;

    private RaycastHit2D hit;
    private void Awake()
    {
        target = GameObject.Find("Tower").transform;
        yHeight = collider.size.y * ((Mathf.Abs(collider.offset.y) + 1));
    }
    void FixedUpdate()
    {
        // Shoots ray to ground
        hit = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, ground);

        // Check if grounded
        if (hit.distance <= yHeight + 0.02f + snapVector.y)
            grounded = true;
        else
            grounded = false;

        // Adds gravity to velocity
        velocity.y += gravity * Time.fixedDeltaTime;

        // Check if touching wall
        if (Physics2D.OverlapCircle(leftCollider.position, 0.1f, ground))
        {
            collisionSide = -1;
        }
        else if (Physics2D.OverlapCircle(rightCollider.position, 0.1f, ground))
        {
            collisionSide = 1;
        }
        else
        {
            collisionSide = 0;
        }

        // If grounded do stuff
        if (grounded && velocity.y < 0)
        {
            // Stops velocity
            velocity.y = 0;

            // Sets up direction relative to ground
            lookVector = hit.normal;

            // Snaps AI to ground and sets sprite to slope direction
            transform.position = hit.point + new Vector2(0, yHeight) + snapVector;

            // Rotate sprite to current normal
            sp.transform.up = Vector2.Lerp(sp.transform.up, lookVector, Time.deltaTime * 10);

            // Get delta between target and enemy
            delta = target.position - transform.position;
            absoluteX = Mathf.Abs(delta.x);
        }
        if (grounded)
        {
            // Moves AI along ground
            if ((delta.x > distanceLimiter || delta.x < -distanceLimiter) && collisionSide == 0) // d > 4 OR d < -4
            {
                speedUp = Mathf.Lerp(speedUp, speed, Time.fixedDeltaTime * 2);
                movementVector = delta.x / absoluteX;
                if (movementVector == 1)
                {
                    movement = sp.transform.right * speedUp;
                    Debug.Log("moving right: " + movement);
                }
                else if (movementVector == -1)
                {
                    movement = -sp.transform.right * speedUp;
                    Debug.Log("moving left: " + movement);
                }
            }
            else
            {
                speedUp = Mathf.Lerp(speedUp, 0, Time.fixedDeltaTime * 10);
                if (movementVector == 1)
                    movement = sp.transform.right * speedUp;
                else if (movementVector == -1)
                    movement = -sp.transform.right * speedUp;
            }
            transform.position = new Vector2(transform.position.x + movement.x * Time.fixedDeltaTime, transform.position.y + movement.y * Time.fixedDeltaTime);
        }
        rb.position += velocity * Time.fixedDeltaTime;
    }
    private void Update()
    {
        if (delta.x / absoluteX == 1)
        {
            sp.flipX = false;
        }
        else if (delta.x / absoluteX == -1)
        {
            sp.flipX = true;
        }
    }
}
