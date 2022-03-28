Shader "Custom/Particles/unlit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        finalScale("finalScale", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                float4 targetPos : TEXCOORD1;
                float4 targetColor : TEXCOORD2;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 targetPos : TEXCOORD1;
                float4 targetColor : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float finalScale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex*finalScale);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                o.targetColor = v.targetColor;
                o.targetPos = v.targetPos;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
            //    fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col = i.color;
           //     fixed4 col = i.targetColor;

                // apply fog
          //      UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
