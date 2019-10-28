// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "GanGanKamen/NextPage"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_PageTex("PageTexture", 2D) = "white" {}
		_AlphaMask("AlphaMask", Range(0, 1)) = 0.1
		_Flip("Flip",Range(-1, 1)) = 0
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 puv : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _PageTex;
			float4 _PageTex_ST;
			float _AlphaMask;
			float _Flip;

			float l2(float x)
			{
				return 1 - _Flip + 0.1 * cos(x * 2);
			}

			float l1(float y)
			{
				return _Flip + 0.1 * sin(y * 3);
			}

			float l0(float x)
			{
				return x - _Flip;
			}

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.puv = TRANSFORM_TEX(v.uv, _PageTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				//コンテンツとページテクスチャ色取得
				float4 content_col = tex2D(_MainTex, i.uv);
				float4 page_col = tex2D(_PageTex, i.puv);

				//L0より右の描画を無視
				float l0_y = l0(i.uv.x);
				clip(i.uv.y - l0_y);

				//範囲内ならば暗い色に
				if (i.uv.x > l1(i.uv.y) && i.uv.y < l2(i.uv.x))
					content_col = float4(0.5, 0.5, 0.5, 1);

				//ページ内容のうち一定の値より透明なものはページの色にすり替える
				if (content_col.a < _AlphaMask)
					return page_col;

				return content_col * page_col;
			}
			ENDCG
		}
	}
}