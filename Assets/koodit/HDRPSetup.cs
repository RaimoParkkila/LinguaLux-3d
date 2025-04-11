using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class HDRPSetup : EditorWindow
{
    [MenuItem("Tools/Setup HDRP Settings")]
    static void Init()
    {
        // Luodaan uusi ikkunan avaus Unityn editoriin
        HDRPSetup window = (HDRPSetup)EditorWindow.GetWindow(typeof(HDRPSetup));
        window.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Setup HDRP"))
        {
            SetupHDRP();
        }
    }

    static void SetupHDRP()
    {
        // Tarkistetaan, ett‰ HDRP on asennettu
        if (!IsHDRPInstalled())
        {
            Debug.LogError("HDRP is not installed. Please install HDRP from the Package Manager.");
            return;
        }

        // Asetetaan HDRP pipeline
        SetHDRPPipeline();

        // Asetetaan Skybox
        SetHDRPSkybox();

        // Asetetaan valaistusasetukset
        SetLightingSettings();

        Debug.Log("HDRP setup complete!");
    }

    // Tarkistaa, onko HDRP asennettu
    static bool IsHDRPInstalled()
    {
        var hdrpPackage = UnityEditor.PackageManager.PackageInfo.FindForAssetPath("Packages/com.unity.render-pipelines.high-definition");
        return hdrpPackage != null;
    }

    // Asettaa HDRP render pipeline asetukset
    static void SetHDRPPipeline()
    {
        var hdrpAsset = AssetDatabase.LoadAssetAtPath<RenderPipelineAsset>("Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipelineResources/HDRPDefaultRendererSettings.asset");

        if (hdrpAsset != null)
        {
            GraphicsSettings.renderPipelineAsset = hdrpAsset;
        }
        else
        {
            Debug.LogError("Failed to find HDRP Asset.");
        }
    }

    // Asettaa HDRP Skyboxin
    static void SetHDRPSkybox()
    {
        // M‰‰ritet‰‰n HDRP:lle sopiva Skybox materiaalin asetuksiksi
        Material skyboxMaterial = AssetDatabase.LoadAssetAtPath<Material>("Packages/com.unity.render-pipelines.high-definition/Runtime/Materials/Skybox/DefaultHDRI.material");
        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
        }
        else
        {
            Debug.LogError("Failed to find HDRP Skybox material.");
        }
    }

    // Asettaa valaistusasetukset HDRP:lle
    static void SetLightingSettings()
    {
        // Valaistusasetukset
        LightmapSettings.lightmapsMode = LightmapsMode.NonDirectional;
        QualitySettings.SetQualityLevel(5, true); // Asennetaan korkein laatu
    }
}
