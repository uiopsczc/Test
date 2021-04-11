// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "XGame/PostProcess" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_GrayBlend ("Gray blend", Range (0, 1)) = 0
		_RadialBlueBlend ("Radial Blue blend", Range (0, 1)) = 0
		_RadialBlueDist ("Radial Blue Dist", Range (0, 1)) = 0
		_RadialBlueCenter ("Radial Blue Center", Range (0, 1)) = 0
	}
	SubShader {
		Pass {
			Cull Off Lighting Off ZWrite Off ZTest Always

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ GRAY
			#pragma multi_compile __ RADIAL_BLUR

			#include "UnityCG.cginc"

#if defined(GRAY)
			uniform float _GrayBlend;
#endif
#if defined(RADIAL_BLUR)
			uniform float _RadialBlueBlend;
			uniform float _RadialBlueDist;
			uniform float _RadialBlueCenter;
#endif

			uniform sampler2D _MainTex;
			
			struct appdata
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
			};

			v2f vert( appdata v )
			{
				v2f o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			fixed4 frag(v2f i) : COLOR {
				fixed4 result = tex2D(_MainTex, i.uv);

#if defined(RADIAL_BLUR)
				half2 offset = 0.5 - i.uv;
				half2 dir = normalize(offset);
				half2 len = max(length(offset) - _RadialBlueCenter, 0);
				offset = dir * len * _RadialBlueDist;
				fixed3 radial_blur = result.rgb
								   + tex2D(_MainTex, i.uv + offset * 0.05).rgb
								   + tex2D(_MainTex, i.uv + offset * 0.1).rgb
								   + tex2D(_MainTex, i.uv + offset * 0.25).rgb
								   + tex2D(_MainTex, i.uv + offset * 0.2).rgb
								   + tex2D(_MainTex, i.uv + offset * 0.35).rgb
								   + tex2D(_MainTex, i.uv + offset * 0.3).rgb;
				radial_blur = radial_blur / 7;
				result.rgb = lerp(result.rgb, radial_blur, _RadialBlueBlend);
#endif

#if defined(GRAY)
				fixed lum = result.r*.3 + result.g*.59 + result.b*.11;
				fixed3 bw = fixed3( lum, lum, lum );
				result.rgb = lerp(result.rgb, bw, _GrayBlend);
#endif
				return result;
			}
			ENDCG
		}
	}
}