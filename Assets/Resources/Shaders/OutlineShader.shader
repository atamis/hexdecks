Shader "Custom/OutlineShader" {
	Properties {
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel Snap", Float) = 0 

		[PerRendererData] _Outline ("Outline", Float) = 0
		[PerRendererData] _OutlineColor("Outline Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags {
			"Queue" = "Transparent"
			"IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

		Cull Off 
		Lighting Off
		ZWrite Off 
		Blend One OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma shader_feature ETC1_EXTERNAL_ALPHA

			#include "UnityCG.cginc"

			struct appdata_t {
				float4 pos 	: POSITION;
				float4 col	: COLOR;
				float2 uv 	: TEXCOORD0;
			};

			struct v2f {
				float4 pos 	: SV_POSITION;
				fixed4 col 	: COLOR;
				float2 uv 	: TEXCOORD0;

			};

			float _Outline;
			fixed4 _Color;
			fixed4 _OutlineColor;

			v2f vert (appdata_t v) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.pos);
				o.uv = v.uv;
				o.col = v.col * _Color;

				// clamps the texel to the nearest pixel
				#ifdef PIXELSNAP_ON
				o.pos = UnityPixelSnap(o.pos);
				#endif

				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float4 _MainTex_TexelSize;

			fixed4 SampleTexture(float2 uv) {
				fixed4 col = tex2D(_MainTex, uv);

				#if ETC1_EXTERNAL_ALPHA
				col.a = tex2D(_AlphaTex, uv).r;
				#endif

				return col;
			}

			fixed4 frag (v2f i) : SV_Target {
				fixed4 col = SampleTexture(i.uv) * i.col;

				if (_Outline > 0 && col.a != 0) {
					fixed4 p_up 	= tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y));
					fixed4 p_right 	= tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0));
					fixed4 p_down 	= tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y));
					fixed4 p_left 	= tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0));

					if (p_up.a * p_down.a * p_right.a * p_left.a == 0) {
						col.rgba = fixed4(1, 1, 1, 1) * _OutlineColor;
					}
				}

				col.rgb *= col.a;

				return col;
			}
			ENDCG
		}
	}
}