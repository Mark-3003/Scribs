using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float yOffset;
    public float speed;

    private Vector2 start;
    void Start()
    {
        Destroy(gameObject, 1 / speed);
        start = transform.position;
        // ADD SCORE HERE
    }
    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, start + new Vector2(0, yOffset), Time.deltaTime * speed);
    }
}
