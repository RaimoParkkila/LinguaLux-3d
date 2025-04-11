using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
 

    // Update is called once per frame
    void Update()
    {

        // Pakota koordinaatit pysym‰‰n samoissa
        Vector3 newPosition = transform.position;
        newPosition.x = 100.0f; // Aseta x-koordinaatti haluttuun arvoon
        newPosition.y = 0.0f - 4; // Aseta y-koordinaatti haluttuun arvoon
        newPosition.z = 0.0f; // Aseta z-koordinaatti haluttuun arvoon
        this.GetComponent<Transform>().position = newPosition;
        this.GetComponent<Transform>().Rotate(420.0f, -25.8f, 0.0f);
    }
    
}
