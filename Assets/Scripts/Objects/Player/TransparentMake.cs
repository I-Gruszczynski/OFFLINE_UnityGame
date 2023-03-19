using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentMake : MonoBehaviour
{
    [SerializeField]private List<TransparentObject> notTransparent;
    [SerializeField]private List<TransparentObject> alreadyTransparent;
    [SerializeField] private Transform player;
    private new Transform camera;
    public Vector3 offset;
    Vector3 tempcampos;

    // Update is called once per frame
    private void Awake()
    {
        notTransparent = new List<TransparentObject>();
        alreadyTransparent = new List<TransparentObject>();

        camera = this.gameObject.transform;
    }
    void Update()
    {
        GetAll();
        MakeSolid();
        MakeTransparent();
    }

    void GetAll()
    {
        notTransparent.Clear();
        tempcampos = camera.position;
        tempcampos += offset;
        float maxDistance = Vector3.Magnitude(tempcampos - player.position);
        Ray ray = new Ray(tempcampos, player.position - tempcampos);

        var hits = Physics.RaycastAll(ray, maxDistance);

        foreach(var hit in hits)
        {
            if(hit.collider.gameObject.TryGetComponent(out TransparentObject transparentObject))
            {
                if(!notTransparent.Contains(transparentObject))
                {
                    notTransparent.Add(transparentObject);
                }
            }
        }
    }

    void MakeTransparent()
    {
        for(int i = 0;i < notTransparent.Count; i++)
        {
            TransparentObject transparentObject = notTransparent[i];

            if(!alreadyTransparent.Contains(transparentObject))
            {
                transparentObject.Transparent();
                alreadyTransparent.Add(transparentObject);
            }
        }
    }

    void MakeSolid()
    {
        for(int i = alreadyTransparent.Count-1;i >= 0; i--)
        {
            TransparentObject transparentObject = alreadyTransparent[i];

            if(!notTransparent.Contains(transparentObject))
            {
                transparentObject.Solid();
                alreadyTransparent.Remove(transparentObject);
            }
        }
    }
}
