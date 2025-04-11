using UnityEngine;

public class RigidbodyControllerRotation : MonoBehaviour
{
    public float rotationSpeed = 10f; // Pyörimisnopeus asteina sekunnissa
    public float moveSpeed = 2f; // Liikkumisnopeus

    private Rigidbody rb;
    private Vector3 targetPosition = new Vector3(0, 0, 0); // Kohde, johon kamera menee

    void Start()
    {
        Application.targetFrameRate = 60; // Asetetaan tavoite-framerate 60:ksi

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody puuttuu tästä GameObjectista!");
        }

        // Poistetaan gravitaatio, jotta objekti ei liiku alas
        rb.useGravity = false;

        // Asetetaan Rigidbody kinematiikaksi
        rb.isKinematic = true;

        // Varmistetaan, ettei Rigidbody pyöri fysiikkamoottorin vaikutuksesta
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            // Lasketaan pyörimisnopeus
            float yRotation = rotationSpeed * Time.fixedDeltaTime;

            // Pyöriminen paikallisesti Y-akselilla
            Quaternion rotation = Quaternion.Euler(0f, yRotation, 0f);

            // Päivitetään Rigidbodyä pyörimään suhteessa nykyiseen rotaatioon
            transform.Rotate(rotation.eulerAngles);

            // Liikkuminen hitaasti kohti kohdepistettä
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        }
    }
}
