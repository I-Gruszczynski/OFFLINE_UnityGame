using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    private Color kolorobj;
    public GameObject Object;

    public void Awake()
    {
        Solid();
    }

    public void Solid()
    {
        //#####OLDMETODA#####//
        //Object.SetActive(true);
        //transparentObject.SetActive(false);
        kolorobj = Object.GetComponent<Renderer>().material.color;
        //Debug.Log("testSOLID curr = " + Object.GetComponent<Renderer>().material.color);
        Object.GetComponent<Renderer>().material.color = new Color(kolorobj.r, kolorobj.g, kolorobj.b, 1);
    }

    public void Transparent()
    {
        //#####OLDMETODA#####//
        //Object.SetActive(false);
        //transparentObject.SetActive(true);
        kolorobj = Object.GetComponent<Renderer>().material.color;
        //Debug.Log("testTRANSPARENT curr = "+ Object.GetComponent<Renderer>().material.color);
        Object.GetComponent<Renderer>().material.color = new Color(kolorobj.r, kolorobj.g, kolorobj.b, 0.1f);
    }
}
