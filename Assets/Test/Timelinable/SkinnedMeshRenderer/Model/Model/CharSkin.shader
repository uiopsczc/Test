// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Character/Char_Skin" {
  Properties{
    _KeyColor("Forward Color", Color) = (1, 1, 1, 1)
    _FillColor("Fill Color", Color) = (0.5, 0.5, 0.5, 1)
    _BackColor("Background Color", Color) = (0.2, 0.2, 0.2, 1)

    _TranslucentColor("Skin Color", Color) = (0, 0, 0, 1)

    _ShadowColor("Shadow Color", Color) = (0.784, 0.784, 0.784, 1)
    _MainTex("Albedo (RGB)", 2D) = "white" {}
    _MainNor("Normal", 2D) = "bump" {}

    //_NoseBlend("鼻子阴影混合",Range(0,1)) = 0
    //_MainNor2 ("Normal", 2D) = "bump" {}
    _WrapAround("Color Offset", Range(-1, 1)) = 0
    [MaterialEnum(Real, 0, Toon, 1)] RLSource("脸部阴影灯光方式", Int) = 1
    _RampDir("Ramp Directional",Vector) = (0,0,0,1)
    _RampColor("Ramp Color", Color) = (0.784, 0.784, 0.784, 1)
    _RampThreshold("Ramp Threshold", Range(0,1)) = 0.5
    _RampSmooth("Ramp Smoothing", Range(0.01,1)) = 0.1
    _RampColor2("Ramp Color2", Color) = (0.784, 0.784, 0.784, 1)
      //_RampThreshold2 ("Ramp Threshold2", Range(0,1)) = 0.5
      _RampSmooth2("Ramp Smoothing2", Range(0.01,1)) = 0.1

      _RimColor("Rim Color", Color) = (0.26,0.19,0.16,0.0)
          _RimPower("Rim Power", Range(2,18.0)) = 3.0

      //_GlowColor2("Glow Color", Color) = (0, 0, 0, 1)
      //_GlowMaskTex("Glow Texture", 2D) = "white" {}
    //_Glossiness ("Smoothness", Range(0,1)) = 0.5
    //_Metallic ("Metallic", Range(0,1)) = 0.0
    [NoScaleOffset]_ShadowIntTex("Shadow Mask", 2D) = "black" {}

  }
    SubShader{
      Tags { "RenderType" = "Opaque" }
      LOD 200

      CGPROGRAM
    // Physically based Standard lighting model, and enable shadows on all light types
    #pragma surface surf SurfaceSkin nolightmap fullforwardshadows  vertex:vert
    #include "UnityPBSLighting.cginc"
    #include "AutoLight.cginc"
    // Use shader model 3.0 target, to get nicer looking lighting
    #pragma multi_compile SHADER_QUALITY_HIGH SHADER_QUALITY_LOW
    #pragma target 3.0
    int RLSource;


    sampler2D _MainTex;
    sampler2D _MainNor;

    //uniform fixed4 _GlowColor2;

    sampler2D _ShadowIntTex;

    #include "CharCommon.cginc"

    struct Input {
      float2 uv_MainTex :TEXCOORD0;
      //float3 WorldNormal:TEXCOORD1;
      #if !defined(SHADER_QUALITY_LOW)
      float4 tSpace0:TEXCOORD1;
        float4 tSpace1:TEXCOORD2;
        float4 tSpace2:TEXCOORD3;
        #endif
    };
    #if !defined(SHADER_QUALITY_LOW)
      half _Glossiness,_Metallic,_RimPower;
      half  _WrapAround ,_RampThreshold ,_RampSmooth, _RampSmooth2;
      fixed4 _KeyColor , _FillColor,_BackColor ,_TranslucentColor,_ShadowColor,_RampColor,_RampColor2,_RimColor;
      float4 _RampDir;
    #endif
    struct SurfaceOutputSkin
    {
        fixed3 Albedo;      // base (diffuse or specular) color

        fixed3 Normal;      // tangent space normal, if written
        fixed3 Normal2;

        half3 Emission;
        half Metallic;      // 0=non-metal, 1=metal

        half Smoothness;    // 0=rough, 1=smooth
        half Occlusion;     // occlusion (default 1)
        fixed Alpha;        // alpha for transparencies
        half2 UV;
    };
    // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
    // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
    // #pragma instancing_options assumeuniformscaling
    UNITY_INSTANCING_BUFFER_START(Props)
      // put more per-instance properties here
    UNITY_INSTANCING_BUFFER_END(Props)

    void vert(inout appdata_full v, out Input o) {
      UNITY_INITIALIZE_OUTPUT(Input, o);
      o.uv_MainTex = v.texcoord;
      //o.WorldNormal = UnityObjectToWorldNormal(v.normal);
      #if !defined(SHADER_QUALITY_LOW)
      float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
      fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
      fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
      fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
        fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
        o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
        o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
        o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
        #endif
    }

    half InverseLerp(half a, half b, half v) {
      return (v - a) / (b - a);
    }

      inline half4 LightingSurfaceSkin(SurfaceOutputSkin s, half3 viewDir, UnityGI gi)
            {
              s.Albedo += half3(0.1,0,0);

              #if !defined(SHADER_QUALITY_LOW)
              half3 NormalMap = normalize(s.Normal);
              half3 NorOrg = normalize(s.Normal2);


              half3 shadowInt = tex2D(_ShadowIntTex,s.UV);
              half shadow = gi.light.ndotl;

              half ndotl = max(0, dot(NormalMap, gi.light.dir) * 0.5 + 0.5);
              half3 halfDir = normalize(gi.light.dir + viewDir);
        half ndoth = max(0, dot(NormalMap, halfDir));


              half t0 = saturate(InverseLerp(-_WrapAround, 1, ndotl * 2 - 1));
        half t1 = saturate(InverseLerp(-1, max(-0.99, -_WrapAround), ndotl * 2 - 1));
        half3 diffuse = lerp(_BackColor.rgb, lerp(_FillColor.rgb, _KeyColor.rgb, t0), t1);
        half a = ndotl + 0.1;
        half t = 0.5 * saturate(1 - a * ndoth) * saturate(1 - ndotl);

        diffuse = diffuse + _TranslucentColor.rgb * t;

        fixed3 finalDiffuse = ndotl * _LightColor0.rgb;
        fixed3 finalAmbient = ShadeSH9(half4(NormalMap, 1));

        half3 shadowColor = s.Albedo * _ShadowColor.rgb;
        half attenuation = saturate(2.0 * shadow - 1.0);
        ///////////////////////////////////////////////nose


        //////////////////////////////////////////////ramp

                half3 RampNormal = normalize(s.Normal); //lerp( normalize(s.Normal), NorOrg,_RampBlend ) ;
                half Rampndotl;
                if (RLSource < 1)
                  //_RampDir = _WorldSpaceLightPos0;
                 Rampndotl = max(0, dot(RampNormal, normalize(_WorldSpaceLightPos0)) * 0.5 + 0.5);
                else
                  Rampndotl = max(0, dot(RampNormal, normalize(_RampDir)) * 0.5 + 0.5);
                fixed3 ramp = smoothstep(_RampThreshold - _RampSmooth * 0.5, _RampThreshold + _RampSmooth * 0.5, Rampndotl);
                fixed3 ramp2 = smoothstep(_RampThreshold - _RampSmooth2 * 0.5, _RampThreshold + _RampSmooth2 * 0.5, Rampndotl);
                ////////////////////// mix
                          s.Albedo *= diffuse;
                          s.Albedo = s.Albedo * (finalDiffuse + finalAmbient);

                          s.Albedo = lerp(s.Albedo, lerp(shadowColor, s.Albedo, attenuation), shadowInt.r);

                         s.Albedo = lerp(s.Albedo, lerp(lerp(_RampColor*s.Albedo, s.Albedo, shadowInt.b) , s.Albedo,ramp), _RampDir.w);


                         ///////////////////////////////////////////
                                 half rim = 1.0 - saturate(dot(normalize(viewDir), NorOrg));
                                  s.Albedo += _RimColor.rgb * pow(rim, _RimPower) * (_LightColor0*1.4) * shadowInt.g;
                                  /////////////



                                  //s.Albedo =  s.Albedo * ramp2  + (1-ramp2) * s.Albedo *  _RampColor2;
                                  s.Albedo = lerp(s.Albedo,  lerp(s.Albedo * ramp2 + (1 - ramp2) * s.Albedo *  _RampColor2  ,s.Albedo , shadowInt.b) , _RampDir.w);
                                  //////////////

                                   #endif

                                          return  half4(s.Albedo,1);
                                          // return shadowInt.b;


                                              }

                                              inline void LightingSurfaceSkin_GI(
                                                  SurfaceOutputSkin s,
                                                  UnityGIInput data,
                                                  inout UnityGI gi)
                                              {




                                                #if !defined(SHADER_QUALITY_LOW)

                                            unity_SHAr = _Shar;
                                            unity_SHAg = _Shag;
                                            unity_SHAb = _Shab;
                                            unity_SHBr = _Shbr;
                                            unity_SHBg = _Shbg;
                                            unity_SHBb = _Shbb;
                                            unity_SHC = _Shc;

                                                half plus2 = 0.1;
                                          #ifdef UNITY_PASS_FORWARDADD
                                                unity_SHAr = unity_SHAr * plus2;
                                                unity_SHAg = unity_SHAg * plus2;
                                                unity_SHAb = unity_SHAb * plus2;
                                                unity_SHBr = unity_SHBr * plus2;
                                                unity_SHBg = unity_SHBg * plus2;
                                                unity_SHBb = unity_SHBb * plus2;
                                                unity_SHC = unity_SHC * plus2;
                                          #endif

                                                   SurfaceOutputStandard ss;
                                                   ss.Albedo = s.Albedo;

                                                   ss.Normal = s.Normal;
                                                   ss.Emission = s.Emission;
                                                   ss.Metallic = s.Metallic;
                                                   ss.Smoothness = s.Smoothness;
                                                   ss.Occlusion = s.Occlusion;
                                                   ss.Alpha = s.Alpha;

                                                  _LightColor0 = fixed4(0.8,0.8,0.8,1);
                                            gi.light.color = half3(0.8,0.8,0.8);
                                                    LightingStandard_GI(ss, data, gi);

                                                    gi.light.ndotl = data.atten;

                                                  #endif
                                              }


                                              void surf(Input IN, inout SurfaceOutputSkin o) {
                                                  o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
                                                  #if !defined(SHADER_QUALITY_LOW)
                                                  o.Normal = UnpackNormal(tex2D(_MainNor, IN.uv_MainTex));


                                                  o.Normal2 = fixed3(IN.tSpace0.z,IN.tSpace1.z,IN.tSpace2.z);
                                                  o.UV = IN.uv_MainTex;
                                                  o.Smoothness = _Glossiness;
                                                  o.Metallic = _Metallic;
                                                  #endif


                                              }


                                      ENDCG
  }
    FallBack "Diffuse"
}
