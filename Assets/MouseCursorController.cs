using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

 

public class MouseCursorController : MonoBehaviour
{
    // Lis‰‰ t‰ss‰ ‰‰ni AudioClip-muuttujaan
    public AudioClip beepSound;

    void Start()
    {
        // Tarkista, onko ‰‰niefekti m‰‰ritelty
        if (beepSound != null)
        {
            // Toista ‰‰niefekti
         //   AudioSource.PlayClipAtPoint(beepSound, Camera.main.transform.position);
        }

        // Piilota hiiren kursori
        Cursor.visible = false;
    }
}
