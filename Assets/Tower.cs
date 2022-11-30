using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float health;
    public SpriteRenderer tower;
    public GameObject placer;
    public bool activatable;
    private void Update()
    {
        if (activatable)
            if (Input.GetKeyDown(KeyCode.E))
                Activate();
    }
    void Activate()
    {
        tower.enabled = true;
        placer.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            activatable = true;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            activatable = false;
    }
}
