using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShaderDebug : MonoBehaviour
{
    void Start()
    {
        string shaderPath1 = "../Library/PackageCache/com.unity.render-pipelines.high-definition@14.0.9/Runtime/ShaderLibrary/Common.hlsl";
        string shaderPath2 = "../Library/PackageCache/com.unity.render-pipelines.high-definition@14.0.9/Runtime/Material/Builtin/BuiltinData.hlsl";

        Debug.Log("Shader Path 1: " + shaderPath1);
        Debug.Log("Shader Path 2: " + shaderPath2);
    }
}
