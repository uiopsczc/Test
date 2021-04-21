Shader "XGame/Diffuse Emissive" {
  Properties{
    _Color("Color", Color) = (1,1,1,1)
    _MainTex("Albedo (RGB)", 2D) = "white" {}
    _Illum("Emissive (A)", 2D) = "white" {}
    _EmissiveMult("Emissive Mult", Float) = 1
  }
    SubShader{
      Tags { "RenderType" = "Opaque" }
      LOD 200

      CGPROGRAM
      // Physically based Standard lighting model, and enable shadows on all light types
      #pragma surface surf Lambert  

      sampler2D _MainTex;
      sampler2D _Illum;
      float4 _Color;
      float _EmissiveMult;

      struct Input {
        float2 uv_MainTex;
        float2 uv_Illum;
      };

          void surf(Input IN, inout SurfaceOutput o) {
              half4 c = tex2D(_MainTex, IN.uv_MainTex);
              half4 e = tex2D(_Illum, IN.uv_Illum);
              o.Albedo = c.rgb * _Color.rgb;
              o.Emission = c.rgb * (_EmissiveMult * e.a);
              o.Alpha = c.a;
          }
      ENDCG
    }
      FallBack "Diffuse"
        CustomEditor "XGameShaderGUI"
}
