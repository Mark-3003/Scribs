using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Wave Settings")]
    public int wave;
    public int maxWave;
    public WaveEnemies[] waveEnems;
    public float spawnOffset;
    [Header("Settings")]
    public int charge;
    public TextMesh text;
    public float health;
    public float chargeTimer;
    public float damageTimer = 0.75f;
    public float damage = 1;
    public SpriteRenderer tower;
    public GameObject placer;
    public bool activatable;
    public bool activated;
    public List<Enemy> enemies = new List<Enemy>();
    public List<float> timers = new List<float>();

    public float chargeTime;
    public Vector2 topLeftCorner;
    public Vector2 bottomRightCorner;
    private void Update()
    {
        GetCameraCorners();

        if (activatable)
            if (Input.GetKeyDown(KeyCode.E))
                Activate();
        if (activated)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                timers[i] += Time.deltaTime;
                if (timers[i] >= damageTimer)
                {
                    timers[i] -= damageTimer;
                    health -= damage;
                }
            }

            chargeTime += Time.deltaTime;
            if (chargeTime >= chargeTimer)
            {
                chargeTime -= chargeTimer;
                charge += 1;
                text.text = "Charge: " + charge.ToString() + "%";
            }
        }
    }
    void Activate()
    {
        activated = true;
        tower.enabled = true;
        placer.SetActive(false);
        text.gameObject.GetComponent<MeshRenderer>().enabled = true;
        text.text = "Charge: 0%";
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            activatable = true;
        else if (col.tag == "Enemy")
        {
            enemies.Add(col.GetComponent<Enemy>());
            timers.Add(0);
        }

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            activatable = false;
        else if (col.tag == "Enemy")
        {
            timers.RemoveAt(enemies.IndexOf(col.GetComponent<Enemy>()));
            enemies.Remove(col.GetComponent<Enemy>());
        }
    }
    [System.Serializable]
    public struct WaveEnemies{
        public int dogos;
        public int guys;
        public int _3;
    }
    void GetCameraCorners()
    {
        topLeftCorner = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        bottomRightCorner = -topLeftCorner;
    }
    void SpawnInEnemy(GameObject _enemy)
    {
        float _deltaX = topLeftCorner.x - transform.position.x;
        float _dir = _deltaX / Mathf.Abs(_deltaX);

        if(_dir == -1)
        {
            Instantiate(_enemy, new Vector2(topLeftCorner.x - spawnOffset, 0), Quaternion.Euler(0, 0, 0));
        }
        if (_dir == 1)
        {
            Instantiate(_enemy, new Vector2(topLeftCorner.x + spawnOffset, 0), Quaternion.Euler(0, 0, 0));
        }
    }

}
