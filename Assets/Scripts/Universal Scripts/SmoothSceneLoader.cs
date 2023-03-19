using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmoothSceneLoader : MonoBehaviour
{
    public AsyncOperation loadOperation;
    bool loadinginprogress = false;
    public void StartLoadingSceneByName(string name)
    {
        if (!loadinginprogress)
        {
            loadinginprogress = true;
            loadOperation = SceneManager.LoadSceneAsync(name);
            loadOperation.allowSceneActivation = false;
        }
    }
}
