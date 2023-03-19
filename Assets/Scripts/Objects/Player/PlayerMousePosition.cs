using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMousePosition : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
            Plane playerplane = new Plane(Vector3.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hit;

            if (playerplane.Raycast(ray, out hit))
            {
                Vector3 targetpoint = ray.GetPoint(hit);
                Quaternion targetrotation = Quaternion.LookRotation(targetpoint - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, speed * Time.deltaTime);
            }
/*        Vector3 mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
        }
        Debug.Log(mousePosition);
        Vector3 direction = new Vector3(0,mousePosition.y - transform.position.y, 0);

        transform.up  = direction;*/
    }
}
