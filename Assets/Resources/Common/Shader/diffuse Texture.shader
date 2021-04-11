Shader "Custom/Diffuse Texture" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Param1 ("Param1", Range(0, 1)) = 0
	}
	SubShader {
		Tags
		{
			"RenderType"="Transparent" 
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
			"PreviewType"="Plane"
		}
		LOD 200
		
		Cull Off
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float2 _Param1;
		};

		
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);

			float edge = 1;
			edge = step(0.95f, 1);

			o.Albedo = c.rgb * edge;
			o.Alpha = 0;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}