using UnityEngine;

public class RotateLamp : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.up; // Pyörimisakseli (x, y, z)
    public float rotationSpeed = 50f; // Pyörimisnopeus asteina sekunnissa

    void Update()
    {
        // Pyörittää objektia joka frame Update()n aikana
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
