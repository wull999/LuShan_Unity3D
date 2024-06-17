Shader "Custom/DashedLine" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _DashSize ("Dash Size", Range(0.001, 0.1)) = 0.03
        _GapSize ("Gap Size", Range(0.001, 0.1)) = 0.01
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Blend SrcAlpha OneMinusSrcAlpha
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            fixed4 _Color;
            float _DashSize;
            float _GapSize;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                o.color = _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float dash = frac(i.texcoord.x / (_DashSize + _GapSize));
                if (dash > 0.5) {
                    discard;
                }
                return i.color;
            }
            ENDCG
        }
    }
}
