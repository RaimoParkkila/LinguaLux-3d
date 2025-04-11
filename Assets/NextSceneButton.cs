using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButton : MonoBehaviour
{
    // Määritä seuraavan skenaarion nimi tai indeksi
    public string nextSceneName; // Tai voit käyttää myös sceneIndex-muuttujaa

    // Voit myös määrittää seuraavan skenaarion indeksin, jos haluat
    // public int nextSceneIndex;

    // Metodi, joka käynnistää seuraavan skenaarion, kun nappia painetaan
    public void LoadNextScene()
    {
        // Tarkista, onko seuraavan skenaarion nimi määritelty
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            // Lataa seuraava skenaario nimen perusteella
            SceneManager.LoadScene(nextSceneName);
        }
        // Voit myös käyttää indeksiä lataamaan seuraavan skenaarion
        // else if (nextSceneIndex >= 0)
        // {
        //     SceneManager.LoadScene(nextSceneIndex);
        // }
    }

    // Update-metodi suoritetaan jokaisella ruudunpäivityksellä
    void Update()
    {
        // Tarkista, onko välilyönti (spacebar) painettu
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Kutsu LoadNextScene-metodia ladataksesi seuraavan skenaarion
            LoadNextScene();
        }
    }
}
