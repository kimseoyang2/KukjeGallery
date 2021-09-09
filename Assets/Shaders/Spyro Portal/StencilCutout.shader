Shader "DP/Spyro/StencilCutout"
{
    Properties
    {
        _StencilMask("Stencil Mask", int) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry-3" }
        ColorMask 0
        ZWrite Off

        Stencil
        {
            Ref [_StencilMask]
            Comp Always
            Pass replace
        }

        CGPROGRAM
        #pragma surface surf Lambert noshadow
        #pragma target 3.5

        struct Input
        {
            float4 vertex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = (1, 1, 0, 1);
            o.Albedo = c.rgb;
        }
        ENDCG
    }
}
