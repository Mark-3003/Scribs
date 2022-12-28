using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DEBUGSCRIPT : MonoBehaviour
{
    public Text fpsText;
    public Slider loadingBar;
    public GameObject fps;

    private float timer;
    private int frames;
    private void Update()
    {
        frames += 1;
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha0))
            StartCoroutine(LoadSceneAsync(0));
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            StartCoroutine(LoadSceneAsync(1));
        else if (Input.GetKeyDown(KeyCode.Equals))
            ToggleFPS();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(timer >= 0.75f)
        {
            fpsText.text = "FPS: " + (frames * (1 / 0.75f)).ToString();
            frames = 0;
            timer = 0;
        }
    }
    IEnumerator LoadSceneAsync(int id)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(id);

        loadingBar.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;

            yield return null;
        }
    }
    void ToggleFPS()
    {
        fps.SetActive(!fps.activeSelf);
    }
}
