Shader "Custom/SimpleRainbow"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1, 1, 1, 1)  // Oletusv‰ri valkoinen (1, 1, 1) ja t‰ysi kirkkaus (1)
        _Opacity ("Opacity", Range(0.0, 1.0)) = 0.5      // Oletusarvo opasiteetille (l‰pin‰kyvyys) on 0.5
    }

    SubShader
    {
        Tags { "RenderPipeline"="HDRenderPipeline" "Queue"="Overlay" }

        Pass
        {
            // Renderer- ja l‰pin‰kyvyysasetukset
            Blend SrcAlpha OneMinusSrcAlpha  // Alpha-blending-asetus, joka tekee materiaalista l‰pin‰kyv‰n
            ZWrite Off  // Poistetaan kirjoitus z-bufferiin, jotta l‰pin‰kyvyys toimii

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct Attributes
            {
                float3 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = float4(IN.positionOS, 1.0);
                return OUT;
            }

            // Fragment-shader, joka k‰sittelee v‰rit ja opasiteetin
            float4 frag(Varyings IN) : SV_Target
            {
                // K‰ytet‰‰n p‰‰v‰ri‰ ja opasiteetti‰
                float4 mainColor = _MainColor; // P‰‰v‰ri
                mainColor.a = _Opacity;        // Asetetaan opasiteetti (l‰pin‰kyvyys)
                
                return mainColor;
            }

            ENDHLSL
        }
    }
}
