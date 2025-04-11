using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    //public AudioManager audioManager;

    void Start()
    {
        // Alusta sliderin arvo ‰‰nenvoimakkuuden mukaan
      //  volumeSlider.value = audioManager.GetComponent<AudioSource>().volume;
  
    }

    public void OnVolumeChanged()
    {
        float volumeValue = volumeSlider.value;
       // audioManager.SetVolume(volumeValue);
         //     audioManager.SetVolume(volumeValue);
    }
    }
 
