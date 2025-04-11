using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class CheckHDRPSettings : MonoBehaviour
{
    void Start()
    {
        Debug.Log("===== HDRP Asetusten Tarkistus =====");

        // **1. Tarkistetaan HDRP Asset käytössä**
        var hdrpAsset = GraphicsSettings.currentRenderPipeline;
        if (hdrpAsset != null && hdrpAsset.GetType().ToString().Contains("HDRenderPipelineAsset"))
        {
            Debug.Log("✅ HDRP on käytössä!");
        }
        else
        {
            Debug.LogWarning("❌ HDRP Asset ei ole käytössä. Tarkista Graphics Settings!");
        }

        // **2. Kamera-asetukset**
        Camera mainCam = Camera.main;
        mainCam.allowHDR = true;
        Debug.Log("✅ HDR otettu käyttöön kamerassa!");
        if (mainCam != null)
        {
            Debug.Log($"🎥 Kamera löydetty: {mainCam.name}");
            Debug.Log($"- HDR käytössä: {mainCam.allowHDR}");

            // Haetaan HDAdditionalCameraData, jos se on lisätty
            var hdCamData = mainCam.GetComponent<HDAdditionalCameraData>();
            if (hdCamData != null)
            {
                Debug.Log($"- Anti-aliasing: {hdCamData.antialiasing}");
                Debug.Log($"- Dithering: {hdCamData.dithering}");
                Debug.Log($"- Post Processing käytössä: {hdCamData.allowDynamicResolution}");
            }
            else
            {
                Debug.LogWarning("⚠️ Kamera ei sisällä HDAdditionalCameraData-komponenttia.");
            }
        }
        else
        {
            Debug.LogWarning("❌ Main Camera ei löydetty!");
        }

        // **3. Valaistusasetukset**
        Light dirLight = FindObjectOfType<Light>();
        if (dirLight != null)
        {
            Debug.Log($"💡 Valo löydetty: {dirLight.name}");
            Debug.Log($"- Light Type: {dirLight.type}");
            Debug.Log($"- Intensity: {dirLight.intensity}");
            Debug.Log($"- Shadow Type: {dirLight.shadows}");
        }
        else
        {
            Debug.LogWarning("❌ Directional Light puuttuu!");
        }

        // **4. Materiaalin tarkistus**
        Renderer objRenderer = FindObjectOfType<Renderer>();
        if (objRenderer != null && objRenderer.sharedMaterial != null)
        {
            Material mat = objRenderer.sharedMaterial;
            Debug.Log($"🎨 Materiaali löydetty: {mat.name}");
            Debug.Log($"- Shader: {mat.shader.name}");
            Debug.Log($"- Emission käytössä: {mat.IsKeywordEnabled("_EMISSION")}");
        }
        else
        {
            Debug.LogWarning("❌ Materiaalia ei löydetty mistään GameObjectista!");
        }
    }
}
