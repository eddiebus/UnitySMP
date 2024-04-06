Shader "Unlit/UVCircle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _MaxRadius("MaxRadius",float) = 0.8
        _MinRadius("MinRadius",float) = 0.0
        _Angle("Angle",float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            #define PI 3.141592

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR0;
            };

            struct v2f
            {
                fixed4 color : COLOR0;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _MaxRadius;
            float _MinRadius;
            float _Angle;
            float4 _MainTex_ST;
            

            v2f vert (appdata v)
            {
                v2f o;
                o.color = v.color;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }


            float ToDegrees(float radians){
                radians *= 180.0/3.14;
                return radians;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 relPosition = i.uv;
                relPosition = (relPosition - float2(0.5f,0.5f)) * 6.0f;
                float2 normalPos = normalize(relPosition);
                float uvDistance = length(relPosition);

                float2 defaultVec = float2(0,-1);

                float dotprod = dot(
                    defaultVec,
                    normalPos
                );

                float determinant = defaultVec.x * normalPos.y - defaultVec.y * normalPos.x;

                float angle = atan2(
                    -determinant,
                    dotprod
                    ) ;
                

                fixed4 col = tex2D(_MainTex, i.uv);
                if (
                    uvDistance > (_MaxRadius * 3) 
                    || uvDistance < (_MinRadius * 3)
                    // 2 Pi = 1 full circle
                    || (angle + PI) > (_Angle * PI) * 2 ){
                    col = float4(0,0,0,0);
                }

                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col * i.color;
            }
            ENDCG
        }
    }
}