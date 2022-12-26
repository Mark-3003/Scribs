using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Transform point;
    public Vector3 offset;
    public float speed;
    public float shakeMagnitude;

    private Vector3 goTo;

    private void Update()
    {
        point.position = Vector3.Lerp(point.position, player.position + offset,speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            StartCoroutine(Shake(shakeMagnitude));
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopAllCoroutines();
        }

        transform.position = point.position + goTo;
    }
    public IEnumerator Shake(float magnitude)
    {
        float elapsedTime = 0f;

        while(elapsedTime < 1)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            goTo = new Vector3(xOffset, yOffset);

            elapsedTime += 1;
            yield return null;
        }
    }
}