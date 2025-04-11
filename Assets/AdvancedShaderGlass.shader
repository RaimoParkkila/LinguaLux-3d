Shader "Custom/AdvancedRainbowGlass"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1, 1, 1, 1)  // Oletusv‰ri
        _Opacity ("Opacity", Range(0.0, 1.0)) = 0.5      // Oletusarvo opasiteetille
        _FresnelPower ("Fresnel Power", Range(0.1, 5.0)) = 2.0 // Fresnelin voimakkuus
        _EmissionColor ("Emission Color", Color) = (0, 0, 0) // Hehkuv‰ri
        _GlowIntensity ("Glow Intensity", Range(0.0, 1.0)) = 0.5 // Hehkun voimakkuus
    }

    SubShader
    {
        Tags { "RenderPipeline"="HDRenderPipeline" "Queue"="Overlay" }

        Pass
        {
            // Alpha blending ja z-buffer-asetukset
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // Vertex-struktuuri
            struct Attributes
            {
                float3 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            // Varying-struktuuri
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 normalWS : TEXCOORD0;
                float3 viewDirWS : TEXCOORD1;
            };

            // Vertex-shader
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = float4(IN.positionOS, 1.0);
                OUT.normalWS = normalize(IN.normalOS);
                OUT.viewDirWS = normalize(float3(0.0, 0.0, 1.0)); // Oletetaan, ett‰ kamera on paikallaan
                return OUT;
            }

            // Fragment-shader
            float4 frag(Varyings IN) : SV_Target
            {
                // Fresnelin laskeminen
                float fresnel = pow(1.0 - saturate(dot(IN.normalWS, IN.viewDirWS)), _FresnelPower);

                // V‰riliuku (sateenkaari)
                float3 rainbowColor = saturate(float3(sin(fresnel * 6.28), sin(fresnel * 6.28 + 2.0), sin(fresnel * 6.28 + 4.0)));

                // P‰‰v‰ri ja opasiteetti
                float3 finalColor = lerp(float3(1, 1, 1), rainbowColor, fresnel);

                // Hehkuv‰ri ja voimakkuus
                float3 emission = _EmissionColor.rgb * _GlowIntensity;

                // Palauta lopullinen v‰ri (sateenkaari + hehku)
                return float4(finalColor + emission, _Opacity);
            }

            ENDHLSL
        }
    }
}
