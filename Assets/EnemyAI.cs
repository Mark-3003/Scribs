using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class EnemyAI : MonoBehaviour
{
    public SpriteRenderer sp;
    public Rigidbody2D rb;
    public float gravity = -10;
    public float speed = 1;
    public float runawayMultiplier = 1;
    public LayerMask ground;
    public Transform target;
    public Transform feet;
    public Vector2 delta;
    public float distance;
    public float distanceLimiter = 3;
    public float distanceTolerance = 2;
    public BoxCollider2D collider;
    public float absolute;
    public bool grounded;
    public Vector2 velocity;
    public Vector2 movement;
    public float speedUp;
    public float movementVector;
    private void Awake()
    {
        target = GameObject.Find("Player").transform;
    }
    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(feet.position, 0.1f, ground);

        delta = target.position - transform.position;
        distance = delta.x;
        absolute = Mathf.Abs(delta.x);

        velocity.y += gravity * Time.fixedDeltaTime;

        if (grounded && velocity.y < 0)
        {
            velocity.y = 0f;

            RaycastHit2D hit = new RaycastHit2D();
            hit = Physics2D.Raycast(transform.position, -transform.up, Mathf.Infinity, ground);

            if (hit.collider != null)
                if (hit.distance <= (collider.size.y / 2) + 0.01f)
                    transform.position = transform.position + new Vector3(0, collider.size.y / 2, 0);
        }

        if (distance > distanceLimiter || distance < -distanceLimiter) // d > 4 OR d < -4
        {
            speedUp = Mathf.Lerp(speedUp, speed, Time.fixedDeltaTime * 2);
            movementVector = distance / absolute;
            movement = new Vector2(movementVector * speedUp, 0);
        }
        else if (distance < distanceTolerance && (distance / Mathf.Abs(distance) == 1) || distance > -distanceTolerance && (distance / Mathf.Abs(distance)) == -1)
        {
            speedUp = Mathf.Lerp(speedUp, speed * runawayMultiplier, Time.fixedDeltaTime * 2);
            movementVector = distance / -absolute;
            movement = new Vector2(movementVector * speedUp, 0);
        }
        else
        {
            speedUp = Mathf.Lerp(speedUp, 0, Time.fixedDeltaTime * 2);
            movement = new Vector2(movementVector * speedUp, 0);
        }

        rb.MovePosition(rb.position += (movement + velocity) * Time.fixedDeltaTime);
    }
    private void Update()
    {
        if (distance / absolute == 1)
        {
            sp.flipX = false;
        }
        else if (distance / absolute == -1)
        {
            sp.flipX = true;
        }
    }
}
