using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("_ran: " + _ran);
            if(_ran == 0){
                spawnPoint = leftSide;
            }
            else{
                spawnPoint = rightSide;
            }
            Instantiate(dogo, spawnPoint.position, Quaternion.Euler(0, 0, 0));
        }
    }
    public void NextWave()
    {
        activeWave = waveEnems[wave];
        wave += 1;
    }
    public void ActivateScript()
    {
        active = true;
        activeWave = waveEnems[0];
        wave = 1;
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
        Debug.Log("_delta " + _dir);

        _calPos = new Vector2(-spawnOffset + transform.position.x, transform.position.y);

        hit = Physics2D.Raycast(_calPos, transform.up, ground);

        if (hit.collider == null)
        {
            hit = Physics2D.Raycast(_calPos, -transform.up, ground);
        }
        Debug.Log("hit point: " + hit.point + ". _dir: " + _dir);
        Debug.DrawLine(_calPos, hit.point, Color.yellow);
        leftSide.position = new Vector2(hit.point.x, hit.point.y + dogo.GetComponent<BoxCollider2D>().size.y + leftOffset);

        _calPos = new Vector2(spawnOffset + transform.position.x, transform.position.y);

        hit = Physics2D.Raycast(_calPos, transform.up, ground);

        if (hit.collider == null)
        {
            hit = Physics2D.Raycast(_calPos, -transform.up, ground);
        }
        Debug.Log("hit point: " + hit.point + ". _dir: " + _dir);
        Debug.DrawLine(_calPos, hit.point, Color.yellow);
        rightSide.position = new Vector2(hit.point.x, hit.point.y + dogo.GetComponent<BoxCollider2D>().size.y + rightOffset);
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
