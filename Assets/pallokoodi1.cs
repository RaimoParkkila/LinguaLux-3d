using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallokoodi1 : MonoBehaviour
{
    public float rotationSpeed = 0.01f; // Pyˆrimisnopeus

    void Start()
    {
        // Voit lis‰t‰ alkuarvot tai alustuslogiikan tarvittaessa
    }

    void Update()
    {
        // Saadaan nykyinen sijainti
        Vector3 newPosition = transform.position;

        // Rajoitetaan liikett‰ vain y-akselille, eli ei liiku x- tai z-akselilla
        newPosition.x = 0.0f; // Pit‰‰ X-koordinaatin nollassa
        newPosition.z = 0.0f; // Pit‰‰ Z-koordinaatin nollassa

        // T‰m‰ asettaa y-koordinaatin niin, ett‰ se ei mene veden alle
        // S‰ilytet‰‰n pallo kiinte‰ll‰ korkeudella
        if (newPosition.y < -4) // Ei mene alle y-4 (vedenpinnan alapuolelle)
        {
            newPosition.y = -4;
        }

        // P‰ivitet‰‰n sijainti
        transform.position = newPosition;

        // Pyˆritys (vain Y-akselilla)
        transform.Rotate(0.0f, rotationSpeed, 0.0f);
    }
}
