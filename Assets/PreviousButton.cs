using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousSceneButton : MonoBehaviour
{
    // Määritä edellisen skenaarion nimi tai indeksi
    public string previousSceneName; // Tai voit käyttää myös previousSceneIndex-muuttujaa

    // Voit myös määrittää edellisen skenaarion indeksin, jos haluat
    // public int previousSceneIndex;

    // Metodi, joka käynnistää edellisen skenaarion, kun nappia painetaan
    public void LoadPreviousScene()
    {
        // Tarkista, onko edellisen skenaarion nimi määritelty
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            // Lataa edellinen skenaario nimen perusteella
            SceneManager.LoadScene(previousSceneName);
        }
        // Voit myös käyttää indeksiä lataamaan edellisen skenaarion
        // else if (previousSceneIndex >= 0)
        // {
        //     SceneManager.LoadScene(previousSceneIndex);
        // }
    }
}
