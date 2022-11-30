using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour
{
    public PlayerAttack player;
    public List<Enemy> enemy = new List<Enemy>();
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            enemy.Add(col.GetComponent<Enemy>());
            player.UpdateList(enemy);
            Debug.Log(col.name + " has entered trigger");
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            enemy.Remove(col.GetComponent<Enemy>());
            player.UpdateList(enemy);
            Debug.Log(col.name + " has left trigger");
        }
    }
}
