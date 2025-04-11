using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class CameraDebug : MonoBehaviour
{
    void Start()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            Debug.Log($"HDR: {cam.allowHDR}, Render Path: {cam.renderingPath}");
        }
        else
        {
            Debug.Log("No main camera found!");
        }
    }
}
