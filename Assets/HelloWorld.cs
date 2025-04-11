using UnityEngine;
using TMPro;

public class HelloWorld : MonoBehaviour
{
    void Update()
    {
        // Get the TextMeshPro component
        TextMeshPro t1Component = GetComponent<TextMeshPro>();

        // Check if the TextMeshPro component exists
        if (t1Component != null)
        {
            // Set the text value of the TextMeshPro component
            t1Component.text = "TESTI";
        }
        else
        {
            // Log an error message if the TextMeshPro component is not found
            Debug.LogError("TextMeshPro component not found on the GameObject!");
        }
    }
}
