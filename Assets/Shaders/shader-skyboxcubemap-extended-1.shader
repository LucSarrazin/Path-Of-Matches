Shader "Skybox/Cubemap Extended 1"
{
	Properties
	{
		[StyledBanner(Skybox Cubemap Extended)]_SkyboxExtended("< SkyboxExtended >", Float) = 1
		[StyledCategory(Cubemap Settings, 5, 10)]_Cubemapp("[ Cubemapp ]", Float) = 1
		[NoScaleOffset][StyledTextureSingleLine]_Tex("Cubemap (HDR)", CUBE) = "black" {}
		[Space(10)]_Exposure("Cubemap Exposure", Range( 0 , 8)) = 1
		[Gamma]_TintColor("Cubemap Tint Color", Color) = (0.5,0.5,0.5,1)
		_CubemapPosition("Cubemap Position", Float) = 0
		[StyledCategory(Rotation Settings)]_Rotationn("[ Rotationn ]", Float) = 1
		[Toggle(_ENABLEROTATION_ON)] _EnableRotation("Enable Rotation", Float) = 0
		[IntRange][Space(10)]_Rotation("Rotation", Range( 0 , 360)) = 0
		_RotationSpeed("Rotation Speed", Float) = 1
		[StyledCategory(Fog Settings)]_Fogg("[ Fogg ]", Float) = 1
		[Toggle(_ENABLEFOG_ON)] _EnableFog("Enable Fog", Float) = 0
		[StyledMessage(Info, The fog color is controlled by the fog color set in the Lighting panel., _EnableFog, 1, 10, 0)]_FogMessage("# FogMessage", Float) = 0
		[Space(10)]_FogIntensity("Fog Intensity", Range( 0 , 1)) = 1
		_FogHeight("Fog Height", Range( 0 , 1)) = 1
		_FogSmoothness("Fog Smoothness", Range( 0.01 , 1)) = 0.01
		_FogFill("Fog Fill", Range( 0 , 1)) = 0.5
		[HideInInspector]_Tex_HDR("DecodeInstructions", Vector) = (0,0,0,0)
		[ASEEnd]_FogPosition("Fog Position", Float) = 0

		[Header(Stars Settings)]
		_Stars("Stars Texture", 2D) = "black" {}
		_StarsCutoff("Stars Cutoff",  Range(0, 1)) = 0.08
		_StarsSpeed("Stars Move Speed",  Range(0, 1)) = 0.3 
		_StarsSkyColor("Stars Sky Color", Color) = (0.0,0.2,0.1,1)

		[Header(Horizon Settings)]
		_OffsetHorizon("Horizon Offset",  Range(-1, 1)) = 0
		_HorizonIntensity("Horizon Intensity",  Range(0, 10)) = 3.3
		_SunSet("Sunset/Rise Color", Color) = (1,0.8,1,1)
		_HorizonColorDay("Day Horizon Color", Color) = (0,0.8,1,1)
		_HorizonColorNight("Night Horizon Color", Color) = (0,0.8,1,1)

		[Header(Sun Settings)]
		_SunColor("Sun Color", Color) = (1,1,1,1)
		_SunRadius("Sun Radius",  Range(0, 2)) = 0.1

		[Header(Moon Settings)]
		_MoonTex("Moon Texture", 2D) = "black" {}
		_MoonColor("Moon Color", Color) = (1,1,1,1)
		_MoonRadius("Moon Radius",  Range(0, 2)) = 0.15
		_MoonOffset("Moon Crescent",  Range(-1, 1)) = -0.1

		[Header(Day Sky Settings)]
		_DayTopColor("Day Sky Color Top", Color) = (0.4,1,1,1)
		_DayBottomColor("Day Sky Color Bottom", Color) = (0,0.8,1,1)

		[Header(Main Cloud Settings)]
		_BaseNoise("Base Noise", 2D) = "black" {}
		_Distort("Distort", 2D) = "black" {}
		_SecNoise("Secondary Noise", 2D) = "black" {}
		_BaseNoiseScale("Base Noise Scale",  Range(0, 1)) = 0.2
		_DistortScale("Distort Noise Scale",  Range(0, 1)) = 0.06
		_SecNoiseScale("Secondary Noise Scale",  Range(0, 1)) = 0.05
		_Distortion("Extra Distortion",  Range(0, 1)) = 0.1
		_Speed("Movement Speed",  Range(0, 10)) = 1.4
		_CloudCutoff("Cloud Cutoff",  Range(0, 1)) = 0.3
		_Fuzziness("Cloud Fuzziness",  Range(0, 5)) = 0.04
		_FuzzinessUnder("Cloud Fuzziness Under",  Range(0, 1)) = 0.01
		[Toggle(FUZZY)] _FUZZY("Extra Fuzzy clouds", Float) = 1

		[Header(Day Clouds Settings)]
		_CloudColorDayEdge("Clouds Edge Day", Color) = (1,1,1,1)
		_CloudColorDayMain("Clouds Main Day", Color) = (0.8,0.9,0.8,1)
		_CloudColorDayUnder("Clouds Under Day", Color) = (0.6,0.7,0.6,1)
		_Brightness("Cloud Brightness",  Range(1, 10)) = 2.5
		[Header(Night Sky Settings)]
		_NightTopColor("Night Sky Color Top", Color) = (0,0,0,1)
		_NightBottomColor("Night Sky Color Bottom", Color) = (0,0,0.2,1)

		[Header(Night Clouds Settings)]
		_CloudColorNightEdge("Clouds Edge Night", Color) = (0,1,1,1)
		_CloudColorNightMain("Clouds Main Night", Color) = (0,0.2,0.8,1)
		_CloudColorNightUnder("Clouds Under Night", Color) = (0,0.2,0.6,1)
	}

	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue"="Background" "PreviewType"="Skybox" }
		LOD 100
		Zwrite off
		Blend Off

		Pass
		{
			Lighting on
			// Cull front
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			#pragma shader_feature FUZZY
			#include "UnityCG.cginc"

			struct appdata
			{
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 vertex : POSITION;
				float3 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f
			{
				float3 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD1;
				float4 screenPosition : TEXCOORD4;
				float4 screenSpaceLightPos0 : TEXCOORD5;

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD2;
				float4 ase_texcoord2 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _Stars, _BaseNoise, _Distort, _SecNoise;
			sampler2D  _MoonTex;

			float _SunRadius, _MoonRadius, _MoonOffset, _OffsetHorizon;
			float4 _SunColor, _MoonColor;
			float4 _DayTopColor, _DayBottomColor, _NightBottomColor, _NightTopColor;
			float4 _HorizonColorDay, _HorizonColorNight, _SunSet;
			float _StarsCutoff, _StarsSpeed, _HorizonIntensity;
			float _BaseNoiseScale, _DistortScale, _SecNoiseScale, _Distortion;
			float _Speed, _CloudCutoff, _Fuzziness, _FuzzinessUnder, _Brightness;
			float4 _CloudColorDayEdge, _CloudColorDayMain, _CloudColorDayUnder;
			float4 _CloudColorNightEdge, _CloudColorNightMain, _CloudColorNightUnder, _StarsSkyColor;

			uniform half _Cubemapp;
			uniform half _SkyboxExtended;
			uniform half _Rotationn;
			uniform half _FogMessage;
			uniform half4 _Tex_HDR;
			uniform half _Fogg;
			uniform samplerCUBE _Tex;
			uniform float _CubemapPosition;
			uniform half _Rotation;
			uniform half _RotationSpeed;
			uniform half4 _TintColor;
			uniform half _Exposure;
			uniform float _FogPosition;
			uniform half _FogHeight;
			uniform half _FogSmoothness;
			uniform half _FogFill;
			uniform half _FogIntensity;
			inline half3 DecodeHDR1189( float4 Data )
			{
				return DecodeHDR(Data, _Tex_HDR);
			}

			float4 _MoonTex_ST;
			float4x4 _MoonSpaceMatrix;

			v2f vert ( appdata v )
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.screenPosition = ComputeScreenPos(o.vertex);
				o.screenSpaceLightPos0 = ComputeScreenPos( mul(UNITY_MATRIX_VP,_WorldSpaceLightPos0) );

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float lerpResult268 = lerp( 1.0 , ( unity_OrthoParams.y / unity_OrthoParams.x ) , unity_OrthoParams.w);
				half CAMERA_MODE300 = lerpResult268;
				float3 appendResult1220 = (float3(v.vertex.xyz.x , ( v.vertex.xyz.y * CAMERA_MODE300 ) , v.vertex.xyz.z));
				float3 appendResult1208 = (float3(0.0 , -_CubemapPosition , 0.0));
				half3 VertexPos40_g1 = appendResult1220;
				float3 appendResult74_g1 = (float3(0.0 , VertexPos40_g1.y , 0.0));
				float3 VertexPosRotationAxis50_g1 = appendResult74_g1;
				float3 break84_g1 = VertexPos40_g1;
				float3 appendResult81_g1 = (float3(break84_g1.x , 0.0 , break84_g1.z));
				float3 VertexPosOtherAxis82_g1 = appendResult81_g1;
				half Angle44_g1 = ( 1.0 - radians( ( _Rotation + ( _Time.y * _RotationSpeed ) ) ) );
				#ifdef _ENABLEROTATION_ON
				float3 staticSwitch1164 = ( ( VertexPosRotationAxis50_g1 + ( VertexPosOtherAxis82_g1 * cos( Angle44_g1 ) ) + ( cross( float3(0,1,0) , VertexPosOtherAxis82_g1 ) * sin( Angle44_g1 ) ) ) + appendResult1208 );
				#else
				float3 staticSwitch1164 = ( appendResult1220 + appendResult1208 );
				#endif
				float3 vertexToFrag774 = staticSwitch1164;
				o.ase_texcoord1.xyz = vertexToFrag774;
				
				o.ase_texcoord2 = v.vertex;

				//use this formula
				// float3 dir = normalize(v.vertex.pos);
				// float2 uv2 = new float2(dir.x,dir.z) / acos(-dir.y);
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.w = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}

			fixed4 frag ( v2f i ) : SV_Target
			{
				float horizon = abs((i.uv.y * _HorizonIntensity) - _OffsetHorizon);

				// uv for the sky
				float2 skyUV = ((i.worldPos.xz + -_WorldSpaceLightPos0) / (i.worldPos.y + -_WorldSpaceLightPos0));

				// moving clouds
				float baseNoise = tex2D(_BaseNoise, (skyUV - _Time.x) * _BaseNoiseScale).x;

				float noise1 = tex2D(_Distort, ((skyUV + baseNoise) - (_Time.x * _Speed)) * _DistortScale);
				float noise2 = tex2D(_SecNoise, ((skyUV + (noise1 * _Distortion)) - (_Time.x * (_Speed * 0.5))) * _SecNoiseScale);
				float finalNoise = saturate(noise1 * noise2) * 3 * saturate(i.worldPos.y);

#if FUZZY
				float clouds = saturate(smoothstep(_CloudCutoff * baseNoise, _CloudCutoff * baseNoise + _Fuzziness, finalNoise));
				float cloudsunder = saturate(smoothstep(_CloudCutoff* baseNoise, _CloudCutoff * baseNoise + _FuzzinessUnder + _Fuzziness, noise2) * clouds);

#else
				float clouds = saturate(smoothstep(_CloudCutoff, _CloudCutoff + _Fuzziness, finalNoise));
				float cloudsunder = saturate(smoothstep(_CloudCutoff, _CloudCutoff + _Fuzziness + _FuzzinessUnder , noise2) * clouds);

#endif
				float3 cloudsColored = lerp(_CloudColorDayEdge, lerp(_CloudColorDayUnder, _CloudColorDayMain, cloudsunder), clouds) * clouds;
				float3 cloudsColoredNight = lerp(_CloudColorNightEdge, lerp(_CloudColorNightUnder,_CloudColorNightMain , cloudsunder), clouds) * clouds;
				cloudsColoredNight *= horizon;


				cloudsColored = lerp(cloudsColoredNight, cloudsColored, saturate(_WorldSpaceLightPos0.y)); // lerp the night and day clouds over the light direction
				cloudsColored += (_Brightness * cloudsColored * horizon); // add some extra brightness


				float cloudsNegative = (1 - clouds) * horizon;

				float3 sunAndMoon;
				if( mul(UNITY_MATRIX_V,_WorldSpaceLightPos0).z<0 )// sun is visible
				{
					// sun
					float sun = distance(i.uv.xyz, _WorldSpaceLightPos0);
					float sunDisc = 1 - (sun / _SunRadius);
					sunDisc = saturate(sunDisc * 50);
					float3 sunCol = sunDisc * _SunColor * cloudsNegative;
					
					sunAndMoon = sunCol;
				}
				else// moon is visible
				{
					// (crescent) moon
					float screenAspectRatio = _ScreenParams.x/_ScreenParams.y;
					float2 screenUV = i.screenPosition.xy / i.screenPosition.w;// uv coord for this pixel
					screenUV.x *= screenAspectRatio;// compensates for the aspect ratio
					float2 moonUV = i.screenSpaceLightPos0.xy / i.screenSpaceLightPos0.w;// uv coord for moon center
					moonUV.x *= screenAspectRatio;// compensates for the aspect ratio
					float moon = distance( screenUV , moonUV );//distance(i.uv.xyz, -_WorldSpaceLightPos0);
					//use the same UV used for the crescent for the tex. ?
					float crescentMoon = distance(float3(i.uv.x + _MoonOffset, i.uv.yz), -_WorldSpaceLightPos0);
					float crescentMoonDisc = 1 - (crescentMoon / _MoonRadius);
					crescentMoonDisc = saturate(crescentMoonDisc * 50);
					float moonDisc = 1 - (moon / _MoonRadius);
					moonDisc = saturate(moonDisc * 50);
					moonDisc = saturate(moonDisc - crescentMoonDisc);
					float3 moonTexCol = tex2D( _MoonTex , screenUV * _MoonTex_ST.xy + 0.5f - moonUV + _MoonTex_ST.zw );
					float3 moonCol = moonDisc * moonTexCol * _MoonColor * cloudsNegative;

					sunAndMoon = moonCol;
				}

				//stars
				float3 stars = tex2D(_Stars, skyUV + (_StarsSpeed * _Time.x));
				stars *= saturate(-_WorldSpaceLightPos0.y);
				stars = step(_StarsCutoff, stars);
				stars += (baseNoise * _StarsSkyColor);
				stars *= cloudsNegative;

				// gradient day sky
				float3 gradientDay = lerp(_DayBottomColor, _DayTopColor, saturate(horizon));

				// gradient night sky
				float3 gradientNight = lerp(_NightBottomColor, _NightTopColor, saturate(horizon));

				float3 skyGradients = lerp(gradientNight, gradientDay, saturate(_WorldSpaceLightPos0.y)) * cloudsNegative;

				// horizon glow / sunset/rise
				float sunset = saturate((1 - horizon) * saturate(_WorldSpaceLightPos0.y * 5));

				float3 sunsetColoured = sunset * _SunSet;

				//where the horizon disappears at night.
				float3 horizonGlow = saturate((1 - horizon * 5) * saturate(_WorldSpaceLightPos0.y * 10)) * _HorizonColorDay;// 
				float3 horizonGlowNight = saturate((1 - horizon * 5) * saturate(-_WorldSpaceLightPos0.y * 10)) * _HorizonColorNight;//
				horizonGlow += horizonGlowNight;

				float3 combined = sunAndMoon + sunsetColoured + stars + cloudsColored + horizonGlow;
				// return float4(combined,1);

				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float3 vertexToFrag774 = i.ase_texcoord1.xyz;
				half4 Data1189 = texCUBE( _Tex, vertexToFrag774);
				half3 localDecodeHDR1189 = DecodeHDR1189( Data1189 );
				half4 CUBEMAP222 = ( float4( localDecodeHDR1189 , 0.0 ) * unity_ColorSpaceDouble * _TintColor * _Exposure);
				float lerpResult678 = lerp( saturate( pow( (0.0 + (abs( ( i.ase_texcoord2.xyz.y + -_FogPosition ) ) - 0.0) * (1.0 - 0.0) / (_FogHeight - 0.0)) , ( 1.0 - _FogSmoothness ) ) ) , 0.0 , _FogFill);
				float lerpResult1205 = lerp( 1.0 , lerpResult678 , _FogIntensity);
				half FOG_MASK359 = lerpResult1205;
				#ifdef _ENABLEFOG_ON
				float4 staticSwitch1179 = CUBEMAP222;
				#else
				float4 staticSwitch1179 = CUBEMAP222;
				#endif
				
				finalColor = staticSwitch1179;
				// return finalColor;
				float3 FinalC = combined + finalColor;

				return float4(FinalC,1);
			}

			ENDCG
		}

		//include pass interference.
	}

	
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "PreviewType"="Skybox" }
		LOD 0

		CGINCLUDE
		#pragma target 2.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Off
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		
		Pass
		{
			Name "Unlit"

			CGPROGRAM
			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_VERT_POSITION
			#pragma shader_feature_local _ENABLEFOG_ON
			#pragma shader_feature_local _ENABLEROTATION_ON

			struct appdata
			{
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 vertex : POSITION;
				float4 color : COLOR;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform half _Cubemapp;
			uniform half _SkyboxExtended;
			uniform half _Rotationn;
			uniform half _FogMessage;
			uniform half4 _Tex_HDR;
			uniform half _Fogg;
			uniform samplerCUBE _Tex;
			uniform float _CubemapPosition;
			uniform half _Rotation;
			uniform half _RotationSpeed;
			uniform half4 _TintColor;
			uniform half _Exposure;
			uniform float _FogPosition;
			uniform half _FogHeight;
			uniform half _FogSmoothness;
			uniform half _FogFill;
			uniform half _FogIntensity;
			inline half3 DecodeHDR1189( float4 Data )
			{
				return DecodeHDR(Data, _Tex_HDR);
			}
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float lerpResult268 = lerp( 1.0 , ( unity_OrthoParams.y / unity_OrthoParams.x ) , unity_OrthoParams.w);
				half CAMERA_MODE300 = lerpResult268;
				float3 appendResult1220 = (float3(v.vertex.xyz.x , ( v.vertex.xyz.y * CAMERA_MODE300 ) , v.vertex.xyz.z));
				float3 appendResult1208 = (float3(0.0 , -_CubemapPosition , 0.0));
				half3 VertexPos40_g1 = appendResult1220;
				float3 appendResult74_g1 = (float3(0.0 , VertexPos40_g1.y , 0.0));
				float3 VertexPosRotationAxis50_g1 = appendResult74_g1;
				float3 break84_g1 = VertexPos40_g1;
				float3 appendResult81_g1 = (float3(break84_g1.x , 0.0 , break84_g1.z));
				float3 VertexPosOtherAxis82_g1 = appendResult81_g1;
				half Angle44_g1 = ( 1.0 - radians( ( _Rotation + ( _Time.y * _RotationSpeed ) ) ) );
				#ifdef _ENABLEROTATION_ON
				float3 staticSwitch1164 = ( ( VertexPosRotationAxis50_g1 + ( VertexPosOtherAxis82_g1 * cos( Angle44_g1 ) ) + ( cross( float3(0,1,0) , VertexPosOtherAxis82_g1 ) * sin( Angle44_g1 ) ) ) + appendResult1208 );
				#else
				float3 staticSwitch1164 = ( appendResult1220 + appendResult1208 );
				#endif
				float3 vertexToFrag774 = staticSwitch1164;
				o.ase_texcoord1.xyz = vertexToFrag774;
				
				o.ase_texcoord2 = v.vertex;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.w = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float3 vertexToFrag774 = i.ase_texcoord1.xyz;
				half4 Data1189 = texCUBE( _Tex, vertexToFrag774 );
				half3 localDecodeHDR1189 = DecodeHDR1189( Data1189 );
				half4 CUBEMAP222 = ( float4( localDecodeHDR1189 , 0.0 ) * unity_ColorSpaceDouble * _TintColor * _Exposure );
				float lerpResult678 = lerp( saturate( pow( (0.0 + (abs( ( i.ase_texcoord2.xyz.y + -_FogPosition ) ) - 0.0) * (1.0 - 0.0) / (_FogHeight - 0.0)) , ( 1.0 - _FogSmoothness ) ) ) , 0.0 , _FogFill);
				float lerpResult1205 = lerp( 1.0 , lerpResult678 , _FogIntensity);
				half FOG_MASK359 = lerpResult1205;
				float4 lerpResult317 = lerp( unity_FogColor , CUBEMAP222 , FOG_MASK359);
				#ifdef _ENABLEFOG_ON
				float4 staticSwitch1179 = lerpResult317;
				#else
				float4 staticSwitch1179 = CUBEMAP222;
				#endif
				
				finalColor = staticSwitch1179;
				return finalColor;
			}
			ENDCG
		}
	}

	// CustomEditor "SkyboxExtendedShaderGUI"
	
	Fallback "Skybox/Cubemap"
}