using UnityEngine;


public class EmissionUpdater : MonoBehaviour
{
    public Color emissionColor = Color.green;

    void Update()
    {
        // P‰ivit‰ hehkuv‰ri dynaamisesti, esim. pelaajan l‰heisyyden mukaan
        Material mat = GetComponent<Renderer>().material;

        // Varmista, ett‰ emission on p‰‰ll‰
        mat.EnableKeyword("_EMISSION");

        // Aseta dynaamisesti hehkuv‰ri
        mat.SetColor("_EmissionColor", emissionColor);

        // P‰ivit‰ dynaaminen valaistus
        DynamicGI.SetEmissive(GetComponent<Renderer>(), emissionColor);
    }
}
    