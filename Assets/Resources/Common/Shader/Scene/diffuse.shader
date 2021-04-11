Shader "XGame/Scene/Diffuse" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {} 
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Geometry-100"}
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert  

		sampler2D _MainTex;
		float4 _Color;

		struct Input {
			float2 uv_MainTex;
		};

        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb * _Color.rgb;
        }
		ENDCG
	} 
	Fallback "Mobile/VertexLit"
	CustomEditor "XGameShaderGUI"
}
