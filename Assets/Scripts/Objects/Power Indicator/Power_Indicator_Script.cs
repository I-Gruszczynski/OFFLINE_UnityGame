using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Indicator_Script : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    public Color on;
    public Color off;
    public Keypad1 keypad1;
    public void FixedUpdate()
    {
        if (keypad1.power)
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }
    public void Enable()
    {
        MeshRenderer.material.color = on;
    }
    public void Disable()
    {
        MeshRenderer.material.color = off;
    }
}
