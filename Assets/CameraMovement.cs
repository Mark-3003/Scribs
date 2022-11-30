using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float speed;

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset ,speed * Time.deltaTime);
    }
}