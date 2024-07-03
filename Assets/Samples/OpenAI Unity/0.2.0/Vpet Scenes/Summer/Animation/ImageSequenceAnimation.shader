// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/ImageSequenceAnimation"
{
    Properties
    {
        _Color("Color Tint", Color) = (1, 1, 1, 1)
        _MainTex("Image Sequence", 2D) = "white" {}
    //在水平和竖直方向上的关键帧个数
        _HorizontalAmount("Horizontal Amount", Float) = 4
        _VerticalAmount("Vertical Amount", Float) = 4
    //动画播放速度
        _Speed("Speed", Range(1, 100)) = 30
    }
    SubShader
    {
        //序列帧动画通常是透明效果！
            Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}

        Pass
        {
            Tags { "LightMode" = "ForwardBase" }

            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _HorizontalAmount;
            float _VerticalAmount;
            float _Speed;


            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //计算行列数
                float time = floor(_Time.y * _Speed);
                float row = floor(time / _HorizontalAmount);
                float column = time - row * _HorizontalAmount;

                //				half2 uv = float2(i.uv.x /_HorizontalAmount, i.uv.y / _VerticalAmount);
                //				uv.x += column / _HorizontalAmount;
                //				uv.y -= row / _VerticalAmount;
                                half2 uv = i.uv + half2(column, -row);
                                uv.x /= _HorizontalAmount;
                                uv.y /= _VerticalAmount;

                                fixed4 c = tex2D(_MainTex, uv);
                                c.rgb *= _Color;

                                return c;
            }
            ENDCG
        }
    }
            FallBack "Transparent/VertexLit"
}
