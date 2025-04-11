using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.HighDefinition;

public class LightingController : MonoBehaviour
{
    // Set your desired values here
    public float pointLightIntensity = 10f; // Adjust the intensity for Point Lights
    public float directionalLightIntensity = 1f; // Adjust the intensity for Directional Lights
    public float bloomIntensity = 1.5f; // Adjust the Bloom effect intensity
    public float vignetteIntensity = 0.5f; // Adjust the Vignette effect intensity

    void Start()
    {
        // Find and set all lights in the scene
        Light[] lights = FindObjectsOfType<Light>();
        foreach (Light light in lights)
        {
            if (light.type == LightType.Point)
            {
                light.intensity = pointLightIntensity;
            }
            else if (light.type == LightType.Directional)
            {
                light.intensity = directionalLightIntensity;
            }
        }

        // Find and set Post Processing Volume settings
        Volume[] volumes = FindObjectsOfType<Volume>();
        foreach (Volume volume in volumes)
        {
            // Check if PostProcessing Volume is set
            if (volume.profile == null)
            {
                Debug.LogWarning("No Profile found in Volume: " + volume.name);
                continue;
            }

            // HDRP Vignette and Bloom handling
            if (volume.profile.TryGet(out UnityEngine.Rendering.HighDefinition.Vignette vignetteHDRP))
            {
                vignetteHDRP.intensity.value = vignetteIntensity; // Set your desired vignette intensity for HDRP
                Debug.Log("HDRP Vignette intensity set to " + vignetteIntensity);
            }

            if (volume.profile.TryGet(out UnityEngine.Rendering.HighDefinition.Bloom bloomHDRP))
            {
                bloomHDRP.intensity.value = bloomIntensity; // Set your desired bloom intensity for HDRP
                Debug.Log("HDRP Bloom intensity set to " + bloomIntensity);
            }

            // URP Vignette and Bloom handling
            else if (volume.profile.TryGet(out UnityEngine.Rendering.Universal.Vignette vignetteURP))
            {
                vignetteURP.intensity.value = vignetteIntensity; // Set your desired vignette intensity for URP
                Debug.Log("URP Vignette intensity set to " + vignetteIntensity);
            }

            if (volume.profile.TryGet(out UnityEngine.Rendering.Universal.Bloom bloomURP))
            {
                bloomURP.intensity.value = bloomIntensity; // Set your desired bloom intensity for URP
                Debug.Log("URP Bloom intensity set to " + bloomIntensity);
            }
        }
    }
}
