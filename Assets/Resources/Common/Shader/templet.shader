// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/templet" {

	Properties {
		_OffsetX ("Offset X", Range (-1.5, 1.5) ) = 0
		_OffsetY ("Offset Y", Range (-1.5, 1.5) ) = 0
		_OffsetZ ("Offset Z", Range (-1.5, 1.5) ) = 0
		_speed ("speed", Range (1, 5) ) = 1
	}
    SubShader  
    {  
        Pass  
        {  
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		struct VertextOutput {
		float4 pos : SV_POSITION ;
		fixed4 col : COLOR ;
		};

		uniform float _OffsetX;
		uniform float _OffsetY;
		uniform float _OffsetZ;
		float _speed;

		VertextOutput vert ( appdata_base input )
		{
		VertextOutput result;
		result.pos = UnityObjectToClipPos( input.vertex );
		result.col = input.vertex + float4( _CosTime.w * _speed  + 0.5, _SinTime.w * _speed + 0.5, _SinTime.w * _speed + 0.5, 0);
		return result;
		}

		fixed4 frag ( VertextOutput input ) : COLOR
		{
		return input.col;
		}

ENDCG
        }
    }
}
