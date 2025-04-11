Shader "Unlit/ChromaKeyUnlit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _KeyColor("Key Color", Color) = (0,1,0)
        _HueTolerance("Hue Tolerance", Range(0, 1)) = 0.1
        _SaturationTolerance("Saturation Tolerance", Range(0, 1)) = 0.2
        _BrightnessTolerance("Brightness Tolerance", Range(0, 1)) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _KeyColor;
            half _HueTolerance;
            half _SaturationTolerance;
            half _BrightnessTolerance;

            // Function to convert RGB to HSB
            fixed3 RGBToHSB(fixed4 color)
            {
                fixed cMax = max(max(color.r, color.g), color.b);
                fixed cMin = min(min(color.r, color.g), color.b);
                fixed delta = cMax - cMin;

                // Hue
                fixed hue = 0;
                if (delta != 0)
                {
                    if (cMax == color.r) hue = (color.g - color.b) / delta + (color.g < color.b ? 6 : 0);
                    else if (cMax == color.g) hue = (color.b - color.r) / delta + 2;
                    else hue = (color.r - color.g) / delta + 4;
                    hue /= 6.0;
                }

                // Saturation
                fixed saturation = (cMax == 0) ? 0 : delta / cMax;

                // Brightness
                fixed brightness = cMax;

                return fixed3(hue, saturation, brightness);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // Convert KeyColor and pixel color to HSB
                fixed3 keyHSB = RGBToHSB(_KeyColor);
                fixed3 pixelHSB = RGBToHSB(col);

                // Calculate differences
                fixed hueDiff = abs(keyHSB.x - pixelHSB.x);
                fixed saturationDiff = abs(keyHSB.y - pixelHSB.y);
                fixed brightnessDiff = abs(keyHSB.z - pixelHSB.z);

                // Check if pixel is within tolerance
                if (hueDiff <= _HueTolerance && saturationDiff <= _SaturationTolerance && brightnessDiff <= _BrightnessTolerance)
                {
                    clip(-1); // Make pixel transparent
                }

                return col;
            }
            ENDCG
        }
    }
}
