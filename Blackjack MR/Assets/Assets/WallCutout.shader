Shader "Custom/WallCutout"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CutoutMask ("Cutout Mask", 2D) = "black" {}  // A texture that defines where to cut
        _CutoutThreshold ("Cutout Threshold", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags {"Queue"="AlphaTest"}
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _CutoutMask;
            float _CutoutThreshold;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 mask = tex2D(_CutoutMask, i.uv);
                
                // Cut out the pixels where the mask is above the threshold
                clip(mask.r - _CutoutThreshold);

                return col;
            }
            ENDCG
        }
    }
}
