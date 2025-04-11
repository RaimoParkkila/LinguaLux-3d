using System.Collections;
using UnityEngine;

public class Strobo2: MonoBehaviour
{
    public Light stroboscopeLight;
    private bool isStroboOn = false;
    public float strobeSpeed = 0.1f; // Valojen vaihtelun nopeus (sekunneissa)

    void Start()
    {
        // Alustaa Stroboskoopin pois päältä
        Debug.Log("STROBO POIS PÄÄLTÄ");
       // stroboscopeLight.enabled = false;
    }

    public void ToggleStrobo()
    {
        // Vaihtaa stroboskoopin tilan päälle/pois
        Debug.Log("Vaihtaa stroboskoopin tilan päälle/pois");
        isStroboOn = !isStroboOn;
        if (isStroboOn)
        {

          
            StartCoroutine(StrobeEffect());
        }
        else
        {
            StopCoroutine(StrobeEffect());

 
            stroboscopeLight.enabled = false;
        }
    }

    IEnumerator StrobeEffect()
    {
        while (isStroboOn)
        {
            stroboscopeLight.enabled = !stroboscopeLight.enabled;

            yield return new WaitForSeconds(strobeSpeed);
        }
    }
}
