using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObstructionFix : MonoBehaviour
{
    Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TransparentObject>() != null)
        {
            other.gameObject.GetComponent<TransparentObject>().Transparent();
        }
        else if (other.gameObject.GetComponent<CameraObstructionHelper>() != null)
        {
            other.gameObject.GetComponent<CameraObstructionHelper>().hide();
        }
        else if(other.gameObject.GetComponent<MeshRenderer>() != null)
        {
            other.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TransparentObject>() != null)
        {
            other.gameObject.GetComponent<TransparentObject>().Solid();
        }
        else if (other.gameObject.GetComponent<CameraObstructionHelper>() != null)
        {
            other.gameObject.GetComponent<CameraObstructionHelper>().unhide();
        }
        else if (other.gameObject.GetComponent<MeshRenderer>() != null)
        {
            other.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
}
