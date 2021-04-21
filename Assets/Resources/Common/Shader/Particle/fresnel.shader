// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "XGame/Particles/边缘光" {
  Properties{
    _TintColor("Tint Color", Color) = (0.5,0.5,0.5,0)
    _MainTex("Particle Texture", 2D) = "white" {}
    _FresnelColor("_FresnelColor", Color) = (0.5,0.5,0.5,0.5)
    _Width("Width", Range(0, 3)) = 1
    _speed_u("_speed_u", float) = 0
    _speed_v("_speed_v", float) = 0
    _ZOffset("Z Offset", Float) = 0
  }

    Category{
      Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB
      Lighting Off ZWrite Off

      SubShader {
        Pass {

          CGPROGRAM
          #pragma vertex vert
          #pragma fragment frag
          #pragma multi_compile_particles
          #pragma multi_compile_fog

          #include "UnityCG.cginc"

          sampler2D _MainTex;
          float4 _MainTex_ST;
          fixed4 _TintColor;
          fixed4 _FresnelColor;
          float _Width;
          float _speed_u;
          float _speed_v;
          float _ZOffset;

          struct appdata_t {
            float4 vertex : POSITION;
            fixed4 color : COLOR;
            float2 texcoord : TEXCOORD0;
            float3 normal : NORMAL;
          };

          struct v2f {
            float4 vertex : SV_POSITION;
            fixed4 color : COLOR;
            float2 texcoord : TEXCOORD0;
            float4 pos_world : TEXCOORD1;
            float3 normal_world : TEXCOORD2;
          };

          v2f vert(appdata_t v)
          {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.color = 2.0f * v.color;
            o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex) + float2(_speed_u, _speed_v) * _Time.y % 1;
            o.normal_world = UnityObjectToWorldNormal(v.normal);
            o.pos_world = mul(unity_ObjectToWorld, v.vertex);
            o.vertex.z = o.vertex.z + _ZOffset;
            return o;
          }

          sampler2D_float _CameraDepthTexture;
          float _InvFade;

          fixed4 frag(v2f i) : SV_Target
          {
            float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.pos_world.xyz);
            float3 normalDirection = normalize(i.normal_world);
            float NdotL = max(0.0, dot(normalDirection, viewDirection));
            float fresnel = NdotL * _Width;


            fixed4 col = i.color * tex2D(_MainTex, i.texcoord);
            col = col * lerp(_FresnelColor, _TintColor, fresnel);
            return col;
          }
          ENDCG
        }
      }
      CustomEditor "XGameShaderGUI"
    }
}