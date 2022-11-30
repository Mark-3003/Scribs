using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHub : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sp;
    public float gravity = -9.81f;
    public float speed = 5;
    public float jumpHeight;
    public float sprintMultiplier;
    public float airSpeed;
    public Transform feet;
    public float feetRadius = 0.1f;
    public LayerMask ground;

    public float move;
    public Vector2 animVector;
    public Vector2 velocity;
    public bool grounded;
    public bool jump;
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        animVector.x = Input.GetAxisRaw("Horizontal");
        animVector.y = Input.GetAxisRaw("Vertical");

        grounded = Physics2D.OverlapCircle(feet.position, feetRadius, ground);
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }
    void FixedUpdate()
    {
        float _speed;

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (jump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            jump = false;
        }

        velocity.y += gravity * Time.fixedDeltaTime;

        if (Input.GetButton("Fire3"))
        {
            _speed = speed * sprintMultiplier;
        }
        else
        {
            _speed = speed;
        }
        if (!grounded && !Input.GetButton("Fire3"))
        {
            _speed /= airSpeed;
        }

        rb.MovePosition(rb.position + (velocity + new Vector2(move * _speed, 0)) * Time.fixedDeltaTime);
        SpriteFlip();
    }
    void SpriteFlip()
    {
        if (animVector.x < -0.1 && animVector.y == 0)
        {
            sp.flipX = true;
        }
        else if (animVector.x > 0.1 && animVector.y == 0)
        {
            sp.flipX = false;
        }
        else if (animVector.x > 0.1 && animVector.y > 0.1)
        {
            sp.flipX = false;
        }
        else if (animVector.x < -0.1 && animVector.y > 0.1)
        {
            sp.flipX = true;
        }
        else if (animVector.x < -0.1 && animVector.y < -0.1)
        {
            sp.flipX = true;
        }
        else if (animVector.x > 0.1 && animVector.y < -0.1)
        {
            sp.flipX = false;
        }
    }
}
