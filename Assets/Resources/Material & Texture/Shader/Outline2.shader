Shader "Draw/OutlineShader" {
	Properties {

		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white" {}
		_OutlineColor ("Outline Color", Color) = (1,1,0.2,0.9)
		_Outline ("Outline width", Range (0, 1)) = .05
	}
 
CGINCLUDE
#include "UnityCG.cginc"
 
struct appdata {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};
 
struct v2f {
	float4 pos : POSITION;
	float4 color : COLOR;
};
 
uniform float _Outline;
uniform float4 _OutlineColor;
 
v2f vert(appdata v) {
	v2f o;

	v.vertex *= ( 1 + _Outline);

	o.pos = UnityObjectToClipPos(v.vertex);
 
	o.color = _OutlineColor;
	return o;
}
ENDCG
 
	SubShader {
		Tags { "DisableBatching" = "True" }
		Pass {
			Name "OUTLINE"
			Tags {"LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			half4 frag(v2f i) :COLOR { return i.color; }
			ENDCG
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			half4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct vertexInput
			{
				float4 vertex: POSITION;
				float4 texcoord: TEXCOORD0;
			};

			struct vertexOutput
			{
				float4 pos: SV_POSITION;
				float4 texcoord: TEXCOORD0;
			};

			vertexOutput vert(vertexInput v)
			{
				vertexOutput o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.texcoord.xy = (v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw);
				return o;
			}

			half4 frag(vertexOutput i) : COLOR
			{
				return tex2D(_MainTex, i.texcoord) * _Color;
			}

			ENDCG
		}
	}
 
	Fallback "Diffuse"
}