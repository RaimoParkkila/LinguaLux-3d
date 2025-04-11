using UnityEngine;

using UnityEngine;

public class PanelResize : MonoBehaviour
{
    private Vector2 originalSize;

    void Start()
    {
        // Talleta alkuperäinen koko
        originalSize = GetComponent<RectTransform>().sizeDelta;
    }

    void Update()
    {
        // Kun c-näppäintä painetaan, muuta HidePanelButtonin koko nollaksi
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        }
        // Kun c:tä painetaan uudelleen, palauta alkuperäinen koko
        else if (Input.GetKeyDown(KeyCode.C) && GetComponent<RectTransform>().sizeDelta == Vector2.zero)
        {
            GetComponent<RectTransform>().sizeDelta = originalSize;
        }
    }
}
