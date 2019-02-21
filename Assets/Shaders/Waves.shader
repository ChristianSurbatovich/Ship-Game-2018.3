// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Waves"
{
	Properties
	{
		_TessPhongStrength( "Phong Tess Strength", Range( 0, 1 ) ) = 1
		_SurfaceColor("Surface Color", Color) = (0.2548505,0.5770289,0.7830189,0)
		_ShallowWater("Shallow Water", Color) = (0.2548505,0.5770289,0.7830189,0)
		_Lowfoamcolor("Low foam color", Color) = (1,1,1,0)
		_FoamColor("Foam Color", Color) = (1,1,1,0)
		_LowFoam("Low Foam", 2D) = "white" {}
		_CrestFoam("Crest Foam", 2D) = "white" {}
		_TroughFoamMapping("Trough Foam Mapping", Vector) = (0,0,0,0)
		_CrestFoamMapping("Crest Foam Mapping", Vector) = (0,0,0,0)
		_SmoothnessMapping("Smoothness Mapping", Vector) = (0,0,0,0)
		_LargeSwellsCrestInfluence("Large Swells Crest Influence", Float) = 0
		_LargeSwellsFoamShape("Large Swells Foam Shape", Float) = 1
		_SwellsCrestInfluence("Swells Crest Influence", Float) = 0
		_SwellsFoamShape("Swells Foam Shape", Float) = 1
		_SmallWavesCrestInfluence("Small Waves Crest Influence", Float) = 0
		_SmallWavesFoamShape("Small Waves Foam Shape", Float) = 1
		_RipplesCrestInfluence("Ripples Crest Influence", Float) = 1
		_RipplesFoamShape("Ripples Foam Shape", Float) = 1
		_WaterColorMapping("Water Color Mapping", Vector) = (0,0,0,0)
		_TranslucencyMapping("Translucency Mapping", Vector) = (0,0,0,0)
		[Header(Translucency)]
		_Translucency("Strength", Range( 0 , 50)) = 1
		_TransNormalDistortion("Normal Distortion", Range( 0 , 1)) = 0.1
		_TransScattering("Scaterring Falloff", Range( 1 , 50)) = 2
		_TransDirect("Direct", Range( 0 , 1)) = 1
		_TransAmbient("Ambient", Range( 0 , 1)) = 0.2
		_TranslucencyScale("Translucency Scale", Float) = 0
		_TransShadow("Shadow", Range( 0 , 1)) = 0.9
		_DepthFade("Depth Fade", Float) = 0
		_TessellationFactor("Tessellation Factor", Float) = 0
		_MaxDistance("Max Distance", Float) = 0
		_MinDistance("Min Distance", Float) = 0
		_CrestFoamWorldScaling("Crest Foam World Scaling", Float) = 0
		_TroughFoamWorldScaling("Trough Foam World Scaling", Float) = 0
		_DepthScale("Depth Scale", Float) = 2
		_CrestFoamUVOffset("Crest Foam UV Offset", Float) = 0
		_TroughFoamUVOffset("Trough Foam UV Offset", Float) = 0
		_Specularity("Specularity", Float) = 1
		_WakeTextureRotation("Wake Texture Rotation", Float) = 1
		_DepthTextureRotation("Depth  Texture Rotation", Float) = 0
		_FineDetailCutoff("Fine Detail Cutoff", Float) = 250
		_DetailCutoff("Detail Cutoff", Float) = 500
		_DistanceCutoff("Distance Cutoff", Float) = 1000
		_Depth("Depth", 2D) = "white" {}
		_WakeUVMapping("Wake UV Mapping", Vector) = (0.625,0.375,0.375,0.625)
		_WakeTexture("Wake Texture", 2D) = "white" {}
		_SprayOffsetMapping("Spray Offset Mapping", Vector) = (0.21,1,0,1)
		_SprayOffsetShape("Spray Offset Shape", Float) = 0
		_SprayOffsetStrength("Spray Offset Strength", Float) = 0
		_SprayFoamMapping("Spray Foam Mapping", Vector) = (0.21,1,0,1)
		_SprayFoamShape("Spray Foam Shape", Float) = 0
		_SprayFoamStrength("Spray Foam Strength", Float) = 1
		_LargeSwellsSprayShape("Large Swells Spray Shape", Float) = 0
		_WakeUVScaling("WakeUVScaling", Float) = 0
		_LargeSwellsSprayStrength("Large Swells Spray Strength", Float) = 0
		_SwellsSprayShape("Swells Spray Shape", Float) = 0
		_SwellsSprayStrength("Swells Spray Strength", Float) = 0
		_SmallWavesSprayShape("Small Waves Spray Shape", Float) = 0
		_SmallWavesSprayStrength("Small Waves Spray Strength", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Tessellation.cginc"
		#include "Lighting.cginc"
		#pragma target 4.6
		struct Input
		{
			float3 worldPos;
			float4 screenPos;
			float2 uv_texcoord;
		};

		struct SurfaceOutputStandardSpecularCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half3 Specular;
			half Smoothness;
			half Occlusion;
			half Alpha;
			half3 Translucency;
		};

		uniform float4 LargeSwells6;
		uniform float timeOffset;
		uniform float4 LargeSwells5;
		uniform float4 LargeSwells4;
		uniform float4 LargeSwells3;
		uniform float4 LargeSwells2;
		uniform float4 LargeSwells1;
		uniform float _LargeSwellsSprayShape;
		uniform float _LargeSwellsSprayStrength;
		uniform float4 SmallWaves6;
		uniform float4 SmallWaves5;
		uniform float4 SmallWaves4;
		uniform float4 SmallWaves3;
		uniform float4 SmallWaves2;
		uniform float4 SmallWaves1;
		uniform float _SmallWavesSprayShape;
		uniform float _SmallWavesSprayStrength;
		uniform float4 Swells6;
		uniform float4 Swells5;
		uniform float4 Swells4;
		uniform float4 Swells3;
		uniform float4 Swells2;
		uniform float4 Swells1;
		uniform float _SwellsSprayShape;
		uniform float _SwellsSprayStrength;
		uniform sampler2D _WakeTexture;
		uniform sampler2D _Depth;
		uniform float _DepthTextureRotation;
		uniform float4 DepthMapping;
		uniform float _DepthScale;
		uniform float _DistanceCutoff;
		uniform float _DetailCutoff;
		uniform float _FineDetailCutoff;
		uniform float4 Ripples6;
		uniform float4 Ripples5;
		uniform float4 Ripples4;
		uniform float4 Ripples3;
		uniform float4 Ripples2;
		uniform float4 Ripples1;
		uniform float _WakeUVScaling;
		uniform float _WakeTextureRotation;
		uniform float4 _WakeUVMapping;
		uniform float4 _SprayOffsetMapping;
		uniform float _SprayOffsetShape;
		uniform float _SprayOffsetStrength;
		uniform float4 _ShallowWater;
		uniform float4 _SurfaceColor;
		uniform float _LargeSwellsCrestInfluence;
		uniform float _LargeSwellsFoamShape;
		uniform float _SwellsCrestInfluence;
		uniform float _SwellsFoamShape;
		uniform float _SmallWavesCrestInfluence;
		uniform float _SmallWavesFoamShape;
		uniform float _RipplesCrestInfluence;
		uniform float _RipplesFoamShape;
		uniform float4 _WaterColorMapping;
		uniform sampler2D _CameraDepthTexture;
		uniform float _DepthFade;
		uniform float4 _FoamColor;
		uniform sampler2D _CrestFoam;
		uniform float _CrestFoamWorldScaling;
		uniform float _CrestFoamUVOffset;
		uniform float4 _CrestFoamMapping;
		uniform float4 _Lowfoamcolor;
		uniform sampler2D _LowFoam;
		uniform float _TroughFoamWorldScaling;
		uniform float _TroughFoamUVOffset;
		uniform float4 _SprayFoamMapping;
		uniform float _SprayFoamShape;
		uniform float _SprayFoamStrength;
		uniform float4 _TroughFoamMapping;
		uniform float _Specularity;
		uniform float4 _SmoothnessMapping;
		uniform half _Translucency;
		uniform half _TransNormalDistortion;
		uniform half _TransScattering;
		uniform half _TransDirect;
		uniform half _TransAmbient;
		uniform half _TransShadow;
		uniform float4 _TranslucencyMapping;
		uniform float _TranslucencyScale;
		uniform float _MinDistance;
		uniform float _MaxDistance;
		uniform float _TessellationFactor;
		uniform float _TessPhongStrength;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityDistanceBasedTess( v0.vertex, v1.vertex, v2.vertex, _MinDistance,_MaxDistance,_TessellationFactor);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float4 temp_output_3_0_g101 = LargeSwells6;
			float2 temp_output_13_0_g101 = (temp_output_3_0_g101).xy;
			float2 break28_g101 = temp_output_13_0_g101;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float3 temp_output_13_0_g95 = ase_worldPos;
			float dotResult21_g101 = dot( temp_output_13_0_g101 , (temp_output_13_0_g95).xz );
			float4 break7_g101 = temp_output_3_0_g101;
			float temp_output_10_0_g101 = ( ( 2.0 * UNITY_PI ) / break7_g101.w );
			float temp_output_10_0_g95 = timeOffset;
			float temp_output_25_0_g101 = ( ( dotResult21_g101 - ( sqrt( ( 9.8 / temp_output_10_0_g101 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g101 );
			float temp_output_36_0_g101 = cos( temp_output_25_0_g101 );
			float temp_output_26_0_g101 = ( break7_g101.z / temp_output_10_0_g101 );
			float temp_output_34_0_g101 = sin( temp_output_25_0_g101 );
			float3 appendResult48_g101 = (float3(( break28_g101.x * temp_output_36_0_g101 * temp_output_26_0_g101 ) , ( temp_output_26_0_g101 * temp_output_34_0_g101 ) , ( break28_g101.y * temp_output_26_0_g101 * temp_output_36_0_g101 )));
			float4 temp_output_3_0_g97 = LargeSwells5;
			float2 temp_output_13_0_g97 = (temp_output_3_0_g97).xy;
			float2 break28_g97 = temp_output_13_0_g97;
			float dotResult21_g97 = dot( temp_output_13_0_g97 , (temp_output_13_0_g95).xz );
			float4 break7_g97 = temp_output_3_0_g97;
			float temp_output_10_0_g97 = ( ( 2.0 * UNITY_PI ) / break7_g97.w );
			float temp_output_25_0_g97 = ( ( dotResult21_g97 - ( sqrt( ( 9.8 / temp_output_10_0_g97 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g97 );
			float temp_output_36_0_g97 = cos( temp_output_25_0_g97 );
			float temp_output_26_0_g97 = ( break7_g97.z / temp_output_10_0_g97 );
			float temp_output_34_0_g97 = sin( temp_output_25_0_g97 );
			float3 appendResult48_g97 = (float3(( break28_g97.x * temp_output_36_0_g97 * temp_output_26_0_g97 ) , ( temp_output_26_0_g97 * temp_output_34_0_g97 ) , ( break28_g97.y * temp_output_26_0_g97 * temp_output_36_0_g97 )));
			float4 temp_output_3_0_g98 = LargeSwells4;
			float2 temp_output_13_0_g98 = (temp_output_3_0_g98).xy;
			float2 break28_g98 = temp_output_13_0_g98;
			float dotResult21_g98 = dot( temp_output_13_0_g98 , (temp_output_13_0_g95).xz );
			float4 break7_g98 = temp_output_3_0_g98;
			float temp_output_10_0_g98 = ( ( 2.0 * UNITY_PI ) / break7_g98.w );
			float temp_output_25_0_g98 = ( ( dotResult21_g98 - ( sqrt( ( 9.8 / temp_output_10_0_g98 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g98 );
			float temp_output_36_0_g98 = cos( temp_output_25_0_g98 );
			float temp_output_26_0_g98 = ( break7_g98.z / temp_output_10_0_g98 );
			float temp_output_34_0_g98 = sin( temp_output_25_0_g98 );
			float3 appendResult48_g98 = (float3(( break28_g98.x * temp_output_36_0_g98 * temp_output_26_0_g98 ) , ( temp_output_26_0_g98 * temp_output_34_0_g98 ) , ( break28_g98.y * temp_output_26_0_g98 * temp_output_36_0_g98 )));
			float4 temp_output_3_0_g99 = LargeSwells3;
			float2 temp_output_13_0_g99 = (temp_output_3_0_g99).xy;
			float2 break28_g99 = temp_output_13_0_g99;
			float dotResult21_g99 = dot( temp_output_13_0_g99 , (temp_output_13_0_g95).xz );
			float4 break7_g99 = temp_output_3_0_g99;
			float temp_output_10_0_g99 = ( ( 2.0 * UNITY_PI ) / break7_g99.w );
			float temp_output_25_0_g99 = ( ( dotResult21_g99 - ( sqrt( ( 9.8 / temp_output_10_0_g99 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g99 );
			float temp_output_36_0_g99 = cos( temp_output_25_0_g99 );
			float temp_output_26_0_g99 = ( break7_g99.z / temp_output_10_0_g99 );
			float temp_output_34_0_g99 = sin( temp_output_25_0_g99 );
			float3 appendResult48_g99 = (float3(( break28_g99.x * temp_output_36_0_g99 * temp_output_26_0_g99 ) , ( temp_output_26_0_g99 * temp_output_34_0_g99 ) , ( break28_g99.y * temp_output_26_0_g99 * temp_output_36_0_g99 )));
			float4 temp_output_3_0_g100 = LargeSwells2;
			float2 temp_output_13_0_g100 = (temp_output_3_0_g100).xy;
			float2 break28_g100 = temp_output_13_0_g100;
			float dotResult21_g100 = dot( temp_output_13_0_g100 , (temp_output_13_0_g95).xz );
			float4 break7_g100 = temp_output_3_0_g100;
			float temp_output_10_0_g100 = ( ( 2.0 * UNITY_PI ) / break7_g100.w );
			float temp_output_25_0_g100 = ( ( dotResult21_g100 - ( sqrt( ( 9.8 / temp_output_10_0_g100 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g100 );
			float temp_output_36_0_g100 = cos( temp_output_25_0_g100 );
			float temp_output_26_0_g100 = ( break7_g100.z / temp_output_10_0_g100 );
			float temp_output_34_0_g100 = sin( temp_output_25_0_g100 );
			float3 appendResult48_g100 = (float3(( break28_g100.x * temp_output_36_0_g100 * temp_output_26_0_g100 ) , ( temp_output_26_0_g100 * temp_output_34_0_g100 ) , ( break28_g100.y * temp_output_26_0_g100 * temp_output_36_0_g100 )));
			float4 temp_output_3_0_g96 = LargeSwells1;
			float2 temp_output_13_0_g96 = (temp_output_3_0_g96).xy;
			float2 break28_g96 = temp_output_13_0_g96;
			float dotResult21_g96 = dot( temp_output_13_0_g96 , (temp_output_13_0_g95).xz );
			float4 break7_g96 = temp_output_3_0_g96;
			float temp_output_10_0_g96 = ( ( 2.0 * UNITY_PI ) / break7_g96.w );
			float temp_output_25_0_g96 = ( ( dotResult21_g96 - ( sqrt( ( 9.8 / temp_output_10_0_g96 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g96 );
			float temp_output_36_0_g96 = cos( temp_output_25_0_g96 );
			float temp_output_26_0_g96 = ( break7_g96.z / temp_output_10_0_g96 );
			float temp_output_34_0_g96 = sin( temp_output_25_0_g96 );
			float3 appendResult48_g96 = (float3(( break28_g96.x * temp_output_36_0_g96 * temp_output_26_0_g96 ) , ( temp_output_26_0_g96 * temp_output_34_0_g96 ) , ( break28_g96.y * temp_output_26_0_g96 * temp_output_36_0_g96 )));
			float3 temp_output_451_3 = ( appendResult48_g101 + appendResult48_g97 + appendResult48_g98 + appendResult48_g99 + appendResult48_g100 + appendResult48_g96 );
			float temp_output_451_22 = ( temp_output_26_0_g101 + temp_output_26_0_g97 + temp_output_26_0_g98 + temp_output_26_0_g99 + temp_output_26_0_g100 + temp_output_26_0_g96 );
			float temp_output_406_0 = (0.0 + ((temp_output_451_3).y - -temp_output_451_22) * (1.0 - 0.0) / (temp_output_451_22 - -temp_output_451_22));
			float4 temp_output_3_0_g115 = SmallWaves6;
			float2 temp_output_13_0_g115 = (temp_output_3_0_g115).xy;
			float2 break28_g115 = temp_output_13_0_g115;
			float3 temp_output_13_0_g109 = ase_worldPos;
			float dotResult21_g115 = dot( temp_output_13_0_g115 , (temp_output_13_0_g109).xz );
			float4 break7_g115 = temp_output_3_0_g115;
			float temp_output_10_0_g115 = ( ( 2.0 * UNITY_PI ) / break7_g115.w );
			float temp_output_10_0_g109 = timeOffset;
			float temp_output_25_0_g115 = ( ( dotResult21_g115 - ( sqrt( ( 9.8 / temp_output_10_0_g115 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g115 );
			float temp_output_36_0_g115 = cos( temp_output_25_0_g115 );
			float temp_output_26_0_g115 = ( break7_g115.z / temp_output_10_0_g115 );
			float temp_output_34_0_g115 = sin( temp_output_25_0_g115 );
			float3 appendResult48_g115 = (float3(( break28_g115.x * temp_output_36_0_g115 * temp_output_26_0_g115 ) , ( temp_output_26_0_g115 * temp_output_34_0_g115 ) , ( break28_g115.y * temp_output_26_0_g115 * temp_output_36_0_g115 )));
			float4 temp_output_3_0_g111 = SmallWaves5;
			float2 temp_output_13_0_g111 = (temp_output_3_0_g111).xy;
			float2 break28_g111 = temp_output_13_0_g111;
			float dotResult21_g111 = dot( temp_output_13_0_g111 , (temp_output_13_0_g109).xz );
			float4 break7_g111 = temp_output_3_0_g111;
			float temp_output_10_0_g111 = ( ( 2.0 * UNITY_PI ) / break7_g111.w );
			float temp_output_25_0_g111 = ( ( dotResult21_g111 - ( sqrt( ( 9.8 / temp_output_10_0_g111 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g111 );
			float temp_output_36_0_g111 = cos( temp_output_25_0_g111 );
			float temp_output_26_0_g111 = ( break7_g111.z / temp_output_10_0_g111 );
			float temp_output_34_0_g111 = sin( temp_output_25_0_g111 );
			float3 appendResult48_g111 = (float3(( break28_g111.x * temp_output_36_0_g111 * temp_output_26_0_g111 ) , ( temp_output_26_0_g111 * temp_output_34_0_g111 ) , ( break28_g111.y * temp_output_26_0_g111 * temp_output_36_0_g111 )));
			float4 temp_output_3_0_g112 = SmallWaves4;
			float2 temp_output_13_0_g112 = (temp_output_3_0_g112).xy;
			float2 break28_g112 = temp_output_13_0_g112;
			float dotResult21_g112 = dot( temp_output_13_0_g112 , (temp_output_13_0_g109).xz );
			float4 break7_g112 = temp_output_3_0_g112;
			float temp_output_10_0_g112 = ( ( 2.0 * UNITY_PI ) / break7_g112.w );
			float temp_output_25_0_g112 = ( ( dotResult21_g112 - ( sqrt( ( 9.8 / temp_output_10_0_g112 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g112 );
			float temp_output_36_0_g112 = cos( temp_output_25_0_g112 );
			float temp_output_26_0_g112 = ( break7_g112.z / temp_output_10_0_g112 );
			float temp_output_34_0_g112 = sin( temp_output_25_0_g112 );
			float3 appendResult48_g112 = (float3(( break28_g112.x * temp_output_36_0_g112 * temp_output_26_0_g112 ) , ( temp_output_26_0_g112 * temp_output_34_0_g112 ) , ( break28_g112.y * temp_output_26_0_g112 * temp_output_36_0_g112 )));
			float4 temp_output_3_0_g113 = SmallWaves3;
			float2 temp_output_13_0_g113 = (temp_output_3_0_g113).xy;
			float2 break28_g113 = temp_output_13_0_g113;
			float dotResult21_g113 = dot( temp_output_13_0_g113 , (temp_output_13_0_g109).xz );
			float4 break7_g113 = temp_output_3_0_g113;
			float temp_output_10_0_g113 = ( ( 2.0 * UNITY_PI ) / break7_g113.w );
			float temp_output_25_0_g113 = ( ( dotResult21_g113 - ( sqrt( ( 9.8 / temp_output_10_0_g113 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g113 );
			float temp_output_36_0_g113 = cos( temp_output_25_0_g113 );
			float temp_output_26_0_g113 = ( break7_g113.z / temp_output_10_0_g113 );
			float temp_output_34_0_g113 = sin( temp_output_25_0_g113 );
			float3 appendResult48_g113 = (float3(( break28_g113.x * temp_output_36_0_g113 * temp_output_26_0_g113 ) , ( temp_output_26_0_g113 * temp_output_34_0_g113 ) , ( break28_g113.y * temp_output_26_0_g113 * temp_output_36_0_g113 )));
			float4 temp_output_3_0_g114 = SmallWaves2;
			float2 temp_output_13_0_g114 = (temp_output_3_0_g114).xy;
			float2 break28_g114 = temp_output_13_0_g114;
			float dotResult21_g114 = dot( temp_output_13_0_g114 , (temp_output_13_0_g109).xz );
			float4 break7_g114 = temp_output_3_0_g114;
			float temp_output_10_0_g114 = ( ( 2.0 * UNITY_PI ) / break7_g114.w );
			float temp_output_25_0_g114 = ( ( dotResult21_g114 - ( sqrt( ( 9.8 / temp_output_10_0_g114 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g114 );
			float temp_output_36_0_g114 = cos( temp_output_25_0_g114 );
			float temp_output_26_0_g114 = ( break7_g114.z / temp_output_10_0_g114 );
			float temp_output_34_0_g114 = sin( temp_output_25_0_g114 );
			float3 appendResult48_g114 = (float3(( break28_g114.x * temp_output_36_0_g114 * temp_output_26_0_g114 ) , ( temp_output_26_0_g114 * temp_output_34_0_g114 ) , ( break28_g114.y * temp_output_26_0_g114 * temp_output_36_0_g114 )));
			float4 temp_output_3_0_g110 = SmallWaves1;
			float2 temp_output_13_0_g110 = (temp_output_3_0_g110).xy;
			float2 break28_g110 = temp_output_13_0_g110;
			float dotResult21_g110 = dot( temp_output_13_0_g110 , (temp_output_13_0_g109).xz );
			float4 break7_g110 = temp_output_3_0_g110;
			float temp_output_10_0_g110 = ( ( 2.0 * UNITY_PI ) / break7_g110.w );
			float temp_output_25_0_g110 = ( ( dotResult21_g110 - ( sqrt( ( 9.8 / temp_output_10_0_g110 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g110 );
			float temp_output_36_0_g110 = cos( temp_output_25_0_g110 );
			float temp_output_26_0_g110 = ( break7_g110.z / temp_output_10_0_g110 );
			float temp_output_34_0_g110 = sin( temp_output_25_0_g110 );
			float3 appendResult48_g110 = (float3(( break28_g110.x * temp_output_36_0_g110 * temp_output_26_0_g110 ) , ( temp_output_26_0_g110 * temp_output_34_0_g110 ) , ( break28_g110.y * temp_output_26_0_g110 * temp_output_36_0_g110 )));
			float3 temp_output_453_3 = ( appendResult48_g115 + appendResult48_g111 + appendResult48_g112 + appendResult48_g113 + appendResult48_g114 + appendResult48_g110 );
			float temp_output_453_22 = ( temp_output_26_0_g115 + temp_output_26_0_g111 + temp_output_26_0_g112 + temp_output_26_0_g113 + temp_output_26_0_g114 + temp_output_26_0_g110 );
			float temp_output_411_0 = (0.0 + ((temp_output_453_3).y - -temp_output_453_22) * (1.0 - 0.0) / (temp_output_453_22 - -temp_output_453_22));
			float4 temp_output_3_0_g108 = Swells6;
			float2 temp_output_13_0_g108 = (temp_output_3_0_g108).xy;
			float2 break28_g108 = temp_output_13_0_g108;
			float3 temp_output_13_0_g102 = ase_worldPos;
			float dotResult21_g108 = dot( temp_output_13_0_g108 , (temp_output_13_0_g102).xz );
			float4 break7_g108 = temp_output_3_0_g108;
			float temp_output_10_0_g108 = ( ( 2.0 * UNITY_PI ) / break7_g108.w );
			float temp_output_10_0_g102 = timeOffset;
			float temp_output_25_0_g108 = ( ( dotResult21_g108 - ( sqrt( ( 9.8 / temp_output_10_0_g108 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g108 );
			float temp_output_36_0_g108 = cos( temp_output_25_0_g108 );
			float temp_output_26_0_g108 = ( break7_g108.z / temp_output_10_0_g108 );
			float temp_output_34_0_g108 = sin( temp_output_25_0_g108 );
			float3 appendResult48_g108 = (float3(( break28_g108.x * temp_output_36_0_g108 * temp_output_26_0_g108 ) , ( temp_output_26_0_g108 * temp_output_34_0_g108 ) , ( break28_g108.y * temp_output_26_0_g108 * temp_output_36_0_g108 )));
			float4 temp_output_3_0_g104 = Swells5;
			float2 temp_output_13_0_g104 = (temp_output_3_0_g104).xy;
			float2 break28_g104 = temp_output_13_0_g104;
			float dotResult21_g104 = dot( temp_output_13_0_g104 , (temp_output_13_0_g102).xz );
			float4 break7_g104 = temp_output_3_0_g104;
			float temp_output_10_0_g104 = ( ( 2.0 * UNITY_PI ) / break7_g104.w );
			float temp_output_25_0_g104 = ( ( dotResult21_g104 - ( sqrt( ( 9.8 / temp_output_10_0_g104 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g104 );
			float temp_output_36_0_g104 = cos( temp_output_25_0_g104 );
			float temp_output_26_0_g104 = ( break7_g104.z / temp_output_10_0_g104 );
			float temp_output_34_0_g104 = sin( temp_output_25_0_g104 );
			float3 appendResult48_g104 = (float3(( break28_g104.x * temp_output_36_0_g104 * temp_output_26_0_g104 ) , ( temp_output_26_0_g104 * temp_output_34_0_g104 ) , ( break28_g104.y * temp_output_26_0_g104 * temp_output_36_0_g104 )));
			float4 temp_output_3_0_g105 = Swells4;
			float2 temp_output_13_0_g105 = (temp_output_3_0_g105).xy;
			float2 break28_g105 = temp_output_13_0_g105;
			float dotResult21_g105 = dot( temp_output_13_0_g105 , (temp_output_13_0_g102).xz );
			float4 break7_g105 = temp_output_3_0_g105;
			float temp_output_10_0_g105 = ( ( 2.0 * UNITY_PI ) / break7_g105.w );
			float temp_output_25_0_g105 = ( ( dotResult21_g105 - ( sqrt( ( 9.8 / temp_output_10_0_g105 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g105 );
			float temp_output_36_0_g105 = cos( temp_output_25_0_g105 );
			float temp_output_26_0_g105 = ( break7_g105.z / temp_output_10_0_g105 );
			float temp_output_34_0_g105 = sin( temp_output_25_0_g105 );
			float3 appendResult48_g105 = (float3(( break28_g105.x * temp_output_36_0_g105 * temp_output_26_0_g105 ) , ( temp_output_26_0_g105 * temp_output_34_0_g105 ) , ( break28_g105.y * temp_output_26_0_g105 * temp_output_36_0_g105 )));
			float4 temp_output_3_0_g106 = Swells3;
			float2 temp_output_13_0_g106 = (temp_output_3_0_g106).xy;
			float2 break28_g106 = temp_output_13_0_g106;
			float dotResult21_g106 = dot( temp_output_13_0_g106 , (temp_output_13_0_g102).xz );
			float4 break7_g106 = temp_output_3_0_g106;
			float temp_output_10_0_g106 = ( ( 2.0 * UNITY_PI ) / break7_g106.w );
			float temp_output_25_0_g106 = ( ( dotResult21_g106 - ( sqrt( ( 9.8 / temp_output_10_0_g106 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g106 );
			float temp_output_36_0_g106 = cos( temp_output_25_0_g106 );
			float temp_output_26_0_g106 = ( break7_g106.z / temp_output_10_0_g106 );
			float temp_output_34_0_g106 = sin( temp_output_25_0_g106 );
			float3 appendResult48_g106 = (float3(( break28_g106.x * temp_output_36_0_g106 * temp_output_26_0_g106 ) , ( temp_output_26_0_g106 * temp_output_34_0_g106 ) , ( break28_g106.y * temp_output_26_0_g106 * temp_output_36_0_g106 )));
			float4 temp_output_3_0_g107 = Swells2;
			float2 temp_output_13_0_g107 = (temp_output_3_0_g107).xy;
			float2 break28_g107 = temp_output_13_0_g107;
			float dotResult21_g107 = dot( temp_output_13_0_g107 , (temp_output_13_0_g102).xz );
			float4 break7_g107 = temp_output_3_0_g107;
			float temp_output_10_0_g107 = ( ( 2.0 * UNITY_PI ) / break7_g107.w );
			float temp_output_25_0_g107 = ( ( dotResult21_g107 - ( sqrt( ( 9.8 / temp_output_10_0_g107 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g107 );
			float temp_output_36_0_g107 = cos( temp_output_25_0_g107 );
			float temp_output_26_0_g107 = ( break7_g107.z / temp_output_10_0_g107 );
			float temp_output_34_0_g107 = sin( temp_output_25_0_g107 );
			float3 appendResult48_g107 = (float3(( break28_g107.x * temp_output_36_0_g107 * temp_output_26_0_g107 ) , ( temp_output_26_0_g107 * temp_output_34_0_g107 ) , ( break28_g107.y * temp_output_26_0_g107 * temp_output_36_0_g107 )));
			float4 temp_output_3_0_g103 = Swells1;
			float2 temp_output_13_0_g103 = (temp_output_3_0_g103).xy;
			float2 break28_g103 = temp_output_13_0_g103;
			float dotResult21_g103 = dot( temp_output_13_0_g103 , (temp_output_13_0_g102).xz );
			float4 break7_g103 = temp_output_3_0_g103;
			float temp_output_10_0_g103 = ( ( 2.0 * UNITY_PI ) / break7_g103.w );
			float temp_output_25_0_g103 = ( ( dotResult21_g103 - ( sqrt( ( 9.8 / temp_output_10_0_g103 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g103 );
			float temp_output_36_0_g103 = cos( temp_output_25_0_g103 );
			float temp_output_26_0_g103 = ( break7_g103.z / temp_output_10_0_g103 );
			float temp_output_34_0_g103 = sin( temp_output_25_0_g103 );
			float3 appendResult48_g103 = (float3(( break28_g103.x * temp_output_36_0_g103 * temp_output_26_0_g103 ) , ( temp_output_26_0_g103 * temp_output_34_0_g103 ) , ( break28_g103.y * temp_output_26_0_g103 * temp_output_36_0_g103 )));
			float3 temp_output_452_3 = ( appendResult48_g108 + appendResult48_g104 + appendResult48_g105 + appendResult48_g106 + appendResult48_g107 + appendResult48_g103 );
			float temp_output_452_22 = ( temp_output_26_0_g108 + temp_output_26_0_g104 + temp_output_26_0_g105 + temp_output_26_0_g106 + temp_output_26_0_g107 + temp_output_26_0_g103 );
			float temp_output_408_0 = (0.0 + ((temp_output_452_3).y - -temp_output_452_22) * (1.0 - 0.0) / (temp_output_452_22 - -temp_output_452_22));
			float temp_output_512_0 = ( ( pow( ( 1.0 - temp_output_406_0 ) , _LargeSwellsSprayShape ) * _LargeSwellsSprayStrength ) + ( pow( temp_output_411_0 , _SmallWavesSprayShape ) * _SmallWavesSprayStrength ) + ( pow( ( 1.0 - temp_output_408_0 ) , _SwellsSprayShape ) * _SwellsSprayStrength ) + 1.0 );
			float cos194 = cos( ( _DepthTextureRotation * UNITY_PI ) );
			float sin194 = sin( ( _DepthTextureRotation * UNITY_PI ) );
			float2 rotator194 = mul( v.texcoord.xy - float2( 0.5,0.5 ) , float2x2( cos194 , -sin194 , sin194 , cos194 )) + float2( 0.5,0.5 );
			float clampResult446 = clamp( (DepthMapping.z + (tex2Dlod( _Depth, float4( rotator194, 0, 0.0) ).r - DepthMapping.x) * (DepthMapping.w - DepthMapping.z) / (DepthMapping.y - DepthMapping.x)) , 0.0 , 1.0 );
			float temp_output_188_0 = pow( ( 1.0 - clampResult446 ) , _DepthScale );
			float temp_output_157_0 = distance( ase_worldPos , _WorldSpaceCameraPos );
			float3 temp_output_170_0 = ( temp_output_451_3 + temp_output_452_3 );
			float3 temp_output_11_0 = ( temp_output_170_0 + temp_output_453_3 );
			float4 temp_output_3_0_g122 = Ripples6;
			float2 temp_output_13_0_g122 = (temp_output_3_0_g122).xy;
			float2 break28_g122 = temp_output_13_0_g122;
			float3 temp_output_13_0_g116 = ase_worldPos;
			float dotResult21_g122 = dot( temp_output_13_0_g122 , (temp_output_13_0_g116).xz );
			float4 break7_g122 = temp_output_3_0_g122;
			float temp_output_10_0_g122 = ( ( 2.0 * UNITY_PI ) / break7_g122.w );
			float temp_output_10_0_g116 = timeOffset;
			float temp_output_25_0_g122 = ( ( dotResult21_g122 - ( sqrt( ( 9.8 / temp_output_10_0_g122 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g122 );
			float temp_output_36_0_g122 = cos( temp_output_25_0_g122 );
			float temp_output_26_0_g122 = ( break7_g122.z / temp_output_10_0_g122 );
			float temp_output_34_0_g122 = sin( temp_output_25_0_g122 );
			float3 appendResult48_g122 = (float3(( break28_g122.x * temp_output_36_0_g122 * temp_output_26_0_g122 ) , ( temp_output_26_0_g122 * temp_output_34_0_g122 ) , ( break28_g122.y * temp_output_26_0_g122 * temp_output_36_0_g122 )));
			float4 temp_output_3_0_g118 = Ripples5;
			float2 temp_output_13_0_g118 = (temp_output_3_0_g118).xy;
			float2 break28_g118 = temp_output_13_0_g118;
			float dotResult21_g118 = dot( temp_output_13_0_g118 , (temp_output_13_0_g116).xz );
			float4 break7_g118 = temp_output_3_0_g118;
			float temp_output_10_0_g118 = ( ( 2.0 * UNITY_PI ) / break7_g118.w );
			float temp_output_25_0_g118 = ( ( dotResult21_g118 - ( sqrt( ( 9.8 / temp_output_10_0_g118 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g118 );
			float temp_output_36_0_g118 = cos( temp_output_25_0_g118 );
			float temp_output_26_0_g118 = ( break7_g118.z / temp_output_10_0_g118 );
			float temp_output_34_0_g118 = sin( temp_output_25_0_g118 );
			float3 appendResult48_g118 = (float3(( break28_g118.x * temp_output_36_0_g118 * temp_output_26_0_g118 ) , ( temp_output_26_0_g118 * temp_output_34_0_g118 ) , ( break28_g118.y * temp_output_26_0_g118 * temp_output_36_0_g118 )));
			float4 temp_output_3_0_g119 = Ripples4;
			float2 temp_output_13_0_g119 = (temp_output_3_0_g119).xy;
			float2 break28_g119 = temp_output_13_0_g119;
			float dotResult21_g119 = dot( temp_output_13_0_g119 , (temp_output_13_0_g116).xz );
			float4 break7_g119 = temp_output_3_0_g119;
			float temp_output_10_0_g119 = ( ( 2.0 * UNITY_PI ) / break7_g119.w );
			float temp_output_25_0_g119 = ( ( dotResult21_g119 - ( sqrt( ( 9.8 / temp_output_10_0_g119 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g119 );
			float temp_output_36_0_g119 = cos( temp_output_25_0_g119 );
			float temp_output_26_0_g119 = ( break7_g119.z / temp_output_10_0_g119 );
			float temp_output_34_0_g119 = sin( temp_output_25_0_g119 );
			float3 appendResult48_g119 = (float3(( break28_g119.x * temp_output_36_0_g119 * temp_output_26_0_g119 ) , ( temp_output_26_0_g119 * temp_output_34_0_g119 ) , ( break28_g119.y * temp_output_26_0_g119 * temp_output_36_0_g119 )));
			float4 temp_output_3_0_g120 = Ripples3;
			float2 temp_output_13_0_g120 = (temp_output_3_0_g120).xy;
			float2 break28_g120 = temp_output_13_0_g120;
			float dotResult21_g120 = dot( temp_output_13_0_g120 , (temp_output_13_0_g116).xz );
			float4 break7_g120 = temp_output_3_0_g120;
			float temp_output_10_0_g120 = ( ( 2.0 * UNITY_PI ) / break7_g120.w );
			float temp_output_25_0_g120 = ( ( dotResult21_g120 - ( sqrt( ( 9.8 / temp_output_10_0_g120 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g120 );
			float temp_output_36_0_g120 = cos( temp_output_25_0_g120 );
			float temp_output_26_0_g120 = ( break7_g120.z / temp_output_10_0_g120 );
			float temp_output_34_0_g120 = sin( temp_output_25_0_g120 );
			float3 appendResult48_g120 = (float3(( break28_g120.x * temp_output_36_0_g120 * temp_output_26_0_g120 ) , ( temp_output_26_0_g120 * temp_output_34_0_g120 ) , ( break28_g120.y * temp_output_26_0_g120 * temp_output_36_0_g120 )));
			float4 temp_output_3_0_g121 = Ripples2;
			float2 temp_output_13_0_g121 = (temp_output_3_0_g121).xy;
			float2 break28_g121 = temp_output_13_0_g121;
			float dotResult21_g121 = dot( temp_output_13_0_g121 , (temp_output_13_0_g116).xz );
			float4 break7_g121 = temp_output_3_0_g121;
			float temp_output_10_0_g121 = ( ( 2.0 * UNITY_PI ) / break7_g121.w );
			float temp_output_25_0_g121 = ( ( dotResult21_g121 - ( sqrt( ( 9.8 / temp_output_10_0_g121 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g121 );
			float temp_output_36_0_g121 = cos( temp_output_25_0_g121 );
			float temp_output_26_0_g121 = ( break7_g121.z / temp_output_10_0_g121 );
			float temp_output_34_0_g121 = sin( temp_output_25_0_g121 );
			float3 appendResult48_g121 = (float3(( break28_g121.x * temp_output_36_0_g121 * temp_output_26_0_g121 ) , ( temp_output_26_0_g121 * temp_output_34_0_g121 ) , ( break28_g121.y * temp_output_26_0_g121 * temp_output_36_0_g121 )));
			float4 temp_output_3_0_g117 = Ripples1;
			float2 temp_output_13_0_g117 = (temp_output_3_0_g117).xy;
			float2 break28_g117 = temp_output_13_0_g117;
			float dotResult21_g117 = dot( temp_output_13_0_g117 , (temp_output_13_0_g116).xz );
			float4 break7_g117 = temp_output_3_0_g117;
			float temp_output_10_0_g117 = ( ( 2.0 * UNITY_PI ) / break7_g117.w );
			float temp_output_25_0_g117 = ( ( dotResult21_g117 - ( sqrt( ( 9.8 / temp_output_10_0_g117 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g117 );
			float temp_output_36_0_g117 = cos( temp_output_25_0_g117 );
			float temp_output_26_0_g117 = ( break7_g117.z / temp_output_10_0_g117 );
			float temp_output_34_0_g117 = sin( temp_output_25_0_g117 );
			float3 appendResult48_g117 = (float3(( break28_g117.x * temp_output_36_0_g117 * temp_output_26_0_g117 ) , ( temp_output_26_0_g117 * temp_output_34_0_g117 ) , ( break28_g117.y * temp_output_26_0_g117 * temp_output_36_0_g117 )));
			float3 temp_output_454_3 = ( appendResult48_g122 + appendResult48_g118 + appendResult48_g119 + appendResult48_g120 + appendResult48_g121 + appendResult48_g117 );
			float3 temp_output_191_0 = ( temp_output_188_0 * (( temp_output_157_0 < _DistanceCutoff ) ? (( temp_output_157_0 < _DetailCutoff ) ? (( temp_output_157_0 < _FineDetailCutoff ) ? ( temp_output_11_0 + temp_output_454_3 ) :  temp_output_11_0 ) :  temp_output_170_0 ) :  temp_output_451_3 ) );
			float2 temp_output_202_0 = (temp_output_191_0).xz;
			float cos458 = cos( ( _WakeTextureRotation * UNITY_PI ) );
			float sin458 = sin( ( _WakeTextureRotation * UNITY_PI ) );
			float2 rotator458 = mul( v.texcoord.xy - float2( 0.5,0.5 ) , float2x2( cos458 , -sin458 , sin458 , cos458 )) + float2( 0.5,0.5 );
			float2 appendResult481 = (float2(_WakeUVMapping.x , _WakeUVMapping.y));
			float2 appendResult482 = (float2(_WakeUVMapping.z , _WakeUVMapping.w));
			float4 tex2DNode459 = tex2Dlod( _WakeTexture, float4( ( ( temp_output_202_0 * _WakeUVScaling ) + (float2( 0,0 ) + (rotator458 - appendResult481) * (float2( 1,1 ) - float2( 0,0 )) / (appendResult482 - appendResult481)) ), 0, 0.0) );
			float clampResult462 = clamp( (_SprayOffsetMapping.z + (tex2DNode459.r - _SprayOffsetMapping.x) * (_SprayOffsetMapping.w - _SprayOffsetMapping.z) / (_SprayOffsetMapping.y - _SprayOffsetMapping.x)) , 0.0 , 1.0 );
			float3 appendResult466 = (float3(0.0 , ( temp_output_512_0 * ( pow( clampResult462 , _SprayOffsetShape ) * _SprayOffsetStrength ) ) , 0.0));
			v.vertex.xyz += ( appendResult466 + temp_output_191_0 );
			float3 appendResult43_g96 = (float3(( -break28_g96.x * break28_g96.y * break7_g96.z * temp_output_34_0_g96 ) , ( break28_g96.y * break7_g96.z * temp_output_36_0_g96 ) , ( -break28_g96.y * break28_g96.y * break7_g96.z * temp_output_34_0_g96 )));
			float3 appendResult43_g100 = (float3(( -break28_g100.x * break28_g100.y * break7_g100.z * temp_output_34_0_g100 ) , ( break28_g100.y * break7_g100.z * temp_output_36_0_g100 ) , ( -break28_g100.y * break28_g100.y * break7_g100.z * temp_output_34_0_g100 )));
			float3 appendResult43_g99 = (float3(( -break28_g99.x * break28_g99.y * break7_g99.z * temp_output_34_0_g99 ) , ( break28_g99.y * break7_g99.z * temp_output_36_0_g99 ) , ( -break28_g99.y * break28_g99.y * break7_g99.z * temp_output_34_0_g99 )));
			float3 appendResult43_g98 = (float3(( -break28_g98.x * break28_g98.y * break7_g98.z * temp_output_34_0_g98 ) , ( break28_g98.y * break7_g98.z * temp_output_36_0_g98 ) , ( -break28_g98.y * break28_g98.y * break7_g98.z * temp_output_34_0_g98 )));
			float3 appendResult43_g97 = (float3(( -break28_g97.x * break28_g97.y * break7_g97.z * temp_output_34_0_g97 ) , ( break28_g97.y * break7_g97.z * temp_output_36_0_g97 ) , ( -break28_g97.y * break28_g97.y * break7_g97.z * temp_output_34_0_g97 )));
			float3 appendResult43_g101 = (float3(( -break28_g101.x * break28_g101.y * break7_g101.z * temp_output_34_0_g101 ) , ( break28_g101.y * break7_g101.z * temp_output_36_0_g101 ) , ( -break28_g101.y * break28_g101.y * break7_g101.z * temp_output_34_0_g101 )));
			float3 temp_output_451_2 = ( ( ( ( ( ( float3(0,0,1) + appendResult43_g96 ) + appendResult43_g100 ) + appendResult43_g99 ) + appendResult43_g98 ) + appendResult43_g97 ) + appendResult43_g101 );
			float3 appendResult43_g103 = (float3(( -break28_g103.x * break28_g103.y * break7_g103.z * temp_output_34_0_g103 ) , ( break28_g103.y * break7_g103.z * temp_output_36_0_g103 ) , ( -break28_g103.y * break28_g103.y * break7_g103.z * temp_output_34_0_g103 )));
			float3 appendResult43_g107 = (float3(( -break28_g107.x * break28_g107.y * break7_g107.z * temp_output_34_0_g107 ) , ( break28_g107.y * break7_g107.z * temp_output_36_0_g107 ) , ( -break28_g107.y * break28_g107.y * break7_g107.z * temp_output_34_0_g107 )));
			float3 appendResult43_g106 = (float3(( -break28_g106.x * break28_g106.y * break7_g106.z * temp_output_34_0_g106 ) , ( break28_g106.y * break7_g106.z * temp_output_36_0_g106 ) , ( -break28_g106.y * break28_g106.y * break7_g106.z * temp_output_34_0_g106 )));
			float3 appendResult43_g105 = (float3(( -break28_g105.x * break28_g105.y * break7_g105.z * temp_output_34_0_g105 ) , ( break28_g105.y * break7_g105.z * temp_output_36_0_g105 ) , ( -break28_g105.y * break28_g105.y * break7_g105.z * temp_output_34_0_g105 )));
			float3 appendResult43_g104 = (float3(( -break28_g104.x * break28_g104.y * break7_g104.z * temp_output_34_0_g104 ) , ( break28_g104.y * break7_g104.z * temp_output_36_0_g104 ) , ( -break28_g104.y * break28_g104.y * break7_g104.z * temp_output_34_0_g104 )));
			float3 appendResult43_g108 = (float3(( -break28_g108.x * break28_g108.y * break7_g108.z * temp_output_34_0_g108 ) , ( break28_g108.y * break7_g108.z * temp_output_36_0_g108 ) , ( -break28_g108.y * break28_g108.y * break7_g108.z * temp_output_34_0_g108 )));
			float3 temp_output_452_2 = ( ( ( ( ( ( temp_output_451_2 + appendResult43_g103 ) + appendResult43_g107 ) + appendResult43_g106 ) + appendResult43_g105 ) + appendResult43_g104 ) + appendResult43_g108 );
			float3 appendResult43_g110 = (float3(( -break28_g110.x * break28_g110.y * break7_g110.z * temp_output_34_0_g110 ) , ( break28_g110.y * break7_g110.z * temp_output_36_0_g110 ) , ( -break28_g110.y * break28_g110.y * break7_g110.z * temp_output_34_0_g110 )));
			float3 appendResult43_g114 = (float3(( -break28_g114.x * break28_g114.y * break7_g114.z * temp_output_34_0_g114 ) , ( break28_g114.y * break7_g114.z * temp_output_36_0_g114 ) , ( -break28_g114.y * break28_g114.y * break7_g114.z * temp_output_34_0_g114 )));
			float3 appendResult43_g113 = (float3(( -break28_g113.x * break28_g113.y * break7_g113.z * temp_output_34_0_g113 ) , ( break28_g113.y * break7_g113.z * temp_output_36_0_g113 ) , ( -break28_g113.y * break28_g113.y * break7_g113.z * temp_output_34_0_g113 )));
			float3 appendResult43_g112 = (float3(( -break28_g112.x * break28_g112.y * break7_g112.z * temp_output_34_0_g112 ) , ( break28_g112.y * break7_g112.z * temp_output_36_0_g112 ) , ( -break28_g112.y * break28_g112.y * break7_g112.z * temp_output_34_0_g112 )));
			float3 appendResult43_g111 = (float3(( -break28_g111.x * break28_g111.y * break7_g111.z * temp_output_34_0_g111 ) , ( break28_g111.y * break7_g111.z * temp_output_36_0_g111 ) , ( -break28_g111.y * break28_g111.y * break7_g111.z * temp_output_34_0_g111 )));
			float3 appendResult43_g115 = (float3(( -break28_g115.x * break28_g115.y * break7_g115.z * temp_output_34_0_g115 ) , ( break28_g115.y * break7_g115.z * temp_output_36_0_g115 ) , ( -break28_g115.y * break28_g115.y * break7_g115.z * temp_output_34_0_g115 )));
			float3 temp_output_453_2 = ( ( ( ( ( ( temp_output_452_2 + appendResult43_g110 ) + appendResult43_g114 ) + appendResult43_g113 ) + appendResult43_g112 ) + appendResult43_g111 ) + appendResult43_g115 );
			float3 appendResult43_g117 = (float3(( -break28_g117.x * break28_g117.y * break7_g117.z * temp_output_34_0_g117 ) , ( break28_g117.y * break7_g117.z * temp_output_36_0_g117 ) , ( -break28_g117.y * break28_g117.y * break7_g117.z * temp_output_34_0_g117 )));
			float3 appendResult43_g121 = (float3(( -break28_g121.x * break28_g121.y * break7_g121.z * temp_output_34_0_g121 ) , ( break28_g121.y * break7_g121.z * temp_output_36_0_g121 ) , ( -break28_g121.y * break28_g121.y * break7_g121.z * temp_output_34_0_g121 )));
			float3 appendResult43_g120 = (float3(( -break28_g120.x * break28_g120.y * break7_g120.z * temp_output_34_0_g120 ) , ( break28_g120.y * break7_g120.z * temp_output_36_0_g120 ) , ( -break28_g120.y * break28_g120.y * break7_g120.z * temp_output_34_0_g120 )));
			float3 appendResult43_g119 = (float3(( -break28_g119.x * break28_g119.y * break7_g119.z * temp_output_34_0_g119 ) , ( break28_g119.y * break7_g119.z * temp_output_36_0_g119 ) , ( -break28_g119.y * break28_g119.y * break7_g119.z * temp_output_34_0_g119 )));
			float3 appendResult43_g118 = (float3(( -break28_g118.x * break28_g118.y * break7_g118.z * temp_output_34_0_g118 ) , ( break28_g118.y * break7_g118.z * temp_output_36_0_g118 ) , ( -break28_g118.y * break28_g118.y * break7_g118.z * temp_output_34_0_g118 )));
			float3 appendResult43_g122 = (float3(( -break28_g122.x * break28_g122.y * break7_g122.z * temp_output_34_0_g122 ) , ( break28_g122.y * break7_g122.z * temp_output_36_0_g122 ) , ( -break28_g122.y * break28_g122.y * break7_g122.z * temp_output_34_0_g122 )));
			float3 appendResult38_g96 = (float3(( -break28_g96.x * break28_g96.x * temp_output_34_0_g96 * break7_g96.z ) , ( break7_g96.z * temp_output_36_0_g96 * break28_g96.x ) , ( -break28_g96.x * break28_g96.y * break7_g96.z * temp_output_34_0_g96 )));
			float3 appendResult38_g100 = (float3(( -break28_g100.x * break28_g100.x * temp_output_34_0_g100 * break7_g100.z ) , ( break7_g100.z * temp_output_36_0_g100 * break28_g100.x ) , ( -break28_g100.x * break28_g100.y * break7_g100.z * temp_output_34_0_g100 )));
			float3 appendResult38_g99 = (float3(( -break28_g99.x * break28_g99.x * temp_output_34_0_g99 * break7_g99.z ) , ( break7_g99.z * temp_output_36_0_g99 * break28_g99.x ) , ( -break28_g99.x * break28_g99.y * break7_g99.z * temp_output_34_0_g99 )));
			float3 appendResult38_g98 = (float3(( -break28_g98.x * break28_g98.x * temp_output_34_0_g98 * break7_g98.z ) , ( break7_g98.z * temp_output_36_0_g98 * break28_g98.x ) , ( -break28_g98.x * break28_g98.y * break7_g98.z * temp_output_34_0_g98 )));
			float3 appendResult38_g97 = (float3(( -break28_g97.x * break28_g97.x * temp_output_34_0_g97 * break7_g97.z ) , ( break7_g97.z * temp_output_36_0_g97 * break28_g97.x ) , ( -break28_g97.x * break28_g97.y * break7_g97.z * temp_output_34_0_g97 )));
			float3 appendResult38_g101 = (float3(( -break28_g101.x * break28_g101.x * temp_output_34_0_g101 * break7_g101.z ) , ( break7_g101.z * temp_output_36_0_g101 * break28_g101.x ) , ( -break28_g101.x * break28_g101.y * break7_g101.z * temp_output_34_0_g101 )));
			float3 temp_output_451_0 = ( ( ( ( ( ( float3(1,0,0) + appendResult38_g96 ) + appendResult38_g100 ) + appendResult38_g99 ) + appendResult38_g98 ) + appendResult38_g97 ) + appendResult38_g101 );
			float3 appendResult38_g103 = (float3(( -break28_g103.x * break28_g103.x * temp_output_34_0_g103 * break7_g103.z ) , ( break7_g103.z * temp_output_36_0_g103 * break28_g103.x ) , ( -break28_g103.x * break28_g103.y * break7_g103.z * temp_output_34_0_g103 )));
			float3 appendResult38_g107 = (float3(( -break28_g107.x * break28_g107.x * temp_output_34_0_g107 * break7_g107.z ) , ( break7_g107.z * temp_output_36_0_g107 * break28_g107.x ) , ( -break28_g107.x * break28_g107.y * break7_g107.z * temp_output_34_0_g107 )));
			float3 appendResult38_g106 = (float3(( -break28_g106.x * break28_g106.x * temp_output_34_0_g106 * break7_g106.z ) , ( break7_g106.z * temp_output_36_0_g106 * break28_g106.x ) , ( -break28_g106.x * break28_g106.y * break7_g106.z * temp_output_34_0_g106 )));
			float3 appendResult38_g105 = (float3(( -break28_g105.x * break28_g105.x * temp_output_34_0_g105 * break7_g105.z ) , ( break7_g105.z * temp_output_36_0_g105 * break28_g105.x ) , ( -break28_g105.x * break28_g105.y * break7_g105.z * temp_output_34_0_g105 )));
			float3 appendResult38_g104 = (float3(( -break28_g104.x * break28_g104.x * temp_output_34_0_g104 * break7_g104.z ) , ( break7_g104.z * temp_output_36_0_g104 * break28_g104.x ) , ( -break28_g104.x * break28_g104.y * break7_g104.z * temp_output_34_0_g104 )));
			float3 appendResult38_g108 = (float3(( -break28_g108.x * break28_g108.x * temp_output_34_0_g108 * break7_g108.z ) , ( break7_g108.z * temp_output_36_0_g108 * break28_g108.x ) , ( -break28_g108.x * break28_g108.y * break7_g108.z * temp_output_34_0_g108 )));
			float3 temp_output_452_0 = ( ( ( ( ( ( temp_output_451_0 + appendResult38_g103 ) + appendResult38_g107 ) + appendResult38_g106 ) + appendResult38_g105 ) + appendResult38_g104 ) + appendResult38_g108 );
			float3 appendResult38_g110 = (float3(( -break28_g110.x * break28_g110.x * temp_output_34_0_g110 * break7_g110.z ) , ( break7_g110.z * temp_output_36_0_g110 * break28_g110.x ) , ( -break28_g110.x * break28_g110.y * break7_g110.z * temp_output_34_0_g110 )));
			float3 appendResult38_g114 = (float3(( -break28_g114.x * break28_g114.x * temp_output_34_0_g114 * break7_g114.z ) , ( break7_g114.z * temp_output_36_0_g114 * break28_g114.x ) , ( -break28_g114.x * break28_g114.y * break7_g114.z * temp_output_34_0_g114 )));
			float3 appendResult38_g113 = (float3(( -break28_g113.x * break28_g113.x * temp_output_34_0_g113 * break7_g113.z ) , ( break7_g113.z * temp_output_36_0_g113 * break28_g113.x ) , ( -break28_g113.x * break28_g113.y * break7_g113.z * temp_output_34_0_g113 )));
			float3 appendResult38_g112 = (float3(( -break28_g112.x * break28_g112.x * temp_output_34_0_g112 * break7_g112.z ) , ( break7_g112.z * temp_output_36_0_g112 * break28_g112.x ) , ( -break28_g112.x * break28_g112.y * break7_g112.z * temp_output_34_0_g112 )));
			float3 appendResult38_g111 = (float3(( -break28_g111.x * break28_g111.x * temp_output_34_0_g111 * break7_g111.z ) , ( break7_g111.z * temp_output_36_0_g111 * break28_g111.x ) , ( -break28_g111.x * break28_g111.y * break7_g111.z * temp_output_34_0_g111 )));
			float3 appendResult38_g115 = (float3(( -break28_g115.x * break28_g115.x * temp_output_34_0_g115 * break7_g115.z ) , ( break7_g115.z * temp_output_36_0_g115 * break28_g115.x ) , ( -break28_g115.x * break28_g115.y * break7_g115.z * temp_output_34_0_g115 )));
			float3 temp_output_453_0 = ( ( ( ( ( ( temp_output_452_0 + appendResult38_g110 ) + appendResult38_g114 ) + appendResult38_g113 ) + appendResult38_g112 ) + appendResult38_g111 ) + appendResult38_g115 );
			float3 appendResult38_g117 = (float3(( -break28_g117.x * break28_g117.x * temp_output_34_0_g117 * break7_g117.z ) , ( break7_g117.z * temp_output_36_0_g117 * break28_g117.x ) , ( -break28_g117.x * break28_g117.y * break7_g117.z * temp_output_34_0_g117 )));
			float3 appendResult38_g121 = (float3(( -break28_g121.x * break28_g121.x * temp_output_34_0_g121 * break7_g121.z ) , ( break7_g121.z * temp_output_36_0_g121 * break28_g121.x ) , ( -break28_g121.x * break28_g121.y * break7_g121.z * temp_output_34_0_g121 )));
			float3 appendResult38_g120 = (float3(( -break28_g120.x * break28_g120.x * temp_output_34_0_g120 * break7_g120.z ) , ( break7_g120.z * temp_output_36_0_g120 * break28_g120.x ) , ( -break28_g120.x * break28_g120.y * break7_g120.z * temp_output_34_0_g120 )));
			float3 appendResult38_g119 = (float3(( -break28_g119.x * break28_g119.x * temp_output_34_0_g119 * break7_g119.z ) , ( break7_g119.z * temp_output_36_0_g119 * break28_g119.x ) , ( -break28_g119.x * break28_g119.y * break7_g119.z * temp_output_34_0_g119 )));
			float3 appendResult38_g118 = (float3(( -break28_g118.x * break28_g118.x * temp_output_34_0_g118 * break7_g118.z ) , ( break7_g118.z * temp_output_36_0_g118 * break28_g118.x ) , ( -break28_g118.x * break28_g118.y * break7_g118.z * temp_output_34_0_g118 )));
			float3 appendResult38_g122 = (float3(( -break28_g122.x * break28_g122.x * temp_output_34_0_g122 * break7_g122.z ) , ( break7_g122.z * temp_output_36_0_g122 * break28_g122.x ) , ( -break28_g122.x * break28_g122.y * break7_g122.z * temp_output_34_0_g122 )));
			float3 normalizeResult333 = normalize( cross( ( ( ( ( ( ( temp_output_453_2 + appendResult43_g117 ) + appendResult43_g121 ) + appendResult43_g120 ) + appendResult43_g119 ) + appendResult43_g118 ) + appendResult43_g122 ) , ( ( ( ( ( ( temp_output_453_0 + appendResult38_g117 ) + appendResult38_g121 ) + appendResult38_g120 ) + appendResult38_g119 ) + appendResult38_g118 ) + appendResult38_g122 ) ) );
			float3 normalizeResult13 = normalize( cross( temp_output_453_2 , temp_output_453_0 ) );
			float3 normalizeResult169 = normalize( cross( temp_output_452_2 , temp_output_452_0 ) );
			float3 normalizeResult175 = normalize( cross( temp_output_451_2 , temp_output_451_0 ) );
			float3 normalizeResult469 = normalize( ( appendResult466 + ( temp_output_188_0 * (( temp_output_157_0 < _DistanceCutoff ) ? (( temp_output_157_0 < _DetailCutoff ) ? (( temp_output_157_0 < _FineDetailCutoff ) ? normalizeResult333 :  normalizeResult13 ) :  normalizeResult169 ) :  normalizeResult175 ) ) ) );
			v.normal = normalizeResult469;
		}

		inline half4 LightingStandardSpecularCustom(SurfaceOutputStandardSpecularCustom s, half3 viewDir, UnityGI gi )
		{
			#if !DIRECTIONAL
			float3 lightAtten = gi.light.color;
			#else
			float3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, _TransShadow );
			#endif
			half3 lightDir = gi.light.dir + s.Normal * _TransNormalDistortion;
			half transVdotL = pow( saturate( dot( viewDir, -lightDir ) ), _TransScattering );
			half3 translucency = lightAtten * (transVdotL * _TransDirect + gi.indirect.diffuse * _TransAmbient) * s.Translucency;
			half4 c = half4( s.Albedo * translucency * _Translucency, 0 );

			SurfaceOutputStandardSpecular r;
			r.Albedo = s.Albedo;
			r.Normal = s.Normal;
			r.Emission = s.Emission;
			r.Specular = s.Specular;
			r.Smoothness = s.Smoothness;
			r.Occlusion = s.Occlusion;
			r.Alpha = s.Alpha;
			return LightingStandardSpecular (r, viewDir, gi) + c;
		}

		inline void LightingStandardSpecularCustom_GI(SurfaceOutputStandardSpecularCustom s, UnityGIInput data, inout UnityGI gi )
		{
			#if defined(UNITY_PASS_DEFERRED) && UNITY_ENABLE_REFLECTION_BUFFERS
				gi = UnityGlobalIllumination(data, s.Occlusion, s.Normal);
			#else
				UNITY_GLOSSY_ENV_FROM_SURFACE( g, s, data );
				gi = UnityGlobalIllumination( data, s.Occlusion, s.Normal, g );
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandardSpecularCustom o )
		{
			float4 temp_output_3_0_g101 = LargeSwells6;
			float2 temp_output_13_0_g101 = (temp_output_3_0_g101).xy;
			float2 break28_g101 = temp_output_13_0_g101;
			float3 ase_worldPos = i.worldPos;
			float3 temp_output_13_0_g95 = ase_worldPos;
			float dotResult21_g101 = dot( temp_output_13_0_g101 , (temp_output_13_0_g95).xz );
			float4 break7_g101 = temp_output_3_0_g101;
			float temp_output_10_0_g101 = ( ( 2.0 * UNITY_PI ) / break7_g101.w );
			float temp_output_10_0_g95 = timeOffset;
			float temp_output_25_0_g101 = ( ( dotResult21_g101 - ( sqrt( ( 9.8 / temp_output_10_0_g101 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g101 );
			float temp_output_36_0_g101 = cos( temp_output_25_0_g101 );
			float temp_output_26_0_g101 = ( break7_g101.z / temp_output_10_0_g101 );
			float temp_output_34_0_g101 = sin( temp_output_25_0_g101 );
			float3 appendResult48_g101 = (float3(( break28_g101.x * temp_output_36_0_g101 * temp_output_26_0_g101 ) , ( temp_output_26_0_g101 * temp_output_34_0_g101 ) , ( break28_g101.y * temp_output_26_0_g101 * temp_output_36_0_g101 )));
			float4 temp_output_3_0_g97 = LargeSwells5;
			float2 temp_output_13_0_g97 = (temp_output_3_0_g97).xy;
			float2 break28_g97 = temp_output_13_0_g97;
			float dotResult21_g97 = dot( temp_output_13_0_g97 , (temp_output_13_0_g95).xz );
			float4 break7_g97 = temp_output_3_0_g97;
			float temp_output_10_0_g97 = ( ( 2.0 * UNITY_PI ) / break7_g97.w );
			float temp_output_25_0_g97 = ( ( dotResult21_g97 - ( sqrt( ( 9.8 / temp_output_10_0_g97 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g97 );
			float temp_output_36_0_g97 = cos( temp_output_25_0_g97 );
			float temp_output_26_0_g97 = ( break7_g97.z / temp_output_10_0_g97 );
			float temp_output_34_0_g97 = sin( temp_output_25_0_g97 );
			float3 appendResult48_g97 = (float3(( break28_g97.x * temp_output_36_0_g97 * temp_output_26_0_g97 ) , ( temp_output_26_0_g97 * temp_output_34_0_g97 ) , ( break28_g97.y * temp_output_26_0_g97 * temp_output_36_0_g97 )));
			float4 temp_output_3_0_g98 = LargeSwells4;
			float2 temp_output_13_0_g98 = (temp_output_3_0_g98).xy;
			float2 break28_g98 = temp_output_13_0_g98;
			float dotResult21_g98 = dot( temp_output_13_0_g98 , (temp_output_13_0_g95).xz );
			float4 break7_g98 = temp_output_3_0_g98;
			float temp_output_10_0_g98 = ( ( 2.0 * UNITY_PI ) / break7_g98.w );
			float temp_output_25_0_g98 = ( ( dotResult21_g98 - ( sqrt( ( 9.8 / temp_output_10_0_g98 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g98 );
			float temp_output_36_0_g98 = cos( temp_output_25_0_g98 );
			float temp_output_26_0_g98 = ( break7_g98.z / temp_output_10_0_g98 );
			float temp_output_34_0_g98 = sin( temp_output_25_0_g98 );
			float3 appendResult48_g98 = (float3(( break28_g98.x * temp_output_36_0_g98 * temp_output_26_0_g98 ) , ( temp_output_26_0_g98 * temp_output_34_0_g98 ) , ( break28_g98.y * temp_output_26_0_g98 * temp_output_36_0_g98 )));
			float4 temp_output_3_0_g99 = LargeSwells3;
			float2 temp_output_13_0_g99 = (temp_output_3_0_g99).xy;
			float2 break28_g99 = temp_output_13_0_g99;
			float dotResult21_g99 = dot( temp_output_13_0_g99 , (temp_output_13_0_g95).xz );
			float4 break7_g99 = temp_output_3_0_g99;
			float temp_output_10_0_g99 = ( ( 2.0 * UNITY_PI ) / break7_g99.w );
			float temp_output_25_0_g99 = ( ( dotResult21_g99 - ( sqrt( ( 9.8 / temp_output_10_0_g99 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g99 );
			float temp_output_36_0_g99 = cos( temp_output_25_0_g99 );
			float temp_output_26_0_g99 = ( break7_g99.z / temp_output_10_0_g99 );
			float temp_output_34_0_g99 = sin( temp_output_25_0_g99 );
			float3 appendResult48_g99 = (float3(( break28_g99.x * temp_output_36_0_g99 * temp_output_26_0_g99 ) , ( temp_output_26_0_g99 * temp_output_34_0_g99 ) , ( break28_g99.y * temp_output_26_0_g99 * temp_output_36_0_g99 )));
			float4 temp_output_3_0_g100 = LargeSwells2;
			float2 temp_output_13_0_g100 = (temp_output_3_0_g100).xy;
			float2 break28_g100 = temp_output_13_0_g100;
			float dotResult21_g100 = dot( temp_output_13_0_g100 , (temp_output_13_0_g95).xz );
			float4 break7_g100 = temp_output_3_0_g100;
			float temp_output_10_0_g100 = ( ( 2.0 * UNITY_PI ) / break7_g100.w );
			float temp_output_25_0_g100 = ( ( dotResult21_g100 - ( sqrt( ( 9.8 / temp_output_10_0_g100 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g100 );
			float temp_output_36_0_g100 = cos( temp_output_25_0_g100 );
			float temp_output_26_0_g100 = ( break7_g100.z / temp_output_10_0_g100 );
			float temp_output_34_0_g100 = sin( temp_output_25_0_g100 );
			float3 appendResult48_g100 = (float3(( break28_g100.x * temp_output_36_0_g100 * temp_output_26_0_g100 ) , ( temp_output_26_0_g100 * temp_output_34_0_g100 ) , ( break28_g100.y * temp_output_26_0_g100 * temp_output_36_0_g100 )));
			float4 temp_output_3_0_g96 = LargeSwells1;
			float2 temp_output_13_0_g96 = (temp_output_3_0_g96).xy;
			float2 break28_g96 = temp_output_13_0_g96;
			float dotResult21_g96 = dot( temp_output_13_0_g96 , (temp_output_13_0_g95).xz );
			float4 break7_g96 = temp_output_3_0_g96;
			float temp_output_10_0_g96 = ( ( 2.0 * UNITY_PI ) / break7_g96.w );
			float temp_output_25_0_g96 = ( ( dotResult21_g96 - ( sqrt( ( 9.8 / temp_output_10_0_g96 ) ) * ( temp_output_10_0_g95 + _Time.y ) ) ) * temp_output_10_0_g96 );
			float temp_output_36_0_g96 = cos( temp_output_25_0_g96 );
			float temp_output_26_0_g96 = ( break7_g96.z / temp_output_10_0_g96 );
			float temp_output_34_0_g96 = sin( temp_output_25_0_g96 );
			float3 appendResult48_g96 = (float3(( break28_g96.x * temp_output_36_0_g96 * temp_output_26_0_g96 ) , ( temp_output_26_0_g96 * temp_output_34_0_g96 ) , ( break28_g96.y * temp_output_26_0_g96 * temp_output_36_0_g96 )));
			float3 temp_output_451_3 = ( appendResult48_g101 + appendResult48_g97 + appendResult48_g98 + appendResult48_g99 + appendResult48_g100 + appendResult48_g96 );
			float temp_output_451_22 = ( temp_output_26_0_g101 + temp_output_26_0_g97 + temp_output_26_0_g98 + temp_output_26_0_g99 + temp_output_26_0_g100 + temp_output_26_0_g96 );
			float temp_output_406_0 = (0.0 + ((temp_output_451_3).y - -temp_output_451_22) * (1.0 - 0.0) / (temp_output_451_22 - -temp_output_451_22));
			float4 temp_output_3_0_g108 = Swells6;
			float2 temp_output_13_0_g108 = (temp_output_3_0_g108).xy;
			float2 break28_g108 = temp_output_13_0_g108;
			float3 temp_output_13_0_g102 = ase_worldPos;
			float dotResult21_g108 = dot( temp_output_13_0_g108 , (temp_output_13_0_g102).xz );
			float4 break7_g108 = temp_output_3_0_g108;
			float temp_output_10_0_g108 = ( ( 2.0 * UNITY_PI ) / break7_g108.w );
			float temp_output_10_0_g102 = timeOffset;
			float temp_output_25_0_g108 = ( ( dotResult21_g108 - ( sqrt( ( 9.8 / temp_output_10_0_g108 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g108 );
			float temp_output_36_0_g108 = cos( temp_output_25_0_g108 );
			float temp_output_26_0_g108 = ( break7_g108.z / temp_output_10_0_g108 );
			float temp_output_34_0_g108 = sin( temp_output_25_0_g108 );
			float3 appendResult48_g108 = (float3(( break28_g108.x * temp_output_36_0_g108 * temp_output_26_0_g108 ) , ( temp_output_26_0_g108 * temp_output_34_0_g108 ) , ( break28_g108.y * temp_output_26_0_g108 * temp_output_36_0_g108 )));
			float4 temp_output_3_0_g104 = Swells5;
			float2 temp_output_13_0_g104 = (temp_output_3_0_g104).xy;
			float2 break28_g104 = temp_output_13_0_g104;
			float dotResult21_g104 = dot( temp_output_13_0_g104 , (temp_output_13_0_g102).xz );
			float4 break7_g104 = temp_output_3_0_g104;
			float temp_output_10_0_g104 = ( ( 2.0 * UNITY_PI ) / break7_g104.w );
			float temp_output_25_0_g104 = ( ( dotResult21_g104 - ( sqrt( ( 9.8 / temp_output_10_0_g104 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g104 );
			float temp_output_36_0_g104 = cos( temp_output_25_0_g104 );
			float temp_output_26_0_g104 = ( break7_g104.z / temp_output_10_0_g104 );
			float temp_output_34_0_g104 = sin( temp_output_25_0_g104 );
			float3 appendResult48_g104 = (float3(( break28_g104.x * temp_output_36_0_g104 * temp_output_26_0_g104 ) , ( temp_output_26_0_g104 * temp_output_34_0_g104 ) , ( break28_g104.y * temp_output_26_0_g104 * temp_output_36_0_g104 )));
			float4 temp_output_3_0_g105 = Swells4;
			float2 temp_output_13_0_g105 = (temp_output_3_0_g105).xy;
			float2 break28_g105 = temp_output_13_0_g105;
			float dotResult21_g105 = dot( temp_output_13_0_g105 , (temp_output_13_0_g102).xz );
			float4 break7_g105 = temp_output_3_0_g105;
			float temp_output_10_0_g105 = ( ( 2.0 * UNITY_PI ) / break7_g105.w );
			float temp_output_25_0_g105 = ( ( dotResult21_g105 - ( sqrt( ( 9.8 / temp_output_10_0_g105 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g105 );
			float temp_output_36_0_g105 = cos( temp_output_25_0_g105 );
			float temp_output_26_0_g105 = ( break7_g105.z / temp_output_10_0_g105 );
			float temp_output_34_0_g105 = sin( temp_output_25_0_g105 );
			float3 appendResult48_g105 = (float3(( break28_g105.x * temp_output_36_0_g105 * temp_output_26_0_g105 ) , ( temp_output_26_0_g105 * temp_output_34_0_g105 ) , ( break28_g105.y * temp_output_26_0_g105 * temp_output_36_0_g105 )));
			float4 temp_output_3_0_g106 = Swells3;
			float2 temp_output_13_0_g106 = (temp_output_3_0_g106).xy;
			float2 break28_g106 = temp_output_13_0_g106;
			float dotResult21_g106 = dot( temp_output_13_0_g106 , (temp_output_13_0_g102).xz );
			float4 break7_g106 = temp_output_3_0_g106;
			float temp_output_10_0_g106 = ( ( 2.0 * UNITY_PI ) / break7_g106.w );
			float temp_output_25_0_g106 = ( ( dotResult21_g106 - ( sqrt( ( 9.8 / temp_output_10_0_g106 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g106 );
			float temp_output_36_0_g106 = cos( temp_output_25_0_g106 );
			float temp_output_26_0_g106 = ( break7_g106.z / temp_output_10_0_g106 );
			float temp_output_34_0_g106 = sin( temp_output_25_0_g106 );
			float3 appendResult48_g106 = (float3(( break28_g106.x * temp_output_36_0_g106 * temp_output_26_0_g106 ) , ( temp_output_26_0_g106 * temp_output_34_0_g106 ) , ( break28_g106.y * temp_output_26_0_g106 * temp_output_36_0_g106 )));
			float4 temp_output_3_0_g107 = Swells2;
			float2 temp_output_13_0_g107 = (temp_output_3_0_g107).xy;
			float2 break28_g107 = temp_output_13_0_g107;
			float dotResult21_g107 = dot( temp_output_13_0_g107 , (temp_output_13_0_g102).xz );
			float4 break7_g107 = temp_output_3_0_g107;
			float temp_output_10_0_g107 = ( ( 2.0 * UNITY_PI ) / break7_g107.w );
			float temp_output_25_0_g107 = ( ( dotResult21_g107 - ( sqrt( ( 9.8 / temp_output_10_0_g107 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g107 );
			float temp_output_36_0_g107 = cos( temp_output_25_0_g107 );
			float temp_output_26_0_g107 = ( break7_g107.z / temp_output_10_0_g107 );
			float temp_output_34_0_g107 = sin( temp_output_25_0_g107 );
			float3 appendResult48_g107 = (float3(( break28_g107.x * temp_output_36_0_g107 * temp_output_26_0_g107 ) , ( temp_output_26_0_g107 * temp_output_34_0_g107 ) , ( break28_g107.y * temp_output_26_0_g107 * temp_output_36_0_g107 )));
			float4 temp_output_3_0_g103 = Swells1;
			float2 temp_output_13_0_g103 = (temp_output_3_0_g103).xy;
			float2 break28_g103 = temp_output_13_0_g103;
			float dotResult21_g103 = dot( temp_output_13_0_g103 , (temp_output_13_0_g102).xz );
			float4 break7_g103 = temp_output_3_0_g103;
			float temp_output_10_0_g103 = ( ( 2.0 * UNITY_PI ) / break7_g103.w );
			float temp_output_25_0_g103 = ( ( dotResult21_g103 - ( sqrt( ( 9.8 / temp_output_10_0_g103 ) ) * ( temp_output_10_0_g102 + _Time.y ) ) ) * temp_output_10_0_g103 );
			float temp_output_36_0_g103 = cos( temp_output_25_0_g103 );
			float temp_output_26_0_g103 = ( break7_g103.z / temp_output_10_0_g103 );
			float temp_output_34_0_g103 = sin( temp_output_25_0_g103 );
			float3 appendResult48_g103 = (float3(( break28_g103.x * temp_output_36_0_g103 * temp_output_26_0_g103 ) , ( temp_output_26_0_g103 * temp_output_34_0_g103 ) , ( break28_g103.y * temp_output_26_0_g103 * temp_output_36_0_g103 )));
			float3 temp_output_452_3 = ( appendResult48_g108 + appendResult48_g104 + appendResult48_g105 + appendResult48_g106 + appendResult48_g107 + appendResult48_g103 );
			float temp_output_452_22 = ( temp_output_26_0_g108 + temp_output_26_0_g104 + temp_output_26_0_g105 + temp_output_26_0_g106 + temp_output_26_0_g107 + temp_output_26_0_g103 );
			float temp_output_408_0 = (0.0 + ((temp_output_452_3).y - -temp_output_452_22) * (1.0 - 0.0) / (temp_output_452_22 - -temp_output_452_22));
			float4 temp_output_3_0_g115 = SmallWaves6;
			float2 temp_output_13_0_g115 = (temp_output_3_0_g115).xy;
			float2 break28_g115 = temp_output_13_0_g115;
			float3 temp_output_13_0_g109 = ase_worldPos;
			float dotResult21_g115 = dot( temp_output_13_0_g115 , (temp_output_13_0_g109).xz );
			float4 break7_g115 = temp_output_3_0_g115;
			float temp_output_10_0_g115 = ( ( 2.0 * UNITY_PI ) / break7_g115.w );
			float temp_output_10_0_g109 = timeOffset;
			float temp_output_25_0_g115 = ( ( dotResult21_g115 - ( sqrt( ( 9.8 / temp_output_10_0_g115 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g115 );
			float temp_output_36_0_g115 = cos( temp_output_25_0_g115 );
			float temp_output_26_0_g115 = ( break7_g115.z / temp_output_10_0_g115 );
			float temp_output_34_0_g115 = sin( temp_output_25_0_g115 );
			float3 appendResult48_g115 = (float3(( break28_g115.x * temp_output_36_0_g115 * temp_output_26_0_g115 ) , ( temp_output_26_0_g115 * temp_output_34_0_g115 ) , ( break28_g115.y * temp_output_26_0_g115 * temp_output_36_0_g115 )));
			float4 temp_output_3_0_g111 = SmallWaves5;
			float2 temp_output_13_0_g111 = (temp_output_3_0_g111).xy;
			float2 break28_g111 = temp_output_13_0_g111;
			float dotResult21_g111 = dot( temp_output_13_0_g111 , (temp_output_13_0_g109).xz );
			float4 break7_g111 = temp_output_3_0_g111;
			float temp_output_10_0_g111 = ( ( 2.0 * UNITY_PI ) / break7_g111.w );
			float temp_output_25_0_g111 = ( ( dotResult21_g111 - ( sqrt( ( 9.8 / temp_output_10_0_g111 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g111 );
			float temp_output_36_0_g111 = cos( temp_output_25_0_g111 );
			float temp_output_26_0_g111 = ( break7_g111.z / temp_output_10_0_g111 );
			float temp_output_34_0_g111 = sin( temp_output_25_0_g111 );
			float3 appendResult48_g111 = (float3(( break28_g111.x * temp_output_36_0_g111 * temp_output_26_0_g111 ) , ( temp_output_26_0_g111 * temp_output_34_0_g111 ) , ( break28_g111.y * temp_output_26_0_g111 * temp_output_36_0_g111 )));
			float4 temp_output_3_0_g112 = SmallWaves4;
			float2 temp_output_13_0_g112 = (temp_output_3_0_g112).xy;
			float2 break28_g112 = temp_output_13_0_g112;
			float dotResult21_g112 = dot( temp_output_13_0_g112 , (temp_output_13_0_g109).xz );
			float4 break7_g112 = temp_output_3_0_g112;
			float temp_output_10_0_g112 = ( ( 2.0 * UNITY_PI ) / break7_g112.w );
			float temp_output_25_0_g112 = ( ( dotResult21_g112 - ( sqrt( ( 9.8 / temp_output_10_0_g112 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g112 );
			float temp_output_36_0_g112 = cos( temp_output_25_0_g112 );
			float temp_output_26_0_g112 = ( break7_g112.z / temp_output_10_0_g112 );
			float temp_output_34_0_g112 = sin( temp_output_25_0_g112 );
			float3 appendResult48_g112 = (float3(( break28_g112.x * temp_output_36_0_g112 * temp_output_26_0_g112 ) , ( temp_output_26_0_g112 * temp_output_34_0_g112 ) , ( break28_g112.y * temp_output_26_0_g112 * temp_output_36_0_g112 )));
			float4 temp_output_3_0_g113 = SmallWaves3;
			float2 temp_output_13_0_g113 = (temp_output_3_0_g113).xy;
			float2 break28_g113 = temp_output_13_0_g113;
			float dotResult21_g113 = dot( temp_output_13_0_g113 , (temp_output_13_0_g109).xz );
			float4 break7_g113 = temp_output_3_0_g113;
			float temp_output_10_0_g113 = ( ( 2.0 * UNITY_PI ) / break7_g113.w );
			float temp_output_25_0_g113 = ( ( dotResult21_g113 - ( sqrt( ( 9.8 / temp_output_10_0_g113 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g113 );
			float temp_output_36_0_g113 = cos( temp_output_25_0_g113 );
			float temp_output_26_0_g113 = ( break7_g113.z / temp_output_10_0_g113 );
			float temp_output_34_0_g113 = sin( temp_output_25_0_g113 );
			float3 appendResult48_g113 = (float3(( break28_g113.x * temp_output_36_0_g113 * temp_output_26_0_g113 ) , ( temp_output_26_0_g113 * temp_output_34_0_g113 ) , ( break28_g113.y * temp_output_26_0_g113 * temp_output_36_0_g113 )));
			float4 temp_output_3_0_g114 = SmallWaves2;
			float2 temp_output_13_0_g114 = (temp_output_3_0_g114).xy;
			float2 break28_g114 = temp_output_13_0_g114;
			float dotResult21_g114 = dot( temp_output_13_0_g114 , (temp_output_13_0_g109).xz );
			float4 break7_g114 = temp_output_3_0_g114;
			float temp_output_10_0_g114 = ( ( 2.0 * UNITY_PI ) / break7_g114.w );
			float temp_output_25_0_g114 = ( ( dotResult21_g114 - ( sqrt( ( 9.8 / temp_output_10_0_g114 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g114 );
			float temp_output_36_0_g114 = cos( temp_output_25_0_g114 );
			float temp_output_26_0_g114 = ( break7_g114.z / temp_output_10_0_g114 );
			float temp_output_34_0_g114 = sin( temp_output_25_0_g114 );
			float3 appendResult48_g114 = (float3(( break28_g114.x * temp_output_36_0_g114 * temp_output_26_0_g114 ) , ( temp_output_26_0_g114 * temp_output_34_0_g114 ) , ( break28_g114.y * temp_output_26_0_g114 * temp_output_36_0_g114 )));
			float4 temp_output_3_0_g110 = SmallWaves1;
			float2 temp_output_13_0_g110 = (temp_output_3_0_g110).xy;
			float2 break28_g110 = temp_output_13_0_g110;
			float dotResult21_g110 = dot( temp_output_13_0_g110 , (temp_output_13_0_g109).xz );
			float4 break7_g110 = temp_output_3_0_g110;
			float temp_output_10_0_g110 = ( ( 2.0 * UNITY_PI ) / break7_g110.w );
			float temp_output_25_0_g110 = ( ( dotResult21_g110 - ( sqrt( ( 9.8 / temp_output_10_0_g110 ) ) * ( temp_output_10_0_g109 + _Time.y ) ) ) * temp_output_10_0_g110 );
			float temp_output_36_0_g110 = cos( temp_output_25_0_g110 );
			float temp_output_26_0_g110 = ( break7_g110.z / temp_output_10_0_g110 );
			float temp_output_34_0_g110 = sin( temp_output_25_0_g110 );
			float3 appendResult48_g110 = (float3(( break28_g110.x * temp_output_36_0_g110 * temp_output_26_0_g110 ) , ( temp_output_26_0_g110 * temp_output_34_0_g110 ) , ( break28_g110.y * temp_output_26_0_g110 * temp_output_36_0_g110 )));
			float3 temp_output_453_3 = ( appendResult48_g115 + appendResult48_g111 + appendResult48_g112 + appendResult48_g113 + appendResult48_g114 + appendResult48_g110 );
			float temp_output_453_22 = ( temp_output_26_0_g115 + temp_output_26_0_g111 + temp_output_26_0_g112 + temp_output_26_0_g113 + temp_output_26_0_g114 + temp_output_26_0_g110 );
			float temp_output_411_0 = (0.0 + ((temp_output_453_3).y - -temp_output_453_22) * (1.0 - 0.0) / (temp_output_453_22 - -temp_output_453_22));
			float4 temp_output_3_0_g122 = Ripples6;
			float2 temp_output_13_0_g122 = (temp_output_3_0_g122).xy;
			float2 break28_g122 = temp_output_13_0_g122;
			float3 temp_output_13_0_g116 = ase_worldPos;
			float dotResult21_g122 = dot( temp_output_13_0_g122 , (temp_output_13_0_g116).xz );
			float4 break7_g122 = temp_output_3_0_g122;
			float temp_output_10_0_g122 = ( ( 2.0 * UNITY_PI ) / break7_g122.w );
			float temp_output_10_0_g116 = timeOffset;
			float temp_output_25_0_g122 = ( ( dotResult21_g122 - ( sqrt( ( 9.8 / temp_output_10_0_g122 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g122 );
			float temp_output_36_0_g122 = cos( temp_output_25_0_g122 );
			float temp_output_26_0_g122 = ( break7_g122.z / temp_output_10_0_g122 );
			float temp_output_34_0_g122 = sin( temp_output_25_0_g122 );
			float3 appendResult48_g122 = (float3(( break28_g122.x * temp_output_36_0_g122 * temp_output_26_0_g122 ) , ( temp_output_26_0_g122 * temp_output_34_0_g122 ) , ( break28_g122.y * temp_output_26_0_g122 * temp_output_36_0_g122 )));
			float4 temp_output_3_0_g118 = Ripples5;
			float2 temp_output_13_0_g118 = (temp_output_3_0_g118).xy;
			float2 break28_g118 = temp_output_13_0_g118;
			float dotResult21_g118 = dot( temp_output_13_0_g118 , (temp_output_13_0_g116).xz );
			float4 break7_g118 = temp_output_3_0_g118;
			float temp_output_10_0_g118 = ( ( 2.0 * UNITY_PI ) / break7_g118.w );
			float temp_output_25_0_g118 = ( ( dotResult21_g118 - ( sqrt( ( 9.8 / temp_output_10_0_g118 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g118 );
			float temp_output_36_0_g118 = cos( temp_output_25_0_g118 );
			float temp_output_26_0_g118 = ( break7_g118.z / temp_output_10_0_g118 );
			float temp_output_34_0_g118 = sin( temp_output_25_0_g118 );
			float3 appendResult48_g118 = (float3(( break28_g118.x * temp_output_36_0_g118 * temp_output_26_0_g118 ) , ( temp_output_26_0_g118 * temp_output_34_0_g118 ) , ( break28_g118.y * temp_output_26_0_g118 * temp_output_36_0_g118 )));
			float4 temp_output_3_0_g119 = Ripples4;
			float2 temp_output_13_0_g119 = (temp_output_3_0_g119).xy;
			float2 break28_g119 = temp_output_13_0_g119;
			float dotResult21_g119 = dot( temp_output_13_0_g119 , (temp_output_13_0_g116).xz );
			float4 break7_g119 = temp_output_3_0_g119;
			float temp_output_10_0_g119 = ( ( 2.0 * UNITY_PI ) / break7_g119.w );
			float temp_output_25_0_g119 = ( ( dotResult21_g119 - ( sqrt( ( 9.8 / temp_output_10_0_g119 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g119 );
			float temp_output_36_0_g119 = cos( temp_output_25_0_g119 );
			float temp_output_26_0_g119 = ( break7_g119.z / temp_output_10_0_g119 );
			float temp_output_34_0_g119 = sin( temp_output_25_0_g119 );
			float3 appendResult48_g119 = (float3(( break28_g119.x * temp_output_36_0_g119 * temp_output_26_0_g119 ) , ( temp_output_26_0_g119 * temp_output_34_0_g119 ) , ( break28_g119.y * temp_output_26_0_g119 * temp_output_36_0_g119 )));
			float4 temp_output_3_0_g120 = Ripples3;
			float2 temp_output_13_0_g120 = (temp_output_3_0_g120).xy;
			float2 break28_g120 = temp_output_13_0_g120;
			float dotResult21_g120 = dot( temp_output_13_0_g120 , (temp_output_13_0_g116).xz );
			float4 break7_g120 = temp_output_3_0_g120;
			float temp_output_10_0_g120 = ( ( 2.0 * UNITY_PI ) / break7_g120.w );
			float temp_output_25_0_g120 = ( ( dotResult21_g120 - ( sqrt( ( 9.8 / temp_output_10_0_g120 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g120 );
			float temp_output_36_0_g120 = cos( temp_output_25_0_g120 );
			float temp_output_26_0_g120 = ( break7_g120.z / temp_output_10_0_g120 );
			float temp_output_34_0_g120 = sin( temp_output_25_0_g120 );
			float3 appendResult48_g120 = (float3(( break28_g120.x * temp_output_36_0_g120 * temp_output_26_0_g120 ) , ( temp_output_26_0_g120 * temp_output_34_0_g120 ) , ( break28_g120.y * temp_output_26_0_g120 * temp_output_36_0_g120 )));
			float4 temp_output_3_0_g121 = Ripples2;
			float2 temp_output_13_0_g121 = (temp_output_3_0_g121).xy;
			float2 break28_g121 = temp_output_13_0_g121;
			float dotResult21_g121 = dot( temp_output_13_0_g121 , (temp_output_13_0_g116).xz );
			float4 break7_g121 = temp_output_3_0_g121;
			float temp_output_10_0_g121 = ( ( 2.0 * UNITY_PI ) / break7_g121.w );
			float temp_output_25_0_g121 = ( ( dotResult21_g121 - ( sqrt( ( 9.8 / temp_output_10_0_g121 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g121 );
			float temp_output_36_0_g121 = cos( temp_output_25_0_g121 );
			float temp_output_26_0_g121 = ( break7_g121.z / temp_output_10_0_g121 );
			float temp_output_34_0_g121 = sin( temp_output_25_0_g121 );
			float3 appendResult48_g121 = (float3(( break28_g121.x * temp_output_36_0_g121 * temp_output_26_0_g121 ) , ( temp_output_26_0_g121 * temp_output_34_0_g121 ) , ( break28_g121.y * temp_output_26_0_g121 * temp_output_36_0_g121 )));
			float4 temp_output_3_0_g117 = Ripples1;
			float2 temp_output_13_0_g117 = (temp_output_3_0_g117).xy;
			float2 break28_g117 = temp_output_13_0_g117;
			float dotResult21_g117 = dot( temp_output_13_0_g117 , (temp_output_13_0_g116).xz );
			float4 break7_g117 = temp_output_3_0_g117;
			float temp_output_10_0_g117 = ( ( 2.0 * UNITY_PI ) / break7_g117.w );
			float temp_output_25_0_g117 = ( ( dotResult21_g117 - ( sqrt( ( 9.8 / temp_output_10_0_g117 ) ) * ( temp_output_10_0_g116 + _Time.y ) ) ) * temp_output_10_0_g117 );
			float temp_output_36_0_g117 = cos( temp_output_25_0_g117 );
			float temp_output_26_0_g117 = ( break7_g117.z / temp_output_10_0_g117 );
			float temp_output_34_0_g117 = sin( temp_output_25_0_g117 );
			float3 appendResult48_g117 = (float3(( break28_g117.x * temp_output_36_0_g117 * temp_output_26_0_g117 ) , ( temp_output_26_0_g117 * temp_output_34_0_g117 ) , ( break28_g117.y * temp_output_26_0_g117 * temp_output_36_0_g117 )));
			float3 temp_output_454_3 = ( appendResult48_g122 + appendResult48_g118 + appendResult48_g119 + appendResult48_g120 + appendResult48_g121 + appendResult48_g117 );
			float temp_output_454_22 = ( temp_output_26_0_g122 + temp_output_26_0_g118 + temp_output_26_0_g119 + temp_output_26_0_g120 + temp_output_26_0_g121 + temp_output_26_0_g117 );
			float temp_output_435_0 = (0.0 + (( ( _LargeSwellsCrestInfluence * pow( temp_output_406_0 , _LargeSwellsFoamShape ) ) + ( _SwellsCrestInfluence * pow( temp_output_408_0 , _SwellsFoamShape ) ) + ( _SmallWavesCrestInfluence * pow( temp_output_411_0 , _SmallWavesFoamShape ) ) + ( _RipplesCrestInfluence * pow( (0.0 + ((temp_output_454_3).y - -temp_output_454_22) * (1.0 - 0.0) / (temp_output_454_22 - -temp_output_454_22)) , _RipplesFoamShape ) ) ) - 0.0) * (1.0 - 0.0) / (( _LargeSwellsCrestInfluence + _SwellsCrestInfluence + _SmallWavesCrestInfluence + _RipplesCrestInfluence ) - 0.0));
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth80 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPos ))));
			float distanceDepth80 = abs( ( screenDepth80 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _DepthFade ) );
			float clampResult88 = clamp( distanceDepth80 , 0.5 , 1.0 );
			float clampResult131 = clamp( ( ( 1.0 - (_WaterColorMapping.z + (temp_output_435_0 - _WaterColorMapping.x) * (_WaterColorMapping.w - _WaterColorMapping.z) / (_WaterColorMapping.y - _WaterColorMapping.x)) ) + clampResult88 ) , 0.0 , 1.0 );
			float4 lerpResult126 = lerp( _ShallowWater , _SurfaceColor , clampResult131);
			float2 temp_output_151_0 = (ase_worldPos).xz;
			float cos194 = cos( ( _DepthTextureRotation * UNITY_PI ) );
			float sin194 = sin( ( _DepthTextureRotation * UNITY_PI ) );
			float2 rotator194 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos194 , -sin194 , sin194 , cos194 )) + float2( 0.5,0.5 );
			float clampResult446 = clamp( (DepthMapping.z + (tex2D( _Depth, rotator194 ).r - DepthMapping.x) * (DepthMapping.w - DepthMapping.z) / (DepthMapping.y - DepthMapping.x)) , 0.0 , 1.0 );
			float temp_output_188_0 = pow( ( 1.0 - clampResult446 ) , _DepthScale );
			float temp_output_157_0 = distance( ase_worldPos , _WorldSpaceCameraPos );
			float3 temp_output_170_0 = ( temp_output_451_3 + temp_output_452_3 );
			float3 temp_output_11_0 = ( temp_output_170_0 + temp_output_453_3 );
			float3 temp_output_191_0 = ( temp_output_188_0 * (( temp_output_157_0 < _DistanceCutoff ) ? (( temp_output_157_0 < _DetailCutoff ) ? (( temp_output_157_0 < _FineDetailCutoff ) ? ( temp_output_11_0 + temp_output_454_3 ) :  temp_output_11_0 ) :  temp_output_170_0 ) :  temp_output_451_3 ) );
			float2 temp_output_202_0 = (temp_output_191_0).xz;
			float4 lerpResult65 = lerp( float4( 0,0,0,0 ) , tex2D( _CrestFoam, ( ( temp_output_151_0 * _CrestFoamWorldScaling ) + ( _CrestFoamUVOffset * temp_output_202_0 ) ) ) , (_CrestFoamMapping.z + (temp_output_435_0 - _CrestFoamMapping.x) * (_CrestFoamMapping.w - _CrestFoamMapping.z) / (_CrestFoamMapping.y - _CrestFoamMapping.x)));
			float4 clampResult69 = clamp( lerpResult65 , float4( 0,0,0,0 ) , float4( 1,0.9901667,0.99,0.01176471 ) );
			float4 lerpResult63 = lerp( lerpResult126 , _FoamColor , clampResult69);
			float cos458 = cos( ( _WakeTextureRotation * UNITY_PI ) );
			float sin458 = sin( ( _WakeTextureRotation * UNITY_PI ) );
			float2 rotator458 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos458 , -sin458 , sin458 , cos458 )) + float2( 0.5,0.5 );
			float2 appendResult481 = (float2(_WakeUVMapping.x , _WakeUVMapping.y));
			float2 appendResult482 = (float2(_WakeUVMapping.z , _WakeUVMapping.w));
			float4 tex2DNode459 = tex2D( _WakeTexture, ( ( temp_output_202_0 * _WakeUVScaling ) + (float2( 0,0 ) + (rotator458 - appendResult481) * (float2( 1,1 ) - float2( 0,0 )) / (appendResult482 - appendResult481)) ) );
			float clampResult485 = clamp( (_SprayFoamMapping.z + (tex2DNode459.r - _SprayFoamMapping.x) * (_SprayFoamMapping.w - _SprayFoamMapping.z) / (_SprayFoamMapping.y - _SprayFoamMapping.x)) , 0.0 , 1.0 );
			float temp_output_512_0 = ( ( pow( ( 1.0 - temp_output_406_0 ) , _LargeSwellsSprayShape ) * _LargeSwellsSprayStrength ) + ( pow( temp_output_411_0 , _SmallWavesSprayShape ) * _SmallWavesSprayStrength ) + ( pow( ( 1.0 - temp_output_408_0 ) , _SwellsSprayShape ) * _SwellsSprayStrength ) + 1.0 );
			float4 lerpResult114 = lerp( float4( 0,0,0,0 ) , tex2D( _LowFoam, ( ( temp_output_151_0 * _TroughFoamWorldScaling ) + ( _TroughFoamUVOffset * temp_output_202_0 ) ) ) , ( ( ( pow( clampResult485 , _SprayFoamShape ) * _SprayFoamStrength ) * temp_output_512_0 ) + ( 1.0 - (_TroughFoamMapping.z + (temp_output_435_0 - _TroughFoamMapping.x) * (_TroughFoamMapping.w - _TroughFoamMapping.z) / (_TroughFoamMapping.y - _TroughFoamMapping.x)) ) ));
			float4 clampResult116 = clamp( lerpResult114 , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
			float4 lerpResult121 = lerp( lerpResult63 , _Lowfoamcolor , clampResult116);
			o.Albedo = lerpResult121.rgb;
			float3 temp_cast_1 = (_Specularity).xxx;
			o.Specular = temp_cast_1;
			o.Smoothness = (_SmoothnessMapping.z + (( 1.0 - temp_output_435_0 ) - _SmoothnessMapping.x) * (_SmoothnessMapping.w - _SmoothnessMapping.z) / (_SmoothnessMapping.y - _SmoothnessMapping.x));
			float clampResult79 = clamp( ( (_TranslucencyMapping.z + (temp_output_435_0 - _TranslucencyMapping.x) * (_TranslucencyMapping.w - _TranslucencyMapping.z) / (_TranslucencyMapping.y - _TranslucencyMapping.x)) * _TranslucencyScale ) , 0.01 , 10.0 );
			float3 temp_cast_2 = (clampResult79).xxx;
			o.Translucency = temp_cast_2;
			o.Alpha = clampResult88;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardSpecularCustom keepalpha fullforwardshadows exclude_path:deferred vertex:vertexDataFunc tessellate:tessFunction tessphong:_TessPhongStrength 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.6
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
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
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandardSpecularCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandardSpecularCustom, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15900
7;40;1906;1033;1457.518;2198.959;4.494759;True;False
Node;AmplifyShaderEditor.Vector4Node;1;-697.5943,2902.256;Float;False;Global;LargeSwells1;Large Swells 1;4;0;Create;True;0;0;False;0;0,0,0,0;0.9889363,0.1483404,0.075,124.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;52;-377.279,3174.493;Float;False;Global;LargeSwells5;Large Swells 5;8;0;Create;True;0;0;False;0;0,0,0,0;0.9805807,-0.1961161,0.06,153;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;2;-694.1076,3123.968;Float;False;Global;LargeSwells2;Large Swells 2;5;0;Create;True;0;0;False;0;0,0,0,0;0.9950372,0.09950373,0.06,148.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;3;-378.229,2950.898;Float;False;Global;LargeSwells4;Large Swells 4;7;0;Create;True;0;0;False;0;0,0,0,0;0.9889363,-0.1483404,0.075,121.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;98;-690.8003,3417.683;Float;False;Global;LargeSwells3;Large Swells 3;6;0;Create;True;0;0;False;0;0,0,0,0;0.9987524,0.04993762,0.045,166.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;142;42.04762,3480.688;Float;False;Global;timeOffset;timeOffset;60;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;44;-1414.541,2121.324;Float;False;Constant;_Vector1;Vector 1;8;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldPosInputsNode;158;-929.0418,1418.401;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector4Node;53;-369.1735,3412.323;Float;False;Global;LargeSwells6;Large Swells 6;9;0;Create;True;0;0;False;0;0,0,0,0;0.9950372,-0.09950373,0.045,181.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;43;-1388.07,1944.975;Float;False;Constant;_Vector0;Vector 0;8;0;Create;True;0;0;False;0;1,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector4Node;220;1585.835,3490.032;Float;False;Global;Swells6;Swells 6;15;0;Create;True;0;0;False;0;0,0,0,0;0.8346094,-0.5508422,0.075,79;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;219;1576.614,3286.11;Float;False;Global;Swells5;Swells 5;14;0;Create;True;0;0;False;0;0,0,0,0;0.8962124,-0.4436251,0.0875,63;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;111;1528.033,3026.778;Float;False;Global;Swells4;Swells 4;13;0;Create;True;0;0;False;0;0,0,0,0;0.9496287,-0.3133775,0.09999999,42;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;451;127.7822,2651.288;Float;False;WaveBlock;-1;;95;daa6ec5b9e2ba324f8ded38a2d863a12;0;10;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;FLOAT3;0,0,0;False;10;FLOAT;0;False;15;FLOAT4;0,0,0,0;False;16;FLOAT4;0,0,0,0;False;17;FLOAT4;0,0,0,0;False;18;FLOAT4;0,0,0,0;False;19;FLOAT4;0,0,0,0;False;20;FLOAT4;0,0,0,0;False;4;FLOAT;22;FLOAT3;3;FLOAT3;0;FLOAT3;2
Node;AmplifyShaderEditor.Vector4Node;100;1110.708,2905.884;Float;False;Global;Swells1;Swells 1;10;0;Create;True;0;0;False;0;0,0,0,0;0.9866593,0.1627988,0.09999999,45;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;109;1232.957,3508.955;Float;False;Global;Swells3;Swells 3;12;0;Create;True;0;0;False;0;0,0,0,0;0.8962124,0.4436251,0.075,77;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;102;1145.204,3249.073;Float;False;Global;Swells2;Swells 2;11;0;Create;True;0;0;False;0;0,0,0,0;0.9496287,0.3133775,0.0875,64;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;349;1817.333,989.0065;Float;False;Property;_DepthTextureRotation;Depth  Texture Rotation;37;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;340;2167.371,3977.033;Float;False;Global;SmallWaves3;Small Waves 3;18;0;Create;True;0;0;False;0;0,0,0,0;1,0,0.09,19;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PiNode;196;2110.964,1011.854;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;452;1876.228,2728.18;Float;False;WaveBlock;-1;;102;daa6ec5b9e2ba324f8ded38a2d863a12;0;10;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;FLOAT3;0,0,0;False;10;FLOAT;0;False;15;FLOAT4;0,0,0,0;False;16;FLOAT4;0,0,0,0;False;17;FLOAT4;0,0,0,0;False;18;FLOAT4;0,0,0,0;False;19;FLOAT4;0,0,0,0;False;20;FLOAT4;0,0,0,0;False;4;FLOAT;22;FLOAT3;3;FLOAT3;0;FLOAT3;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;195;2037.788,736.7133;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;341;2422.68,3779.788;Float;False;Global;SmallWaves5;Small Waves 5;20;0;Create;True;0;0;False;0;0,0,0,0;0.9119215,-0.4103647,0.1,16.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;238;2155.999,3741.294;Float;False;Global;SmallWaves2;Small Waves 2;17;0;Create;True;0;0;False;0;0,0,0,0;0.9578263,0.2873479,0.1,15.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;237;2122.408,3505.4;Float;False;Global;SmallWaves1;Small Waves 1;16;0;Create;True;0;0;False;0;0,0,0,0;0.9119215,0.4103647,0.11,10.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;342;2461.481,3988.897;Float;False;Global;SmallWaves6;Small Waves 6;21;0;Create;True;0;0;False;0;0,0,0,0;0.8574929,-0.5144958,0.09,20;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;339;2427.98,3579.196;Float;False;Global;SmallWaves4;Small Waves 4;19;0;Create;True;0;0;False;0;0,0,0,0;0.9578263,-0.2873479,0.11,11;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;346;3757.377,3743.302;Float;False;Global;Ripples5;Ripples 5;26;0;Create;True;0;0;False;0;0,0,0,0;0.9078,-0.4194036,0.078,3.6;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;347;3762.675,3542.708;Float;False;Global;Ripples4;Ripples 4;25;0;Create;True;0;0;False;0;0,0,0,0;0.974342,-0.225073,0.08400001,1.8;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;345;3502.066,3940.545;Float;False;Global;Ripples3;Ripples 3;24;0;Create;True;0;0;False;0;0,0,0,0;0.7344654,0.678646,0.072,6.4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;343;3490.695,3704.805;Float;False;Global;Ripples2;Ripples 2;23;0;Create;True;0;0;False;0;0,0,0,0;0.8219258,0.5695946,0.078,3.2;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;453;2939.915,2843.112;Float;False;WaveBlock;-1;;109;daa6ec5b9e2ba324f8ded38a2d863a12;0;10;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;FLOAT3;0,0,0;False;10;FLOAT;0;False;15;FLOAT4;0,0,0,0;False;16;FLOAT4;0,0,0,0;False;17;FLOAT4;0,0,0,0;False;18;FLOAT4;0,0,0,0;False;19;FLOAT4;0,0,0,0;False;20;FLOAT4;0,0,0,0;False;4;FLOAT;22;FLOAT3;3;FLOAT3;0;FLOAT3;2
Node;AmplifyShaderEditor.SimpleAddOpNode;170;2489.716,2141.542;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector4Node;348;3488.895,3506.666;Float;False;Global;Ripples1;Ripples 1;22;0;Create;True;0;0;False;0;0,0,0,0;0.9078,0.4194036,0.08400001,1.6;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;194;2416.11,827.0891;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;0.84;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;344;3796.176,3952.408;Float;False;Global;Ripples6;Ripples 6;27;0;Create;True;0;0;False;0;0,0,0,0;0.8219258,-0.5695946,0.072,7.2;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldSpaceCameraPos;159;558.0982,1633.148;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FunctionNode;454;4237.242,3317.909;Float;False;WaveBlock;-1;;116;daa6ec5b9e2ba324f8ded38a2d863a12;0;10;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;FLOAT3;0,0,0;False;10;FLOAT;0;False;15;FLOAT4;0,0,0,0;False;16;FLOAT4;0,0,0,0;False;17;FLOAT4;0,0,0,0;False;18;FLOAT4;0,0,0,0;False;19;FLOAT4;0,0,0,0;False;20;FLOAT4;0,0,0,0;False;4;FLOAT;22;FLOAT3;3;FLOAT3;0;FLOAT3;2
Node;AmplifyShaderEditor.SimpleAddOpNode;11;3369.217,2405.329;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;177;2646.175,747.5374;Float;True;Property;_Depth;Depth;41;0;Create;True;0;0;False;0;4122a1f72799903469c22fbd37287560;4122a1f72799903469c22fbd37287560;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;445;2671.777,1132.975;Float;False;Global;DepthMapping;Depth Mapping;64;0;Create;True;0;0;False;0;0.21,1,0,1;0.2,1,0,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DistanceOpNode;157;1334.284,1542.969;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;214;2606.479,2183.965;Float;False;Property;_FineDetailCutoff;Fine Detail Cutoff;38;0;Create;True;0;0;False;0;250;400;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;221;4852.996,2782.507;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCRemapNode;203;2990.419,821.7617;Float;False;5;0;FLOAT;0;False;1;FLOAT;0.2;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;216;3765.758,2110.993;Float;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;163;2806.548,1466.961;Float;False;Property;_DetailCutoff;Detail Cutoff;39;0;Create;True;0;0;False;0;500;500;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;446;3298.42,835.6389;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;210;3665.906,1541.24;Float;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;193;3569.35,845.7526;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;171;3045.396,1285.679;Float;False;Property;_DistanceCutoff;Distance Cutoff;40;0;Create;True;0;0;False;0;1000;800;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;182;3352.396,1090.261;Float;False;Property;_DepthScale;Depth Scale;32;0;Create;True;0;0;False;0;2;15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;188;3788.303,893.7136;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;213;3844.712,1255.739;Float;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;455;2063.502,446.0599;Float;False;Property;_WakeTextureRotation;Wake Texture Rotation;36;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;457;2357.133,468.9075;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;480;2117.813,278.7221;Float;False;Property;_WakeUVMapping;Wake UV Mapping;42;0;Create;True;0;0;False;0;0.625,0.375,0.375,0.625;0.375,0.375,0.625,0.625;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;456;2135.913,155.3323;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;191;4261.02,1033.328;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwizzleNode;410;3008.456,2590.234;Float;False;FLOAT;1;1;2;2;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;403;1777.798,2428.5;Float;False;FLOAT;1;1;2;2;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;481;2407.882,249.6187;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SwizzleNode;413;4359.224,3058.69;Float;False;FLOAT;1;1;2;2;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;402;-22.97066,2288.371;Float;False;FLOAT;1;1;2;2;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;412;3203.866,2664.501;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;414;4554.634,3132.958;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;409;1864.303,2518.914;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;458;2410.026,40.4637;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;0.84;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;482;2486.913,345.4222;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;519;-209.6202,98.66484;Float;False;Property;_WakeUVScaling;WakeUVScaling;52;0;Create;True;0;0;False;0;0;0.001;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;202;-1087.222,-882.0415;Float;False;FLOAT2;0;2;2;2;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NegateNode;407;177.3841,2363.277;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;474;2663.344,182.693;Float;False;5;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT2;1,1;False;3;FLOAT2;0,0;False;4;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;415;4734.265,3107.604;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;432;4560.265,2965.67;Float;False;Property;_RipplesFoamShape;Ripples Foam Shape;16;0;Create;True;0;0;False;0;1;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;408;2062.69,2371.598;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;406;413.9108,2257.046;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;429;1883.122,2237.059;Float;False;Property;_SwellsFoamShape;Swells Foam Shape;12;0;Create;True;0;0;False;0;1;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;425;184.6302,1987.181;Float;False;Property;_LargeSwellsFoamShape;Large Swells Foam Shape;10;0;Create;True;0;0;False;0;1;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;518;526.2086,4.327759;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;411;3358.497,2637.147;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;431;3253.053,2555.242;Float;False;Property;_SmallWavesFoamShape;Small Waves Foam Shape;14;0;Create;True;0;0;False;0;1;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;520;2818.224,47.47551;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;427;641.1305,2112.48;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;423;4603.665,2882.924;Float;False;Property;_RipplesCrestInfluence;Ripples Crest Influence;15;0;Create;True;0;0;False;0;1;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;418;2008.685,2116.252;Float;False;Property;_SwellsCrestInfluence;Swells Crest Influence;11;0;Create;True;0;0;False;0;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;430;3542.759,2562.019;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;416;298.6104,1819.247;Float;False;Property;_LargeSwellsCrestInfluence;Large Swells Crest Influence;9;0;Create;True;0;0;False;0;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;421;3376.643,2482.904;Float;False;Property;_SmallWavesCrestInfluence;Small Waves Crest Influence;13;0;Create;True;0;0;False;0;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;428;2124.827,2221.836;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;433;4849.972,2972.447;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;422;4988.874,2899.616;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;419;2293.424,2111.105;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;459;2899.014,174.6759;Float;True;Property;_WakeTexture;Wake Texture;43;0;Create;True;0;0;False;0;c7bda04f563c9fb4fb1f869508a25639;c7bda04f563c9fb4fb1f869508a25639;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;484;1040.106,-1246.761;Float;False;Property;_SprayFoamMapping;Spray Foam Mapping;48;0;Create;True;0;0;False;0;0.21,1,0,1;0.21,1,0,0.3;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;420;3735.895,2473.979;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;417;931.119,2038.316;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;502;-283.0217,1436.37;Float;False;Property;_SwellsSprayShape;Swells Spray Shape;54;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;150;-2180.056,-1787.764;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.OneMinusNode;506;-393.9925,1339.433;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;498;-466.4778,1097.716;Float;False;Property;_LargeSwellsSprayShape;Large Swells Spray Shape;51;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;483;1354.018,-1223.644;Float;False;5;0;FLOAT;0;False;1;FLOAT;0.2;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;424;1267.994,1695.927;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;434;1328.146,2509.938;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;507;-21.74036,1590.926;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;509;253.9476,1276.053;Float;False;Property;_SmallWavesSprayShape;Small Waves Spray Shape;56;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;460;2828.798,537.2393;Float;False;Property;_SprayOffsetMapping;Spray Offset Mapping;45;0;Create;True;0;0;False;0;0.21,1,0,1;0.21,1,0,0.3;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;441;1032.379,-68.99595;Float;False;Property;_WaterColorMapping;Water Color Mapping;17;0;Create;True;0;0;False;0;0,0,0,0;0,0.66,0,3;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;508;588.4825,1240.894;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;435;1589.015,1293.802;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;485;1540.353,-1195.775;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;487;1379.765,-1030.163;Float;False;Property;_SprayFoamShape;Spray Foam Shape;49;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;496;-131.943,1062.556;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;256;-1026.778,-1400.408;Float;False;Property;_CrestFoamWorldScaling;Crest Foam World Scaling;30;0;Create;True;0;0;False;0;0;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;500;51.51315,1401.211;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;499;-31.01959,1191.874;Float;False;Property;_LargeSwellsSprayStrength;Large Swells Spray Strength;53;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CrossProductOpNode;12;3731.641,2639.644;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;503;189.2212,1536.659;Float;False;Property;_SwellsSprayStrength;Swells Spray Strength;55;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CrossProductOpNode;334;5128.142,3320.174;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;510;726.1906,1376.342;Float;False;Property;_SmallWavesSprayStrength;Small Waves Spray Strength;57;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SwizzleNode;151;-1908.451,-1769.084;Float;False;FLOAT2;0;2;2;2;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;201;-1060.01,-1275.485;Float;False;Property;_CrestFoamUVOffset;Crest Foam UV Offset;33;0;Create;True;0;0;False;0;0;-0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;461;3236.588,278.8151;Float;False;5;0;FLOAT;0;False;1;FLOAT;0.2;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;1245.746,129.2954;Float;False;Property;_DepthFade;Depth Fade;26;0;Create;True;0;0;False;0;0;15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;259;-247.3833,-1471.457;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;155;-1060.816,-1601.92;Float;False;Property;_TroughFoamWorldScaling;Trough Foam World Scaling;31;0;Create;True;0;0;False;0;0;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;200;-244.616,-1333.71;Float;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;462;3527.231,539.142;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;486;1724.242,-1199.473;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;80;1489.931,125.8923;Float;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;497;282.4512,1047.432;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CrossProductOpNode;168;3013.415,2170.509;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCRemapNode;438;1445.102,-153.122;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;437;1214.547,-1406.674;Float;False;Property;_TroughFoamMapping;Trough Foam Mapping;6;0;Create;True;0;0;False;0;0,0,0,0;0,0.26,0.65,1.09;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;515;840.2722,1080.452;Float;False;Constant;_Float1;Float 1;55;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;511;1002.877,1225.77;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;479;1560.252,-997.978;Float;False;Property;_SprayFoamStrength;Spray Foam Strength;50;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;255;-1041.8,-1506.603;Float;False;Property;_TroughFoamUVOffset;Trough Foam UV Offset;34;0;Create;True;0;0;False;0;0;-0.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;333;5179.564,3016.607;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;501;465.9073,1386.087;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;464;3536.517,700.2845;Float;False;Property;_SprayOffsetShape;Spray Offset Shape;46;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;13;4010.068,2565.192;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CrossProductOpNode;176;2008.69,1958.49;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;156;-270.6503,-1760.559;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalizeNode;169;3247.525,2119.382;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;88;1789.487,137.4554;Float;False;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;199;427.7266,-1228.911;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;478;1893.951,-1221.506;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;512;1145.327,981.5892;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;215;4516.225,2206.995;Float;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCRemapNode;113;1590.828,-1365.648;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;258;-267.8034,-1653.872;Float;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;436;635.8329,-704.9103;Float;False;Property;_CrestFoamMapping;Crest Foam Mapping;7;0;Create;True;0;0;False;0;0,0,0,0;0.1,1,0,4.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;130;1867.21,-168.4233;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;463;3800.527,650.1837;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;471;3756.381,770.0044;Float;False;Property;_SprayOffsetStrength;Spray Offset Strength;47;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;61;789.8047,-1053.564;Float;True;Property;_CrestFoam;Crest Foam;5;0;Create;True;0;0;False;0;8304dd4a36d805543a8b0a8e0674efc7;8304dd4a36d805543a8b0a8e0674efc7;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;67;1026.13,-816.3941;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;122;1794.779,-1427.323;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;211;3832.924,1678.487;Float;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;128;2189.062,-43.51353;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;513;2041.628,-1228.911;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;175;2317.466,1888.755;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;470;4031.916,693.9448;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;198;219.8927,-1705.953;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;65;1798.755,-866.3545;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;514;4213.336,802.0064;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCCompareLower;212;4332.854,1682.795;Float;False;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;477;2082.135,-1369.568;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;127;1840.29,-625.6807;Float;False;Property;_ShallowWater;Shallow Water;1;0;Create;True;0;0;False;0;0.2548505,0.5770289,0.7830189,0;0.258989,0.5192887,0.5660378,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;131;2399.995,-336.2394;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;78;3564.269,240.0334;Float;False;Property;_TranslucencyMapping;Translucency Mapping;18;0;Create;True;0;0;False;0;0,0,0,0;0,1,0,4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;14;1942.696,-429.6638;Float;False;Property;_SurfaceColor;Surface Color;0;0;Create;True;0;0;False;0;0.2548505,0.5770289,0.7830189,0;0.1627803,0.4102331,0.5849056,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;117;1430.461,-1616.152;Float;True;Property;_LowFoam;Low Foam;4;0;Create;True;0;0;False;0;8304dd4a36d805543a8b0a8e0674efc7;cd460ee4ac5c1e746b7a734cc7cc64dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;69;2105.589,-803.4451;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0.9901667,0.99,0.01176471;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;76;3895.883,322.3129;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;114;2246.628,-1456.724;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;126;2731.493,-511.9281;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;64;2539.948,-872.0561;Float;False;Property;_FoamColor;Foam Color;3;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;466;4638.677,763.2129;Float;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;192;4896.251,1239.695;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;73;4131.081,600.0723;Float;False;Property;_TranslucencyScale;Translucency Scale;25;0;Create;True;0;0;False;0;0;1.25;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;146;5489.341,1139.28;Float;False;Property;_MaxDistance;Max Distance;28;0;Create;True;0;0;False;0;0;500;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;144;5493.341,987.2798;Float;False;Property;_TessellationFactor;Tessellation Factor;27;0;Create;True;0;0;False;0;0;30;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;444;3600.459,-213.5452;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;116;2861.305,-1457.199;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,1;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;145;5480.341,1053.28;Float;False;Property;_MinDistance;Min Distance;29;0;Create;True;0;0;False;0;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;63;3123.121,-573.3148;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector4Node;443;3660.205,-73.01666;Float;False;Property;_SmoothnessMapping;Smoothness Mapping;8;0;Create;True;0;0;False;0;0,0,0,0;0.5,1,0.1,1.25;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;129;4433.525,470.1807;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;468;5058.931,1041.731;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;115;2256.235,-1224.491;Float;False;Property;_Lowfoamcolor;Low foam color;2;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;442;4124.705,-237.8889;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;230;5081.373,-68.20343;Float;False;Property;_Specularity;Specularity;35;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;121;3802.411,-569.9574;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;79;4830.89,424.7903;Float;False;3;0;FLOAT;0;False;1;FLOAT;0.01;False;2;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;465;5068.522,777.0534;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DistanceBasedTessNode;143;5728.341,1088.28;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.NormalizeNode;469;5262.324,1002.329;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;5935.596,76.9453;Float;False;True;6;Float;ASEMaterialInspector;0;0;StandardSpecular;Waves;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;1;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;2;Custom;0.5;True;True;0;True;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;0;20;1000;1500;True;1;True;2;5;False;-1;10;False;-1;0;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;44;19;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;451;11;43;0
WireConnection;451;12;44;0
WireConnection;451;13;158;0
WireConnection;451;10;142;0
WireConnection;451;15;1;0
WireConnection;451;16;2;0
WireConnection;451;17;98;0
WireConnection;451;18;3;0
WireConnection;451;19;52;0
WireConnection;451;20;53;0
WireConnection;196;0;349;0
WireConnection;452;11;451;0
WireConnection;452;12;451;2
WireConnection;452;13;158;0
WireConnection;452;10;142;0
WireConnection;452;15;100;0
WireConnection;452;16;102;0
WireConnection;452;17;109;0
WireConnection;452;18;111;0
WireConnection;452;19;219;0
WireConnection;452;20;220;0
WireConnection;453;11;452;0
WireConnection;453;12;452;2
WireConnection;453;13;158;0
WireConnection;453;10;142;0
WireConnection;453;15;237;0
WireConnection;453;16;238;0
WireConnection;453;17;340;0
WireConnection;453;18;339;0
WireConnection;453;19;341;0
WireConnection;453;20;342;0
WireConnection;170;0;451;3
WireConnection;170;1;452;3
WireConnection;194;0;195;0
WireConnection;194;2;196;0
WireConnection;454;11;453;0
WireConnection;454;12;453;2
WireConnection;454;13;158;0
WireConnection;454;10;142;0
WireConnection;454;15;348;0
WireConnection;454;16;343;0
WireConnection;454;17;345;0
WireConnection;454;18;347;0
WireConnection;454;19;346;0
WireConnection;454;20;344;0
WireConnection;11;0;170;0
WireConnection;11;1;453;3
WireConnection;177;1;194;0
WireConnection;157;0;158;0
WireConnection;157;1;159;0
WireConnection;221;0;11;0
WireConnection;221;1;454;3
WireConnection;203;0;177;1
WireConnection;203;1;445;1
WireConnection;203;2;445;2
WireConnection;203;3;445;3
WireConnection;203;4;445;4
WireConnection;216;0;157;0
WireConnection;216;1;214;0
WireConnection;216;2;221;0
WireConnection;216;3;11;0
WireConnection;446;0;203;0
WireConnection;210;0;157;0
WireConnection;210;1;163;0
WireConnection;210;2;216;0
WireConnection;210;3;170;0
WireConnection;193;0;446;0
WireConnection;188;0;193;0
WireConnection;188;1;182;0
WireConnection;213;0;157;0
WireConnection;213;1;171;0
WireConnection;213;2;210;0
WireConnection;213;3;451;3
WireConnection;457;0;455;0
WireConnection;191;0;188;0
WireConnection;191;1;213;0
WireConnection;410;0;453;3
WireConnection;403;0;452;3
WireConnection;481;0;480;1
WireConnection;481;1;480;2
WireConnection;413;0;454;3
WireConnection;402;0;451;3
WireConnection;412;0;453;22
WireConnection;414;0;454;22
WireConnection;409;0;452;22
WireConnection;458;0;456;0
WireConnection;458;2;457;0
WireConnection;482;0;480;3
WireConnection;482;1;480;4
WireConnection;202;0;191;0
WireConnection;407;0;451;22
WireConnection;474;0;458;0
WireConnection;474;1;481;0
WireConnection;474;2;482;0
WireConnection;415;0;413;0
WireConnection;415;1;414;0
WireConnection;415;2;454;22
WireConnection;408;0;403;0
WireConnection;408;1;409;0
WireConnection;408;2;452;22
WireConnection;406;0;402;0
WireConnection;406;1;407;0
WireConnection;406;2;451;22
WireConnection;518;0;202;0
WireConnection;518;1;519;0
WireConnection;411;0;410;0
WireConnection;411;1;412;0
WireConnection;411;2;453;22
WireConnection;520;0;518;0
WireConnection;520;1;474;0
WireConnection;427;0;406;0
WireConnection;427;1;425;0
WireConnection;430;0;411;0
WireConnection;430;1;431;0
WireConnection;428;0;408;0
WireConnection;428;1;429;0
WireConnection;433;0;415;0
WireConnection;433;1;432;0
WireConnection;422;0;423;0
WireConnection;422;1;433;0
WireConnection;419;0;418;0
WireConnection;419;1;428;0
WireConnection;459;1;520;0
WireConnection;420;0;421;0
WireConnection;420;1;430;0
WireConnection;417;0;416;0
WireConnection;417;1;427;0
WireConnection;506;0;406;0
WireConnection;483;0;459;1
WireConnection;483;1;484;1
WireConnection;483;2;484;2
WireConnection;483;3;484;3
WireConnection;483;4;484;4
WireConnection;424;0;417;0
WireConnection;424;1;419;0
WireConnection;424;2;420;0
WireConnection;424;3;422;0
WireConnection;434;0;416;0
WireConnection;434;1;418;0
WireConnection;434;2;421;0
WireConnection;434;3;423;0
WireConnection;507;0;408;0
WireConnection;508;0;411;0
WireConnection;508;1;509;0
WireConnection;435;0;424;0
WireConnection;435;2;434;0
WireConnection;485;0;483;0
WireConnection;496;0;506;0
WireConnection;496;1;498;0
WireConnection;500;0;507;0
WireConnection;500;1;502;0
WireConnection;12;0;453;2
WireConnection;12;1;453;0
WireConnection;334;0;454;2
WireConnection;334;1;454;0
WireConnection;151;0;150;0
WireConnection;461;0;459;1
WireConnection;461;1;460;1
WireConnection;461;2;460;2
WireConnection;461;3;460;3
WireConnection;461;4;460;4
WireConnection;259;0;151;0
WireConnection;259;1;256;0
WireConnection;200;0;201;0
WireConnection;200;1;202;0
WireConnection;462;0;461;0
WireConnection;486;0;485;0
WireConnection;486;1;487;0
WireConnection;80;0;81;0
WireConnection;497;0;496;0
WireConnection;497;1;499;0
WireConnection;168;0;452;2
WireConnection;168;1;452;0
WireConnection;438;0;435;0
WireConnection;438;1;441;1
WireConnection;438;2;441;2
WireConnection;438;3;441;3
WireConnection;438;4;441;4
WireConnection;511;0;508;0
WireConnection;511;1;510;0
WireConnection;333;0;334;0
WireConnection;501;0;500;0
WireConnection;501;1;503;0
WireConnection;13;0;12;0
WireConnection;176;0;451;2
WireConnection;176;1;451;0
WireConnection;156;0;151;0
WireConnection;156;1;155;0
WireConnection;169;0;168;0
WireConnection;88;0;80;0
WireConnection;199;0;259;0
WireConnection;199;1;200;0
WireConnection;478;0;486;0
WireConnection;478;1;479;0
WireConnection;512;0;497;0
WireConnection;512;1;511;0
WireConnection;512;2;501;0
WireConnection;512;3;515;0
WireConnection;215;0;157;0
WireConnection;215;1;214;0
WireConnection;215;2;333;0
WireConnection;215;3;13;0
WireConnection;113;0;435;0
WireConnection;113;1;437;1
WireConnection;113;2;437;2
WireConnection;113;3;437;3
WireConnection;113;4;437;4
WireConnection;258;0;255;0
WireConnection;258;1;202;0
WireConnection;130;0;438;0
WireConnection;463;0;462;0
WireConnection;463;1;464;0
WireConnection;61;1;199;0
WireConnection;67;0;435;0
WireConnection;67;1;436;1
WireConnection;67;2;436;2
WireConnection;67;3;436;3
WireConnection;67;4;436;4
WireConnection;122;0;113;0
WireConnection;211;0;157;0
WireConnection;211;1;163;0
WireConnection;211;2;215;0
WireConnection;211;3;169;0
WireConnection;128;0;130;0
WireConnection;128;1;88;0
WireConnection;513;0;478;0
WireConnection;513;1;512;0
WireConnection;175;0;176;0
WireConnection;470;0;463;0
WireConnection;470;1;471;0
WireConnection;198;0;156;0
WireConnection;198;1;258;0
WireConnection;65;1;61;0
WireConnection;65;2;67;0
WireConnection;514;0;512;0
WireConnection;514;1;470;0
WireConnection;212;0;157;0
WireConnection;212;1;171;0
WireConnection;212;2;211;0
WireConnection;212;3;175;0
WireConnection;477;0;513;0
WireConnection;477;1;122;0
WireConnection;131;0;128;0
WireConnection;117;1;198;0
WireConnection;69;0;65;0
WireConnection;76;0;435;0
WireConnection;76;1;78;1
WireConnection;76;2;78;2
WireConnection;76;3;78;3
WireConnection;76;4;78;4
WireConnection;114;1;117;0
WireConnection;114;2;477;0
WireConnection;126;0;127;0
WireConnection;126;1;14;0
WireConnection;126;2;131;0
WireConnection;466;1;514;0
WireConnection;192;0;188;0
WireConnection;192;1;212;0
WireConnection;444;0;435;0
WireConnection;116;0;114;0
WireConnection;63;0;126;0
WireConnection;63;1;64;0
WireConnection;63;2;69;0
WireConnection;129;0;76;0
WireConnection;129;1;73;0
WireConnection;468;0;466;0
WireConnection;468;1;192;0
WireConnection;442;0;444;0
WireConnection;442;1;443;1
WireConnection;442;2;443;2
WireConnection;442;3;443;3
WireConnection;442;4;443;4
WireConnection;121;0;63;0
WireConnection;121;1;115;0
WireConnection;121;2;116;0
WireConnection;79;0;129;0
WireConnection;465;0;466;0
WireConnection;465;1;191;0
WireConnection;143;0;144;0
WireConnection;143;1;145;0
WireConnection;143;2;146;0
WireConnection;469;0;468;0
WireConnection;0;0;121;0
WireConnection;0;3;230;0
WireConnection;0;4;442;0
WireConnection;0;7;79;0
WireConnection;0;9;88;0
WireConnection;0;11;465;0
WireConnection;0;12;469;0
WireConnection;0;14;143;0
ASEEND*/
//CHKSM=2D03535ACC8A585627D99B007536C90E4F0895EE