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
        }
        
        if ((delta.x > distanceLimiter || delta.x < -distanceLimiter) && collisionSide == 0) // d > 4 OR d < -4
        {
            speedUp = Mathf.Lerp(speedUp, speed, Time.fixedDeltaTime * 2);
            movementVector = delta.x / absoluteX;
            movement = new Vector2(movementVector * speedUp, 0);
        }
        else
        {
            speedUp = Mathf.Lerp(speedUp, 0, Time.fixedDeltaTime * 10);
            movement = new Vector2(movementVector * speedUp, 0);
        }

        rb.MovePosition(rb.position += (movement) * Time.fixedDeltaTime);
        rb.position += ((velocity * Time.deltaTime) + snapVector);
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
    void WTF()
    {
        // Finds distance from AI centre to ground
        RaycastHit2D hit = new RaycastHit2D();
        hit = Physics2D.Raycast(feet.position, -transform.up, 1f, ground);

        // If in ground shoot a ray up
        if (hit.collider == null)
        {
            hit = Physics2D.Raycast(feet.position, transform.up, Mathf.Infinity, ground);
        }

        float _rayDelta = (hit.point.y - feet.position.y) / (Mathf.Abs(hit.point.y - feet.position.y));
        Debug.Log("RAY DELTA: " + _rayDelta + ". Hit distance: " + hit.distance);

        if (_rayDelta == -1)
        {
            snapVector = new Vector2(0, -(hit.distance + snapOffset));
            Debug.Log("Snap Vector: " + snapVector);
        }
        else if (_rayDelta == 1)
        {
            snapVector = new Vector2(0, (hit.distance + snapOffset));
            Debug.Log("Snap Vector: " + snapVector);
        }
        else
        {
            snapVector = Vector2.zero;
        }

        // DEBUG
        Debug.DrawLine(feet.position, hit.point, Color.yellow);
    }
}
