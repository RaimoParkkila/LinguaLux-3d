using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class LightChange : MonoBehaviour
{
    public Slider slider;
    public Light sceneLight;
    public FirstPersonController controller; // Viittaus FirstPersonController-olioon

    // Lis‰‰ ToggleController-skripti kameraan
    private ToggleController toggleController;

    public Text messageText; // Viittaus Text-komponenttiin

    void Start()
    {
        if (messageText == null) Debug.LogError("messageText is null!");
        if (slider == null) Debug.LogError("slider is null!");
        if (sceneLight == null) Debug.LogError("sceneLight is null!");
        if (controller == null) Debug.LogError("controller is null!");
        if (toggleController == null) Debug.LogError("toggleController is null!");
    }

    // Funktio, joka kutsutaan, kun liukus‰‰timen arvoa muutetaan
    public void OnSliderValueChanged()
    {
        Debug.Log("Slider value changed");

        messageText.text = "Slider value changed";
        // Kutsu ToggleControllerin metodia liukus‰‰dint‰ muutettaessa
        toggleController.ToggleControllerState(false);
    }

    // Funktio, joka kutsutaan, kun liukus‰‰timen liikkeen lopetetaan
    public void OnSliderValueChangeEnded()
    {
        Debug.Log("Slider value change ended");
        messageText.text = "Slider value change ended";
        // Kutsu ToggleControllerin metodia liukus‰‰timen liikkeen loputtua
        toggleController.ToggleControllerState(true);
    }

    void Update()
    {

     
        if (Input.GetKey(KeyCode.Escape))
        {
            // Est‰ kameran liike
            controller.enabled = false;
        }
        else
        {
            // Salli kameran liike
            controller.enabled = true;
        }

        // Aseta valon intensiteetti liukus‰‰timen arvon perusteella
        sceneLight.intensity = slider.value;
        Debug.Log("Slider value change ended: " + slider.value.ToString());
    }
}
