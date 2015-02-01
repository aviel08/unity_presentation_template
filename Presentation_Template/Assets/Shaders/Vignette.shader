Shader "Custom/Vignette" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_VignettePower ("VignettePower", Range(0.0,6.0)) = 5.5
	}
	SubShader 
	{
		Pass
		{
		
		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest
		#include "UnityCG.cginc"
		
		struct v2f 
		{
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
		float2 uv2 : TEXCOORD1;
		};
		
		float4 _MainTex_TexelSize;
		
		v2f vert( appdata_img v ) 
		{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord.xy;
		o.uv2 = v.texcoord.xy;

		#if UNITY_UV_STARTS_AT_TOP
		if (_MainTex_TexelSize.y < 0)
			 o.uv2.y = 1.0 - o.uv2.y;
		#endif

		return o;
		} 

		uniform sampler2D _MainTex;
		uniform sampler2D _VignetteTex;
		uniform float _VignettePower;
		uniform float _Blur;
		
		float4 frag(v2f i) : COLOR
		{
		float4 renderTex = tex2D(_MainTex, i.uv);
		float4 colorBlur = tex2D (_VignetteTex, i.uv2);
		float2 dist = (i.uv - 0.5f) * 2.0f;
		float coorddot = dot(dist, dist);
		dist.x = 1 - coorddot  * _VignettePower * 0.1f;
		renderTex = lerp(renderTex, colorBlur, saturate(_Blur * coorddot));
		return renderTex * dist.x;
		
		}

		ENDCG
		} 
	}
}
