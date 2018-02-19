Shader "Retro3D/Transparent"
{
	Properties
	{
		_MainTex("Base", 2D) = "white" {}
		_Color("Color", Color) = (0.5, 0.5, 0.5, 1)
		_Transparency("Transparency", Range(0.0,0.5)) = 0.5
		_CutoutThresh("Cutout Threshold", Range(0.0,1.0)) = 0.2
		_GeoRes("Geometric Resolution", Float) = 7
	}
		SubShader{
			Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" "LightMode" = "ForwardBase" }
			LOD 100

			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

		Pass
	{
		CGPROGRAM

#include "UnityCG.cginc"

#pragma vertex vert
#pragma fragment frag

		struct v2f
	{
		float4 position : SV_POSITION;
		float3 texcoord : TEXCOORD;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float4 _Color;
	float _Transparency;
	float _CutoutThresh;
	float _GeoRes;

	v2f vert(appdata_base v)
	{
		v2f o;

		float4 wp = mul(UNITY_MATRIX_MV, v.vertex);
		wp.xyz = floor(wp.xyz * _GeoRes) / _GeoRes;

		float4 sp = mul(UNITY_MATRIX_P, wp);
		o.position = sp;

		float2 uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.texcoord = float3(uv * sp.w, sp.w);

		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		float2 uv = i.texcoord.xy / i.texcoord.z;
		half4 main_color = tex2D(_MainTex, uv) * _Color * 2;
		main_color.a = _Transparency;
		clip(main_color.r - _CutoutThresh);
		return main_color;
		//return tex2D(_MainTex, uv) * _Color * 2;
	}

		ENDCG
	}
	}
}
