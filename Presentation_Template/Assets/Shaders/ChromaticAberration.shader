Shader "Custom/ChromaticAberration" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader 
	{
		Pass
		{
		
		CGPROGRAM
		#pragma vertex vert_img
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		uniform float _AberrationOffset;

		float4 frag(v2f_img i) : COLOR
		{

			float2 coords = i.uv.xy;
			
			_AberrationOffset /= 300.0f;
			
			//Red Channel
			float4 red = tex2D(_MainTex , coords.xy - _AberrationOffset);
			//Green Channel
			float4 green = tex2D(_MainTex, coords.xy);
			//Blue Channel
			float4 blue = tex2D(_MainTex, coords.xy + _AberrationOffset);
			
			float4 finalColor = float4(red.r, green.g, blue.b, 1.0f);
			return finalColor;
		
		}

		ENDCG
		} 
	}
}