Shader "XGame/Diffuse 2side" {
  Properties{
    _Color("Color", Color) = (1,1,1,1)
    _MainTex("Albedo (RGB)", 2D) = "white" {}
    _Cutoff("Cutoff Value", Range(0,1)) = 0.5
  }
    SubShader{
      Tags { "RenderType" = "Opaque" }
      LOD 200
      cull off

      CGPROGRAM
      // Physically based Standard lighting model, and enable shadows on all light types
      #pragma surface surf Lambert alphatest:_Cutoff 

      sampler2D _MainTex;
      float4 _Color;

      struct Input {
        float2 uv_MainTex;
      };

          void surf(Input IN, inout SurfaceOutput o) {
              half4 c = tex2D(_MainTex, IN.uv_MainTex);
              o.Albedo = c.rgb * _Color.rgb;
              o.Alpha = c.a;
          }
      ENDCG
    }
      FallBack "Diffuse"
        CustomEditor "XGameShaderGUI"
}
