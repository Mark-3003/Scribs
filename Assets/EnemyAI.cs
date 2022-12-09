using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class EnemyAI : MonoBehaviour
{
    public SpriteRenderer sp;
    public Rigidbody2D rb;
    public BoxCollider2D collider;
    public float gravity = -10;
    public float speed = 1;
    public float snapOffset;
    public LayerMask ground;
    public Transform target;
    public Transform feet;
    public Transform leftCollider;
    public Transform rightCollider;

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

    private void Awake()
    {
        target = GameObject.Find("Tower").transform;
    }
    private void FixedUpdate()
    {
        // Check if touching ground
        grounded = Physics2D.OverlapCircle(feet.position, 0.1f, ground);

        // Find what wall AI is touching
        if(Physics2D.OverlapCircle(leftCollider.position, 0.1f, ground)){
            collisionSide = -1;
        }
        else if(Physics2D.OverlapCircle(rightCollider.position, 0.1f, ground)){
            collisionSide = 1;
        }
        else{
            collisionSide = 0;
        }

        // Delta.x = Distance on x axis
        delta = target.position - transform.position;

        // x Distance but always positive
        absoluteX = Mathf.Abs(delta.x);

        // Adding gravity to the AI
        velocity.y += gravity * Time.fixedDeltaTime;

        if (grounded && velocity.y < 0)
        {
            // If grounded remove fall velocity
            velocity.y = 0f;

            // Finds distance from AI centre to ground
            RaycastHit2D hit = new RaycastHit2D();
            hit = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, ground);

            // If AI close to floor, snap it to floor
            if (hit.collider != null)
                if (hit.distance <= collider.size.y / 2 + 0.01f)
                    snapVector = new Vector2(0, -(hit.distance - collider.size.x / 2 + snapOffset));
        }

        if ((delta.x > distanceLimiter || delta.x < -distanceLimiter) && collisionSide == 0) // d > 4 OR d < -4
        {
            speedUp = Mathf.Lerp(speedUp, speed, Time.fixedDeltaTime * 2);
            movementVector = delta.x / absoluteX;
            movement = new Vector2(movementVector * speedUp, 0);
        }
        else if(collisionSide == -1)
        {
            RaycastHit2D hit = new RaycastHit2D();

            hit = Physics2D.Raycast(leftCollider.position, -transform.right, 1f, ground);

            Vector2 _newPos = hit.point + new Vector2(collider.size.x / 2, 0) - collider.offset;
            movement = Vector2.zero;
            speedUp = 0;

            rb.position = _newPos;
        }
        else if(collisionSide == 1)
        {
            RaycastHit2D hit = new RaycastHit2D();

            hit = Physics2D.Raycast(rightCollider.position, transform.right, 1f, ground);

            Vector2 _newPos = hit.point - new Vector2(collider.size.x / 2, 0) + collider.offset;
            movement = Vector2.zero;
            speedUp = 0;

            rb.position = _newPos;
        }
        else
        {
            speedUp = Mathf.Lerp(speedUp, 0, Time.fixedDeltaTime * 10);
            movement = new Vector2(movementVector * speedUp, 0);
        }

        rb.MovePosition(rb.position += (movement + velocity + snapVector) * Time.fixedDeltaTime);
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
