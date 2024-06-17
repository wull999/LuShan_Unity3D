Shader "Custom/OutlineShader"
{
    Properties
    {
        _Color ("Outline Color", Color) = (1,1,0,1)
        _OutlineWidth ("Outline Width", Range (0.002, 0.03)) = 0.01
    }
    
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        
        Cull Front
        
        ZWrite On
        ZTest LEqual
        
        Pass
        {
            Name "OUTLINE"
            
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
                float4 pos : SV_POSITION;
            };
            
            float _OutlineWidth;
            float4 _Color;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            half4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
