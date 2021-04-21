// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Character/Char_Eye" {
  Properties{
    _Color("主颜色(RGB)", Color) = (1, 1, 1, 1)
    _MainTex("主颜色贴图 (RGB)", 2D) = "white" { }

  //_SpecLightDir("高光方向(XYZ)", Vector) = (1, 0, 1, 0)
  //_SpecColor("高光颜色(RGB)", Color) = (1, 1, 1, 1)
  //_SpecIntensity("高光强度", Range(0, 8)) = 4
  //_Shininess("高光锐利度", Float) = 600

  [NoScaleOffset]_ReflectionCube("环境反射贴图", Cube) = "_Skybox" {}
  _ReflectionColor("反射颜色(RGB)", Color) = (0.5, 0.5, 0.5, 1)
  }

    SubShader{
        Tags{ "RenderType" = "Opaque" }
        Cull[_Cull]

        Pass{
    //Tags{ "LightMode" = "ForwardBase" }

    CGPROGRAM
    #pragma vertex vert
    #pragma fragment frag
    //#pragma multi_compile_fwdbase
    #pragma target 3.0
    #pragma multi_compile SHADER_QUALITY_HIGH SHADER_QUALITY_LOW

    #include "UnityCG.cginc"

    fixed4 _Color;
    sampler2D _MainTex;
    float4 _MainTex_ST;

    half4 _SpecLightDir;
    fixed4 _SpecColor;
    half _SpecIntensity;
    half _Shininess;

    samplerCUBE _ReflectionCube;
    fixed4 _ReflectionColor;

    //fixed4 _LightColor0;


    struct v2f {
      float4 pos : SV_POSITION;
      float2 texcoord : TEXCOORD0;
      float3 worldPos : TEXCOORD1;
      half3 worldNormal : TEXCOORD2;
    };


    v2f vert(appdata_full v) {
      v2f o;
      o.pos = UnityObjectToClipPos(v.vertex);
      o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

      float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
      half3 worldNormal = UnityObjectToWorldNormal(v.normal);
      o.worldPos = worldPos;
      o.worldNormal = worldNormal;

      return o;
    }


    fixed4 frag(v2f i) : SV_Target {
      float4 color = tex2D(_MainTex, i.texcoord) * _Color;

      half3 worldNormal;
      worldNormal = i.worldNormal;
      float3 worldPos = i.worldPos;
      fixed3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));

      half3 lightDir = normalize(_SpecLightDir.xyz);

      /*
      #if defined(SHADER_QUALITY_HIGH)
                float nh = max(0, dot(worldNormal, normalize(lightDir + worldViewDir)));
                float spec = pow(nh, _Shininess);
                fixed3 finalSpecular = _SpecColor.rgb * spec * _SpecIntensity;
      #endif
      */
      //#if defined(SHADER_QUALITY_HIGH) || defined(SHADER_QUALITY_MEDIUM)
      #if !defined(SHADER_QUALITY_LOW)
                float3 worldRefl = reflect(-worldViewDir, worldNormal);
                fixed3 finalReflection = texCUBE(_ReflectionCube, worldRefl).rgb * _ReflectionColor.rgb;
      #endif

                color.rgb = color.rgb
      #if !defined(SHADER_QUALITY_LOW)
                  +finalReflection
      #endif
                  //#if defined(SHADER_QUALITY_HIGH)
                    //					+ finalSpecular
                  //#endif
                              ;
                            color.a = 1;

                            return color;
                          }



                          ENDCG
                        }
  }

    //Fallback "Legacy Shaders/Diffuse"
                            Fallback Off
}