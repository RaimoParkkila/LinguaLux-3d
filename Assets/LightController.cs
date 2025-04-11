using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    public Light lightComponent;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public void vaihda_varit()
    {
        // Otetaan liukus‰‰timist‰ v‰riarvot
        float red = redSlider.value;
        float green = greenSlider.value;
        float blue = blueSlider.value;

        // Asetetaan uusi v‰ri valolle
        lightComponent.color = new Color(red, green, blue);
    }
}
    