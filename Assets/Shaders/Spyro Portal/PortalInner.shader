Shader "DP/Spyro/PortalInner"
{
    Properties
    {
        _Color("Albedo (RGB)", Color) = (1, 1, 1, 1)
        _DistortTex("Distortion Texture", 2D) = "white" {}
        _DistortSpeed("Distortion Scroll Speed", float) = 2
        _DistortAmount("Distortion Amount", float) = 5
        _OutlineColor("Outline Color (RGB)", Color) = (0, 1, 0, 1)
        _OutlineStrength("Outline Strength", float) = 8
        _OutlineThresholdMax("Outline Threshold Max", float) = 1
        _FadeDistance("Distortion Fade Start Distance", float) = 20
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry+1" }
        ZWrite Off

        CGPROGRAM
        #pragma surface surf NoLighting noshadow vertex:vert
        #pragma target 3.5

        struct Input
        {
            float4 vertex;

            float4 grabUV;
            float4 distortUV;
            float4 screenPos;
            float3 worldPos;
        };

        float4 _Color;

        sampler2D _GrabTexture;
        sampler2D _DistortTex;
        sampler2D _CameraDepthTexture;

        float4 _DistortTex_ST;

        float _DistortSpeed;
        float _DistortAmount;

        float4 _OutlineColor;
        float _OutlineThresholdMax;
        float _OutlineStrength;
        float _FadeDistance;

        fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            fixed4 c;
            c.rgb = s.Albedo;
            c.a = s.Alpha;
            return c;
        }

        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.grabUV = ComputeGrabScreenPos(o.vertex);

            // [TEXTURE UVs] : TRANSFORM_TEX only needs to be done once, as the second distortion texture
            //          uses the same UVs as the first texture. The UVs are then animated with _Time value.
            o.distortUV.xy = TRANSFORM_TEX(v.texcoord, _DistortTex);
            o.distortUV.zw = o.distortUV.xy;

            o.distortUV.y -= _DistortSpeed * _Time.x;
            o.distortUV.z += _DistortSpeed * _Time.x;

            o.screenPos = ComputeScreenPos(o.vertex);
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            // [DISTORTION EFFECT] : The normal texture needs to be unpacked twice, as they move at different speeds,
            //          they need to use their appropriate UVs which are animated in vert function.
            float2 distortTexture = UnpackNormal(tex2D(_DistortTex, IN.distortUV.xy)).xy;
            distortTexture *= _DistortAmount / 100;
            float2 distortTexture2 = UnpackNormal(tex2D(_DistortTex, IN.distortUV.zw)).xy;
            distortTexture2 *= _DistortAmount / 100;
            float combinedDistortion = distortTexture + distortTexture2;
            
            // [UV CALCULATION & DISTORTION SCALING] : Because the UVs are being manipulated, the effect needs
            //          be scaled down as camera moves away, otherwise "holes" will appear in portal.
            float4 grabPassUV = IN.grabUV;
            float fade = 1 - saturate(fwidth(grabPassUV) * _FadeDistance);
            grabPassUV.xy += combinedDistortion * IN.grabUV * fade;

            // [GRAB PASS SAMPLING] : Sampling the GrabPass texture to render portal background.
            fixed4 grabPassTexture = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(grabPassUV));

            // [OUTLINE EFFECT USING DEPTH] : The outline is created using depth texture supplied by the camera.
            float sceneDepth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos)));
            float surfaceDepth = -mul(UNITY_MATRIX_V, float4(IN.worldPos.xyz, 1)).z;
            float difference = sceneDepth - surfaceDepth;
            float intersect = 0;

            if (difference > 0)
                intersect = 1 - saturate(difference / _OutlineThresholdMax);
            
            float4 intersectColor = intersect * _OutlineStrength * _OutlineColor;
            fixed4 finalColor = fixed4(lerp(grabPassTexture * _Color, intersectColor, pow(intersect, 4.0)));

            o.Albedo = finalColor.rgb;
        }
        ENDCG
    }
}