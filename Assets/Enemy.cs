using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float health = 100;

    public Transform lookVector;
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
    public void Death()
    {
        Destroy(gameObject);
        Destroy(lookVector);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bullet"))
        {
            GameObject _effect = Instantiate(col.GetComponent<Bullet>().effect, col.transform.position, col.transform.rotation);
            Damage(col.GetComponent<Bullet>().damage);
            Destroy(col.gameObject);
            Destroy(_effect, 3f);
        }
    }
}
