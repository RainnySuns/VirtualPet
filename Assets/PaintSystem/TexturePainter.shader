Shader "TNTC/TexturePainter"{   

    Properties{
        _PainterColor ("Painter Color", Color) = (0, 0, 0, 0)
        _PatternTexture ("Pattern Texture", 2D) = "white" {}
    }

    SubShader{
        Cull Off ZWrite Off ZTest Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"



			sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float3 _PainterPosition;
            float _Radius;
            float _Hardness;
            float _Strength;
            float4 _PainterColor;
            float _PrepareUV;
            sampler2D _PatternTexture;
            float4 _PatternTexture_ST;
            float _RotationAngle;


            struct appdata{
                float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
            };

            struct v2f{
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 worldPos : TEXCOORD1;
            };

            float2 Rotate(float2 coord, float angle)
            {
                float s = sin(angle);
                float c = cos(angle);
                return float2(c * coord.x - s * coord.y, s * coord.x + c * coord.y);
            }

            float mask(float3 position, float3 center, float radius, float hardness){
                float m = distance(center, position);
                return 1 - smoothstep(radius * hardness, radius, m);    
            }

            v2f vert (appdata v){
                v2f o;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = v.uv;
				float4 uv = float4(0, 0, 0, 1);
                uv.xy = float2(1, _ProjectionParams.x) * (v.uv.xy * float2( 2, 2) - float2(1, 1));
				o.vertex = uv; 
                return o;
            }

            fixed4 frag (v2f i) : SV_Target{   
                if(_PrepareUV > 0 ){
                    return float4(0, 0, 1, 1);
                }         

                // float2 patternCoord = i.worldPos.xz / 2;
                float2 patternCoord = (i.worldPos.xz - _PainterPosition.xz) * 0.5 + 0.25; 
                //patternCoord = Rotate(patternCoord, _RotationAngle);

                float patternValue = tex2D(_PatternTexture, patternCoord).a;  // Assuming the texture is greyscale
                float4 col = tex2D(_MainTex, i.uv);
                float f = mask(i.worldPos, _PainterPosition, _Radius, _Hardness) * patternValue;
                float edge = f * _Strength;
                return lerp(col, _PainterColor, edge);
            }
            ENDCG
        }
    }
}