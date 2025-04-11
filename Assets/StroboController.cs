using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
using UnityEngine.UI;
 

public class StroboController : MonoBehaviour
{
    public Light stroboscopeLight;
    private bool isStroboOn = false;
   // public float lightIntensity = 20.0f;

    void Start()
    {
        // Alustaa Stroboskoopin pois päältä

       // Debug.Log("STROBO POIS PÄÄLTÄ");
       // stroboscopeLight.enabled = false;
    }

    public void ToggleStrobo()
    {
        // Vaihtaa stroboskoopin tilan päälle/pois

        Debug.Log("Vaihtaa stroboskoopin tilan päälle/pois");
        isStroboOn = !isStroboOn;
        stroboscopeLight.enabled = isStroboOn;
        // stroboscopeLight.intensity = lightIntensity;
     //   stroboscopeLight.intensity = 100.0f;


    }
}