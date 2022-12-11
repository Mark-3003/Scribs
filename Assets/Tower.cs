using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tower : MonoBehaviour
{
    [Header("Settings")]
    public int charge;
    public TextMesh text;
    public TMP_Text waveSliderText;
    public GameObject UI;
    public float health;
    public float chargeTimer;
    public float breakTimer;
    public float damageTimer = 0.75f;
    public float damage = 1;
    public SpriteRenderer tower;
    public GameObject placer;
    public bool activatable;
    public bool activated;
    public List<Enemy> enemies = new List<Enemy>();
    public List<float> timers = new List<float>();

    public bool breaktime;
    public WaveSystem waves;
    public Transform player;
    public float chargeTime;
    public float breakTime;
    private void Awake()
    {
        waves = GetComponent<WaveSystem>();
        player = GameObject.Find("Player").transform;
    }
    private void Update()
    {
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

            if(charge >= 100)
            {
                charge = 0;
                breaktime = true;
                text.text = "Charge: " + charge.ToString() + "%";
                waveSliderText.text = "BREAKTIME:";
                breakTime = 0;
                chargeTime = 0;
                waves.ToggleEnemies();

                GameObject[] allObjs = GameObject.FindObjectsOfType<GameObject>();
                for(int i = 0; i < allObjs.Length; i++)
                {
                    if (allObjs[i].name == "Enemy(Clone)")
                        allObjs[i].GetComponent<Enemy>().Death();
                }
            }

            chargeTime += Time.deltaTime;
            if (chargeTime >= chargeTimer && !breaktime)
            {
                chargeTime -= chargeTimer;
                charge += 1;
                text.text = "Charge: " + charge.ToString() + "%";
                waves.SetSlider(charge);
            }
            else if(chargeTime >= breakTimer && breaktime)
            {
                breaktime = false;
                chargeTime = 0;
                waveSliderText.text = "CHARGE:";
                waves.NextWave();
                waves.ToggleEnemies();
            }
            if (breaktime)
            {
                breakTime += Time.deltaTime;
                waves.SetSlider((breakTime / breakTimer) * 100);
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
        UI.SetActive(true);
        waves.ActivateScript();
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
}
