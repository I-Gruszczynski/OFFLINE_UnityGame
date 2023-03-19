using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothSceneSwitchALLOWER : MonoBehaviour
{
    public SmoothSceneLoader loader;

    public void SmoothLoader_Allowchange()
    {
        loader.loadOperation.allowSceneActivation = true;
    }
}
