using UnityEngine;

public class GameSettings : MonoBehaviour
{
    void Start()
    {
        // Rajaa framerate 60:een
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Application.targetFrameRate = 60; // Tavoitellaan 60 fps
        Screen.SetResolution(1280, 720, true); // Aseta pienempi resoluutio
        QualitySettings.SetQualityLevel(2); // Matalampi grafiikka-asetus
        QualitySettings.SetQualityLevel(0); // Alhaisin laatuasetukset
        QualitySettings.vSyncCount = 0; // Poista V-Sync
        Application.targetFrameRate = 60; // Aseta kiinte‰ frame rate
        Camera.main.farClipPlane = 500; // Aseta pienempi piirtoet‰isyys


        // M‰‰rit‰ vSync-asetukset
        // QualitySettings.vSyncCount = 1; // Yksi vSync, joka voi parantaa suorituskyky‰
    }
}
