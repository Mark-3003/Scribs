using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    [Header("Wave Settings")]
    public int wave;
    public int maxWave;
    public bool spawnADogo;
    public WaveEnemies[] waveEnems;
    public float spawnOffset;
    public float leftOffset;
    public float rightOffset;
    public LayerMask ground;

    [Header("Text")]
    public TMP_Text waveText;
    public Slider waveSlider;

    [Header("Enemies")]
    public GameObject dogo;

    [Header("Points")]
    public Transform player;
    public Transform leftSide;
    public Transform rightSide;

    [Header("HIDDEN")]
    public bool active;
    public WaveEnemies activeWave;
    public float timer;
    public Transform spawnPoint;
    public bool enemiescanspawn;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= activeWave.timerTime)
        {
            timer -= activeWave.timerTime;
            SpawnInEnemy();
        }
    }

    public void SpawnInEnemy()
    {
        for(int i = 0; i < activeWave.dogos; i++)
        {
            int _ran = Random.Range(0, 2);
            if(_ran == 0){
                spawnPoint = leftSide;
            }
            else{
                spawnPoint = rightSide;
            }
            if (enemiescanspawn)
            {
                Enemy _enem = Instantiate(dogo, spawnPoint.position, Quaternion.Euler(0, 0, 0)).GetComponent<Enemy>();

                RaycastHit2D hit = new RaycastHit2D();
                hit = Physics2D.Raycast(spawnPoint.position, -transform.up, Mathf.Infinity, ground);

                _enem.gameObject.transform.up = hit.normal;
            }
        }
    }
    public void NextWave()
    {
        activeWave = waveEnems[wave];
        wave += 1;
        waveText.text = "Wave: " + wave;
    }
    public void ActivateScript()
    {
        active = true;
        activeWave = waveEnems[0];
        wave = 1;
        waveText.enabled = true;
        waveText.text = "Wave: " + wave;
        ResetEnemySpawns();
        SpawnInEnemy();
    }
    void ResetEnemySpawns()
    {
        RaycastHit2D hit = new RaycastHit2D();

        Vector2 _calPos;
        int _ran = Random.Range(0, 1);
        float _delta = player.position.x - transform.position.x;
        float _dir = Mathf.Abs(_delta) / _delta;

        _calPos = new Vector2(-spawnOffset + transform.position.x, transform.position.y);

        hit = Physics2D.Raycast(_calPos, transform.up, Mathf.Infinity, ground);

        if (hit.collider == null)
        {
            hit = Physics2D.Raycast(_calPos, -transform.up, Mathf.Infinity, ground);
        }
        Debug.DrawLine(_calPos, hit.point, Color.yellow);
        leftSide.position = new Vector2(hit.point.x, hit.point.y + dogo.GetComponent<BoxCollider2D>().size.y + leftOffset);

        _calPos = new Vector2(spawnOffset + transform.position.x, transform.position.y);

        hit = Physics2D.Raycast(_calPos, transform.up, Mathf.Infinity, ground);

        if (hit.collider == null)
        {
            hit = Physics2D.Raycast(_calPos, -transform.up, Mathf.Infinity, ground);
        }
        Debug.Log("hit point: " + hit.point + ". _dir: " + _dir);
        Debug.DrawLine(_calPos, hit.point, Color.yellow);
        rightSide.position = new Vector2(hit.point.x, hit.point.y + dogo.GetComponent<BoxCollider2D>().size.y + rightOffset);
    }
    public void SetSlider(float _charge)
    {
        waveSlider.value = _charge;
    }
    public void ToggleEnemies()
    {
        enemiescanspawn = !enemiescanspawn;
    }
}
[System.Serializable]
public struct WaveEnemies
{
    public int dogos;
    public int guys;
    public int _3;
    public float timerTime;
}
