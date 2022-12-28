using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rb;
    public GameObject effect;
    public float speed;
 
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 10f);
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
