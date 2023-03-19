using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObstructionHelper : MonoBehaviour
{
    private List<MeshRenderer> Meshies;
    private void Awake()
    {
        Meshies = new List<MeshRenderer>();
    }

    public void hide()
    {
        Meshies.Clear();
        foreach (Transform child in transform)
        {
            if(child.gameObject.GetComponent<MeshRenderer>() != null)
            {
                if (child.gameObject.GetComponent<MeshRenderer>().enabled)
                {
                    Meshies.Add(child.gameObject.GetComponent<MeshRenderer>());
                }
            }
        }
        foreach (MeshRenderer inlist in Meshies)
        {
            inlist.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }


    public void unhide()
    {
        foreach (MeshRenderer inlist in Meshies)
        {
            inlist.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
        Meshies.Clear();
    }
}
