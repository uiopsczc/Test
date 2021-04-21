// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "XGame/UI/Additive" {
  Properties{
    _TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
    _MainTex("Particle Texture", 2D) = "white" {}

  //-------------------add----------------------
  _MinX("Min X", Float) = -10
  _MaxX("Max X", Float) = 10
  _MinY("Min Y", Float) = -10
  _MaxY("Max Y", Float) = 10
    //-------------------add----------------------
  }

    Category{
      Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
      Blend SrcAlpha One
      ColorMask RGB
      Cull Off Lighting Off ZWrite Off

      SubShader {
        Pass {

          CGPROGRAM
          #pragma vertex vert
          #pragma fragment frag
          #pragma multi_compile_particles

          #include "UnityCG.cginc"

          sampler2D _MainTex;
          fixed4 _TintColor;
          //-------------------add----------------------
          float _MinX;
          float _MaxX;
          float _MinY;
          float _MaxY;
          //-------------------add----------------------

          struct appdata_t {
            float4 vertex : POSITION;
            fixed4 color : COLOR;
            float2 texcoord : TEXCOORD0;
          };

          struct v2f {
            float4 vertex : SV_POSITION;
            fixed4 color : COLOR;
            float2 texcoord : TEXCOORD0;
            //-------------------add----------------------
            float3 vpos : TEXCOORD1;
            //-------------------add----------------------
          };

          float4 _MainTex_ST;

          v2f vert(appdata_t v)
          {
            v2f o;
            //-------------------add----------------------
            //o.vpos = v.vertex.xyz;
            //o.vpos = mul(_Object2World, v.vertex).xyz;
            o.vpos = UnityObjectToClipPos(v.vertex).xyz;
            //-------------------add----------------------
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.color = v.color;
            o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
            return o;
          }

          sampler2D_float _CameraDepthTexture;
          float _InvFade;

          fixed4 frag(v2f i) : SV_Target
          {
            fixed4 col = 2.0f * i.color * _TintColor * tex2D(_MainTex, i.texcoord);
          //-------------------add----------------------
          col.a *= (i.vpos.x >= _MinX);
                col.a *= (i.vpos.x <= _MaxX);
          col.a *= (i.vpos.y >= _MinY);
          col.a *= (i.vpos.y <= _MaxY);
          //col.rgb *= col.a;
          //-------------------add----------------------
          return col;
        }
        ENDCG
      }
    }
    CustomEditor "XGameShaderGUI"
  }
}