using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class CheckGlowSettings : MonoBehaviour
{
    void Start()
    {
        // TARKISTETAAN HDR
        if (Camera.main.allowHDR)
            Debug.Log("✅ HDR on käytössä kamerassa.");
        else
            Debug.LogWarning("❌ HDR ei ole päällä! Laita se päälle Project Settings → Player → Other Settings.");

        // TARKISTETAAN POST PROCESSING LAYER
        var postProcessLayer = Camera.main.GetComponent<UnityEngine.Rendering.PostProcessing.PostProcessLayer>();
        if (postProcessLayer != null && postProcessLayer.enabled)
            Debug.Log("✅ Kamerassa on Post Processing Layer.");
        else
            Debug.LogWarning("❌ Post Processing Layer puuttuu kamerasta!");

        // TARKISTETAAN POST PROCESSING VOLUME
        var postProcessVolume = FindObjectOfType<PostProcessVolume>();
        if (postProcessVolume != null && postProcessVolume.isGlobal)
            Debug.Log("✅ Post Processing Volume löytyy ja on globaali.");
        else
            Debug.LogWarning("❌ Post Processing Volume ei ole oikein asetettu!");

        // TARKISTETAAN BLOOM
        if (postProcessVolume != null)
        {
            if (postProcessVolume.profile.TryGetSettings(out Bloom bloom))
            {
                if (bloom.intensity.value > 1f)
                    Debug.Log("✅ Bloom on käytössä ja intensiteetti on riittävä.");
                else
                    Debug.LogWarning("⚠️ Bloom löytyy, mutta intensiteetti on matala. Kokeile nostaa arvoa!");
            }
            else
            {
                Debug.LogWarning("❌ Bloom-efektiä ei ole lisätty Post Processing Volumeen!");
            }
        }

        // TARKISTETAAN EMISSION-MATERIAALI
        GameObject glowingObject = GameObject.FindWithTag("GlowingObject");
        if (glowingObject != null)
        {
            Renderer renderer = glowingObject.GetComponent<Renderer>();
            if (renderer != null && renderer.sharedMaterial != null && renderer.sharedMaterial.IsKeywordEnabled("_EMISSION"))
            {
                Debug.Log("✅ Emission-materiaali on käytössä.");
            }
            else
            {
                Debug.LogWarning("❌ Emission-materiaalia ei ole oikein asetettu!");
            }
        }
        else
        {
            Debug.LogWarning("❌ Etikettiin 'GlowingObject' merkittyä objektia ei löytynyt!");
        }
    }
}
