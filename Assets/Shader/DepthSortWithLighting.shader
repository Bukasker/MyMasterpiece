Shader"Custom/DepthSortWithLighting" {
    Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
    }
 
    SubShader {
        Tags { "Queue" = "Transparent" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"
 
struct appdata_t
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
    float3 normal : NORMAL;
};
 
struct v2f
{
    float4 vertex : SV_POSITION;
    float2 uv : TEXCOORD0;
    float3 normal : TEXCOORD1;
    float4 color : COLOR;
};
 
sampler2D _MainTex;
float4 _LightColor0;
 
v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    o.normal = normalize(UnityObjectToWorldNormal(v.normal));
    return o;
}
 
half4 frag(v2f i) : SV_Target
{
                // Sort sprites by their Z position (negative Z values render on top)
    half depth = i.vertex.z;
 
                // Calculate lighting
    half3 lightDir = normalize(float3(0.5, 0.5, -1)); // Example light direction
    half3 normal = normalize(i.normal);
    half lighting = max(dot(normal, lightDir), 0.0);
 
                // Combine lighting with texture
    half4 col = tex2D(_MainTex, i.uv) * _LightColor0 * lighting;
 
                // Use the depth as the alpha value
    col.a = depth;
 
    return col;
}
            ENDCG
        }
    }
}
