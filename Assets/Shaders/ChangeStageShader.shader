Shader "Custom/CircleTransition"
{
    Properties
    {
        _MainTex ("Current Scene", 2D) = "white" {}
        _NextTex ("Next Scene", 2D) = "black" {}
        _Cutoff ("Cutoff Radius", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            sampler2D _MainTex;
            sampler2D _NextTex;
            float _Cutoff;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);

                if (dist < _Cutoff)
                    return tex2D(_NextTex, i.uv); // 中心：次のシーン
                else
                    return tex2D(_MainTex, i.uv); // 外側：現在のシーン
            }
            ENDCG
        }
    }
}