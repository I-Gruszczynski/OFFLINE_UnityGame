using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorArea;
    public bool czyBron = true;
    bool czyCelownik = false;

    // Start is called before the first frame update
    void Start()
    {

     }

    // Update is called once per frame
    void Update()
    {
      //  if (Input.GetKeyDown("l"))
      //      czyBron = !czyBron;
        if (czyBron)
        {
            if (!czyCelownik)
            {
                Vector2 hotspot = new Vector2(cursorArea.width / 2f, cursorArea.height / 2f);
                Cursor.SetCursor(cursorArea, hotspot, CursorMode.Auto);
                czyCelownik = true;
            }
        }
        else
        {
            if (czyCelownik)
            {
                Cursor.SetCursor(default, Vector2.zero, CursorMode.ForceSoftware);
                czyCelownik = false;
            }
        }
    }
}
