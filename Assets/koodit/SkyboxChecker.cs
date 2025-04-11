using UnityEngine;

public class SkyboxChecker : MonoBehaviour
{
    void Start()
    {
        // Tarkista, onko Skybox asetettu oikein
        Material skyboxMaterial = RenderSettings.skybox;

        if (skyboxMaterial != null)
        {
            // Jos Skybox on asetettu, tulostetaan viesti
            Debug.Log("Skybox on asetettu oikein: " + skyboxMaterial.name);
        }
        else
        {
            // Jos Skybox ei ole asetettu, tulostetaan virheilmoitus
            Debug.LogError("Skybox ei ole asetettu!");
        }
    }
}
