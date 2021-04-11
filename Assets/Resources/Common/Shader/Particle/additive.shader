// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "XGame/Particles/Additive" {
	Properties {
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
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

				#include "UnityCG.cginc"

				sampler2D _MainTex;
				fixed4 _TintColor;
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
				};
			
				float4 _MainTex_ST;

				v2f vert (appdata_t v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.color = 2.0f * _TintColor * v.color;
					o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
					o.vertex.z = o.vertex.z + _ZOffset;
					return o;
				}

				sampler2D_float _CameraDepthTexture;
				float _InvFade;
			
				fixed4 frag (v2f i) : SV_Target
				{
					return tex2D(_MainTex, i.texcoord) * i.color;
				}
				ENDCG 
			}
		}	
		CustomEditor "XGameShaderGUI"
	}
}