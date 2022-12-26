using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int type;
    public GameObject bullet;
    public SpriteRenderer sp;
    public Transform hitbox;
    public Transform hitPivot;
    public float health = 100;
    public int attackDamage = 20;
    public float attackWait = 0.05f;

    public float attackTimer;
    public List<Enemy> enemies = new List<Enemy>();

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            sp.enabled = true;

            if (type == 1)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Damage(attackDamage);
                }
            }
        }
        else if (Input.GetButton("Fire1"))
        {
            if (type == 2 && attackTimer > attackWait)
            {
                attackTimer = 0;
                Instantiate(bullet, hitbox.position, hitPivot.rotation);
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            sp.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            type = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            type = 2;
    }
    public void Hit(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Debug.Log("L bozo");
        }
    }
    public void UpdateList(List<Enemy> _enemies)
    {
        enemies = _enemies;
    }
}
