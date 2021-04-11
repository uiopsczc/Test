Shader "Custom/templet2" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_face_x_start ("face_x_start", Range(0, 1)) = 0
		_face_x_end ("face_x_end", Range(0, 1)) = 0.27
		_face_y_start ("face_y_start", Range(0, 1)) = 0
		_face_y_end ("face_y_end", Range(0, 1)) =  0.159
		offest_x ("offest_x", Range(0, 1)) = 0
		offest_y ("offest_y", Range(0, 1)) = 0
	}
	SubShader {
		Tags
		{
			"RenderType"="Transparent" 
		}
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		float _face_x_start;
		float _face_x_end;
		float _face_y_start;
		float _face_y_end;

		float offest_x;
		float offest_y;
		struct Input {
			float2 uv_MainTex;
		};

		
		void surf (Input IN, inout SurfaceOutput o) {
			if(IN.uv_MainTex.x > _face_x_start && IN.uv_MainTex.x < _face_x_end
			 && IN.uv_MainTex.y > _face_y_start && IN.uv_MainTex.y < _face_y_end )
			{
				IN.uv_MainTex.x = IN.uv_MainTex.x + offest_x;
				IN.uv_MainTex.y = IN.uv_MainTex.y + offest_y;
				half4 d = tex2D (_MainTex, IN.uv_MainTex);
				o.Albedo = d.rgb;
			}
			else{
				half4 c = tex2D (_MainTex, IN.uv_MainTex);
				float edge = 1;
				o.Albedo = c.rgb;
			}
			o.Alpha = 0;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
