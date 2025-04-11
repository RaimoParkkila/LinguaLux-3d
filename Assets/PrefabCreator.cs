using UnityEngine;

public class PrefabCreator : MonoBehaviour
{
    public GameObject roofPrefab;
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public float wallHeight = 2f;
    public float roomRadius = 5f;
    public float doorWidth = 1f; // Oviaukon leveys suhteessa seinän pituuteen

    void Start()
    {
        // Luodaan katto
        GameObject roof = Instantiate(roofPrefab, transform.position + Vector3.up * wallHeight, Quaternion.identity);
        roof.transform.localScale = new Vector3(roomRadius * 2, 1, roomRadius * 2);

        // Luodaan lattia
        GameObject floor = Instantiate(floorPrefab, transform.position, Quaternion.identity);
        floor.transform.localScale = new Vector3(roomRadius * 2, 1, roomRadius * 2);

        // Luodaan seinät
        for (int i = 0; i < 6; i++)
        {
            float angle = 60f * i;
            Vector3 wallPosition = transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)) * roomRadius;
            GameObject wall = Instantiate(wallPrefab, wallPosition, Quaternion.Euler(0f, angle - 30f, 0f));
            wall.transform.localScale = new Vector3(1, wallHeight, roomRadius * 0.1f);

            // Lisätään oviaukko yhteen seinään
            if (i == 0)
            {
                // Laske oviaukon leveys
                float doorLength = roomRadius * 0.1f * doorWidth;

                // Luo oviaukko
                GameObject door = GameObject.CreatePrimitive(PrimitiveType.Cube);
                door.transform.parent = wall.transform;
                door.transform.localPosition = new Vector3((roomRadius * 0.1f - doorLength) / 2, 0, roomRadius * 0.1f / 2);
                door.transform.localScale = new Vector3(doorLength, wallHeight, 0.1f);
                Destroy(door.GetComponent<BoxCollider>()); // Poista kollideri, jotta se ei estä oviaukkoa
            }
        }
    }
}
