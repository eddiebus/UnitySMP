Shader "Unlit/Sh_FlatPost"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineWidth("OutlineWidth",float) = 2.0
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

            float _OutlineWidth;
            UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }


            float GetOutlineValue(float uv){
                float2 uvPoint[4] = {
                    uv + float2(0, _OutlineWidth),
                    uv + float2(_OutlineWidth, _OutlineWidth),
                    uv + float2(0, -_OutlineWidth),
                    uv + float2(-_OutlineWidth, -_OutlineWidth),
                    };

                int uvCount = 4;

                float result = 0;
                for (uint i = 0; i < 4; i++ ){
                    result += tex2D(_CameraDepthTexture,uvPoint[i]);
                }
                result -=  tex2D(_CameraDepthTexture,uv) * uvCount;
                return 0.0;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                if (GetOutlineValue(i.uv) > 0.0){
                    col = float4(0,0,0,0);
                    }
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
