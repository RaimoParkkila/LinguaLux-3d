Shader "CustomRenderTexture/AdvancedAutoCustomTexture"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1, 1, 1, 1)  // Oletusväri
        _Opacity ("Opacity", Range(0.0, 1.0)) = 0.5      // Oletusarvo opasiteetille
        _FresnelPower ("Fresnel Power", Range(0.1, 5.0)) = 2.0 // Fresnelin voimakkuus
        _EmissionColor ("Emission Color", Color) = (0, 0, 0) // Hehkuväri
        _GlowIntensity ("Glow Intensity", Range(0.0, 1.0)) = 0.5 // Hehkun voimakkuus
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _MainColor;
        fixed _Opacity;
        fixed _FresnelPower;
        fixed4 _EmissionColor;
        fixed _GlowIntensity;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
            float3 worldNormal;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Fresnelin laskeminen
            float fresnel = pow(1.0 - saturate(dot(IN.worldNormal, normalize(IN.worldPos))), _FresnelPower);

            // Sateenkaari väri
            float3 rainbowColor = saturate(float3(sin(fresnel * 6.28), sin(fresnel * 6.28 + 2.0), sin(fresnel * 6.28 + 4.0)));

            // Väri ja opasiteetti
            fixed4 texColor = fixed4(rainbowColor, _Opacity);

            // Hehkuväri ja voimakkuus
            fixed3 emission = _EmissionColor.rgb * _GlowIntensity;

            // Lopullinen väri
            o.Albedo = texColor.rgb;
            o.Emission = emission;
            o.Alpha = texColor.a;
        }
        ENDCG
    }

    Fallback "Diffuse"
}
    