using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DEBUGSCRIPT : MonoBehaviour
{
    public Slider loadingBar;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            StartCoroutine(LoadSceneAsync(0));
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            StartCoroutine(LoadSceneAsync(1));

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
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
}
