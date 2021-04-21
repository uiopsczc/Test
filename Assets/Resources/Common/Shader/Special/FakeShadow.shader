// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "XGame/Special/FakeShadow" {
  Properties{
    _TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
    _MainTex("Particle Texture", 2D) = "white" {}
    _ZOffset("_ZOffset", float) = 2
  }

    Category{
      Tags { "Queue" = "Geometry-30" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane"}
      Blend SrcAlpha OneMinusSrcAlpha
      Cull Off Lighting Off ZWrite Off

      SubShader {
        Pass {

          CGPROGRAM
          #pragma vertex vert
          #pragma fragment frag

          #include "UnityCG.cginc"

          struct appdata_t {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
          };

          struct v2f {
            float4 vertex : SV_POSITION;
            fixed4 color : COLOR;
            float2 texcoord : TEXCOORD0;
          };

          sampler2D _MainTex;
          fixed4 _TintColor;
          float4 _MainTex_ST;
          float _ZOffset;

          v2f vert(appdata_t v)
          {
            v2f o;
            o.vertex = mul(unity_ObjectToWorld, v.vertex);
            float3 dir = normalize(_WorldSpaceCameraPos - o.vertex.xyz);
            o.vertex.xyz += dir * _ZOffset;
            o.vertex = mul(UNITY_MATRIX_VP, o.vertex);
            o.color = 2.0f * _TintColor;
            o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
            return o;
          }

          fixed4 frag(v2f i) : SV_Target
          {
            //return fixed4(0, 0, 0, i.vertex.z);
            return tex2D(_MainTex, i.texcoord) * i.color;
          }
          ENDCG
        }
      }
      CustomEditor "XGameShaderGUI"
    }
}