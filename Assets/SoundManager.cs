using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;

public class SoundManager : MonoBehaviour


{

    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        // Liit‰ ChangeVolume-funktio volumeSliderin onValueChanged-tapahtumaan
        volumeSlider.onValueChanged.AddListener(ChangeVolume);

        // Lataa tallennettu ‰‰nenvoimakkuus
        Load();
    }


    public void Load()
    {
        AudioListener.volume = 0;
        Debug.Log("B2");
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        Save();
    }


    // Update is called once per frame
    public void Save()
    {

        Debug.Log("C");
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
 
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume; // Aseta ‰‰nenvoimakkuus AudioListeneriin
    }

}


