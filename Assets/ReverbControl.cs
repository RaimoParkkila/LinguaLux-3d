using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ReverbControl : MonoBehaviour
{
    public AudioMixer mixer;
    public bool isReverbOn = false;
    public AudioMixerGroup mixerGroup;
    public AudioSource audioSource;
    public float myPitchValue;
    public float myReverb;

    public void Start()
    {
        // Aseta ‰‰nil‰hteen outputAudioMixerGroup
        if (audioSource != null && mixerGroup != null)
        {
            audioSource.outputAudioMixerGroup = mixerGroup;
            audioSource.pitch = 5;
        }
        else
        {
            Debug.LogError("AudioSource tai AudioMixerGroup puuttuu!");
        }
    }

    public void ToggleReverb(bool enableReverb)
    {
        isReverbOn = enableReverb;

        if (isReverbOn)
        {
            // Ota kaiku p‰‰lle asettamalla reverb-m‰‰r‰ positiiviseksi arvoksi

            Debug.Log("Kaiku p‰‰lle");
            //.audioSource.outputAudioMixerGroup

            Debug.Log(mixer.FindMatchingGroups("Music")[0]);




            mixer.GetFloat("pitch", out myPitchValue);
            print("myPitchValue: " + myPitchValue.ToString());



            try
            {
                mixer.SetFloat("pitch", 5.0f); // Aseta t‰h‰n sopiva reverb-m‰‰r‰


                mixer.GetFloat("myPitch", out myPitchValue);
                print("myPitch: " + myPitchValue.ToString());
             
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            //mixer.SetFloat("Pitch", 20.0f); // Aseta t‰h‰n sopiva reverb-m‰‰r‰
        }
        else
        {
            // Poista kaiku asettamalla reverb-m‰‰r‰ nollaksi tai negatiiviseksi

            Debug.Log("Kaiku pois");
  
            try
            {
                mixer.SetFloat("pitch", .0f); // Negatiivinen arvo sammuttaa kaikun
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}
