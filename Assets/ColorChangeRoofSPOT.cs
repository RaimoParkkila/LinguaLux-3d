using UnityEngine;
using UnityEngine.UI;

public class ColorChangeRoofightSPOT : MonoBehaviour
{
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public Light sceneLight;


    void Start()
    {
        // Alustaa Stroboskoopin pois p‰‰lt‰
        Debug.Log("Color change script started RoofSpot");

    }

    public void Update()
    {


        Debug.Log("Color change script RoofSpot updated");
        //Color newColor = new(redSlider.value, greenSlider.value, blueSlider.value);


        //sceneLight.color = Color.Lerp(sceneLight.color, newColor, Time.deltaTime / 0.5f);


        // float defaultRed = 1.0f;  // Oletusarvo punaiselle v‰riarvolle
        //  float defaultGreen = 1.0f;  // Oletusarvo vihre‰lle v‰riarvolle
        // float defaultBlue = 1.0f;  // Oletusarvo siniselle v‰riarvolle


        float defaultRed = 1.0f;  // Oletusarvo punaiselle v‰riarvolle
        float defaultGreen = 1.0f;  // Oletusarvo vihre‰lle v‰riarvolle
        float defaultBlue = 1.0f;  // Oletusarvo siniselle v‰riarvolle

        float redValue = redSlider.value;
        float greenValue = greenSlider.value;
        float blueValue = blueSlider.value;

        // Tarkista punaisen v‰riarvon asetus
        if (redValue == 0.0f)  // Tarkista, onko arvo nolla
        {
            redValue = defaultRed;  // Aseta oletusarvo, jos arvo on nolla
        }

        // Tarkista vihre‰n v‰riarvon asetus
        if (greenValue == 0.0f)  // Tarkista, onko arvo nolla
        {
            greenValue = defaultGreen;  // Aseta oletusarvo, jos arvo on nolla
        }

        // Tarkista sinisen v‰riarvon asetus
        if (blueValue == 0.0f)  // Tarkista, onko arvo nolla
        {
            blueValue = defaultBlue;  // Aseta oletusarvo, jos arvo on nolla
        }

        // Luo uusi v‰ri uusilla arvoilla
        Color newColor = new Color(redValue, greenValue, blueValue);



        //oletusarvo

        // Color newColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        sceneLight.color = newColor;

        Debug.Log("Color change script started ROOF SPOT" + sceneLight.color);

    }
}
