// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Sea Height"
{
	Properties
	{
		_SeaDepthTexture("SeaDepthTexture", 2D) = "white" {}
		_DepthMapRotation("DepthMapRotation", Float) = 0
		_DepthScale("Depth Scale", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha noshadow novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _SeaDepthTexture;
		uniform float _DepthMapRotation;
		uniform float4 DepthMapping;
		uniform float _DepthScale;
		uniform float4 LargeSwells6;
		uniform float timeOffset;
		uniform float4 LargeSwells5;
		uniform float4 LargeSwells4;
		uniform float4 LargeSwells3;
		uniform float4 LargeSwells2;
		uniform float4 LargeSwells1;
		uniform float4 Swells6;
		uniform float4 Swells5;
		uniform float4 Swells4;
		uniform float4 Swells3;
		uniform float4 Swells2;
		uniform float4 Swells1;
		uniform float4 SmallWaves6;
		uniform float4 SmallWaves5;
		uniform float4 SmallWaves4;
		uniform float4 SmallWaves3;
		uniform float4 SmallWaves2;
		uniform float4 SmallWaves1;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float cos69 = cos( ( _DepthMapRotation * UNITY_PI ) );
			float sin69 = sin( ( _DepthMapRotation * UNITY_PI ) );
			float2 rotator69 = mul( i.uv_texcoord - float2( 0.5,0.5 ) , float2x2( cos69 , -sin69 , sin69 , cos69 )) + float2( 0.5,0.5 );
			float clampResult74 = clamp( (DepthMapping.z + (tex2D( _SeaDepthTexture, rotator69 ).r - DepthMapping.x) * (DepthMapping.w - DepthMapping.z) / (DepthMapping.y - DepthMapping.x)) , 0.0 , 1.0 );
			float4 temp_output_3_0_g21 = LargeSwells6;
			float2 normalizeResult12_g21 = normalize( (temp_output_3_0_g21).xy );
			float2 break28_g21 = normalizeResult12_g21;
			float3 ase_worldPos = i.worldPos;
			float3 temp_output_13_0_g1 = ase_worldPos;
			float dotResult21_g21 = dot( normalizeResult12_g21 , (temp_output_13_0_g1).xz );
			float4 break7_g21 = temp_output_3_0_g21;
			float temp_output_10_0_g21 = ( ( 2.0 * UNITY_PI ) / break7_g21.w );
			float temp_output_10_0_g1 = timeOffset;
			float temp_output_25_0_g21 = ( ( dotResult21_g21 - ( sqrt( ( 9.81 / temp_output_10_0_g21 ) ) * ( temp_output_10_0_g1 + _Time.y ) ) ) * temp_output_10_0_g21 );
			float temp_output_36_0_g21 = cos( temp_output_25_0_g21 );
			float temp_output_26_0_g21 = ( break7_g21.z / temp_output_10_0_g21 );
			float temp_output_34_0_g21 = sin( temp_output_25_0_g21 );
			float3 appendResult48_g21 = (float3(( break28_g21.x * temp_output_36_0_g21 * temp_output_26_0_g21 ) , ( temp_output_26_0_g21 * temp_output_34_0_g21 ) , ( break28_g21.y * temp_output_26_0_g21 * temp_output_36_0_g21 )));
			float4 temp_output_3_0_g17 = LargeSwells5;
			float2 normalizeResult12_g17 = normalize( (temp_output_3_0_g17).xy );
			float2 break28_g17 = normalizeResult12_g17;
			float dotResult21_g17 = dot( normalizeResult12_g17 , (temp_output_13_0_g1).xz );
			float4 break7_g17 = temp_output_3_0_g17;
			float temp_output_10_0_g17 = ( ( 2.0 * UNITY_PI ) / break7_g17.w );
			float temp_output_25_0_g17 = ( ( dotResult21_g17 - ( sqrt( ( 9.81 / temp_output_10_0_g17 ) ) * ( temp_output_10_0_g1 + _Time.y ) ) ) * temp_output_10_0_g17 );
			float temp_output_36_0_g17 = cos( temp_output_25_0_g17 );
			float temp_output_26_0_g17 = ( break7_g17.z / temp_output_10_0_g17 );
			float temp_output_34_0_g17 = sin( temp_output_25_0_g17 );
			float3 appendResult48_g17 = (float3(( break28_g17.x * temp_output_36_0_g17 * temp_output_26_0_g17 ) , ( temp_output_26_0_g17 * temp_output_34_0_g17 ) , ( break28_g17.y * temp_output_26_0_g17 * temp_output_36_0_g17 )));
			float4 temp_output_3_0_g18 = LargeSwells4;
			float2 normalizeResult12_g18 = normalize( (temp_output_3_0_g18).xy );
			float2 break28_g18 = normalizeResult12_g18;
			float dotResult21_g18 = dot( normalizeResult12_g18 , (temp_output_13_0_g1).xz );
			float4 break7_g18 = temp_output_3_0_g18;
			float temp_output_10_0_g18 = ( ( 2.0 * UNITY_PI ) / break7_g18.w );
			float temp_output_25_0_g18 = ( ( dotResult21_g18 - ( sqrt( ( 9.81 / temp_output_10_0_g18 ) ) * ( temp_output_10_0_g1 + _Time.y ) ) ) * temp_output_10_0_g18 );
			float temp_output_36_0_g18 = cos( temp_output_25_0_g18 );
			float temp_output_26_0_g18 = ( break7_g18.z / temp_output_10_0_g18 );
			float temp_output_34_0_g18 = sin( temp_output_25_0_g18 );
			float3 appendResult48_g18 = (float3(( break28_g18.x * temp_output_36_0_g18 * temp_output_26_0_g18 ) , ( temp_output_26_0_g18 * temp_output_34_0_g18 ) , ( break28_g18.y * temp_output_26_0_g18 * temp_output_36_0_g18 )));
			float4 temp_output_3_0_g19 = LargeSwells3;
			float2 normalizeResult12_g19 = normalize( (temp_output_3_0_g19).xy );
			float2 break28_g19 = normalizeResult12_g19;
			float dotResult21_g19 = dot( normalizeResult12_g19 , (temp_output_13_0_g1).xz );
			float4 break7_g19 = temp_output_3_0_g19;
			float temp_output_10_0_g19 = ( ( 2.0 * UNITY_PI ) / break7_g19.w );
			float temp_output_25_0_g19 = ( ( dotResult21_g19 - ( sqrt( ( 9.81 / temp_output_10_0_g19 ) ) * ( temp_output_10_0_g1 + _Time.y ) ) ) * temp_output_10_0_g19 );
			float temp_output_36_0_g19 = cos( temp_output_25_0_g19 );
			float temp_output_26_0_g19 = ( break7_g19.z / temp_output_10_0_g19 );
			float temp_output_34_0_g19 = sin( temp_output_25_0_g19 );
			float3 appendResult48_g19 = (float3(( break28_g19.x * temp_output_36_0_g19 * temp_output_26_0_g19 ) , ( temp_output_26_0_g19 * temp_output_34_0_g19 ) , ( break28_g19.y * temp_output_26_0_g19 * temp_output_36_0_g19 )));
			float4 temp_output_3_0_g20 = LargeSwells2;
			float2 normalizeResult12_g20 = normalize( (temp_output_3_0_g20).xy );
			float2 break28_g20 = normalizeResult12_g20;
			float dotResult21_g20 = dot( normalizeResult12_g20 , (temp_output_13_0_g1).xz );
			float4 break7_g20 = temp_output_3_0_g20;
			float temp_output_10_0_g20 = ( ( 2.0 * UNITY_PI ) / break7_g20.w );
			float temp_output_25_0_g20 = ( ( dotResult21_g20 - ( sqrt( ( 9.81 / temp_output_10_0_g20 ) ) * ( temp_output_10_0_g1 + _Time.y ) ) ) * temp_output_10_0_g20 );
			float temp_output_36_0_g20 = cos( temp_output_25_0_g20 );
			float temp_output_26_0_g20 = ( break7_g20.z / temp_output_10_0_g20 );
			float temp_output_34_0_g20 = sin( temp_output_25_0_g20 );
			float3 appendResult48_g20 = (float3(( break28_g20.x * temp_output_36_0_g20 * temp_output_26_0_g20 ) , ( temp_output_26_0_g20 * temp_output_34_0_g20 ) , ( break28_g20.y * temp_output_26_0_g20 * temp_output_36_0_g20 )));
			float4 temp_output_3_0_g22 = LargeSwells1;
			float2 normalizeResult12_g22 = normalize( (temp_output_3_0_g22).xy );
			float2 break28_g22 = normalizeResult12_g22;
			float dotResult21_g22 = dot( normalizeResult12_g22 , (temp_output_13_0_g1).xz );
			float4 break7_g22 = temp_output_3_0_g22;
			float temp_output_10_0_g22 = ( ( 2.0 * UNITY_PI ) / break7_g22.w );
			float temp_output_25_0_g22 = ( ( dotResult21_g22 - ( sqrt( ( 9.81 / temp_output_10_0_g22 ) ) * ( temp_output_10_0_g1 + _Time.y ) ) ) * temp_output_10_0_g22 );
			float temp_output_36_0_g22 = cos( temp_output_25_0_g22 );
			float temp_output_26_0_g22 = ( break7_g22.z / temp_output_10_0_g22 );
			float temp_output_34_0_g22 = sin( temp_output_25_0_g22 );
			float3 appendResult48_g22 = (float3(( break28_g22.x * temp_output_36_0_g22 * temp_output_26_0_g22 ) , ( temp_output_26_0_g22 * temp_output_34_0_g22 ) , ( break28_g22.y * temp_output_26_0_g22 * temp_output_36_0_g22 )));
			float4 temp_output_3_0_g38 = Swells6;
			float2 normalizeResult12_g38 = normalize( (temp_output_3_0_g38).xy );
			float2 break28_g38 = normalizeResult12_g38;
			float3 temp_output_13_0_g33 = ase_worldPos;
			float dotResult21_g38 = dot( normalizeResult12_g38 , (temp_output_13_0_g33).xz );
			float4 break7_g38 = temp_output_3_0_g38;
			float temp_output_10_0_g38 = ( ( 2.0 * UNITY_PI ) / break7_g38.w );
			float temp_output_10_0_g33 = timeOffset;
			float temp_output_25_0_g38 = ( ( dotResult21_g38 - ( sqrt( ( 9.81 / temp_output_10_0_g38 ) ) * ( temp_output_10_0_g33 + _Time.y ) ) ) * temp_output_10_0_g38 );
			float temp_output_36_0_g38 = cos( temp_output_25_0_g38 );
			float temp_output_26_0_g38 = ( break7_g38.z / temp_output_10_0_g38 );
			float temp_output_34_0_g38 = sin( temp_output_25_0_g38 );
			float3 appendResult48_g38 = (float3(( break28_g38.x * temp_output_36_0_g38 * temp_output_26_0_g38 ) , ( temp_output_26_0_g38 * temp_output_34_0_g38 ) , ( break28_g38.y * temp_output_26_0_g38 * temp_output_36_0_g38 )));
			float4 temp_output_3_0_g34 = Swells5;
			float2 normalizeResult12_g34 = normalize( (temp_output_3_0_g34).xy );
			float2 break28_g34 = normalizeResult12_g34;
			float dotResult21_g34 = dot( normalizeResult12_g34 , (temp_output_13_0_g33).xz );
			float4 break7_g34 = temp_output_3_0_g34;
			float temp_output_10_0_g34 = ( ( 2.0 * UNITY_PI ) / break7_g34.w );
			float temp_output_25_0_g34 = ( ( dotResult21_g34 - ( sqrt( ( 9.81 / temp_output_10_0_g34 ) ) * ( temp_output_10_0_g33 + _Time.y ) ) ) * temp_output_10_0_g34 );
			float temp_output_36_0_g34 = cos( temp_output_25_0_g34 );
			float temp_output_26_0_g34 = ( break7_g34.z / temp_output_10_0_g34 );
			float temp_output_34_0_g34 = sin( temp_output_25_0_g34 );
			float3 appendResult48_g34 = (float3(( break28_g34.x * temp_output_36_0_g34 * temp_output_26_0_g34 ) , ( temp_output_26_0_g34 * temp_output_34_0_g34 ) , ( break28_g34.y * temp_output_26_0_g34 * temp_output_36_0_g34 )));
			float4 temp_output_3_0_g35 = Swells4;
			float2 normalizeResult12_g35 = normalize( (temp_output_3_0_g35).xy );
			float2 break28_g35 = normalizeResult12_g35;
			float dotResult21_g35 = dot( normalizeResult12_g35 , (temp_output_13_0_g33).xz );
			float4 break7_g35 = temp_output_3_0_g35;
			float temp_output_10_0_g35 = ( ( 2.0 * UNITY_PI ) / break7_g35.w );
			float temp_output_25_0_g35 = ( ( dotResult21_g35 - ( sqrt( ( 9.81 / temp_output_10_0_g35 ) ) * ( temp_output_10_0_g33 + _Time.y ) ) ) * temp_output_10_0_g35 );
			float temp_output_36_0_g35 = cos( temp_output_25_0_g35 );
			float temp_output_26_0_g35 = ( break7_g35.z / temp_output_10_0_g35 );
			float temp_output_34_0_g35 = sin( temp_output_25_0_g35 );
			float3 appendResult48_g35 = (float3(( break28_g35.x * temp_output_36_0_g35 * temp_output_26_0_g35 ) , ( temp_output_26_0_g35 * temp_output_34_0_g35 ) , ( break28_g35.y * temp_output_26_0_g35 * temp_output_36_0_g35 )));
			float4 temp_output_3_0_g36 = Swells3;
			float2 normalizeResult12_g36 = normalize( (temp_output_3_0_g36).xy );
			float2 break28_g36 = normalizeResult12_g36;
			float dotResult21_g36 = dot( normalizeResult12_g36 , (temp_output_13_0_g33).xz );
			float4 break7_g36 = temp_output_3_0_g36;
			float temp_output_10_0_g36 = ( ( 2.0 * UNITY_PI ) / break7_g36.w );
			float temp_output_25_0_g36 = ( ( dotResult21_g36 - ( sqrt( ( 9.81 / temp_output_10_0_g36 ) ) * ( temp_output_10_0_g33 + _Time.y ) ) ) * temp_output_10_0_g36 );
			float temp_output_36_0_g36 = cos( temp_output_25_0_g36 );
			float temp_output_26_0_g36 = ( break7_g36.z / temp_output_10_0_g36 );
			float temp_output_34_0_g36 = sin( temp_output_25_0_g36 );
			float3 appendResult48_g36 = (float3(( break28_g36.x * temp_output_36_0_g36 * temp_output_26_0_g36 ) , ( temp_output_26_0_g36 * temp_output_34_0_g36 ) , ( break28_g36.y * temp_output_26_0_g36 * temp_output_36_0_g36 )));
			float4 temp_output_3_0_g37 = Swells2;
			float2 normalizeResult12_g37 = normalize( (temp_output_3_0_g37).xy );
			float2 break28_g37 = normalizeResult12_g37;
			float dotResult21_g37 = dot( normalizeResult12_g37 , (temp_output_13_0_g33).xz );
			float4 break7_g37 = temp_output_3_0_g37;
			float temp_output_10_0_g37 = ( ( 2.0 * UNITY_PI ) / break7_g37.w );
			float temp_output_25_0_g37 = ( ( dotResult21_g37 - ( sqrt( ( 9.81 / temp_output_10_0_g37 ) ) * ( temp_output_10_0_g33 + _Time.y ) ) ) * temp_output_10_0_g37 );
			float temp_output_36_0_g37 = cos( temp_output_25_0_g37 );
			float temp_output_26_0_g37 = ( break7_g37.z / temp_output_10_0_g37 );
			float temp_output_34_0_g37 = sin( temp_output_25_0_g37 );
			float3 appendResult48_g37 = (float3(( break28_g37.x * temp_output_36_0_g37 * temp_output_26_0_g37 ) , ( temp_output_26_0_g37 * temp_output_34_0_g37 ) , ( break28_g37.y * temp_output_26_0_g37 * temp_output_36_0_g37 )));
			float4 temp_output_3_0_g39 = Swells1;
			float2 normalizeResult12_g39 = normalize( (temp_output_3_0_g39).xy );
			float2 break28_g39 = normalizeResult12_g39;
			float dotResult21_g39 = dot( normalizeResult12_g39 , (temp_output_13_0_g33).xz );
			float4 break7_g39 = temp_output_3_0_g39;
			float temp_output_10_0_g39 = ( ( 2.0 * UNITY_PI ) / break7_g39.w );
			float temp_output_25_0_g39 = ( ( dotResult21_g39 - ( sqrt( ( 9.81 / temp_output_10_0_g39 ) ) * ( temp_output_10_0_g33 + _Time.y ) ) ) * temp_output_10_0_g39 );
			float temp_output_36_0_g39 = cos( temp_output_25_0_g39 );
			float temp_output_26_0_g39 = ( break7_g39.z / temp_output_10_0_g39 );
			float temp_output_34_0_g39 = sin( temp_output_25_0_g39 );
			float3 appendResult48_g39 = (float3(( break28_g39.x * temp_output_36_0_g39 * temp_output_26_0_g39 ) , ( temp_output_26_0_g39 * temp_output_34_0_g39 ) , ( break28_g39.y * temp_output_26_0_g39 * temp_output_36_0_g39 )));
			float4 temp_output_3_0_g45 = SmallWaves6;
			float2 normalizeResult12_g45 = normalize( (temp_output_3_0_g45).xy );
			float2 break28_g45 = normalizeResult12_g45;
			float3 temp_output_13_0_g40 = ase_worldPos;
			float dotResult21_g45 = dot( normalizeResult12_g45 , (temp_output_13_0_g40).xz );
			float4 break7_g45 = temp_output_3_0_g45;
			float temp_output_10_0_g45 = ( ( 2.0 * UNITY_PI ) / break7_g45.w );
			float temp_output_10_0_g40 = timeOffset;
			float temp_output_25_0_g45 = ( ( dotResult21_g45 - ( sqrt( ( 9.81 / temp_output_10_0_g45 ) ) * ( temp_output_10_0_g40 + _Time.y ) ) ) * temp_output_10_0_g45 );
			float temp_output_36_0_g45 = cos( temp_output_25_0_g45 );
			float temp_output_26_0_g45 = ( break7_g45.z / temp_output_10_0_g45 );
			float temp_output_34_0_g45 = sin( temp_output_25_0_g45 );
			float3 appendResult48_g45 = (float3(( break28_g45.x * temp_output_36_0_g45 * temp_output_26_0_g45 ) , ( temp_output_26_0_g45 * temp_output_34_0_g45 ) , ( break28_g45.y * temp_output_26_0_g45 * temp_output_36_0_g45 )));
			float4 temp_output_3_0_g41 = SmallWaves5;
			float2 normalizeResult12_g41 = normalize( (temp_output_3_0_g41).xy );
			float2 break28_g41 = normalizeResult12_g41;
			float dotResult21_g41 = dot( normalizeResult12_g41 , (temp_output_13_0_g40).xz );
			float4 break7_g41 = temp_output_3_0_g41;
			float temp_output_10_0_g41 = ( ( 2.0 * UNITY_PI ) / break7_g41.w );
			float temp_output_25_0_g41 = ( ( dotResult21_g41 - ( sqrt( ( 9.81 / temp_output_10_0_g41 ) ) * ( temp_output_10_0_g40 + _Time.y ) ) ) * temp_output_10_0_g41 );
			float temp_output_36_0_g41 = cos( temp_output_25_0_g41 );
			float temp_output_26_0_g41 = ( break7_g41.z / temp_output_10_0_g41 );
			float temp_output_34_0_g41 = sin( temp_output_25_0_g41 );
			float3 appendResult48_g41 = (float3(( break28_g41.x * temp_output_36_0_g41 * temp_output_26_0_g41 ) , ( temp_output_26_0_g41 * temp_output_34_0_g41 ) , ( break28_g41.y * temp_output_26_0_g41 * temp_output_36_0_g41 )));
			float4 temp_output_3_0_g42 = SmallWaves4;
			float2 normalizeResult12_g42 = normalize( (temp_output_3_0_g42).xy );
			float2 break28_g42 = normalizeResult12_g42;
			float dotResult21_g42 = dot( normalizeResult12_g42 , (temp_output_13_0_g40).xz );
			float4 break7_g42 = temp_output_3_0_g42;
			float temp_output_10_0_g42 = ( ( 2.0 * UNITY_PI ) / break7_g42.w );
			float temp_output_25_0_g42 = ( ( dotResult21_g42 - ( sqrt( ( 9.81 / temp_output_10_0_g42 ) ) * ( temp_output_10_0_g40 + _Time.y ) ) ) * temp_output_10_0_g42 );
			float temp_output_36_0_g42 = cos( temp_output_25_0_g42 );
			float temp_output_26_0_g42 = ( break7_g42.z / temp_output_10_0_g42 );
			float temp_output_34_0_g42 = sin( temp_output_25_0_g42 );
			float3 appendResult48_g42 = (float3(( break28_g42.x * temp_output_36_0_g42 * temp_output_26_0_g42 ) , ( temp_output_26_0_g42 * temp_output_34_0_g42 ) , ( break28_g42.y * temp_output_26_0_g42 * temp_output_36_0_g42 )));
			float4 temp_output_3_0_g43 = SmallWaves3;
			float2 normalizeResult12_g43 = normalize( (temp_output_3_0_g43).xy );
			float2 break28_g43 = normalizeResult12_g43;
			float dotResult21_g43 = dot( normalizeResult12_g43 , (temp_output_13_0_g40).xz );
			float4 break7_g43 = temp_output_3_0_g43;
			float temp_output_10_0_g43 = ( ( 2.0 * UNITY_PI ) / break7_g43.w );
			float temp_output_25_0_g43 = ( ( dotResult21_g43 - ( sqrt( ( 9.81 / temp_output_10_0_g43 ) ) * ( temp_output_10_0_g40 + _Time.y ) ) ) * temp_output_10_0_g43 );
			float temp_output_36_0_g43 = cos( temp_output_25_0_g43 );
			float temp_output_26_0_g43 = ( break7_g43.z / temp_output_10_0_g43 );
			float temp_output_34_0_g43 = sin( temp_output_25_0_g43 );
			float3 appendResult48_g43 = (float3(( break28_g43.x * temp_output_36_0_g43 * temp_output_26_0_g43 ) , ( temp_output_26_0_g43 * temp_output_34_0_g43 ) , ( break28_g43.y * temp_output_26_0_g43 * temp_output_36_0_g43 )));
			float4 temp_output_3_0_g44 = SmallWaves2;
			float2 normalizeResult12_g44 = normalize( (temp_output_3_0_g44).xy );
			float2 break28_g44 = normalizeResult12_g44;
			float dotResult21_g44 = dot( normalizeResult12_g44 , (temp_output_13_0_g40).xz );
			float4 break7_g44 = temp_output_3_0_g44;
			float temp_output_10_0_g44 = ( ( 2.0 * UNITY_PI ) / break7_g44.w );
			float temp_output_25_0_g44 = ( ( dotResult21_g44 - ( sqrt( ( 9.81 / temp_output_10_0_g44 ) ) * ( temp_output_10_0_g40 + _Time.y ) ) ) * temp_output_10_0_g44 );
			float temp_output_36_0_g44 = cos( temp_output_25_0_g44 );
			float temp_output_26_0_g44 = ( break7_g44.z / temp_output_10_0_g44 );
			float temp_output_34_0_g44 = sin( temp_output_25_0_g44 );
			float3 appendResult48_g44 = (float3(( break28_g44.x * temp_output_36_0_g44 * temp_output_26_0_g44 ) , ( temp_output_26_0_g44 * temp_output_34_0_g44 ) , ( break28_g44.y * temp_output_26_0_g44 * temp_output_36_0_g44 )));
			float4 temp_output_3_0_g46 = SmallWaves1;
			float2 normalizeResult12_g46 = normalize( (temp_output_3_0_g46).xy );
			float2 break28_g46 = normalizeResult12_g46;
			float dotResult21_g46 = dot( normalizeResult12_g46 , (temp_output_13_0_g40).xz );
			float4 break7_g46 = temp_output_3_0_g46;
			float temp_output_10_0_g46 = ( ( 2.0 * UNITY_PI ) / break7_g46.w );
			float temp_output_25_0_g46 = ( ( dotResult21_g46 - ( sqrt( ( 9.81 / temp_output_10_0_g46 ) ) * ( temp_output_10_0_g40 + _Time.y ) ) ) * temp_output_10_0_g46 );
			float temp_output_36_0_g46 = cos( temp_output_25_0_g46 );
			float temp_output_26_0_g46 = ( break7_g46.z / temp_output_10_0_g46 );
			float temp_output_34_0_g46 = sin( temp_output_25_0_g46 );
			float3 appendResult48_g46 = (float3(( break28_g46.x * temp_output_36_0_g46 * temp_output_26_0_g46 ) , ( temp_output_26_0_g46 * temp_output_34_0_g46 ) , ( break28_g46.y * temp_output_26_0_g46 * temp_output_36_0_g46 )));
			float temp_output_58_0 = ( ( temp_output_26_0_g21 + temp_output_26_0_g17 + temp_output_26_0_g18 + temp_output_26_0_g19 + temp_output_26_0_g20 + temp_output_26_0_g22 ) + ( temp_output_26_0_g38 + temp_output_26_0_g34 + temp_output_26_0_g35 + temp_output_26_0_g36 + temp_output_26_0_g37 + temp_output_26_0_g39 ) + ( temp_output_26_0_g45 + temp_output_26_0_g41 + temp_output_26_0_g42 + temp_output_26_0_g43 + temp_output_26_0_g44 + temp_output_26_0_g46 ) );
			float3 temp_cast_0 = ((0.0 + (( pow( ( 1.0 - clampResult74 ) , _DepthScale ) * (( ( appendResult48_g21 + appendResult48_g17 + appendResult48_g18 + appendResult48_g19 + appendResult48_g20 + appendResult48_g22 ) + ( appendResult48_g38 + appendResult48_g34 + appendResult48_g35 + appendResult48_g36 + appendResult48_g37 + appendResult48_g39 ) + ( appendResult48_g45 + appendResult48_g41 + appendResult48_g42 + appendResult48_g43 + appendResult48_g44 + appendResult48_g46 ) )).y ) - -temp_output_58_0) * (1.0 - 0.0) / (temp_output_58_0 - -temp_output_58_0))).xxx;
			o.Albedo = temp_cast_0;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15900
7;76;1906;997;3862.024;1733.631;4.101521;True;False
Node;AmplifyShaderEditor.RangedFloatNode;71;-772.5158,-528.9332;Float;False;Property;_DepthMapRotation;DepthMapRotation;1;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;70;-483.8035,-532.9394;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;68;-515.8525,-790.67;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;13;-1389.053,1146.878;Float;False;Global;LargeSwells4;Large Swells 4;16;0;Create;True;0;0;False;0;0,0,0,0;5,-0.7410001,0.075,121.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RotatorNode;69;-203.3705,-670.4847;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;10;-1427.009,897.6777;Float;False;Global;LargeSwells3;Large Swells 3;17;0;Create;True;0;0;False;0;0,0,0,0;5,0.247,0.045,166.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;5;-1401.1,454.2461;Float;False;Global;LargeSwells1;Large Swells 1;19;0;Create;True;0;0;False;0;0,0,0,0;5,0.7410001,0.075,124.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;47;-935.745,-137.0999;Float;False;Global;timeOffset;timeOffset;21;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;16;-1414.962,1590.31;Float;False;Global;LargeSwells6;Large Swells 6;14;0;Create;True;0;0;False;0;0,0,0,0;5,-0.494,0.045,181.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;8;-1399.578,647.6104;Float;False;Global;LargeSwells2;Large Swells 2;18;0;Create;True;0;0;False;0;0,0,0,0;5,0.494,0.06,148.5;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;60;-560.7251,-341.6531;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector4Node;14;-1387.531,1340.242;Float;False;Global;LargeSwells5;Large Swells 5;15;0;Create;True;0;0;False;0;0,0,0,0;5,-0.988,0.06,153;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;23;-75.10001,982.8809;Float;False;Global;Swells3;Swells 3;10;0;Create;True;0;0;False;0;0,0,0,0;1,0.495,0.06,77;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;20;-49.19101,539.45;Float;False;Global;Swells1;Swells 1;12;0;Create;True;0;0;False;0;0,0,0,0;1,0.165,0.08,45;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;27;-35.62201,1425.446;Float;False;Global;Swells5;Swells 5;8;0;Create;True;0;0;False;0;0,0,0,0;1,-0.495,0.07,63;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;26;-37.144,1232.082;Float;False;Global;Swells4;Swells 4;9;0;Create;True;0;0;False;0;0,0,0,0;1,-0.33,0.08,42;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;1;-362.2872,202.4146;Float;False;WaveBlock;-1;;1;daa6ec5b9e2ba324f8ded38a2d863a12;0;10;11;FLOAT3;1,0,0;False;12;FLOAT3;0,0,1;False;13;FLOAT3;0,0,0;False;10;FLOAT;0;False;15;FLOAT4;0,0,0,0;False;16;FLOAT4;0,0,0,0;False;17;FLOAT4;0,0,0,0;False;18;FLOAT4;0,0,0,0;False;19;FLOAT4;0,0,0,0;False;20;FLOAT4;0,0,0,0;False;4;FLOAT;22;FLOAT3;3;FLOAT3;0;FLOAT3;2
Node;AmplifyShaderEditor.SamplerNode;67;33.97545,-693.8272;Float;True;Property;_SeaDepthTexture;SeaDepthTexture;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;21;-47.66901,732.8138;Float;False;Global;Swells2;Swells 2;11;0;Create;True;0;0;False;0;0,0,0,0;1,0.33,0.07,64;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;73;170.4773,-473.0508;Float;False;Global;DepthMapping;Depth Mapping;22;0;Create;True;0;0;False;0;0,0,0,0;0.2,1,0,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;29;-63.05302,1675.514;Float;False;Global;Swells6;Swells 6;7;0;Create;True;0;0;False;0;0,0,0,0;1,-0.66,0.06,79;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;35;942.9023,644.5129;Float;False;Global;SmallWaves1;Small Waves 1;5;0;Create;True;0;0;False;0;0,0,0,0;1,0.45,0.088,16.8;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;44;929.0403,1780.577;Float;False;Global;SmallWaves6;Small Waves 6;19;0;Create;True;0;0;False;0;0,0,0,0;1,-0.6,0.072,32;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;42;956.4713,1530.509;Float;False;Global;SmallWaves5;Small Waves 5;1;0;Create;True;0;0;False;0;0,0,0,0;1,-0.45,0.08000001,26.4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;72;574.0997,-660.1688;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;41;954.9493,1337.145;Float;False;Global;SmallWaves4;Small Waves 4;2;0;Create;True;0;0;False;0;0,0,0,0;1,-0.3,0.088,17.6;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;38;916.9935,1087.944;Float;False;Global;SmallWaves3;Small Waves 3;3;0;Create;True;0;0;False;0;0,0,0,0;1,0,0.072,30.4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;31;695.4133,231.2488;Float;False;WaveBlock;-1;;33;daa6ec5b9e2ba324f8ded38a2d863a12;0;10;11;FLOAT3;1,0,0;False;12;FLOAT3;0,0,1;False;13;FLOAT3;0,0,0;False;10;FLOAT;0;False;15;FLOAT4;0,0,0,0;False;16;FLOAT4;0,0,0,0;False;17;FLOAT4;0,0,0,0;False;18;FLOAT4;0,0,0,0;False;19;FLOAT4;0,0,0,0;False;20;FLOAT4;0,0,0,0;False;4;FLOAT;22;FLOAT3;3;FLOAT3;0;FLOAT3;2
Node;AmplifyShaderEditor.Vector4Node;36;944.4243,837.8769;Float;False;Global;SmallWaves2;Small Waves 2;4;0;Create;True;0;0;False;0;0,0,0,0;1,0.3,0.08000001,24.8;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;32;1439.807,332.8813;Float;False;WaveBlock;-1;;40;daa6ec5b9e2ba324f8ded38a2d863a12;0;10;11;FLOAT3;1,0,0;False;12;FLOAT3;0,0,1;False;13;FLOAT3;0,0,0;False;10;FLOAT;0;False;15;FLOAT4;0,0,0,0;False;16;FLOAT4;0,0,0,0;False;17;FLOAT4;0,0,0,0;False;18;FLOAT4;0,0,0,0;False;19;FLOAT4;0,0,0,0;False;20;FLOAT4;0,0,0,0;False;4;FLOAT;22;FLOAT3;3;FLOAT3;0;FLOAT3;2
Node;AmplifyShaderEditor.ClampOpNode;74;848.2438,-651.8416;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;77;975.6437,-466.4416;Float;False;Property;_DepthScale;Depth Scale;2;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;75;1038.043,-628.9416;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;46;1731.324,203.6951;Float;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SwizzleNode;66;1821.224,-60.58905;Float;False;FLOAT;1;1;2;2;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;76;1273.344,-587.3416;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;58;1495.642,-219.7865;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;1998.364,-128.9808;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;59;1970.658,-217.3918;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;57;2276.483,-443.4395;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2678.528,-200.3797;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;Sea Height;False;False;False;False;False;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;10;10;500;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;70;0;71;0
WireConnection;69;0;68;0
WireConnection;69;2;70;0
WireConnection;1;13;60;0
WireConnection;1;10;47;0
WireConnection;1;15;5;0
WireConnection;1;16;8;0
WireConnection;1;17;10;0
WireConnection;1;18;13;0
WireConnection;1;19;14;0
WireConnection;1;20;16;0
WireConnection;67;1;69;0
WireConnection;72;0;67;1
WireConnection;72;1;73;1
WireConnection;72;2;73;2
WireConnection;72;3;73;3
WireConnection;72;4;73;4
WireConnection;31;11;1;0
WireConnection;31;12;1;2
WireConnection;31;13;60;0
WireConnection;31;10;47;0
WireConnection;31;15;20;0
WireConnection;31;16;21;0
WireConnection;31;17;23;0
WireConnection;31;18;26;0
WireConnection;31;19;27;0
WireConnection;31;20;29;0
WireConnection;32;11;31;0
WireConnection;32;12;31;2
WireConnection;32;13;60;0
WireConnection;32;10;47;0
WireConnection;32;15;35;0
WireConnection;32;16;36;0
WireConnection;32;17;38;0
WireConnection;32;18;41;0
WireConnection;32;19;42;0
WireConnection;32;20;44;0
WireConnection;74;0;72;0
WireConnection;75;0;74;0
WireConnection;46;0;1;3
WireConnection;46;1;31;3
WireConnection;46;2;32;3
WireConnection;66;0;46;0
WireConnection;76;0;75;0
WireConnection;76;1;77;0
WireConnection;58;0;1;22
WireConnection;58;1;31;22
WireConnection;58;2;32;22
WireConnection;78;0;76;0
WireConnection;78;1;66;0
WireConnection;59;0;58;0
WireConnection;57;0;78;0
WireConnection;57;1;59;0
WireConnection;57;2;58;0
WireConnection;0;0;57;0
ASEEND*/
//CHKSM=9C9C8E2FE7E60C3A6F188B6FC97BE5DDD0A99C54