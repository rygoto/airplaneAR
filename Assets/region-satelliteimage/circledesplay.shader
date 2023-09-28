Shader "Custom/CircularMask"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Radius ("Radius", Range(0, 0.5)) = 0.25
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" }
        LOD 100
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float _Radius;
            
            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            half4 frag (v2f i) : SV_Target
            {
                // 中心座標(0.5, 0.5)からの距離を計算
                float2 center = float2(0.5, 0.5);
                float distance = length(i.uv - center);
                
                // 円内のピクセルのみ表示し、それ以外のピクセルは透明にする
                if (distance <= _Radius)
                {
                    half4 col = tex2D(_MainTex, i.uv);
                    return col;
                }
                else
                {
                    return half4(0, 0, 0, 0); // 透明
                }
            }
            ENDCG
        }
    }
}
