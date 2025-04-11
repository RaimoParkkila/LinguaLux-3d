using System;
using UnityEngine;
using UnityEngine.Audio;

public class MixerVolumeController : MonoBehaviour
{
    // The range of the volume slider on a mixer group
    const float minVolume = -80f;
    const float maxVolume = 20f;

    public AudioMixer mixer;

    [Range(minVolume, maxVolume)]
    public float volume;

    float previousVolume;

    void Update()
    {

        // Sets the exposed parameter "volume" in the audio mixer,
        // In this example the parameter is assumed to be the volume of a mixer group.
        // It could be any other exposable mixer parameter.
        if (!Mathf.Approximately(volume, previousVolume))
        {
            mixer.SetFloat("volume", volume);
        }

        previousVolume = volume;
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Mixer volume");
        var newVolume = GUILayout.HorizontalSlider(volume, minVolume, maxVolume, GUILayout.Width(300));

        if (!Mathf.Approximately(newVolume, previousVolume))
        {
            volume = newVolume;
            mixer.SetFloat("volume", volume);
        }

        GUILayout.EndHorizontal();
    }
}
