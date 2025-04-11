using UnityEngine;

public class EnableHDR : MonoBehaviour
{
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        if (cam != null)
        {
            cam.allowHDR = true;  // Laitetaan HDR päälle
            Debug.Log("✅ HDR otettu käyttöön kamerassa!");
        }
        else
        {
            Debug.LogWarning("❌ Kameraa ei löytynyt tästä objektista!");
        }
    }
}
        