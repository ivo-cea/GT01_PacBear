Shader "Custom/GhostShader"
{
    Properties
    {
        _RegularColor ("Regular Color", Color) = (1,1,1,1)
        _BlinkColor ("Blink Color", Color) = (1,1,1,1)
        _DeadColor ("Dead Color", Color) = (1,1,1,1)

        _IsAlive ("Alive", Float) = 1 
        _IsBlinking ("Blinking", Float) = 0

        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags {"RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _RegularColor;
        fixed4 _BlinkColor;
        fixed4 _DeadColor;
        float _IsAlive;
        float _IsBlinking;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //The final color while blinking switches from _BlinkColor to white
            fixed4 blink = lerp(_BlinkColor, fixed4(1,1,1,1), sin(_Time.z));

            //The color while alive is based on whether or not it's blinking
            fixed4 alive = lerp(_RegularColor, blink, _IsBlinking);

            //When dead he's a different color as well
            fixed4 mainColor = lerp(_DeadColor, alive, _IsAlive);

            fixed4 text = tex2D (_MainTex, IN.uv_MainTex);
            // Albedo comes from a texture tinted by color
            fixed4 texturedColor = text.x * mainColor;
            o.Albedo = texturedColor.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = texturedColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
