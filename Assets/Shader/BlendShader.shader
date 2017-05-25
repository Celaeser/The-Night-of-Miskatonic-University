Shader "Custom/Blender" {
	Properties {
		_MainTex ("Main Texture (RGBA)", 2D) = "white" {}
		_SubTex ("Sub Texture (RGBA)", 2D) = "white" {}
	}
	SubShader {  
        Pass {
            CGPROGRAM  
            #pragma vertex vert_img  
            #pragma fragment frag  
              
            #include "UnityCG.cginc"  
              
            uniform sampler2D _MainTex;  
			uniform sampler2D _SubTex;
              
            fixed4 frag(v2f_img i) : COLOR  
            {  
				fixed4 mainTex = tex2D(_MainTex, i.uv);
				fixed4 subTex = tex2D(_SubTex, i.uv);

				fixed4 blendTex = mainTex;
				if (subTex.a == 1) {
					blendTex = subTex;
				}
				
				return blendTex;
            }

            ENDCG  
        }  
    }  
    FallBack "Diffuse"  
}