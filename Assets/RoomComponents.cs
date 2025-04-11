using UnityEngine;

public class RoomComponents : MonoBehaviour
{
    public GameObject floor; // Lattia
    public GameObject[] walls; // Seinät
    public GameObject roof; // Katto
    public int roomRadius = 5;     // Huoneen säde
    public float wallHeight = 2f;  // Seinän korkeus

    // Tässä voit lisätä tarvittaessa muita huoneen osia, kuten huonekalut, valaisimet jne.

    // Metodi, jolla voit kutsua huoneen osien asettamista
    public void SetRoomComponents(GameObject floorPrefab, GameObject wallPrefab, GameObject roofPrefab)
    {
        // Aseta lattia
        floor = Instantiate(floorPrefab, transform.position, Quaternion.identity, transform);
        // Aseta seinät
        walls = new GameObject[6];
        for (int i = 0; i < 6; i++)
        {
            float angle = 60f * i;
            Vector3 wallPosition = transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)) * roomRadius;
            walls[i] = Instantiate(wallPrefab, wallPosition, Quaternion.Euler(0f, angle - 30f, 0f), transform);
        }
        // Aseta katto
        roof = Instantiate(roofPrefab, transform.position + Vector3.up * wallHeight, Quaternion.identity, transform);
    }
}
