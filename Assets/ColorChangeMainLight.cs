using UnityEngine;
 

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BezierTubeGenerator : MonoBehaviour
{
    public Transform startPoint;
    public Transform controlPoint;
    public Transform endPoint;
    public int segmentCount = 10;  // Kuinka monta segmenttiä putkessa on

    private void Start()
    {
        GenerateTube();
    }

    void GenerateTube()
    {
        if (startPoint == null || controlPoint == null || endPoint == null)
        {
            Debug.LogError("Bezier-käyrän pisteitä puuttuu!");
            return;
        }

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[segmentCount + 1];
        int[] triangles = new int[segmentCount * 6];

        // Lasketaan Bezier-käyrän pisteet
        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            vertices[i] = CalculateBezierPoint(t, startPoint.position, controlPoint.position, endPoint.position);
        }

        // Luodaan putken seinät
        for (int i = 0; i < segmentCount; i++)
        {
            int index = i * 6;
            triangles[index] = i;
            triangles[index + 1] = i + 1;
            triangles[index + 2] = i + 2 < vertices.Length ? i + 2 : i + 1;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    }

    // Lasketaan Bezier-käyrän piste
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        return u * u * p0 + 2 * u * t * p1 + t * t * p2;
    }
}
