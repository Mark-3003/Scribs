using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rb;
    public GameObject effect;
    public float speed;

    private float timer;
 
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 10)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ground") || col.CompareTag("Enemy"))
        {
            GameObject _effect = Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(_effect, 3f);
        }
    }
}
