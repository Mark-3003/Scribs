using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public SpriteRenderer sp;
    public Rigidbody2D rb;
    public float gravity;
    public float speed;
    public float airSpeed;
    public float sprintMultiplier = 1;
    public float jumpHeight;
    public LayerMask ground;
    public Transform feet;
    public Transform hitPivot;

    public int type;
    public float feetRadius;
    public Vector2 velocity;
    public float move;
    public bool jump;
    public bool grounded;
    public Vector2 rbvelo;
    public Vector2 animVector;
    public Vector2 mousePos;
    private void Start()
    {
        
    }

    void Update(){
        move = Input.GetAxis("Horizontal");
        animVector.x = Input.GetAxisRaw("Horizontal");
        animVector.y = Input.GetAxisRaw("Vertical");

        grounded = Physics2D.OverlapCircle(feet.position, feetRadius, ground);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            type = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            type = 2;
    }
    void FixedUpdate(){
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
        if(animVector.x < -0.1 && animVector.y == 0)
        {
            sp.flipX = true;
            if(type == 1)
                hitPivot.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if(animVector.x > 0.1 && animVector.y == 0)
        {
            sp.flipX = false;
            if (type == 1)
                hitPivot.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(animVector.x > 0.1 && animVector.y > 0.1)
        {
            sp.flipX = false;
            if (type == 1)
                hitPivot.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (animVector.x == 0 && animVector.y > 0.1)
        {
            if (type == 1)
                hitPivot.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (animVector.x < -0.1 && animVector.y > 0.1)
        {
            sp.flipX = true;
            if (type == 1)
                hitPivot.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (animVector.x < -0.1 && animVector.y < -0.1)
        {
            sp.flipX = true;
            if (type == 1)
                hitPivot.rotation = Quaternion.Euler(0, 0, 225);
        }
        else if (animVector.x == 0 && animVector.y < -0.1)
        {
            if (type == 1)
                hitPivot.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (animVector.x > 0.1 && animVector.y < -0.1)
        {
            sp.flipX = false;
            if (type == 1)
                hitPivot.rotation = Quaternion.Euler(0, 0, 315);
        }
        if(type == 2)
        {
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            hitPivot.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
