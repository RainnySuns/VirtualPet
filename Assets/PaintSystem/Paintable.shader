// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Paintable"
{
	Properties
	{
		_BumpNoiseScale("BumpNoiseScale", Float) = 1
		_ShapeNoiseScale("ShapeNoiseScale", Float) = 1
		_Mask("_Mask", 2D) = "black" {}
	    _MaskShape("MaskShape", 2D) = "black"{}
		_NormalStrength("NormalStrength", Float) = 0
		_NormalMap("NormalMap", 2D) = "bump" {}
		_MainTexture("MainTexture", 2D) = "white" {}
		_Albedo("Albedo", Color) = (1,1,1,0)
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_PaintMetallic("PaintMetallic", Range( 0 , 1)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_PaintSmoothness("PaintSmoothness", Range( 0 , 1)) = 0
		_GlitterTiling("GlitterTiling", Vector) = (1,1,0,0)
		_GlitterTexture("GlitterTexture", 2D) = "white" {}
		_GlitterIntensity("GlitterIntensity", Float) = -0.47
		_GlitterColor("GlitterColor", Color) = (0.8773585,0.8773585,0.8773585,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
		#else//ASE Sampling Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
		#endif//ASE Sampling Macros

		#ifdef UNITY_PASS_SHADOWCASTER
		#undef INTERNAL_DATA
		#undef WorldReflectionVector
		#undef WorldNormalVector
		#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
		#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
		#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		
			struct Input
		{
			float2 uv_texcoord;
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldPos;
		};

		UNITY_DECLARE_TEX2D_NOSAMPLER(_NormalMap);
		UNITY_DECLARE_TEX2D_NOSAMPLER(_MainTexture);
		uniform float4 _MainTexture_ST;
		SamplerState sampler_NormalMap;
		uniform float _NormalStrength;
		uniform float _BumpNoiseScale;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_Mask);
		SamplerState sampler_Mask;
		uniform float4 _Mask_ST;
		uniform float _ShapeNoiseScale;
		SamplerState sampler_MainTexture;
		uniform float4 _Albedo;
		uniform float4 _GlitterColor;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_GlitterTexture);
		SamplerState sampler_GlitterTexture;
		uniform float2 _GlitterTiling;
		uniform float _GlitterIntensity;
		uniform float _Metallic;
		uniform float _PaintMetallic;
		uniform float _Smoothness;
		uniform float _PaintSmoothness;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST.xy + _MainTexture_ST.zw;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldPos = i.worldPos;
			float3 temp_output_16_0_g1 = ( ase_worldPos * 100.0 );
			float3 crossY18_g1 = cross( ase_worldNormal , ddy( temp_output_16_0_g1 ) );
			float3 worldDerivativeX2_g1 = ddx( temp_output_16_0_g1 );
			float dotResult6_g1 = dot( crossY18_g1 , worldDerivativeX2_g1 );
			float crossYDotWorldDerivX34_g1 = abs( dotResult6_g1 );
			float2 temp_cast_0 = (9.92).xx;
			float2 uv_TexCoord3 = i.uv_texcoord * temp_cast_0;
			float simplePerlin2D2 = snoise( uv_TexCoord3*_BumpNoiseScale );
			simplePerlin2D2 = simplePerlin2D2*0.5 + 0.5;
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode8 = SAMPLE_TEXTURE2D( _Mask, sampler_Mask, uv_Mask );
			float lerpResult6 = lerp( ( simplePerlin2D2 + 0.3 ) , ( simplePerlin2D2 * tex2DNode8.a ) , 0.3);
			float temp_output_20_0_g1 = ( lerpResult6 * 72.5 * _NormalStrength * tex2DNode8.a );
			float3 crossX19_g1 = cross( ase_worldNormal , worldDerivativeX2_g1 );
			float3 break29_g1 = ( sign( crossYDotWorldDerivX34_g1 ) * ( ( ddx( temp_output_20_0_g1 ) * crossY18_g1 ) + ( ddy( temp_output_20_0_g1 ) * crossX19_g1 ) ) );
			float3 appendResult30_g1 = (float3(break29_g1.x , -break29_g1.y , break29_g1.z));
			float3 normalizeResult39_g1 = normalize( ( ( crossYDotWorldDerivX34_g1 * ase_worldNormal ) - appendResult30_g1 ) );
			float3 ase_worldTangent = WorldNormalVector( i, float3( 1, 0, 0 ) );
			float3 ase_worldBitangent = WorldNormalVector( i, float3( 0, 1, 0 ) );
			float3x3 ase_worldToTangent = float3x3( ase_worldTangent, ase_worldBitangent, ase_worldNormal );
			float3 worldToTangentDir42_g1 = mul( ase_worldToTangent, normalizeResult39_g1);
			float simplePerlin2D17 = snoise( uv_TexCoord3*_ShapeNoiseScale );
			simplePerlin2D17 = simplePerlin2D17*0.5 + 0.5;
			float temp_output_21_0 = step( ( 1.0 - tex2DNode8.a ) , ( tex2DNode8.a + ( tex2DNode8.a * simplePerlin2D17 ) ) );
			float3 lerpResult15 = lerp( UnpackScaleNormal( SAMPLE_TEXTURE2D( _NormalMap, sampler_NormalMap, uv_MainTexture ), _NormalStrength ) , worldToTangentDir42_g1 , temp_output_21_0);
			o.Normal = lerpResult15;
			float4 lerpResult27 = lerp( ( SAMPLE_TEXTURE2D( _MainTexture, sampler_MainTexture, uv_MainTexture ) * _Albedo ) , SAMPLE_TEXTURE2D( _Mask, sampler_Mask, uv_Mask ) , temp_output_21_0);
			o.Albedo = lerpResult27.rgb;
			float4 tex2DNode37 = SAMPLE_TEXTURE2D( _GlitterTexture, sampler_GlitterTexture, (i.uv_texcoord*_GlitterTiling + 0.0) );
			float3 appendResult38 = (float3(tex2DNode37.r , tex2DNode37.g , tex2DNode37.b));
			float3 normalizeResult40 = normalize( ( appendResult38 - float3( 0.5,0.5,0.5 ) ) );
			float3 ase_vertexNormal = mul( unity_WorldToObject, float4( ase_worldNormal, 0 ) );
			float3 normalizeResult44 = normalize( ( normalizeResult40 + ase_vertexNormal ) );
			float3 ase_worldViewDir = Unity_SafeNormalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float dotResult49 = dot( normalizeResult44 , mul( float4( ase_worldViewDir , 0.0 ), unity_WorldToObject ).xyz );
			o.Emission = ( _GlitterColor * pow( saturate( dotResult49 ) , exp2( ( _GlitterIntensity + 1.0 ) ) ) * temp_output_21_0 ).rgb;
			float lerpResult30 = lerp( _Metallic , _PaintMetallic , temp_output_21_0);
			o.Metallic = lerpResult30;
			float lerpResult31 = lerp( _Smoothness , _PaintSmoothness , temp_output_21_0);
			o.Smoothness = lerpResult31;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
