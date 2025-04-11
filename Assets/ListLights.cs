using UnityEngine;

public class ListLights : MonoBehaviour
{
    [System.Obsolete]
    void Start()
    {
        // Haetaan kaikki scene:ssa olevat Light-komponentit
        Light[] lights = FindObjectsOfType<Light>();
        // K‰yd‰‰n l‰pi kaikki valot
        foreach (Light light in lights)
        {
            // Tulostetaan valon nimi
            Debug.Log("Light Name: " + light.name);

            // S‰‰det‰‰n valon kirkkaus
            // light.intensity = 500.0f; // Korkea kirkkaus (suurentamalla t‰t‰ voit saada eritt‰in kirkasta valoa)


            // S‰‰det‰‰n valon v‰ri
            // light.color = new Color(1f, 1f, 1f, 1f); // Valkoinen valo, voit vaihtaa v‰ri‰

            // Jos valo on suuntavalon (Directional Light) tyyppinen, s‰‰det‰‰n sen kulmaa (esim. auringon valo)
            // if (light.type == LightType.Directional)
            // {
            light.transform.rotation = Quaternion.Euler(50f, 90f, 0f); // Voit s‰‰t‰‰ kulmaa aurinkoa simuloivaksi
            // }

            // Pehme‰mpi varjo
            // light.shadows = LightShadows.Soft; // Pehme‰ varjo, voit kokeilla myˆs LightShadows.Hard

            // Lis‰‰ muutoksia tarvittaessa
        }
    }
}