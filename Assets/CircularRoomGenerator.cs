using System.IO;
using UnityEngine;

public class CircularRoomGenerator : MonoBehaviour
{
    public GameObject floorPrefab; // Prefab lattiaa varten
    public GameObject wallPrefab;  // Prefab sein‰‰ varten
    public GameObject roofPrefab;  // Prefab kattoa varten

    public int roomRadius = 5;     // Huoneen s‰de
    public float wallHeight = 2f;  // Sein‰n korkeus

    void Start()
    {
        GenerateRoom();
    }

    void GenerateRoom()
    {
        // Luodaan lattia
        GameObject floor = Instantiate(floorPrefab, transform.position, Quaternion.identity);
        floor.transform.localScale = new Vector3(roomRadius * 2, 1, roomRadius * 2);

        // Luodaan katto
        GameObject roof = Instantiate(roofPrefab, transform.position + Vector3.up * wallHeight, Quaternion.identity);
        roof.transform.localScale = new Vector3(roomRadius * 2, 1, roomRadius * 2);

        // Luodaan sein‰t
        for (int angle = 0; angle < 360; angle += 60)
        {
            float radians = angle * Mathf.Deg2Rad;
            Vector3 wallPosition = transform.position + new Vector3(Mathf.Cos(radians), 0f, Mathf.Sin(radians)) * roomRadius;
            GameObject wall = Instantiate(wallPrefab, wallPosition, Quaternion.Euler(0f, angle, 0f));
            wall.transform.localScale = new Vector3(1, wallHeight, roomRadius * 0.1f);
        }

        // Tallenna huoneen tiedot
        SaveRoomData();
    }

    void SaveRoomData()
    {
        // Luo tiedoston tallennuskansioon
        string filePath = Application.dataPath + "/RoomData.txt";
        StreamWriter writer = new StreamWriter(filePath);

        // Tallenna huoneen tiedot
        writer.WriteLine("RoomShape:Circular");
        writer.WriteLine("RoomRadius:" + roomRadius);
        writer.WriteLine("WallHeight:" + wallHeight);

        // Sulje tiedosto
        writer.Close();

        Debug.Log("Room data saved to: " + filePath);
    }
}
