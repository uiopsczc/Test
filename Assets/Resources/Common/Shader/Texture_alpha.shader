Shader "Unlit/Texture_alpha" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

Category {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	ZWrite Off
	Alphatest Greater 0
	Blend SrcAlpha OneMinusSrcAlpha 
	SubShader {
		Material {
			Diffuse [_Color]
		}
		Pass {
			ColorMaterial AmbientAndDiffuse
        	SetTexture [_MainTex] {
        }
        SetTexture [_MainTex] {
            constantColor [_Color]
            Combine previous * constant
        }  
		}
	} 
}
}