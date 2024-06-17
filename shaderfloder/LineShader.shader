Shader "Custom/LineShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _LineWidth ("Line Width", Float) = 10
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
            };
            
            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };
            
            fixed4 _Color;
            float _LineWidth;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = _Color;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                return i.color;
            }
            
            ENDCG
        }
    }
}
