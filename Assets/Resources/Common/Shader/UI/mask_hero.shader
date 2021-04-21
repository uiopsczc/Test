// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "XGame/UI/MaskHero"
{
  Properties
  {
    _MainTex("Base (RGB), Alpha (A)", 2D) = "white" {}
    _ColorMask("Color Mask", Float) = 15
    _MaskTex("Alpha (A)", 2D) = "white" {}

    [HideInInspector] _StencilComp("Stencil Comparison", Float) = 8
    [HideInInspector] _Stencil("Stencil ID", Float) = 0
    [HideInInspector] _StencilOp("Stencil Operation", Float) = 0
    [HideInInspector] _StencilWriteMask("Stencil Write Mask", Float) = 255
    [HideInInspector] _StencilReadMask("Stencil Read Mask", Float) = 255
  }

    SubShader
    {
      LOD 100

      Tags
      {
        "Queue" = "Transparent"
        "IgnoreProjector" = "True"
        "RenderType" = "Transparent"
        "PreviewType" = "Plane"
      }

      Stencil
      {
        Ref[_Stencil]
        Comp[_StencilComp]
        Pass[_StencilOp]
        ReadMask[_StencilReadMask]
        WriteMask[_StencilWriteMask]
      }

      Cull Off
      Lighting Off
      ZWrite Off
      ZTest[unity_GUIZTestMode]
      Offset - 1, -1
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask[_ColorMask]

      Pass
      {
        CGPROGRAM
          #pragma vertex vert
          #pragma fragment frag
          #include "UnityCG.cginc"

          struct appdata_t
          {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
            fixed4 color : COLOR;
          };

          struct v2f
          {
            float4 vertex : SV_POSITION;
            half2 texcoord : TEXCOORD0;
            half2 texcoord1 : TEXCOORD1;
            fixed4 color : COLOR;
          };

          sampler2D _MainTex;
          sampler2D _MaskTex;
          float4 _MainTex_ST;
          float4 _MaskTex_ST;

          v2f vert(appdata_t v)
          {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
            o.texcoord1 = TRANSFORM_TEX(v.texcoord, _MaskTex);
            o.color = v.color;
  #ifdef UNITY_HALF_TEXEL_OFFSET
            o.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1,1);
  #endif
            return o;
          }

          fixed4 frag(v2f i) : COLOR
          {
            fixed4 mask = tex2D(_MaskTex, i.texcoord1);
            fixed4 col = tex2D(_MainTex, i.texcoord) * i.color * mask.a;

            return col;
          }
        ENDCG
      }
    }
}
