// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "XGame/Special/RealShadow"
{
  Properties
  {
    _MainTex("Texture", 2D) = "white" {}
    _Alpha("_Alpha", float) = 1
    _ZOffset("_ZOffset", float) = 2
  }
    SubShader
    {
      Tags { "Queue" = "Geometry-40" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane"}
      Blend SrcAlpha OneMinusSrcAlpha
      Cull Off Lighting Off ZWrite Off
      LOD 100

      Pass
      {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        // make fog work
        #pragma multi_compile_fog

        #include "UnityCG.cginc"

        struct appdata
        {
          float4 vertex : POSITION;
          float2 uv : TEXCOORD0;
        };

        struct v2f
        {
          float4 vertex : SV_POSITION;
          float2 uv : TEXCOORD0;
          float4 cam : TEXCOORD2;
        };

        sampler2D _MainTex;
        float4x4 _CamMatrix;
        float _Alpha;
        float _ZOffset;

        v2f vert(appdata v)
        {
          v2f o;
          o.vertex = mul(unity_ObjectToWorld, v.vertex);
          float3 dir = normalize(_WorldSpaceCameraPos - o.vertex.xyz);
          o.vertex.xyz += dir * _ZOffset;
          o.vertex = mul(UNITY_MATRIX_VP, o.vertex);
          o.cam = mul(_CamMatrix, mul(unity_ObjectToWorld, v.vertex));
          o.cam = o.cam * 0.5f + float4(0.5f, 0.5f, 0.5f, 0.5f);
          return o;
        }

        fixed4 frag(v2f i) : SV_Target
        {
          float a = tex2D(_MainTex, float2(i.cam.x, i.cam.y)).r;
          a = saturate((saturate(i.cam.z) - a) * 100);
          return float4(0, 0, 0, a * _Alpha);
        }
        ENDCG
      }
    }
      CustomEditor "XGameShaderGUI"
}
