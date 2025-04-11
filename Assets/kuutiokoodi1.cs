using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour


{






    // Update is called once per frame
    void Update()
    {

        // Pakota koordinaatit pysym‰‰n samoissa
        Vector3 newPosition = transform.position;
        newPosition.x = 100.0f; // Aseta x-koordinaatti haluttuun arvoon
        newPosition.y = 0.0f - 4; // Aseta y-koordinaatti haluttuun arvoon
        newPosition.z = 0.0f; // Aseta z-koordinaatti haluttuun arvoon
                              // this.GetComponent<Transform>().position = newPosition;
        this.GetComponent<Transform>().Rotate(0.0f, 0.8f, 0.0f);
    }
}
