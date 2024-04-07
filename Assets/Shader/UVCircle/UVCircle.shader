Shader "Unlit/UVCircle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _MinAngle("MinAngle",float) = 0.0
        _MaxAngle("MaxAngle",float) = 2.0
        _MaxRadius("MaxRadius",float) = 0.8
        _MinRadius("MinRadius",float) = 0.0
        _Fill("Fill",float) = 0.0
        [KeywordEnum(Forward,Backward,Center)] _FillType("FillType",int) = 0
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
            float _MinAngle;
            float _MaxAngle;
            float _MaxRadius;
            float _MinRadius;
            float _Fill;
            int _FillType;
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
                // Get Vector Angle
                float pointAngle = atan2(
                    -determinant,
                    dotprod
                    ) + PI;
                pointAngle /= PI * 2;
                
                bool angleOk = pointAngle > _MinAngle;
                bool radiusOk = uvDistance < (_MaxRadius * 3) && uvDistance > (_MinRadius * 3);
                 
                bool inRange = false;

                if (angleOk){
                    if (_Fill > 1.0){
                        _Fill = 1.0;
                    }
                    else if (_Fill < 0.0){
                       _Fill = 0.0;
                    }
                    switch(_FillType)
                    {
                        // Forward Direction
                        case (0):
                        {
                            float range = _MaxAngle - _MinAngle;
                            float maxanglePoint = _Fill * (range) + _MinAngle;
                            inRange = pointAngle < maxanglePoint && pointAngle < _MaxAngle;
                            break;
                        }
                        // Backward (Reverse) Direction
                        case (1):
                        {
                            float range = _MaxAngle - _MinAngle;
                            float minAnglePoint = _MaxAngle - (_Fill * (range));
                            inRange = pointAngle > minAnglePoint && pointAngle < _MaxAngle;
                            break;
                        }
                        case (2):
                        {
                            float range = _MaxAngle - _MinAngle;
                            float offset = (range/2) * (1.0f - _Fill);
                            inRange = pointAngle > _MinAngle + offset && pointAngle < _MaxAngle - offset;
                            break;
                        }
                        default:
                        {
                            inRange = false;
                            break;
                        }
                    }
                }
                fixed4 col = tex2D(_MainTex, i.uv);
                if ( !angleOk || !radiusOk || !inRange)
                {
                    col = float4(0,0,0,0);

                    
                }

                //UNITY_APPLY_FOG(i.fogCoord, col);
                return col * i.color;
            }
            ENDCG
        }
    }
}
