using UnityEngine;

public class PyramidGenerator : MonoBehaviour
{
    public int pyramidSize = 5; // M‰‰rit‰ pyramidin koko (korkeus)

    void Start()
    {
        GeneratePyramid();
    }

    void GeneratePyramid()
    {
        // Luodaan pyramidin huippu keskelle
        Vector3 topPosition = transform.position + Vector3.up * pyramidSize;

        // Luodaan pyramidin kerrokset
        for (int i = 0; i < pyramidSize; i++)
        {
            // Lasketaan kerroksen koko ja sijainti
            int layerSize = pyramidSize - i;
            Vector3 layerPosition = topPosition - Vector3.up * i;

            // Luodaan kerroksen neliˆ
            for (int j = 0; j < layerSize; j++)
            {
                // Sijainti ja koko
                Vector3 brickPosition = layerPosition + new Vector3(j - (layerSize - 1) / 2f, 0, 0);
                GameObject brick = GameObject.CreatePrimitive(PrimitiveType.Cube);
                brick.transform.position = brickPosition;
            }
        }
    }
}

