using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLauncher : Singletone<SceneLauncher>
{
    private Coroutine sceneChange = null;
    public bool isFirst = true;

    public void SceneChange(string scene)
    {
        if (sceneChange != null)
        {
            StopCoroutine(sceneChange);
            sceneChange = null;
        }

        sceneChange = StartCoroutine(LoadScne(scene));

    }

    IEnumerator LoadScne(string scene)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);

        while (!asyncOperation.isDone)
        {
            Debug.Log("Loading.... " + asyncOperation.progress * 100);
            yield return null;
        }
    }

}
