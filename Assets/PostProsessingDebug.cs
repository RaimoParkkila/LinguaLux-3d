using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingDebug : MonoBehaviour
{
    public PostProcessVolume volume;

    void Start()
    {
        // Tarkistetaan Post Processing Layer ja Volume
        PostProcessLayer postProcessLayer = GetComponent<PostProcessLayer>();
        PostProcessVolume postProcessVolume = FindObjectOfType<PostProcessVolume>();

        if (postProcessLayer != null)
        {
            Debug.Log("Post Processing Layer löytyi.");
        }
        else
        {
            Debug.LogWarning("Post Processing Layer ei löytynyt.");
        }

        if (postProcessVolume != null)
        {
            Debug.Log("Post Processing Volume löytyi.");
        }
        else
        {
            Debug.LogWarning("Post Processing Volume ei löytynyt.");
        }

        if (postProcessLayer != null && postProcessLayer.enabled)
        {
            Debug.Log("Post Processing Layer on aktivoitu.");
        }
        else
        {
            Debug.LogWarning("Post Processing Layer ei ole aktivoitu.");
        }

        // Liioitellaan Vignette ja Bloom -efektejä
        if (postProcessVolume != null)
        {
            // Hakee Vignette- ja Bloom-efektit profile.settings -listasta
            foreach (var setting in postProcessVolume.profile.settings)
            {
                if (setting is Vignette vignette)
                {
                    vignette.intensity.value = 1.5f; // Liioiteltu intensiteetti
                    vignette.smoothness.value = 0.5f; // Liioiteltu pehmeys
                    vignette.center.value = new Vector2(0.5f, 0.5f); // Keskitetty efekti
                    Debug.Log("Vignette liioiteltu.");
                }

                if (setting is Bloom bloom)
                {
                    bloom.intensity.value = 10f; // Liioiteltu kirkkaus
                    bloom.threshold.value = 0.6f; // Liioiteltu kynnysarvo
                    Debug.Log("Bloom liioiteltu.");
                }
            }
        }
    }
}
