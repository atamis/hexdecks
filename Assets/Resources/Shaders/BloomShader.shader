// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BloomShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _intensity ("Intensity", Float) = 0.0075
        _bloom ("Bloom", Float) = 0.5
    }

    SubShader {
        Tags { "Queue" = "Transparent" "RenderType" = "Opaque" }

        // Bloom Pass
        Pass {
            Name "Bloom"
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            uniform float _bloom;

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _MainTex_ST;

            v2f vert (appdata_img v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_DEPTH(o.depth);
                o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
                return o;
            }

            half4 frag(v2f i): COLOR {
                half4 col = tex2D(_MainTex, i.uv);
                half4 bloom = col;
                col.rgb = pow(bloom.rgb, _bloom);
                col.rgb *= bloom;
                return col;
            }
            ENDCG
        }
    }
    FallBack off
}
