using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float health = 100;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    public void Damage(int _damage)
    {
        health -= _damage;
        Debug.Log("Enemy has took " + _damage + " damage");

        if(health <= 0){
            Death();
        }
    }
    void Death()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "ParticleBullets")
        {
            Debug.Log("Bullet hit " + gameObject.name);
        }
    }
}
