Shader "Custom/Blender" {
	Properties {
		_MainTex ("Main Texture (RGBA)", 2D) = "white" {}
		_SubTex ("Sub Texture (RGBA)", 2D) = "white" {}
		_Darkness("Dark Opacity", Range(0.0, 1.0)) = 1.0
	}
	SubShader {  
        Pass {
            CGPROGRAM  
            #pragma vertex vert_img  
            #pragma fragment frag  
              
            #include "UnityCG.cginc"  
              
            uniform sampler2D _MainTex;  
			uniform sampler2D _SubTex;
			uniform float _Darkness = 1.0f;
              
            fixed4 frag(v2f_img i) : COLOR  
            {  
				fixed4 mainTex = tex2D(_MainTex, i.uv);
				fixed4 subTex = tex2D(_SubTex, i.uv);

				fixed4 blendTex = mainTex;
				if (subTex.a == 1) {
					if (subTex.r == 0 && subTex.g == 0 && subTex.b == 0) {
						mainTex.r *= (1.0 - _Darkness);
						mainTex.g *= (1.0 - _Darkness);
						mainTex.b *= (1.0 - _Darkness);
						blendTex = mainTex;
					}
					else {
						blendTex = subTex;
					}
				}
				
				return blendTex;
            }

            ENDCG  
        }  
    }  
    FallBack "Diffuse"  
}