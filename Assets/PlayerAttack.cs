using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int type;
    public SpriteRenderer sp;
    public float health = 100;
    public int attackDamage = 20;
    public float attackCooldown = 0.2f;

    public float attackTimer;
    public List<Enemy> enemies = new List<Enemy>();
    private void Update(){
        if (Input.GetButtonDown("Fire1"))
        {
            sp.enabled = true;
            Attack();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            sp.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            type = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            type = 2;
        attackTimer += Time.deltaTime;
    }
    void Attack()
    {
        if(attackTimer >= attackCooldown)
        {
            attackTimer = 0;
            if (type == 1)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Damage(attackDamage);
                }
            }
            else if(type == 2)
            {

            }
            else
            {

            }
        }
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
