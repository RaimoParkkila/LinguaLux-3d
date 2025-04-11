using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light[] lights; // T‰h‰n lis‰‰ kaikki valot, jotka haluat syttyv‰n automaattisesti

    void Start()
    {
        Debug.Log("LightManager Start function called.");

        // K‰y l‰pi kaikki valot ja sytyt‰ ne
        foreach (Light light in lights)
        {
            light.enabled = true;
            Debug.Log("Light " + light.name + " enabled.");
        }
    }

}
