// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "XGame/Particles_2Tex/Additive" {
	Properties {
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_TintColor2 ("Tint Color2", Color) = (0.5,0.5,0.5,0.5)
		_MainTex2 ("Particle Texture2", 2D) = "white" {}
		_speed_u ("_speed_u", float) = 0
		_speed_v ("_speed_v", float) = 0
		_ZOffset ("Z Offset", Float) = 0
	}

	Category {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane"}
		Blend SrcAlpha One
		ColorMask RGB
		Cull Off Lighting Off ZWrite Off
	
		SubShader {
			Pass {
		
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_particles
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

				sampler2D _MainTex;
				sampler2D _MainTex2;
				fixed4 _TintColor;
				fixed4 _TintColor2;
				float _speed_u;
				float _speed_v;
				float _ZOffset;
			
				struct appdata_t {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
					float2 texcoord2 : TEXCOORD1;
				};
			
				float4 _MainTex_ST;
				float4 _MainTex2_ST;

				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = 4.0f * _TintColor * _TintColor2 * v.color;
					o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
					o.texcoord2 = TRANSFORM_TEX(v.texcoord,_MainTex2) + float2(_speed_u, _speed_v) * _Time.y % 1;
					o.vertex.z = o.vertex.z + _ZOffset;
					return o;
				}

				sampler2D_float _CameraDepthTexture;
				float _InvFade;
			
				fixed4 frag (v2f i) : SV_Target
				{
					return tex2D(_MainTex, i.texcoord) * tex2D(_MainTex2, i.texcoord2) * i.color;
				}
				ENDCG 
			}
		}	
		CustomEditor "XGameShaderGUI"
	}
}