// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "XGame/Terrain" {
	Properties {
		_Control ("Control (RGBA)", 2D) = "red" {}
		_Splat0 ("Layer 0 (R)", 2D) = "white" {}
		_Splat1 ("Layer 1 (G)", 2D) = "white" {}
		_Splat2 ("Layer 2 (B)", 2D) = "white" {}
		_Splat3 ("Layer 3 (A)", 2D) = "white" {}
	}
	
	SubShader {
		Tags {
			"SplatCount" = "4"
			"Queue" = "Geometry-80"
			"RenderType" = "Opaque"
		}
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert vertex:SplatmapVert finalcolor:myfinal exclude_path:prepass exclude_path:deferred
		#pragma multi_compile_fog
		
		sampler2D _Control;
		float4 _Control_ST;
		sampler2D _Splat0,_Splat1,_Splat2,_Splat3;
		
		struct Input {
			float2 uv_Splat0 : TEXCOORD0;
			float2 uv_Splat1 : TEXCOORD1;
			float2 uv_Splat2 : TEXCOORD2;
			float2 uv_Splat3 : TEXCOORD3;
			float2 tc_Control : TEXCOORD4;	// Not prefixing '_Contorl' with 'uv' allows a tighter packing of interpolators, which is necessary to support directional lightmap.
			UNITY_FOG_COORDS(5)
		};
		
		void SplatmapVert(inout appdata_full v, out Input data)
		{
			UNITY_INITIALIZE_OUTPUT(Input, data);
			data.tc_Control = TRANSFORM_TEX(v.texcoord, _Control);	// Need to manually transform uv here, as we choose not to use 'uv' prefix for this texcoord.
			float4 pos = UnityObjectToClipPos (v.vertex);
			UNITY_TRANSFER_FOG(data, pos);
		}
		
        void surf (Input IN, inout SurfaceOutput o) {
			half4 splat_control = tex2D(_Control, IN.tc_Control);

			fixed4 mixedDiffuse = 0.0f;
			mixedDiffuse += splat_control.r * tex2D(_Splat0, IN.uv_Splat0);
			mixedDiffuse += splat_control.g * tex2D(_Splat1, IN.uv_Splat1);
			mixedDiffuse += splat_control.b * tex2D(_Splat2, IN.uv_Splat2);
			mixedDiffuse += splat_control.a * tex2D(_Splat3, IN.uv_Splat3);
			
            o.Albedo = mixedDiffuse.rgb;
            o.Alpha = 1;
        }
		void myfinal(Input IN, SurfaceOutput o, inout fixed4 color)
		{
			color.rgb *= color.a;
			color.a = 1.0f;
			UNITY_APPLY_FOG(IN.fogCoord, color);
		}

		ENDCG

	} 
	
	FallBack "Mobile/VertexLit"
	CustomEditor "XGameShaderGUI"
}
